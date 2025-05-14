using TaskFlow.Application.Dtos.UserDto;
using TaskFlow.Application.Dtos.PermissionDto;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappers
{
    public static class UserMappers
    {
        public static UserResponseDto ToDto(this User user)
        {
            return new UserResponseDto(
                user.Id, user.Name, user.Email, user.CreatAt.ToLocalTime(),
                user.UserPermissions.Select(up => new PermissionsDto
                {
                    Name = up.Permission.Name,
                    Description = up.Permission.Description,
                }));
        }
    }
}