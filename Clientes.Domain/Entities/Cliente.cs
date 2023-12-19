namespace Clientes.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public IEnumerable<Telefone> Telefones { get; set; }

        public Cliente() : this(string.Empty, string.Empty, new List<Telefone>()) { }

        public Cliente(string nome, string email, IEnumerable<Telefone> telefones)
        {
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }
    }
}
