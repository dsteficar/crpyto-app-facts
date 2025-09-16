using Domain.Entity.Authentication;
using Domain.Entity.Trading.Bots;
using Domain.Entity.Trading.Graphs;
using Domain.Entity.Users;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, int>, IDataProtectionKeyContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ParallelChannel> ParallelChannels { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DbSet<UserSettings> UserSettings { get; set; }

        public DbSet<TradingBotSettings> TradingBotSettings { get; set; }

        public DbSet<TradingBotTask> TradingBotTask { get; set; }   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

                //optionsBuilder.UseLazyLoadingProxies();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<ParallelChannel>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.ParallelChannels)
                .HasForeignKey(pc => pc.UserId);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Price1)
                .HasPrecision(18, 8);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Price2)
                .HasPrecision(18, 8);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Price3)
                .HasPrecision(18, 8);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Timestamp1)
                .HasPrecision(20, 8);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Timestamp2)
                .HasPrecision(20, 8);

            modelBuilder.Entity<ParallelChannel>()
                .Property(p => p.Timestamp3)
                .HasPrecision(20, 8);

            modelBuilder.Entity<UserSettings>()
                .HasOne(us => us.User)
                .WithOne(u => u.UserSettings)
                .HasForeignKey<UserSettings>(us => us.UserId); 

            modelBuilder.Entity<UserSettings>()
                .HasIndex(us => us.UserId)
                .IsUnique(); // Ensure uniqueness for UserId
        }
    }

    //public interface IDataProtectionKeyContext
    //{
    //    DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    //}
}
