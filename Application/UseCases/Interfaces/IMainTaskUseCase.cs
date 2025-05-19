using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface IMainTaskUseCase
    {
        Task<long> CreateMainTask(MainTaskRequestDto request,CancellationToken token);

        Task<IEnumerable<MainTaskResponseDto>> GetAllMainTasks(long userId,bool detail, CancellationToken token);

        Task<IEnumerable<MainTaskResponseDto>> GetAllMyTasks(bool detail, CancellationToken token);

        Task<MainTaskResponseDto> GetMainTaskById(long mainTaskId, bool detail,CancellationToken token);

        Task UpdateStatus(MainTaskUpdateStatusDto mainTaskDto,CancellationToken token);
    }
}
