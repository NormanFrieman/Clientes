using System.Net;

namespace Clientes.Application.Abstractions.ErrorsMessage
{
    public static class TelefoneErros
    {
        public static Error PhoneNotBelong = new(HttpStatusCode.BadRequest, "Telefone não pertence ao cliente");

        public static Error PhoneAlreadyUsed(string telefone) =>
            new(HttpStatusCode.BadRequest, $"{telefone} já está em uso");

        public static Error PhonesAlreadyUsed(IEnumerable<string> telefones) =>
            new(HttpStatusCode.BadRequest, telefones.Select(x => $"{x} já está em uso").ToArray());
    }
}
