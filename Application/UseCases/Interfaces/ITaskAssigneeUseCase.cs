using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.TaskAssigneeDto;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface ITaskAssigneeUseCase
    {
        Task<IEnumerable<TaskAssigneeResponseDto>> GetAllDesignationsByUser(long userId, CancellationToken token);
        Task AssignUserToTask(TaskAssigneeRequestDto request, CancellationToken token);
        Task UnlinkUserToTask(TaskAssigneeRequestDto request, CancellationToken token);
    }
}
