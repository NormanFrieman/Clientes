using Clientes.Domain.Entities;

namespace Clientes.Domain.Interfaces
{
    public interface ITelefoneRepository
    {
        Task<Telefone> UpdateTelefone(Guid clienteId, string numero, Telefone telefoneAtualizado);
    }
}
