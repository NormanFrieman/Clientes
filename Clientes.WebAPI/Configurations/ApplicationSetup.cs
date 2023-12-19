using Clientes.Application.Interfaces;
using Clientes.Application.Services;

namespace Clientes.WebAPI.Configurations
{
    public static class ApplicationSetup
    {
        public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ITelefoneService, TelefoneService>();

            return services;
        }
    }
}
