using Clientes.Application.Dtos;

namespace Clientes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetClientes(string? numero = null);
        Task<ClienteDto> CreateCliente(ClienteDto cliente);
        Task<ClienteDto> UpdateCliente(Guid clienteId, string email);
        Task DeleteCliente(string email);
    }
}
