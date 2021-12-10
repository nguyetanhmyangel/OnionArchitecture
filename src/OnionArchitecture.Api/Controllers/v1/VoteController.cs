using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Votes.Commands.Create;
using OnionArchitecture.Application.Features.Votes.Commands.Delete;
using OnionArchitecture.Application.Features.Votes.Commands.Update;
using OnionArchitecture.Application.Features.Votes.Queries.Get;
using OnionArchitecture.Application.Features.Votes.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class VoteController : BaseApiController<VoteController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var votes = await _mediator.Send(new GetVoteQuery());
            return Ok(votes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vote = await _mediator.Send(new GetVoteByIdQuery() { Id = id });
            return Ok(vote);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateVoteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateVoteCommand command)
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
            return Ok(await _mediator.Send(new DeleteVoteCommand { Id = id }));
        }
    }
}