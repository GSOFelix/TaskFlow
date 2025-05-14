using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Dtos.MainTaskDto
{
    public record MainTaskResponseDto(
        long Id,
        string Title,
        string Description,
        ETaskStatus Status,
        DateTime CreateAt,
        long UserId,
        IEnumerable<TaskAssigneeResponseDto>? TaskAssignees,
        IEnumerable<CommentsResponseDto>? Comments
        );
}


