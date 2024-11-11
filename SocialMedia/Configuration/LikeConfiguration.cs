using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;

namespace SocialMedia.Configuration
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasOne(e => e.user)
           .WithMany(s => s.likes)
           .HasForeignKey(e => e.UserId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.post)
           .WithMany(s => s.like)
           .HasForeignKey(e => e.postId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
