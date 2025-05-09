using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Context;

namespace TaskFlow.Infra.Repositories
{
    public class TaskAssigneeRepository : ITaskAssigneeRepository
    {
        private readonly AppDbContext _context;

        public TaskAssigneeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskAssignee>> GetAllbyUserAsync(long userId, CancellationToken token)
        {
            return await _context.TaskAssignees.Where(u => u.UserId == userId).ToListAsync(token);
        }
    }
}
