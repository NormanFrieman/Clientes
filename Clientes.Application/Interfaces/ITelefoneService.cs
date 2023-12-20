using Clientes.Application.Abstractions;
using Clientes.Application.Dtos;

namespace Clientes.Application.Interfaces
{
    public interface ITelefoneService
    {
        Task<Result> UpdateTelefone(Guid clienteId, string ddd, string numero, TelefoneDto telefoneAtualizado);
    }
}
