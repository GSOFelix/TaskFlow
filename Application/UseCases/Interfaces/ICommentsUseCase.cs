using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.CommentsDto;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface ICommentsUseCase
    {
        Task<long> PostComment(CommentsRequestDto request, CancellationToken token);

        Task<CommentsResponseDto> GetCommentById(long commentId, CancellationToken token);

        Task<IEnumerable<CommentsResponseDto>> GetCommentsByMainTaskId(long mainTaskId,CancellationToken token);

        Task<CommentsResponseDto> UpdateComment(UpdateCommentRequestDto request, CancellationToken token);

        Task DeleteComment(long commentId, long userId,CancellationToken token);
    }
}
