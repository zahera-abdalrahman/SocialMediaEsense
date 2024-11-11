using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Configuration;

namespace SocialMedia.Data
{
    public class SocialMediaContext:IdentityDbContext<User>
    {
        IConfiguration configure;

        public SocialMediaContext(IConfiguration _configure)
        {
            configure = _configure;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configure.GetConnectionString("SqlCon"));

            base.OnConfiguring(optionsBuilder);
        }

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new CommentConfiguration().Configure(builder.Entity<Comment>());
            new LikeConfiguration().Configure(builder.Entity<Like>());
            new PostConfiguration().Configure(builder.Entity<Post>());


            base.OnModelCreating(builder);
        }

        public DbSet<Like> likes { get; set; }

        public DbSet<Comment> comments { get; set; }
        public DbSet<Post> posts { get; set; }

    }
}
