using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Context.EntitiesConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.MainTaskId).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("TIMESTAMP WITH TIME ZONE");

            builder.HasOne(m => m.MainTask).WithMany(x => x.Comments).HasForeignKey(m => m.MainTaskId);
            builder.HasOne(u => u.User).WithMany(x => x.Comments).HasForeignKey(u => u.UserId);
        }
    }
}
