using System.Threading.Tasks;
using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class MainTaskUseCase : IMainTaskUseCase
    {
        private readonly IMainTaskRepository _repository;
        private readonly IUserUseCase _userUseCase;

        public MainTaskUseCase(IMainTaskRepository repository, IUserUseCase userUseCase)
        {
            _repository = repository;
            _userUseCase = userUseCase;
        }

        public async Task<long> CreateMainTask(MainTaskRequestDto request, CancellationToken token)
        {
            // Verifica existencia 
            await _userUseCase.GetUserById(request.UserId, token);

            return await _repository.InsertAsync(request.ToEntity(), token);
        }

        public async Task<IEnumerable<MainTaskResponseDto>> GetAllMainTasks(long userId, bool detail, CancellationToken token)
        {
            // veifica exisencia
            await _userUseCase.GetUserById(userId, token);

            var tasks = await _repository.GetAllByUserAsync(userId, detail, token);

            if (!tasks.Any())
                throw new NotFoundException("Nehuma tarefa encontrada");

            return detail
                ? tasks.ToListDetailDto()
                : tasks.ToListDto();
        }

        public async Task<MainTaskResponseDto> GetMainTaskById(long id, bool detail, CancellationToken token)
        {
            var task = await _repository.GetByIdAsync(id, detail, token);

            if (task is null)
                throw new NotFoundException("Tarefa não encontrada");
            
            return detail 
                ? task.ToDetailDto()
                : task.ToDto(); 
        }
    }
}
