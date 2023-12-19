using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
using Clientes.Domain.Enums;
using Clientes.Domain.Interfaces;

namespace Clientes.Application.Services
{
    public class TelefoneService : ITelefoneService
    {
        private readonly ITelefoneRepository _repository;

        public TelefoneService(ITelefoneRepository repository)
        {
            _repository = repository;
        }

        public async Task<TelefoneDto> UpdateTelefone(Guid clienteId, string numero, TelefoneDto telefoneAtualizado)
        {
            var telefone = await _repository.UpdateTelefone(clienteId, numero, new Telefone(telefoneAtualizado.Numero, TelefoneTipo.FIXO.Equals(telefoneAtualizado.Tipo) ? ETelefoneTipo.Fixo : ETelefoneTipo.Celular));

            return new TelefoneDto(telefone);
        }
    }
}
