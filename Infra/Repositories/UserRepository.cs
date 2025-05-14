using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Context;

namespace TaskFlow.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken token)
        {
            var query = _context.Users
                 .AsNoTracking()
                 .Include(x => x.UserPermissions)
                 .ThenInclude(x => x.Permission)
                 .Where(x => x.Email == email);

            return await query.FirstOrDefaultAsync(token);
        }

        public async Task<User?> GetByIdAsync(long id, CancellationToken token)
        {
            var query = _context.Users
                 .AsNoTracking()
                 .Include(x => x.UserPermissions)
                 .ThenInclude(x => x.Permission)
                 .Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync(token);
        }

        public async Task<long> InsertAsync(User user, CancellationToken token)
        {
            await _context.Users.AddAsync(user, token);
            await _context.SaveChangesAsync(token);
            return user.Id;
        }
    }
}
