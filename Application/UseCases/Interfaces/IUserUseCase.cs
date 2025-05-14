using TaskFlow.Application.Dtos.UserDto;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface IUserUseCase
    {
        /// <summary>
        /// Recupera os dados de um usuário com base no seu ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser consultado.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>Objeto com as informações do usuário.</returns>
        Task<UserResponseDto> GetUserById(long id, CancellationToken token);

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="request">Objeto contendo os dados do usuário a ser criado.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>ID do usuário recém-criado.</returns>
        Task<long> CreateUser(UserRequestDto request, CancellationToken token);
    }
}
