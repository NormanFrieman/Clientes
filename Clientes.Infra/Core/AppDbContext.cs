using Clientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
    }
}
