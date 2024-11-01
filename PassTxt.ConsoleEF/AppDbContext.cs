// AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PassTxt.ConsoleEF.Models;

namespace PassTxt.ConsoleEF
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=vm-portainer1.local;Database=PassTxt;User Id=passtxt;Password=Password2024;TrustServerCertificate=True;");
        }
    }
}

