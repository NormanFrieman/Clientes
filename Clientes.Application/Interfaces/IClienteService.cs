using Clientes.Application.Abstractions;
using Clientes.Application.Dtos;

namespace Clientes.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetClientes(string? numero = null);
        Task<Result> CreateCliente(ClienteDto cliente);
        Task<Result> UpdateCliente(Guid clienteId, string email);
        Task<Result> DeleteCliente(string email);
    }
}
