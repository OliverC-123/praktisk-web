using Microsoft.EntityFrameworkCore;

namespace ScubaAPI.Models

{
    public class StedContext : DbContext
    {
        public StedContext(DbContextOptions<StedContext> options) : base(options){ }
        public DbSet<Sted> Steder => Set<Sted>();
        public DbSet<Type> Type => Set<Type>();
        public DbSet<Tur> Turer => Set<Tur>();
        public DbSet<IMG> Images => Set<IMG>();
        public DbSet<Tilmeld> Signup => Set<Tilmeld>();
        public DbSet<Kontakt> Kontakt => Set<Kontakt>();
    }
}
