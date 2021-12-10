using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Enjoins.Commands.Create;
using OnionArchitecture.Application.Features.Enjoins.Commands.Delete;
using OnionArchitecture.Application.Features.Enjoins.Commands.Update;
using OnionArchitecture.Application.Features.Enjoins.Queries.Get;
using OnionArchitecture.Application.Features.Enjoins.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class EnjoinController : BaseApiController<EnjoinController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var enjoins = await _mediator.Send(new GetEnjoinQuery());
            return Ok(enjoins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enjoin = await _mediator.Send(new GetEnjoinByIdQuery() { Id = id });
            return Ok(enjoin);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEnjoinCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateEnjoinCommand command)
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
            return Ok(await _mediator.Send(new DeleteEnjoinCommand { Id = id }));
        }
    }
}