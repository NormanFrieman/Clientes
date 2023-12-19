using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
using Clientes.Domain.Enums;
using Clientes.Domain.Interfaces;

namespace Clientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClienteDto> CreateCliente(ClienteDto cliente)
        {
            var novoCliente = await _repository.CreateCliente(new Cliente(cliente.Nome, cliente.Email, cliente.Telefones
                .Select(x => new Telefone(x.Numero, TelefoneTipo.FIXO.Equals(x.Tipo) ? ETelefoneTipo.Fixo : ETelefoneTipo.Celular))
                .ToArray()));

            return new ClienteDto(novoCliente);
        }

        public async Task DeleteCliente(string email)
        {
            await _repository.DeleteCliente(email);
        }

        public async Task<IEnumerable<ClienteDto>> GetClientes(string? numero = null)
        {
            return (await _repository.GetClientesAsync(numero))
                .Select(x => new ClienteDto(x.Id, x.Nome, x.Email, x.Telefones
                    .Select(y => new TelefoneDto(y))
                    .ToArray()))
                .ToArray();
        }

        public async Task<ClienteDto> UpdateCliente(Guid clienteId, ClienteDto clienteAtualizado)
        {
            var cliente = await _repository.UpdateCliente(clienteId, new Cliente(
                clienteAtualizado.Nome,
                clienteAtualizado.Email,
                clienteAtualizado.Telefones
                    .Select(x => new Telefone(x.Numero, TelefoneTipo.FIXO.Equals(x.Tipo) ? ETelefoneTipo.Fixo : ETelefoneTipo.Celular))
                    .ToArray()
                )
            );

            return new ClienteDto(cliente);
        }
    }
}
