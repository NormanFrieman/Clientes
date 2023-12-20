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

        public async Task UpdateTelefone(Guid clienteId, string ddd, string numero, Telefone telefoneAtualizado)
        {
            var telefone = await _appDbContext.Telefone.SingleAsync(x => x.ClienteId == clienteId && x.Ddd.Equals(ddd) && x.Numero.Equals(numero));

            telefone.Tipo = telefoneAtualizado.Tipo;
            telefone.Numero = telefoneAtualizado.Numero;

            _appDbContext.Telefone.Update(telefone);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> PhoneAlreadUsed(string ddd, string numero) =>
            await _appDbContext.Telefone.AnyAsync(x => x.Ddd.Equals(ddd) && x.Numero.Equals(numero));

        public async Task<IEnumerable<string>> PhonesAlreadUsed(IEnumerable<string> dddNumeros) => 
            await _appDbContext.Telefone.Where(x => dddNumeros.Contains(string.Concat(x.Ddd, x.Numero))).Select(x => $"({x.Ddd}) {x.Numero}").ToArrayAsync();

        public async Task<bool> PhoneBelong(Guid clienteId, string ddd, string numero) =>
            await _appDbContext.Telefone.AnyAsync(x => x.ClienteId == clienteId && x.Ddd.Equals(ddd) && x.Numero.Equals(numero));
    }
}
