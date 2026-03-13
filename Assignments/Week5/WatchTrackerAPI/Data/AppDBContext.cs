using Microsoft.EntityFrameworkCore;
using WatchTrackerAPI.Models.Entities;

namespace WatchTrackerAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMediaProgress>()
                .HasKey(mediaProgress => new { mediaProgress.UserId, mediaProgress.MediaId });

            modelBuilder.Entity<UserMediaProgress>()
                 .HasOne(mediaProgress => mediaProgress.User)
                 .WithMany(progress => progress.Progresses)
                 .HasForeignKey(mediaProgress => mediaProgress.UserId);

            modelBuilder.Entity<UserMediaProgress>()
                .HasOne(mediaProgress => mediaProgress.Media)
                .WithMany(progress => progress.UserProgress)
                .HasForeignKey(mediaProgress => mediaProgress.MediaId);
        }

        public DbSet<Media> MediaContent { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMediaProgress> MediaProgresses { get; set; }
    }
}
