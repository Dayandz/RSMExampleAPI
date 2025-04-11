using DBZ.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZ.Infraestructure.Data
{
    public class DragonBallDbContext : DbContext
    {
        public DragonBallDbContext(DbContextOptions<DragonBallDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Transformation> Transformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Transformation>()
                .HasOne(t => t.Character)
                .WithMany(c => c.Transformations)
                .HasForeignKey(t => t.CharacterId);
        }
    }
}
