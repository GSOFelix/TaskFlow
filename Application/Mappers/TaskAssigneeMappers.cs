using TaskFlow.Application.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappers
{
    public static class TaskAssigneeMappers
    {
        public static IEnumerable<TaskAssigneeResponseDto> ToListDto(this IEnumerable<TaskAssignee> assignees)
        {
            return assignees.Select(x =>
            new TaskAssigneeResponseDto
            {
                Id = x.Id,
                UserId = x.UserId,
                MainTaskId = x.MainTaskId,
            });
        }

        


    }
}
