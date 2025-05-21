using System.Security.Claims;
using System.Threading.Tasks;
using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Application.Helpers;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Enums;
using TaskFlow.Application.Auth;
using TaskFlow.Application.Validations;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class MainTaskUseCase : IMainTaskUseCase
    {
        private readonly IMainTaskRepository _repository;
        private readonly IUserUseCase _userUseCase;
        private readonly IHttpContextAccessor _contextAccessor;

        public MainTaskUseCase(IMainTaskRepository repository, IUserUseCase userUseCase, IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _userUseCase = userUseCase;
            _contextAccessor = httpContext;

        }

        public async Task<long> CreateMainTask(MainTaskRequestDto request, CancellationToken token)
        {
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // Verifica existencia 
            await _userUseCase.GetUserById(userId, token);

            return await _repository.InsertAsync(request.ToEntity(userId), token);
        }

        public async Task<IEnumerable<MainTaskResponseDto>> GetAllMainTasks(long userId, bool detail, CancellationToken token)
        {
            if (userId <= 0)
                throw new BadRequestException("O código de usuario é inválido");

            // veifica exisencia
            await _userUseCase.GetUserById(userId, token);

            var tasks = await _repository.GetAllByUserAsync(userId, detail, token);

            if (!tasks.Any())
                throw new NotFoundException("Nehuma tarefa encontrada");

            return detail
                ? tasks.ToListDetailDto()
                : tasks.ToListDto();
        }

        public async Task<IEnumerable<MainTaskResponseDto>> GetAllMyTasks(bool detail, CancellationToken token)
        {
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            await _userUseCase.GetUserById(userId, token);

            var tasks = await _repository.GetAllByUserAsync(userId, detail, token)
                ?? throw new NotFoundException("Nehuma tarefa encontrada");

            return detail
               ? tasks.ToListDetailDto()
               : tasks.ToListDto();
        }

        public async Task<MainTaskResponseDto> GetMainTaskById(long mainTaskId, bool detail, CancellationToken token)
        {
            if (mainTaskId <= 0)
                throw new BadRequestException("O Id da tarefa é inválido");

            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            var task = await _repository.GetByIdAsync(mainTaskId, detail, token)
                ?? throw new NotFoundException("Tarefa não encontrada");

            PermissionValidator.Validate(user, task, userId);

            return detail
                ? task.ToDetailDto()
                : task.ToDto();
        }

        public async Task UpdateStatus(MainTaskUpdateStatusDto mainTaskDto, CancellationToken token)
        {
            ValidateParameters.ValidateId(mainTaskDto.MainTaskId);

            // Seleciona o usuario da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // verificar se a tarefa existe 
            var task = await _repository.GetByIdAsync(mainTaskDto.MainTaskId, true, token) ??
                throw new NotFoundException("Tarefa não encontrada");

            PermissionValidator.ValidateStatusTask(user, task, userId, mainTaskDto.Status);
            PermissionValidator.Validate(user, task, userId);

            //Salva no banco de dados 
            task.ChangeStatus(mainTaskDto.Status);
            await _repository.UpdateAsync(task, token);
        }

        public async Task<MainTaskResponseDto> UpdateMainTask(long mainTaskId, MainTaskUpdateRequestDto requestDto, CancellationToken token)
        {
            ValidateParameters.ValidateId(mainTaskId);

            // Seleciona o usuario da requisição
            var user = _contextAccessor.HttpContext?.User;
            var userId = UserContextHelper.EffectiveUserId(user);

            // verificar se a tarefa existe 
            var task = await _repository.GetByIdAsync(mainTaskId, true, token) ??
                throw new NotFoundException("Tarefa não encontrada");
            
            //verifica se ele é o criador admin ou vinculado
            PermissionValidator.Validate(user, task, userId);

           
            if(PermissionValidator.IsCreatorOrAdmin(user,task, userId))
            {
                task.Title = requestDto.Title;
                task.Description = requestDto.Description;
                task.DeadLine = requestDto.Deadline;
                task.Priority = requestDto.Priority; 
            }

            // valida se o status enviado é o Done.
            PermissionValidator.ValidateStatusTask(user, task, userId, requestDto.Status);

            task.ChangeStatus(requestDto.Status);
            task.Progress = requestDto.Progress;

            await _repository.UpdateAsync(task, token);
        }
    }
}
