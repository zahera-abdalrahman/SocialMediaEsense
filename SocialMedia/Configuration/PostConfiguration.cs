using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;

namespace SocialMedia.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasOne(e => e.User)
            .WithMany(s => s.posts)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
