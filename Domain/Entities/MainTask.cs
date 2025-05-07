using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class MainTask
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ETaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TaskAssignee> TaskAssignees { get; set; } = [];
        public List<Subtask> Subtasks { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];

        public MainTask(string title, string description, ETaskStatus status)
        {
            Title = title;
            Description = description;
            Status = status;
            CreatedAt = DateTime.Now;
        }
    }


    public class Subtask
    {
        public long Id { get; set; }
        public long MainTaskId { get; set; }
        public MainTask MainTask { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ETaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Subtask(long mainTaskId, string title, string description, ETaskStatus status)
        {
            MainTaskId = mainTaskId;
            Title = title;
            Description = description;
            Status = status;
            CreatedAt = DateTime.Now;
        }
    }

    public class Comment
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Users User { get; set; } = null!;
        public long MainTaskId { get; set; }
        public MainTask MainTask { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Comment(long userId, long mainTaskId, string text)
        {
            UserId = userId;
            MainTaskId = mainTaskId;
            Text = text;
            CreatedAt = DateTime.Now;
        }
    }

    public class TaskAssignee
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long MainTaskId { get; set; }

        public Users User { get; set; } = null!;
        public MainTask MainTask { get; set; } = null!;

        public TaskAssignee(long userId, long mainTaskId)
        {
            UserId = userId;
            MainTaskId = mainTaskId;
        }
    }
}



