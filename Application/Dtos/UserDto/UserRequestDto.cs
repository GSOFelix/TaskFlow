using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Dtos.UserDto
{
    public class UserRequestDto
    {
        [Required]
        [MaxLength(155, ErrorMessage = "Tamanho máximo de 155 caracteres")]
        public string Name { get; init; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; init; } = string.Empty;
    }
}
