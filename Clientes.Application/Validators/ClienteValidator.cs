using Clientes.Application.Dtos;
using FluentValidation;

namespace Clientes.Application.Validators
{
    public class ClienteValidator : AbstractValidator<ClienteDto>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty()
                .WithMessage("É necessário informar o nome do cliente");

            RuleFor(cliente => cliente.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("É necessário informar o email do cliente")
                .EmailAddress()
                .WithMessage("Email informado não está em um formato válido");

            RuleFor(cliente => cliente.Telefones)
                .NotEmpty()
                .WithMessage("É necessário informar pelo menos um telefone");
        }
    }
}