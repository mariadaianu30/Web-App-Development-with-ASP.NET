using Microsoft.EntityFrameworkCore;
using Laborator5.Models;
namespace Laborator5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>
        options)
        : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
