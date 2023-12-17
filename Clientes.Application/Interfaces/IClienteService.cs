using Clientes.Application.Dtos;

namespace Clientes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetClientes(string? telefone = null);
        Task<ClienteDTO> CreateCliente(ClienteDTO cliente);
        Task<ClienteDTO> UpdateCliente(Guid clienteId, ClienteDTO clienteAtualizado);
        Task DeleteCliente(Guid clienteId);
    }
}
