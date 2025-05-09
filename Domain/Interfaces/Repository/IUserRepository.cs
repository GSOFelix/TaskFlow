using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retorna o usuário especifico pelo Id
        /// </summary>
        /// <param name="id"> Id do usuário</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>O usuário se existir</returns>
        Task<User?> GetByIdAsync(long id,CancellationToken token);

        /// <summary>
        /// Insere um novo usuário no banco de dados
        /// </summary>
        /// <param name="user">Objeto de usuário a ser inserido</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>Id do usuário inserido</returns>
        Task<long> InsertAsync(User user,CancellationToken token);
    }
}
