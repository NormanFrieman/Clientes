using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto cliente)
        {
            var result = await _clienteService.CreateCliente(cliente);
            if (!result.IsSuccess)
                return StatusCode((int)result.Error.Code, result.Error.Messages);

            return Ok(result.Body);
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            return Ok(await _clienteService.GetClientes());
        }

        [HttpGet("{ddd}/{numero}")]
        public async Task<IActionResult> ReadByPhone([FromRoute] string ddd, string numero)
        {
            return Ok(await _clienteService.GetClientes(ddd + numero));
        }

        [HttpPut("{clienteId}")]
        public async Task<IActionResult> Update([FromRoute] Guid clienteId, [FromBody] string email)
        {
            var result = await _clienteService.UpdateCliente(clienteId, email);
            if (!result.IsSuccess)
                return StatusCode((int)result.Error.Code, result.Error.Messages);

            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete([FromRoute] string email)
        {
            var result = await _clienteService.DeleteCliente(email);
            if (!result.IsSuccess)
                return StatusCode((int)result.Error.Code, result.Error.Messages);

            return NoContent();
        }
    }
}
