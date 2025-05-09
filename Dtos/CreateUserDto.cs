using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Dtos
{
    public record CreateUserDto(
        [property: Required]
        [property: MaxLength(155, ErrorMessage = "Tamanho máximo de 155 caracteres")]
        string Name,

        [property: Required]
        [property: EmailAddress(ErrorMessage = "Email inválido")]
        string Email,

        [property: Required]
        [property: MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        string Password
    );
}
