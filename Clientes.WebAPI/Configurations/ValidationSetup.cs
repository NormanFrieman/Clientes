using Clientes.Application.Validators;
using FluentValidation;

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
