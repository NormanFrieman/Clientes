using Clientes.Domain.Entities;
using Clientes.Domain.Enums;

namespace Clientes.Application.Dtos
{
    public class TelefoneDto
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }

        public TelefoneDto() : this(string.Empty, string.Empty) { }

        public TelefoneDto(string numero, string tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }

        public TelefoneDto(Telefone telefone)
        {
            Numero = telefone.Numero;
            Tipo = telefone.Tipo == ETelefoneTipo.Fixo ? TelefoneTipo.FIXO : TelefoneTipo.CELULAR;
        }
    }
}
