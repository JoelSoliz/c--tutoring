using Microsoft.EntityFrameworkCore;
using WatchPartyAPI.Models;

namespace WatchPartyAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participant>()
                .HasKey(p => new { p.UserId, p.WatchPartyId });

            // Participant to User
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participations)
                .HasForeignKey(p => p.UserId);

            // Participant to WatchParty
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.WatchParty)
                .WithMany(wp => wp.Participants)
                .HasForeignKey(p => p.WatchPartyId);

            // WatchParty to Host
            modelBuilder.Entity<WatchParty>()
                .HasOne(wp => wp.Host)
                .WithMany()
                .HasForeignKey(wp => wp.HostUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // WatchParty to Episode
            modelBuilder.Entity<WatchParty>()
                .HasOne(wp => wp.CurrentEpisode)
                .WithMany(e => e.WatchParties)
                .HasForeignKey(wp => wp.CurrentEpisodeId);

            modelBuilder.Entity<Participant>()
                .Property(p => p.Role)
                .HasConversion<string>();

            modelBuilder.Entity<WatchParty>()
                .Property(wp => wp.Status)
                .HasConversion<string>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WatchParty> WatchParties { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
