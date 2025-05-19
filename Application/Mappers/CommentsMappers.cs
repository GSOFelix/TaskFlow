using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.CommentsDto;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappers
{
    public static class CommentsMappers
    {
        public static Comment ToEntity(this CommentsRequestDto dto,long userId)
        {
            return new Comment(userId, dto.MainTaskId, dto.Comment);
        }

        public static IEnumerable<CommentsResponseDto> ToListDto(this IEnumerable<Comment> comments)
        {
            return comments.Select(c => new CommentsResponseDto
            {
                Id = c.Id,
                UserId = c.UserId,
                Text = c.Text,
                CreatedAt = c.CreatedAt.ToLocalTime(),
            });
        }

        public static CommentsResponseDto ToDto(this Comment comment)
        {
            return new CommentsResponseDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt.ToLocalTime(),
            };
        }
    }
}
