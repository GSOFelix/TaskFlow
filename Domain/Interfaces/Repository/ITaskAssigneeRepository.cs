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
    }
}
