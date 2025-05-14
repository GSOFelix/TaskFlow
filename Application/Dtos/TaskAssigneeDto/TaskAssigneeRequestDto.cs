using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Dtos.TaskAssigneeDto
{
    public record TaskAssigneeRequestDto(
        [Required]
        long UserId,

        [Required]
        long MainTaskId);
}
