using Clientes.Infra.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.Infra.Configuration
{
    public static class PersistanceSetup
    {
        public static IServiceCollection AddPersistanceSetup(this IServiceCollection services, string connection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));

            return services;
        }
    }
}
