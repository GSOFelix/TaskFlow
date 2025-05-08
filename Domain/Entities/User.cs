using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatAt { get; private set; }
        public List<TaskAssignee> TaskAssignees { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
        public List<MainTask> MainTasks { get; set; } = [];

        public User(string name, string email, string passWordHash)
        {
            DomainValidation(name, passWordHash); 
            Email = email;
            CreatAt = DateTime.UtcNow;
        }

        private void DomainValidation(string name, string passWordHash)
        {
            DomainRule.When(string.IsNullOrWhiteSpace(name), "O nome é obrigatorio");
            DomainRule.When(name.Length < 3 , "O nome deve ter no mínimo 3 caracteres");
            Name = name;
            DomainRule.When(string.IsNullOrWhiteSpace(passWordHash), "A senha é obrigatoria");
            PasswordHash = passWordHash;
        }


    }
}
