using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Context.EntitiesConfigurations
{
    public class TaskAssigneeConfigurations : IEntityTypeConfiguration<TaskAssignee>
    {
        public void Configure(EntityTypeBuilder<TaskAssignee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.MainTaskId).IsRequired();
            
            builder.HasOne(u => u.User).WithMany(x => x.TaskAssignees).HasForeignKey(u => u.UserId);
            builder.HasOne(m => m.MainTask).WithMany(x => x.TaskAssignees).HasForeignKey(m => m.MainTaskId);
        }
    }
}
