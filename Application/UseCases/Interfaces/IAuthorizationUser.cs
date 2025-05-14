using TaskFlow.Application.Dtos.AuthDto;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface IAuthorizationUser
    {
        Task<AuthResponseDto> AuthenticateUser(AuthRequestDto request,CancellationToken token);

    }
}
