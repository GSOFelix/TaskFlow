namespace TaskFlow.Application.Dtos
{
    public record CommentsResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
