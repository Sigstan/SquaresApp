using Microsoft.EntityFrameworkCore;
using SquaresApp.Storage.Entities;

namespace SquaresApp.Storage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Point> Points { get; set; }
    }
}