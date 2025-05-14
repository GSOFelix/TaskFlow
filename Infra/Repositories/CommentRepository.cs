using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infra.Context;

namespace TaskFlow.Infra.Repositories
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
            var query = _context.Comments
                 .AsNoTracking()
                 .OrderByDescending(c => c.CreatedAt)
                 .Where(c => c.MainTaskId == mainTaskId);

            return await query.ToListAsync(token);
        }

        public async Task<Comment?> GetByIdAsync(long commentId, CancellationToken token)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId, token);
        }

        public async Task<long> InsertAsync(Comment comment, CancellationToken token)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync(token);
            return comment.Id;
        }

        public async Task<Comment> UpdateAsync(Comment comment, CancellationToken token)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(token);
            return comment;
        }
    }
}
