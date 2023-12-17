using Clientes.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Application.Dtos
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string[] Telefones { get; set; }

        public ClienteDTO(Guid id, string nome, string email, string[] telefones)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }

        public static implicit operator ClienteDTO(Cliente cliente) =>
            new ClienteDTO(cliente.Id, cliente.Nome, cliente.Email, cliente.Telefones);
    }
}
