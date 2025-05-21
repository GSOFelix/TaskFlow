using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface IMainTaskRepository
    {
        /// <summary>
        /// Retorna todas as tarefas pertencentes a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário dono das tarefas.</param>
        /// <param name="detail">Detalha os comentarios e pessoas destinadas a tarefa</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>Lista de tarefas do usuário.</returns>
        Task<IEnumerable<MainTask>> GetAllByUserAsync(long userId, bool detail,CancellationToken token);

        /// <summary>
        /// Retorna uma tarefa específica pelo seu ID.
        /// </summary>
        /// <param name="mainTaskId">ID da tarefa principal.</param>
        /// <param name="detail">Detalha os comentarios e pessoas destinadas a tarefa</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>A tarefa correspondente, se existir.</returns>
        Task<MainTask?> GetByIdAsync(long mainTaskId,bool detail ,CancellationToken token);

        /// <summary>
        /// Insere uma nova tarefa no banco de dados.
        /// </summary>
        /// <param name="mainTask">Objeto da tarefa a ser inserido.</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>ID da nova tarefa inserida.</returns>
        Task<long> InsertAsync(MainTask mainTask, CancellationToken token);

        /// <summary>
        /// Atualiza uma tarefa no banco de dados.
        /// </summary>
        /// <param name="mainTask">Objeto da tarefa a ser atualizado.</param>
        /// <param name="token">Token de cancelamento</param>
        Task<MainTask> UpdateAsync(MainTask mainTask, CancellationToken token);

    }
}
