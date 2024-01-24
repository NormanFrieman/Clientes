using Clientes.Application.Interfaces;
using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;
using Clientes.Infra.Core;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISessao _sessao;

        public ClienteRepository(AppDbContext appDbContext, ISessao sessao)
        {
            _appDbContext = appDbContext;
            _sessao = sessao;
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            cliente.DataCriacao = _sessao.CurrentTime();

            _appDbContext.Cliente.Add(cliente);
            await _appDbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task DeleteCliente(string email)
        {
            var cliente = await _appDbContext.Cliente.SingleOrDefaultAsync(x => x.Email.Equals(email))
                ?? throw new ArgumentException("Cliente não encontrado", nameof(email));

            cliente.DataFim = _sessao.CurrentTime();

            _appDbContext.Cliente.Update(cliente);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync(string? dddNumero = null)
        {
            var clientes = await _appDbContext.Cliente
                .Include(x => x.Telefones)
                .Where(x => dddNumero == null || x.Telefones.Any(tel => (tel.Ddd + tel.Numero).Equals(dddNumero)))
                .ToArrayAsync();

            return clientes;
        }

        public async Task<Cliente> UpdateCliente(Guid clienteId, string email)
        {
            var cliente = await _appDbContext.Cliente.FindAsync(clienteId)
                ?? throw new ArgumentException("Cliente não encontrado", nameof(clienteId));

            cliente.Email = email;

            _appDbContext.Cliente.Update(cliente);
            await _appDbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task<bool> EmailAlreadyUsed(string email) =>
            await _appDbContext.Cliente.AnyAsync(x => x.Email.Equals(email));

        public async Task<bool> UserExists(Guid clienteId) =>
            await _appDbContext.Cliente.AnyAsync(x => x.Id == clienteId && x.DataFim == null);
    }
}
