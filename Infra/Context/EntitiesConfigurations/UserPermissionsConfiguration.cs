using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infra.Context.EntitiesConfigurations
{
    public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(up => new { up.UserId, up.PermissionId });

            builder.HasOne(u => u.User)
                .WithMany(up => up.UserPermissions)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(p => p.Permission)
                .WithMany(up => up.UserPermissions)
                .HasForeignKey(p => p.PermissionId);

            builder.Property(x => x.AssignedAt).IsRequired().HasColumnType("TIMESTAMP WITH TIME ZONE");
        }
    }
}
