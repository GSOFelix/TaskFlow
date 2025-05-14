using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.TaskAssigneeDto;
using TaskFlow.Application.Mappers;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class TaskAssigneeUseCase : ITaskAssigneeUseCase
    {
        private readonly ITaskAssigneeRepository _repository;
        private readonly IUserUseCase _userUseCase;
        private readonly IMainTaskUseCase _mainTaskUseCase;

        public TaskAssigneeUseCase(ITaskAssigneeRepository repository, IUserUseCase userUseCase, IMainTaskUseCase mainTaskUseCase)
        {
            _repository = repository;
            _userUseCase = userUseCase;
            _mainTaskUseCase = mainTaskUseCase;
        }

        public async Task AssignUserToTask(TaskAssigneeRequestDto request, CancellationToken token)
        {
            // verificar se a tarefa existe
            await _mainTaskUseCase.GetMainTaskById(request.MainTaskId, false, token);

            // verificar se o usuario a ser vinculado existe 
            await _userUseCase.GetUserById(request.UserId, token);

            // vincula usuaria a tarefa
            await _repository.InsertAsync(new TaskAssignee(request.UserId, request.MainTaskId), token);
        }

        public async Task<IEnumerable<TaskAssigneeResponseDto>> GetAllDesignationsByUser(long userId, CancellationToken token)
        {
            var assignees = await _repository.GetAllbyUserAsync(userId, token);

            return assignees.ToListDto() ?? throw new NotFoundException("Nehuma tarefa designada ao usuário");
        }

        public async Task UnlinkUserToTask(TaskAssigneeRequestDto request, CancellationToken token)
        {
            // Verificar se a tarefa existe
            await _mainTaskUseCase.GetMainTaskById(request.MainTaskId, false, token);

            // Verificar se o usuario a ser desvinculado existe 
            await _userUseCase.GetUserById(request.UserId, token);

            // Seleciona o relacionamento com base na tarefa e no usuário
            var assignee = await _repository.GetByIdAsync(request.MainTaskId, request.UserId, token)
                ?? throw new NotFoundException("Relacionamento não encontrado");

            // Deleta relacionamento
            await _repository.DeleteAsync(assignee, token);
        }
    }
}
