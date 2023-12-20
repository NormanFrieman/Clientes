using Clientes.Domain.Enums;

namespace Clientes.Domain.Entities
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Ddd { get; set; }
        public ETelefoneTipo Tipo { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Telefone() : this(string.Empty, string.Empty, ETelefoneTipo.Fixo, null!) { }

        public Telefone(string numero, string ddd, ETelefoneTipo tipo) : this(numero, ddd, tipo, null!) { }

        public Telefone(string numero, string ddd, string tipo) : this(numero, ddd, TelefoneTipo.FIXO.Equals(tipo) ? ETelefoneTipo.Fixo : ETelefoneTipo.Celular) { }

        public Telefone(string numero, string ddd, ETelefoneTipo tipo, Cliente cliente)
        {
            Numero = numero;
            Ddd = ddd;
            Tipo = tipo;
            Cliente = cliente;
        }
    }
}
