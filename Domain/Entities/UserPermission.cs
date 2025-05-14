namespace TaskFlow.Domain.Entities
{
    public class UserPermission
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public long PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        private UserPermission() { }
        public UserPermission(int userId, int permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
            AssignedAt = DateTime.UtcNow;
        }
    }
}
