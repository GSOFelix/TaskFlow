using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Dtos
{
    public record UserResponseDto(
        long Id,
        string Name,
        string Email,
        DateTime CreatAt
    );

   
}
