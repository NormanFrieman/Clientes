using Clientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Telefone> Telefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<Telefone>()
                .HasIndex(x => new { x.Ddd, x.Numero }).IsUnique();
        }
    }
}
