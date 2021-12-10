using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Functions.Commands.Create;
using OnionArchitecture.Application.Features.Functions.Commands.Delete;
using OnionArchitecture.Application.Features.Functions.Commands.Update;
using OnionArchitecture.Application.Features.Functions.Queries.Get;
using OnionArchitecture.Application.Features.Functions.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class FunctionController : BaseApiController<FunctionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var functions = await _mediator.Send(new GetFunctionQuery());
            return Ok(functions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var function = await _mediator.Send(new GetFunctionByIdQuery() { Id = id });
            return Ok(function);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateFunctionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateFunctionCommand command)
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
            return Ok(await _mediator.Send(new DeleteFunctionCommand { Id = id }));
        }
    }
}