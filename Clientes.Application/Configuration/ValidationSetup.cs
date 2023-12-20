using Clientes.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Clientes.WebAPI.Configurations
{
    public static class ValidationSetup
    {
        public static void AddValidationSetup(this IMvcBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
        }
    }
}
