namespace TaskFlow.Domain.Entities
{
    public class Users
    {
        public long Id { get; set; }
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatAt { get; private set; }
        public List<TaskAssignee> TaskAssignees { get; set; } = [];

        public Users(string name, string email, string passWordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passWordHash;
            CreatAt = DateTime.Now;
        }


    }
}
