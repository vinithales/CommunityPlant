using CommunityPlant.Domain.Entities;
using CommunityPlant.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Task = CommunityPlant.Domain.Entities.Task;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Administrator> Administrators { get; set; } = default!;
        public DbSet<Garden> Gardens { get; set; } = default!;
        public DbSet<Task> Tasks { get; set; } = default!;
        public DbSet<TaskHistory> TaskHistories { get; set; } = default!;
        public DbSet<WeatherData> WeatherData { get; set; } = default!;
        public DbSet<Plant> Plants { get; set; } = default!;
        public DbSet<PlantedCrop> PlantedCrops { get; set; } = default!;
        public DbSet<Participation> Participations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");

            // Administrator configurations
            modelBuilder.Entity<Administrator>()
                .Property(a => a.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumTypeUser)Enum.Parse(typeof(EnumTypeUser), v)
                );

            // User configurations
            modelBuilder.Entity<User>()
                .Property(u => u.TypeUser)
                .HasConversion(
                    v => v.ToString(),
                    v => (EnumTypeUser)Enum.Parse(typeof(EnumTypeUser), v)
                );

            // Task relationships
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Garden)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t => t.GardenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Task>()
                .HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Garden relationships
            modelBuilder.Entity<Garden>()
                .HasOne(g => g.CreatedByUser)
                .WithMany()
                .HasForeignKey(g => g.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Participation relationships
            modelBuilder.Entity<Participation>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participations)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participation>()
                .HasOne(p => p.Garden)
                .WithMany(g => g.Participations)
                .HasForeignKey(p => p.GardenId)
                .OnDelete(DeleteBehavior.Cascade);

            // PlantedCrop relationships
            modelBuilder.Entity<PlantedCrop>()
                .HasOne(pc => pc.Garden)
                .WithMany(g => g.PlantedCrops)
                .HasForeignKey(pc => pc.GardenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlantedCrop>()
                .HasOne(pc => pc.Plant)
                .WithMany(p => p.PlantedCrops)
                .HasForeignKey(pc => pc.PlantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlantedCrop>()
                .HasOne(pc => pc.PlantedByUser)
                .WithMany()
                .HasForeignKey(pc => pc.PlantedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // TaskHistory relationships
            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.Task)
                .WithMany(t => t.TaskHistories)
                .HasForeignKey(th => th.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.User)
                .WithMany()
                .HasForeignKey(th => th.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // WeatherData relationships
            modelBuilder.Entity<WeatherData>()
                .HasOne(wd => wd.Garden)
                .WithMany(g => g.WeatherData)
                .HasForeignKey(wd => wd.GardenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
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
