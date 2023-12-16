namespace Clientes.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string[] Telefones { get; set; }

        public Cliente() : this(string.Empty, string.Empty, Array.Empty<string>()) { }

        public Cliente(string nome, string email, string[] telefones)
        {
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }
    }
}
