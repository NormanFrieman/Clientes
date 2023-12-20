using Clientes.Domain.Entities;

namespace Clientes.Domain.Interfaces
{
    public interface ITelefoneRepository
    {
        Task UpdateTelefone(Guid clienteId, string ddd, string numero, Telefone telefoneAtualizado);
        Task<bool> PhoneAlreadUsed(string ddd, string numero);
        Task<IEnumerable<string>> PhonesAlreadUsed(IEnumerable<string> dddNumeros);
        Task<bool> PhoneBelong(Guid clienteId, string ddd, string numero);
    }
}
