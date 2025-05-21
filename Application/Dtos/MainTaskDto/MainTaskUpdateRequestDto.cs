using System.ComponentModel.DataAnnotations;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Dtos.MainTaskDto
{
    public record MainTaskUpdateRequestDto(
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(70, ErrorMessage = "Tamanho máximo de 70 caracteres")]
        string Title,

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(500, ErrorMessage = "Tamanho máximo de 500 caracteres")]
        string Description,

        [Required]
        int Progress,

        [Required]
        DateTime Deadline,

        [Required]
        [EnumDataType(typeof(EPriority))]
        EPriority Priority,

        [EnumDataType(typeof(ETaskStatus))]
         ETaskStatus Status
        );


}
