using Clientes.Application.Interfaces;

namespace Clientes.Application.Services
{
    public class Sessao : ISessao
    {
        public DateTime CurrentTime() =>
            DateTime.UtcNow;
    }
}
