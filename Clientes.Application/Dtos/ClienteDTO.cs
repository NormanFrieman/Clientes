using Clientes.Domain.Entities;

namespace Clientes.Application.Dtos
{
    public class ClienteDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public IEnumerable<TelefoneDto> Telefones { get; set; }

        public ClienteDto() : this(Guid.Empty, string.Empty, string.Empty, Enumerable.Empty<TelefoneDto>()) { }

        public ClienteDto(Guid id, string nome, string email, IEnumerable<TelefoneDto> telefones)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }

        public ClienteDto(string nome, string email, IEnumerable<TelefoneDto> telefones)
        {
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }

        public ClienteDto(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
            Email = cliente.Email;
            Telefones = cliente.Telefones.Select(x => new TelefoneDto(x)).ToArray();
        }
    }
}
