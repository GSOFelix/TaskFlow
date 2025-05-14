using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;

namespace TaskFlow.Application.UseCases.Interfaces
{
    public interface IMainTaskUseCase
    {
        Task<long> CreateMainTask(MainTaskRequestDto request,CancellationToken token);

        Task<IEnumerable<MainTaskResponseDto>> GetAllMainTasks(long userId,bool detail, CancellationToken token);

        Task<MainTaskResponseDto> GetMainTaskById(long id, bool detail,CancellationToken token);
    }
}
