using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Dtos
{
    public record MainTaskRequestDto(
        [property: Required]
        [property: MaxLength(70, ErrorMessage = "Tamanho máximo de 70 caracteres")]
        string Title,

        [property: Required]
        [property: MaxLength(500, ErrorMessage = "Tamanho máximo de 500 caracteres")]
        string Description,

        [property: Required]
        long UserId
    );
}
