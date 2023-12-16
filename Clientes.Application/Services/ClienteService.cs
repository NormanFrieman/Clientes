using Clientes.Application.Dtos;

namespace Clientes.Application.Services
{
    public class ClienteService : IClienteService
    {
        public static List<ClienteDTO> Clientes = new List<ClienteDTO>();

        public ClienteDTO CreateCliente(ClienteDTO cliente)
        {
            cliente.Id = Guid.NewGuid();
            Clientes.Add(cliente);

            return cliente;
        }

        public void DeleteCliente(Guid clienteId)
        {
            Clientes.Remove(Clientes.First(x => x.Id == clienteId));
        }

        public IEnumerable<ClienteDTO> GetClientes(string? telefone = null)
        {
            return Clientes.Where(x => telefone == null || x.Telefones.Contains(telefone));
        }

        public ClienteDTO UpdateCliente(Guid clienteId, ClienteDTO clienteAtualizado)
        {
            var cliente = Clientes.First(x => x.Id == clienteId);

            cliente.NomeCompleto = clienteAtualizado.NomeCompleto ?? cliente.NomeCompleto;
            cliente.Email = clienteAtualizado.Email ?? cliente.Email;
            cliente.Telefones = clienteAtualizado.Telefones ?? cliente.Telefones;

            Clientes.Add(cliente);

            return cliente;
        }
    }
}
