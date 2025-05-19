using System.ComponentModel.DataAnnotations;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Dtos.MainTaskDto
{
    public record MainTaskUpdateStatusDto
    {
        [EnumDataType(typeof(ETaskStatus))]
        public ETaskStatus Status { get; set; }

        [Required]
        public long MainTaskId { get; set; }
    }
}
