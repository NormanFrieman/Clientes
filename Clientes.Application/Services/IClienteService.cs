using Clientes.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Application.Services
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> GetClientes(string? telefone = null);
        ClienteDTO CreateCliente(ClienteDTO cliente);
        ClienteDTO UpdateCliente(Guid clienteId, ClienteDTO clienteAtualizado);
        void DeleteCliente(Guid clienteId);
    }
}
