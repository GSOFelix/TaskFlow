using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Context;

namespace TaskFlow.Infra.Repositories
{
    public class MainTaskRepository : IMainTaskRepository
    {
        private readonly AppDbContext _context;

        public MainTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MainTask>> GetAllByUserAsync(long userId, bool detail, CancellationToken token)
        {
            if (detail)
            {
                var query = _context.MainTasks
                    .Include(c => c.Comments)
                    .Include(a => a.TaskAssignees)
                        .ThenInclude(u => u.User)
                    .AsNoTracking()
                    .OrderBy(x => x.Id)
                    .Where(x => x.UserId == userId);

                return await query.ToListAsync(token);
            }

            return await _context.MainTasks.Where(u => u.UserId == userId).ToListAsync(token);
        }

        public async Task<MainTask?> GetByIdAsync(long mainTaskId, bool detail, CancellationToken token)
        {
            if (detail)
            {
                var query = _context.MainTasks
                    .Include(c => c.Comments)
                    .Include(a => a.TaskAssignees)
                    .ThenInclude(u => u.User)
                    .AsNoTracking()
                    .Where(x => x.Id == mainTaskId);

                return await query.FirstOrDefaultAsync(token);

            }

            return await _context.MainTasks.FirstOrDefaultAsync(m => m.Id == mainTaskId, token);
        }

        public async Task<long> InsertAsync(MainTask mainTask, CancellationToken token)
        {
            await _context.MainTasks.AddAsync(mainTask, token);
            await _context.SaveChangesAsync(token);
            return mainTask.Id;
        }
    }
}
