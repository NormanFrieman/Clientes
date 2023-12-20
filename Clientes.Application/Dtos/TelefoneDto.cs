using Clientes.Domain.Entities;
using Clientes.Domain.Enums;

namespace Clientes.Application.Dtos
{
    public class TelefoneDto
    {
        public string Numero { get; set; }
        public string Ddd { get; set; }
        public string Tipo { get; set; }

        public TelefoneDto() : this(string.Empty, string.Empty, string.Empty) { }

        public TelefoneDto(string numero, string ddd, string tipo)
        {
            Numero = numero;
            Ddd = ddd;
            Tipo = tipo;
        }

        public TelefoneDto(Telefone telefone)
        {
            Numero = telefone.Numero;
            Ddd = telefone.Ddd;
            Tipo = telefone.Tipo == ETelefoneTipo.Fixo ? TelefoneTipo.FIXO : TelefoneTipo.CELULAR;
        }
    }
}
