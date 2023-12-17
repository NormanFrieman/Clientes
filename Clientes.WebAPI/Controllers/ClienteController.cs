using Clientes.Application.Dtos;
using Clientes.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IValidator<ClienteDTO> _validator;
        private readonly IClienteService _service;

        public ClienteController(IValidator<ClienteDTO> validator, IClienteService clienteService)
        {
            _validator = validator;
            _service = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDTO cliente)
        {
            var validation = await _validator.ValidateAsync(cliente);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            return Ok(await _service.CreateCliente(cliente));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? telefone = null)
        {
            return Ok(await _service.GetClientes(telefone));
        }

        [HttpPatch("{clienteId}")]
        public async Task<IActionResult> Patch([FromRoute] Guid clienteId, [FromBody] ClienteDTO clienteAtualizado)
        {
            return Ok(await _service.UpdateCliente(clienteId, clienteAtualizado));
        }

        [HttpDelete("{clienteId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid clienteId)
        {
            await _service.DeleteCliente(clienteId);
            return Ok();
        }
    }
}
