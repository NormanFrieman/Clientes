using Clientes.Application.Services;

namespace Clientes.WebAPI.Configurations
{
    public static class ApplicationSetup
    {
        public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();

            return services;
        }
    }
}
