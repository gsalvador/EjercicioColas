using EjercicioColas.Data;
using Microsoft.EntityFrameworkCore;

namespace EjercicioColas
{
    public partial class ManejoColasContext : DbContext
    {
        public ManejoColasContext()
        {
        }

        public ManejoColasContext(DbContextOptions<ManejoColasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cola> Colas { get; set; } = null!;
        public virtual DbSet<TipoCola> TipoColas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Cola>(entity =>
            {
                entity.ToTable("Cola");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCliente)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idCliente");

                entity.Property(e => e.IdTipoCola).HasColumnName("idTipoCola");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Colas)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Cola__idCliente__440B1D61");

                entity.HasOne(d => d.IdTipoColaNavigation)
                    .WithMany(p => p.Colas)
                    .HasForeignKey(d => d.IdTipoCola)
                    .HasConstraintName("FK__Cola__idTipoCola__4316F928");
            });

            modelBuilder.Entity<TipoCola>(entity =>
            {
                entity.ToTable("TipoCola");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Tiempo).HasColumnName("tiempo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
