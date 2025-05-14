namespace TaskFlow.Application.Dtos
{
    public record TaskAssigneeResponseDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public long MainTaskId { get; set; }
    }
}
