using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface ISubTaskRepository
    {
        /// <summary>
        /// Retorna todas as subtarefas pertencentes a uma tarefa principal.
        /// </summary>
        /// <param name="mainTaskId">ID da tarefa principal.</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>Lista de subtarefas da MainTask.</returns>
        Task<IEnumerable<Subtask>> GetAllByIdAsync(long mainTaskId, CancellationToken token);

        /// <summary>
        /// Retorna uma subTarefa específica pelo seu ID.
        /// </summary>
        /// <param name="subTaskId">ID da subTarefa principal.</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>A subTarefa correspondente, se existir.</returns>
        Task<Subtask?> GetByIdAsync(long subTaskId, CancellationToken token);

        /// <summary>
        /// Insere uma nova subTarefa no banco de dados.
        /// </summary>
        /// <param name="subTask">Objeto da subTarefa a ser inserido.</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>ID da nova subTarefa inserida.</returns>
        Task<long> InsertAsync(Subtask subTask,CancellationToken token);
    }
}
