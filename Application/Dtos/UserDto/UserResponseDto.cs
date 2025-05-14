using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Dtos.UserDto
{
    public record UserResponseDto(
        long Id,
        string Name,
        string Email,
        DateTime CreatAt
    );


}
