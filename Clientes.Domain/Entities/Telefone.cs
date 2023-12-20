using Clientes.Domain.Enums;

namespace Clientes.Domain.Entities
{
    public class Telefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public ETelefoneTipo Tipo { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Telefone() : this(string.Empty, ETelefoneTipo.Fixo, null!) { }

        public Telefone(string numero, ETelefoneTipo tipo) : this(numero, tipo, null!) { }

        public Telefone(string numero, string tipo) : this(numero, TelefoneTipo.FIXO.Equals(tipo) ? ETelefoneTipo.Fixo : ETelefoneTipo.Celular) { }

        public Telefone(string numero, ETelefoneTipo tipo, Cliente cliente)
        {
            Numero = numero;
            Tipo = tipo;
            Cliente = cliente;
        }
    }
}
