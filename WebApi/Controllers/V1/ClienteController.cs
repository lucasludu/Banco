using Application.Feautures.Clientes.Commands.CreateClienteCommand;
using Application.Feautures.Clientes.Commands.DeleteClienteCommand;
using Application.Feautures.Clientes.Commands.UpdateClienteCommand;
using Application.Feautures.Clientes.Queries.GetAllClientes;
using Application.Feautures.Clientes.Queries.GetClienteByIdQuery;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ClienteController : BaseApiController
    {

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetAllClientesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllClienteQuery { 
                PageNumber = filter.PageNumber, 
                PageSize = filter.PageSize,
                Nombre = filter.Nombre,
                Apellido = filter.Apellido,
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateClienteCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClienteCommand { Id = id }));
        }
    }
}
