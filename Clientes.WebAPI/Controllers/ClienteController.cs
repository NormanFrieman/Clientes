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
        private readonly IValidator<ClienteDto> _validator;
        private readonly IClienteService _clienteService;

        public ClienteController(IValidator<ClienteDto> validator, IClienteService clienteService)
        {
            _validator = validator;
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto cliente)
        {
            var validation = await _validator.ValidateAsync(cliente);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            return Ok(await _clienteService.CreateCliente(cliente));
        }

        [HttpGet]
        public async Task<IActionResult> Read([FromQuery] string? numero = null)
        {
            return Ok(await _clienteService.GetClientes(numero));
        }

        [HttpPut("{clienteId}")]
        public async Task<IActionResult> Update([FromRoute] Guid clienteId, [FromBody] string email)
        {
            return Ok(await _clienteService.UpdateCliente(clienteId, email));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete([FromRoute] string email)
        {
            await _clienteService.DeleteCliente(email);
            return Ok();
        }
    }
}
