using Clientes.Domain.Entities;

namespace Clientes.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync(string? numero = null);
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(Guid clienteId, Cliente clienteAtualizado);
        Task DeleteCliente(string email);
    }
}
