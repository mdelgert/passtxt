// AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PassTxt.Shared.Models;

namespace PassTxt.Shared
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NoteModel> Notes { get; set; }
    }
}
