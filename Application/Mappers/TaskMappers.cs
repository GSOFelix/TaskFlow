using TaskFlow.Application.Dtos;
using TaskFlow.Application.Dtos.MainTaskDto;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mappers
{
    public static class TaskMappers
    {
        public static IEnumerable<MainTaskResponseDto> ToListDto(this IEnumerable<MainTask> mainTasks)
        {
            return mainTasks.Select(mainTask =>
            new MainTaskResponseDto(
             mainTask.Id,
             mainTask.Title,
             mainTask.Description,
             mainTask.Status,
             mainTask.CreatedAt.ToLocalTime(),
             mainTask.UserId,
             mainTask.Progress,
             mainTask.Priority,
             mainTask.DeadLine.ToLocalTime(),
             mainTask.FinishedAt.ToLocalTime(),
             [],
             [])).ToList();
        }

        public static MainTask ToEntity(this MainTaskRequestDto requestDto, long userId)
        {
            return new MainTask(requestDto.Title, requestDto.Description, userId,
                requestDto.Progress, requestDto.Deadline, requestDto.Priority);
        }

        public static MainTaskResponseDto ToDetailDto(this MainTask mainTask)
        {
            return new MainTaskResponseDto(
                Id: mainTask.Id,
                Title: mainTask.Title,
                Description: mainTask.Description,
                Status: mainTask.Status,
                CreateAt: mainTask.CreatedAt.ToLocalTime(),
                UserId: mainTask.UserId,
                Progress: mainTask.Progress,
                Priority: mainTask.Priority,
                Deadline: mainTask.DeadLine.ToLocalTime(),
                FinishedAt: mainTask.FinishedAt.ToLocalTime(),
                TaskAssignees: mainTask.TaskAssignees.Select(a => new TaskAssigneeResponseDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Name = a.User.Name
                }),
                Comments: mainTask.Comments.Select(c => new CommentsResponseDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt.ToLocalTime(),
                })
            );
        }

        public static MainTaskResponseDto ToDto(this MainTask mainTask)
        {
            return new MainTaskResponseDto(
                Id: mainTask.Id,
                Title: mainTask.Title,
                Description: mainTask.Description,
                Status: mainTask.Status,
                CreateAt: mainTask.CreatedAt,
                UserId: mainTask.UserId,
                Progress: mainTask.Progress,
                Priority: mainTask.Priority,
                Deadline: mainTask.DeadLine.ToLocalTime(),
                FinishedAt: mainTask.FinishedAt.ToLocalTime(),
                TaskAssignees: [],
                Comments: []
            );
        }

        public static IEnumerable<MainTaskResponseDto> ToListDetailDto(this IEnumerable<MainTask> mainTasks)
        {
            return mainTasks.Select(mainTask =>
            new MainTaskResponseDto(
             mainTask.Id,
             mainTask.Title,
             mainTask.Description,
             mainTask.Status,
             mainTask.CreatedAt.ToLocalTime(),
             mainTask.UserId,
             mainTask.Progress,
             mainTask.Priority,
             mainTask.DeadLine.ToLocalTime(),
             mainTask.FinishedAt.ToLocalTime(),
             mainTask.TaskAssignees.Select(x => new TaskAssigneeResponseDto
             {
                 Id = x.Id,
                 UserId = x.UserId,
                 Name = x.User.Name,
             }),
             mainTask.Comments.Select(c => new CommentsResponseDto
             {
                 Id = c.Id,
                 UserId = c.UserId,
                 Text = c.Text,
                 CreatedAt = c.CreatedAt.ToLocalTime(),
             })
             )).ToList();

        }
    }
}
