using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Domain.Entities
{
    public class Permission
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<UserPermission> UserPermissions { get; set; } = [];

        private Permission() { }

        public Permission(string name, string description)
        {
            DomainRule.When(string.IsNullOrEmpty(name), "O nome é obrigatorio");
            Name = name;
            DomainRule.When(string.IsNullOrEmpty(description), "A descrição é obrigatoria");
            Description = description;
        }
    }
}
