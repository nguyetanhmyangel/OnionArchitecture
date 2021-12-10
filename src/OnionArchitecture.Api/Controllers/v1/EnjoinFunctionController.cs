using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Create;
using OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Delete;
using OnionArchitecture.Application.Features.EnjoinFunctions.Commands.Update;
using OnionArchitecture.Application.Features.EnjoinFunctions.Queries.Get;
using OnionArchitecture.Application.Features.EnjoinFunctions.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class EnjoinFunctionController : BaseApiController<EnjoinFunctionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enjoinFunctions = await _mediator.Send(new GetEnjoinFunctionQuery());
            return Ok(enjoinFunctions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enjoinFunction = await _mediator.Send(new GetEnjoinFunctionByIdQuery() { Id = id });
            return Ok(enjoinFunction);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEnjoinFunctionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateEnjoinFunctionCommand command)
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
            return Ok(await _mediator.Send(new DeleteEnjoinFunctionCommand { Id = id }));
        }
    }
}