using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Labels.Commands.Create;
using OnionArchitecture.Application.Features.Labels.Commands.Delete;
using OnionArchitecture.Application.Features.Labels.Commands.Update;
using OnionArchitecture.Application.Features.Labels.Queries.Get;
using OnionArchitecture.Application.Features.Labels.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class LabelController : BaseApiController<LabelController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labels = await _mediator.Send(new GetLabelQuery());
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var label = await _mediator.Send(new GetLabelByIdQuery() { Id = id });
            return Ok(label);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateLabelCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateLabelCommand command)
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
            return Ok(await _mediator.Send(new DeleteLabelCommand { Id = id }));
        }
    }
}