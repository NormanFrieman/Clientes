using Clientes.Domain.Entities;
using Clientes.Domain.Interfaces;
using Clientes.Infra.Core;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly AppDbContext _appDbContext;

        public TelefoneRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Telefone> UpdateTelefone(Guid clienteId, string numero, Telefone telefoneAtualizado)
        {
            var telefone = await _appDbContext.Telefone.SingleOrDefaultAsync(x => x.ClienteId == clienteId && x.Numero.Equals(numero))
                ?? throw new ArgumentException("Telefone não encontrado", nameof(numero));

            telefone.Tipo = telefoneAtualizado.Tipo;
            telefone.Numero = telefoneAtualizado.Numero;

            _appDbContext.Telefone.Update(telefone);
            await _appDbContext.SaveChangesAsync();

            return telefone;
        }
    }
}
