using Microsoft.EntityFrameworkCore;
using TaskFlow.Context;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<User?> GetByIdAsync(int id, CancellationToken token)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == id,token);
        }

        public async Task<long> InsertAsync(User user, CancellationToken token)
        {
            await _context.Users.AddAsync(user,token);
            return await _context.SaveChangesAsync(token);
        }
    }
}
