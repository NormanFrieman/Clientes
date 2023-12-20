using Clientes.Domain.Entities;

namespace Clientes.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesAsync(string? dddNumero = null);
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(Guid clienteId, string email);
        Task DeleteCliente(string email);
        Task<bool> EmailAlreadyUsed(string email);
        Task<bool> UserExists(Guid clienteId);
    }
}
