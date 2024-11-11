using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;

namespace SocialMedia.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(e => e.user)
            .WithMany(s => s.comments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.post)
           .WithMany(s => s.comments)
           .HasForeignKey(e => e.postID)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
