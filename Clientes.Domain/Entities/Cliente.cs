namespace Clientes.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Telefones { get; set; }

        public Cliente(string nome, string email, IEnumerable<string> telefones)
        {
            Nome = nome;
            Email = email;
            Telefones = telefones;
        }
    }
}
