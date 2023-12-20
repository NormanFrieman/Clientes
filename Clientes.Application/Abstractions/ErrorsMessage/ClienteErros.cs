using System.Net;

namespace Clientes.Application.Abstractions.ErrorsMessage
{
    public static class ClienteErrors
    {
        public static readonly Error EmailAlreadyUsed = new(HttpStatusCode.BadRequest, "Email já utilizado");
        public static readonly Error EmailNotFound = new(HttpStatusCode.NotFound, "Email não encontrado");
        public static readonly Error UserNotFound = new(HttpStatusCode.NotFound, "Cliente não encontrado");
    }
}