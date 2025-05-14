using TaskFlow.Application.Dtos.UserDto;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappers
{
    public static class UserMappers
    {
        public static UserResponseDto ToDto(this User user)
        {
            return new UserResponseDto(
                user.Id, user.Name, user.Email, user.CreatAt.ToLocalTime());
        }
    }
}