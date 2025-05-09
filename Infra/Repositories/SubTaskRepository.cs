using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Context;

namespace TaskFlow.Infra.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly AppDbContext _context;

        public SubTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subtask>> GetAllByIdAsync(long mainTaskId, CancellationToken token)
        {
            return await _context.Subtasks.Where(m => m.MainTaskId == mainTaskId).ToListAsync(token);
        }

        public async Task<Subtask?> GetByIdAsync(long subTaskId, CancellationToken token)
        {
            return await _context.Subtasks.FirstOrDefaultAsync(x => x.Id == subTaskId, token);
        }

        public async Task<long> InsertAsync(Subtask subTask, CancellationToken token)
        {
            await _context.Subtasks.AddAsync(subTask, token);
            await _context.SaveChangesAsync(token);
            return subTask.Id;
        }
    }
}
