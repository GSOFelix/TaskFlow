using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface ITaskAssigneeRepository
    {
        /// <summary>
        /// Retorna todas as atribuições de tarefas associadas a um usuário específico.
        /// </summary>
        /// <param name="userId">ID do usuário relacionado às atribuições de tarefas.</param>
        /// <param name="token">Token de cancelamento.</param>
        /// <returns>Lista de atribuições de tarefas do usuário.</returns>
        Task<IEnumerable<TaskAssignee>> GetAllbyUserAsync(long userId, CancellationToken token);

        /// <summary>
        /// Insere um novo relacionamento de atribuição de tarefa no banco de dados.
        /// </summary>
        /// <param name="taskAssignee">Objeto <see cref="TaskAssignee"/> representando a tarefa atribuída ao usuário.</param>
        /// <param name="token">Token de cancelamento para a operação assíncrona.</param>
        /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
        Task InsertAsync(TaskAssignee taskAssignee, CancellationToken token);

        /// <summary>
        /// Remove um relacionamento de atribuição de tarefa do banco de dados.
        /// </summary>
        /// <param name="taskAssignee">Objeto <see cref="TaskAssignee"/> representando a tarefa atribuída ao usuário a ser removida.</param>
        /// <param name="token">Token de cancelamento para a operação assíncrona.</param>
        /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
        Task DeleteAsync(TaskAssignee taskAssignee, CancellationToken token);

        /// <summary>
        /// Localiza um relacionamento por id de tarefa e id de usuario
        /// </summary>
        /// <param name="mainTaskId">Id da tarefa</param>
        /// <param name="UserId">Id do usuario</param>
        /// <param name="token">Token de cancelamento</param>
        /// <returns>Retorna uma atribuição de tarefa</returns>
        Task<TaskAssignee?> GetByIdAsync(long mainTaskId, long UserId, CancellationToken token);
    }
}
