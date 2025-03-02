using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Enums;
using CommunityPlant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = CommunityPlant.Domain.Entities.Task;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Administrator> Administrators { get; set; } = default!;

        public DbSet<Task> Tasks { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Garden> Gardens { get; set; }

        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Administrator>()
                .Property(a => a.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumTypeUser)Enum.Parse(typeof(EnumTypeUser), v)
                );
                
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    Id = 1,
                    Email = "adm@adm.com",
                    Password = "123",
                    TypeUser = EnumTypeUser.Administrator
                }
            );
        }

    }
}