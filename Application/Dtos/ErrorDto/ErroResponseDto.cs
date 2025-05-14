namespace TaskFlow.Application.Dtos.ErrorDto
{
    public record ErroResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

    }
}
