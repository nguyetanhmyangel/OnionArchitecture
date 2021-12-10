using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.LabelMySpaces.Commands.Create;
using OnionArchitecture.Application.Features.LabelMySpaces.Commands.Delete;
using OnionArchitecture.Application.Features.LabelMySpaces.Commands.Update;
using OnionArchitecture.Application.Features.LabelMySpaces.Queries.Get;
using OnionArchitecture.Application.Features.LabelMySpaces.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class LabelMySpaceController : BaseApiController<LabelMySpaceController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labelMySpaces = await _mediator.Send(new GetLabelMySpaceQuery());
            return Ok(labelMySpaces);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var labelMySpace = await _mediator.Send(new GetLabelMySpaceByIdQuery() { Id = id });
            return Ok(labelMySpace);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateLabelMySpaceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateLabelMySpaceCommand command)
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
            return Ok(await _mediator.Send(new DeleteLabelMySpaceCommand { Id = id }));
        }
    }
}