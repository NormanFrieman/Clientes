using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;
using Clientes.Infra.Core;
using Clientes.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Infra.Configuration
{
    public static class PersistanceSetup
    {
        public static IServiceCollection AddPersistanceSetup(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();

            return services;
        }
    }
}
