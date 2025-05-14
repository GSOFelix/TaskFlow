using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Dtos.AuthDto
{
    public record AuthRequestDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PassWord { get; set; } = null!;   
    }
}
