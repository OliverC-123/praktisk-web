using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class SubaContext : DbContext
    {
        public SubaContext(DbContextOptions<SubaContext> options) : base(options)
        {
        }

        // DbSet<> [Database name]
        public DbSet<ContactInfo> ContactInfo => Set<ContactInfo>();

        public DbSet<TourInfo> TourInfo => Set<TourInfo>();
        public DbSet<Login> Login => Set<Login>();
        public DbSet<Gallery> Gallery => Set<Gallery>();
        public DbSet<InputInfo> InputInfo => Set<InputInfo>();
        public DbSet<Products> Products => Set<Products>();
    }
}