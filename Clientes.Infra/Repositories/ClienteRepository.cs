using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;
using Clientes.Infra.Core;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            _appDbContext.Cliente.Add(cliente);
            await _appDbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task DeleteCliente(Guid clienteId)
        {
            var cliente = await _appDbContext.Cliente.FindAsync(clienteId)
                ?? throw new ArgumentException("Cliente não encontrado", nameof(clienteId));
            _appDbContext.Cliente.Remove(cliente);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync(string? telefone = null)
        {
            var clientes = await _appDbContext.Cliente.Where(x => telefone == null || x.Telefones.Contains(telefone)).ToArrayAsync();

            return clientes;
        }

        public async Task<Cliente> UpdateCliente(Guid clienteId, Cliente clienteAtualizado)
        {
            var cliente = await _appDbContext.Cliente.FindAsync(clienteId)
                ?? throw new ArgumentException("Cliente não encontrado", nameof(clienteId));

            cliente.Nome = clienteAtualizado.Nome ?? cliente.Nome;
            cliente.Email = clienteAtualizado.Email ?? cliente.Email;
            cliente.Telefones = clienteAtualizado.Telefones ?? cliente.Telefones;

            _appDbContext.Cliente.Update(cliente);
            await _appDbContext.SaveChangesAsync();

            return cliente;
        }
    }
}
