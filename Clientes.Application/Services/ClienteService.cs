using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
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

        public async Task<ClienteDTO> CreateCliente(ClienteDTO cliente)
        {
            return await _repository.CreateCliente(new Cliente(cliente.Nome, cliente.Email, cliente.Telefones));
        }

        public async Task DeleteCliente(Guid clienteId)
        {
            await _repository.DeleteCliente(clienteId);
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientes(string? telefone = null)
        {
            return (await _repository.GetClientesAsync(telefone))
                .Select(x => new ClienteDTO(x.Id, x.Nome, x.Email, x.Telefones))
                .ToArray();
        }

        public async Task<ClienteDTO> UpdateCliente(Guid clienteId, ClienteDTO clienteAtualizado)
        {
            return await _repository.UpdateCliente(clienteId, new Cliente(clienteAtualizado.Nome, clienteAtualizado.Email, clienteAtualizado.Telefones));
        }
    }
}
