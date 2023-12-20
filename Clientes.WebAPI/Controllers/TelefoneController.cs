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

        [HttpPut("{clienteId}/telefone/{ddd}/{numero}")]
        public async Task<IActionResult> UpdateTelefone([FromRoute] Guid clienteId, [FromRoute] string ddd, [FromRoute] string numero, [FromBody] TelefoneDto telefoneAtualizado)
        {
            var result = await _telefoneService.UpdateTelefone(clienteId, ddd, numero, telefoneAtualizado);
            if (!result.IsSuccess)
                return StatusCode((int)result.Error.Code, result.Error.Messages);

            return Ok();
        }
    }
}
