using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class MainTask
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ETaskStatus Status { get; private set; }
        public EPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime FinishedAt { get; private set; }
        public int Progress { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public List<TaskAssignee> TaskAssignees { get; set; } = [];
        public List<Subtask> Subtasks { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];

        private MainTask() { }

        public MainTask(string title, string description, long userId, int progress, DateTime deadLine, EPriority priority)
        {
            DomainRule.When(string.IsNullOrWhiteSpace(title), "O Título é obrigatório.");
            Title = title;

            DomainRule.When(string.IsNullOrWhiteSpace(description), "A Descrição é obrigatória.");
            Description = description;

            DomainRule.When(userId <= 0, "Id de usuário inválido.");
            UserId = userId;

            DomainRule.When(progress < 0 || progress > 100, "O progresso deve estar entre 0 e 100.");
            Progress = progress;

            DomainRule.When(deadLine < DateTime.UtcNow, "A data de prazo não pode ser menor que a data atual.");
            DeadLine = deadLine;

            Priority = priority;
            Status = ETaskStatus.ToDo;
            CreatedAt = DateTime.UtcNow;

        }

        public void ChangeStatus(ETaskStatus newStatus)
        {
            if (Status == ETaskStatus.Done)
                throw new BadRequestException("A tarefa já está concluída e não pode ser alterada.");

            if (Status == newStatus)
                return;

            Status = newStatus;

            if (newStatus == ETaskStatus.Done)
                FinishedAt = DateTime.Today;
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

        private Subtask() { }

        public Subtask(long mainTaskId, string title, string description)
        {
            DomainRule.When(mainTaskId <= 0, "Id da Tarefa inválido");
            MainTaskId = mainTaskId;
            DomainRule.When(string.IsNullOrWhiteSpace(title), "O Titulo é obrigatorio");
            Title = title;
            DomainRule.When(string.IsNullOrWhiteSpace(description), "A Descrição é obrigatorio");
            Description = description;
            Status = ETaskStatus.ToDo;
            CreatedAt = DateTime.UtcNow;
        }
    }

    public class Comment
    {
        public long Id { get; set; }
        public long UserId { get; private set; }
        public User User { get; set; } = null!;
        public long MainTaskId { get; private set; }
        public MainTask MainTask { get; set; } = null!;
        public string Text { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        private Comment() { }

        public Comment(long userId, long mainTaskId, string text)
        {
            DomainRule.When(userId <= 0, "Id do Usuário inválido");
            UserId = userId;
            DomainRule.When(mainTaskId <= 0, "Id da Tarefa inválido");
            MainTaskId = mainTaskId;
            DomainRule.When(string.IsNullOrWhiteSpace(text), "O Texto é obrigatorio");
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateText(string newText)
        {
            Text = newText.Trim();
            CreatedAt = DateTime.UtcNow;
        }
    }

    public class TaskAssignee
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long MainTaskId { get; set; }

        public User User { get; set; } = null!;
        public MainTask MainTask { get; set; } = null!;

        private TaskAssignee() { }

        public TaskAssignee(long userId, long mainTaskId)
        {
            DomainRule.When(userId <= 0, "Id do Usuário inválido");
            UserId = userId;
            DomainRule.When(mainTaskId <= 0, "Id da Tarefa inválido");
            MainTaskId = mainTaskId;
        }
    }
}



