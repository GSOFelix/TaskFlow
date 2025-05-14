using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Dtos.TaskAssigneeDto;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
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

        public async Task InsertAsync(TaskAssignee taskAssignee, CancellationToken token)
        {
            await _context.TaskAssignees.AddAsync(taskAssignee, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(TaskAssignee taskAssignee, CancellationToken token)
        {
            _context.TaskAssignees.Remove(taskAssignee);
            await _context.SaveChangesAsync(token);
        }

        public async Task<TaskAssignee?> GetByIdAsync(long mainTaskId, long userId, CancellationToken token)
        {
            return await _context.TaskAssignees
                .FirstOrDefaultAsync(x => x.MainTaskId == mainTaskId && x.UserId == userId);
        }
    }
}
