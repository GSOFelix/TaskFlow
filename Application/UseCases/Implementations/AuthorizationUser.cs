using TaskFlow.Application.Auth;
using TaskFlow.Application.Dtos.AuthDto;
using TaskFlow.Application.UseCases.Interfaces;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Services;

namespace TaskFlow.Application.UseCases.Implementations
{
    public class AuthorizationUser : IAuthorizationUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IPassWordService _passWordService;
        private readonly ITokenService _tokenService;

        public AuthorizationUser(IUserRepository userRepository, IPassWordService passWordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passWordService = passWordService;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> AuthenticateUser(AuthRequestDto request, CancellationToken token)
        {
            // Verifica se o usuário existe 
            var user = await _userRepository.GetByEmailAsync(request.Email,token) 
                ?? throw new NotFoundException("Usuario não encontrado.");

            // Verifica se a senha está correta 
            if (!_passWordService.Verify(request.PassWord, user.PasswordHash))
                throw new UnauthorizeException("Usuário ou senha incorrtos.");

            // Gera o Token
            var response = new AuthResponseDto(
                Email: request.Email,
                Token: _tokenService.GenerateToken(user));

            return response;
        }
    }
}
