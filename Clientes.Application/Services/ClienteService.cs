using Clientes.Application.Abstractions;
using Clientes.Application.Abstractions.ErrorsMessage;
using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;
using FluentValidation;

namespace Clientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IValidator<ClienteDto> _validator;
        private readonly IClienteRepository _repository;
        private readonly ITelefoneRepository _telefoneRepository;

        public ClienteService(IValidator<ClienteDto> validator, IClienteRepository repository, ITelefoneRepository telefoneRepository)
        {
            _validator = validator;
            _repository = repository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<Result> CreateCliente(ClienteDto cliente)
        {
            var validation = await _validator.ValidateAsync(cliente);
            if (!validation.IsValid)
                return Result.Failure(validation.Errors);

            if (await _repository.EmailAlreadyUsed(cliente.Email))
                return ClienteErrors.EmailAlreadyUsed;

            var phonesAlreadyUsed = await _telefoneRepository.PhonesAlreadUsed(cliente.Telefones.Select(x => string.Concat(x.Ddd, x.Numero)));
            if (phonesAlreadyUsed.Any())
                return TelefoneErros.PhonesAlreadyUsed(phonesAlreadyUsed);

            var novoCliente = await _repository.CreateCliente(
                new Cliente
                (
                    cliente.Nome,
                    cliente.Email,
                    cliente.Telefones.Select(x => new Telefone(x.Numero, x.Ddd, x.Tipo)).ToArray()
                ));

            return Result.Success(new ClienteDto(novoCliente));
        }

        public async Task<Result> DeleteCliente(string email)
        {
            if (!(await _repository.EmailAlreadyUsed(email)))
                return ClienteErrors.EmailNotFound;

            await _repository.DeleteCliente(email);
            return Result.Success();
        }

        public async Task<IEnumerable<ClienteDto>> GetClientes(string? dddNumero = null)
        {
            return (await _repository.GetClientesAsync(dddNumero))
                .Select(x => new ClienteDto(x.Id, x.Nome, x.Email, x.Telefones
                    .Select(y => new TelefoneDto(y))
                    .ToArray()))
                .ToArray();
        }

        public async Task<Result> UpdateCliente(Guid clienteId, string email)
        {
            if (!(await _repository.UserExists(clienteId)))
                return ClienteErrors.UserNotFound;

            if (await _repository.EmailAlreadyUsed(email))
                return ClienteErrors.EmailAlreadyUsed;

            var cliente = await _repository.UpdateCliente(clienteId, email);
            return Result.Success(new ClienteDto(cliente));
        }
    }
}