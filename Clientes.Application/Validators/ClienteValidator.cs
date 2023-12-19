using Clientes.Application.Dtos;
using FluentValidation;

namespace Clientes.Application.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDto>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty();

            RuleFor(cliente => cliente.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(cliente => cliente.Telefones)
                .NotEmpty();
        }
    }
}
