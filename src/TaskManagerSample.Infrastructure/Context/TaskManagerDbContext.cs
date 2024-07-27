using Microsoft.EntityFrameworkCore;
using TaskManagerSample.Core.Models;

namespace TaskManagerSample.Infrastructure.Context;

public class TaskManagerDbContext : DbContext
{
    public DbSet<Core.Models.User> Users { get; set; }

    public DbSet<Core.Models.Task> Tasks { get; set; }

    public TaskManagerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Email = "adm@adm.com",
                Password = "adm@123",
                Role = "Admin"
            },
            new User
            {
                Email = "user1@user.com",
                Password = "user@222",
                Role = "User"
            }
        );
    }
}