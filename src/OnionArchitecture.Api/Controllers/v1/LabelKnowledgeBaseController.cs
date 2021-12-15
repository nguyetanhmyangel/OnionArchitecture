using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Create;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Delete;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Commands.Update;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.Get;
using OnionArchitecture.Application.Features.LabelKnowledgeBases.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class LabelKnowledgeBaseController : BaseApiController<LabelKnowledgeBaseController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labelMySpaces = await _mediator.Send(new GetLabelKnowledgeBaseQuery());
            return Ok(labelMySpaces);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var labelMySpace = await _mediator.Send(new GetLabelKnowledgeBaseByIdQuery() { Id = id });
            return Ok(labelMySpace);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateLabelKnowledgeBaseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateLabelKnowledgeBaseCommand command)
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
            return Ok(await _mediator.Send(new DeleteLabelKnowledgeBaseCommand { Id = id }));
        }
    }
}