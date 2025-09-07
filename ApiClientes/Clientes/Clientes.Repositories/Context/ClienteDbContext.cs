using Clientes.DTO;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Repositories.Context
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ClienteDTO> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Se mapea el resultado del SP en ClienteDTO
            modelBuilder.Entity<ClienteDTO>(entity =>
            {
                entity.HasNoKey(); // Indica que es una entidad sin clave primaria
                entity.Property(e => e.IdCliente).HasColumnName("IdCliente");
                entity.Property(e => e.Identificacion).HasColumnName("Identificacion");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.Apellido).HasColumnName("Apellido");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
                entity.Property(e => e.FechaActualizacion).HasColumnName("FechaActualizacion");
            });
        }
    }
}
