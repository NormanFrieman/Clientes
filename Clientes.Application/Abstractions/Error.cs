using System.Net;

namespace Clientes.Application.Abstractions
{
    public record class Error
    {
        public HttpStatusCode Code { get; }
        public string[] Messages { get; } = Array.Empty<string>();

        public Error(HttpStatusCode code, params string[] messages)
        {
            Code = code;
            Messages = messages;
        }

        public static readonly Error None = new(HttpStatusCode.OK, string.Empty);
        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}