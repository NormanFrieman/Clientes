using Clientes.Application.Abstractions;
using Clientes.Application.Abstractions.ErrorsMessage;
using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;

namespace Clientes.Application.Services
{
    public class TelefoneService : ITelefoneService
    {
        private readonly ITelefoneRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public TelefoneService(ITelefoneRepository repository, IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }

        public async Task<Result> UpdateTelefone(Guid clienteId, string ddd, string numero, TelefoneDto telefoneAtualizado)
        {
            if (!(await _clienteRepository.UserExists(clienteId)))
                return ClienteErrors.UserNotFound;

            if (!(await _repository.PhoneBelong(clienteId, ddd, numero)))
                return TelefoneErros.PhoneNotBelong;

            if (await _repository.PhoneAlreadUsed(telefoneAtualizado.Ddd, telefoneAtualizado.Numero))
                return TelefoneErros.PhoneAlreadyUsed($"({telefoneAtualizado.Ddd}){telefoneAtualizado.Numero}");

            await _repository.UpdateTelefone(clienteId, ddd, numero, new Telefone(telefoneAtualizado.Numero, telefoneAtualizado.Ddd, telefoneAtualizado.Tipo));
            return Result.Success();
        }
    }
}
