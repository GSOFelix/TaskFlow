using Microsoft.EntityFrameworkCore;
using TaskFlow.Context;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Comment comment, CancellationToken token)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<Comment>> GetAllbyTaskAsync(long mainTaskId, CancellationToken token)
        {
            return await _context.Comments.Where(m => m.MainTaskId == mainTaskId).ToListAsync(token);
        }

        public async Task<Comment?> GetById(long commentId, CancellationToken token)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId, token);
        }

        public async Task<long> InsertAsync(Comment comment, CancellationToken token)
        {
            await _context.Comments.AddAsync(comment);
            return await _context.SaveChangesAsync(token);
        }

        public async Task<Comment> UpdateAsync(Comment comment, CancellationToken token)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(token);
            return comment;
        }
    }
}
