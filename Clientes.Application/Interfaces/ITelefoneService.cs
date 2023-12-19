using Clientes.Application.Dtos;

namespace Clientes.Application.Interfaces
{
    public interface ITelefoneService
    {
        Task<TelefoneDto> UpdateTelefone(Guid clienteId, string numero, TelefoneDto telefoneAtualizado);
    }
}
