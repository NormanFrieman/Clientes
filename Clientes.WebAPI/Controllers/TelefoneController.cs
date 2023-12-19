using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.WebAPI.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class TelefoneController : Controller
    {
        private readonly ITelefoneService _telefoneService;

        public TelefoneController(ITelefoneService telefoneService)
        {
            _telefoneService = telefoneService;
        }

        [HttpPut("{clienteId}/telefone/{numero}")]
        public async Task<IActionResult> UpdateTelefone([FromRoute] Guid clienteId, [FromRoute] string numero, [FromBody] TelefoneDto telefoneAtualizado)
        {
            return Ok(await _telefoneService.UpdateTelefone(clienteId, numero, telefoneAtualizado));
        }
    }
}
