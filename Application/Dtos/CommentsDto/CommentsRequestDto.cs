using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Dtos.CommentsDto
{
    public record CommentsRequestDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long MainTaskId { get; set; }

        [Required]
        [MaxLength(255,ErrorMessage = "Tamanho maximo de 255 caracteres")]
        public string Comment { get; set; } =string.Empty;
    }
}
