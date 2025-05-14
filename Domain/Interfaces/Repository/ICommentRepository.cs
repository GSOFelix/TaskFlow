using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Retorna todos os comentários associados a uma tarefa específica.
        /// </summary>
        /// <param name="mainTaskId">ID da tarefa principal relacionada aos comentários.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>Lista de comentários da tarefa.</returns>
        Task<IEnumerable<Comment>> GetAllbyTaskAsync(long mainTaskId, CancellationToken token);

        /// <summary>
        /// Retorna um comentário específico com base no seu ID.
        /// </summary>
        /// <param name="commentId">ID do comentário.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>Comentário correspondente ao ID informado.</returns>
        Task<Comment?> GetByIdAsync(long commentId, CancellationToken token);

        /// <summary>
        /// Atualiza os dados de um comentário existente.
        /// </summary>
        /// <param name="comment">Comentário com os dados atualizados.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>Comentário atualizado.</returns>
        Task<Comment> UpdateAsync(Comment comment, CancellationToken token);

        /// <summary>
        /// Remove um comentário com base no seu ID.
        /// </summary>
        /// <param name="commentId">ID do comentário a ser removido.</param>
        /// <param name="token">Token de cancelamento.</param>
        Task DeleteAsync(Comment comment, CancellationToken token);

        /// <summary>
        /// Insere um novo comentário no sistema.
        /// </summary>
        /// <param name="comment">Comentário a ser inserido.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>ID do comentário recém-criado.</returns>
        Task<long> InsertAsync(Comment comment, CancellationToken token);

    }
}
