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

            // Configuración de Personajes
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Characters");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(c => c.Ki)
                    .HasMaxLength(35);

                entity.Property(c => c.Race)
                    .HasMaxLength(25);

                entity.Property(c => c.Gender)
                    .HasMaxLength(20);

                entity.Property(c => c.Description)
                    .HasColumnType("varchar(max)");

                entity.Property(c => c.Affiliation)
                    .HasMaxLength(35);
            });

            // Configuración de Transformaciones
            modelBuilder.Entity<Transformation>(entity =>
            {
                entity.ToTable("Transformations");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                    .HasMaxLength(25)
                    .IsRequired();

                entity.Property(t => t.Ki)
                    .HasMaxLength(35);

                entity.HasOne(t => t.Character)
                    .WithMany(c => c.Transformations)
                    .HasForeignKey(t => t.CharacterId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}
