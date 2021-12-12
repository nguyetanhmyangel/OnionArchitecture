using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Create;
using OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Delete;
using OnionArchitecture.Application.Features.AppCommandFunctions.Commands.Update;
using OnionArchitecture.Application.Features.AppCommandFunctions.Queries.Get;
using OnionArchitecture.Application.Features.AppCommandFunctions.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class AppCommandFunctionController : BaseApiController<AppCommandFunctionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enjoinFunctions = await _mediator.Send(new GetAppCommandFunctionQuery());
            return Ok(enjoinFunctions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enjoinFunction = await _mediator.Send(new GetAppCommandFunctionByIdQuery() { Id = id });
            return Ok(enjoinFunction);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateAppCommandFunctionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAppCommandFunctionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteAppCommandFunctionCommand { Id = id }));
        }
    }
}