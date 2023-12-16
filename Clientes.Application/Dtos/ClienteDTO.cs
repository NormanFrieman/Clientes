using System.ComponentModel.DataAnnotations;

namespace Clientes.Application.Dtos
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }

        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Telefones { get; set; }

        public ClienteDTO(Guid id, string nomeCompleto, string email, IEnumerable<string> telefones)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Email = email;
            Telefones = telefones;
        }
    }
}
