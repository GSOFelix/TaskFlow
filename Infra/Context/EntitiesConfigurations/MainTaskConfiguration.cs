using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infra.Context.EntitiesConfigurations
{
    public class MainTaskConfiguration : IEntityTypeConfiguration<MainTask>
    {
        public void Configure(EntityTypeBuilder<MainTask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("TIMESTAMP WITH TIME ZONE");
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Progress).IsRequired();
            builder.Property(x => x.DeadLine).IsRequired().HasColumnType("TIMESTAMP WITH TIME ZONE");
            builder.Property(x => x.FinishedAt).HasColumnType("TIMESTAMP");

            builder.HasOne(u => u.User).WithMany(x => x.MainTasks).HasForeignKey(u => u.UserId);
        }
    }
}
