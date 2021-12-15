using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.KnowledgeBases.Commands.Create;
using OnionArchitecture.Application.Features.KnowledgeBases.Commands.Delete;
using OnionArchitecture.Application.Features.KnowledgeBases.Commands.Update;
using OnionArchitecture.Application.Features.KnowledgeBases.Queries.Get;
using OnionArchitecture.Application.Features.KnowledgeBases.Queries.GetById;
using System.Threading.Tasks;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class KnowledgeBaseController : BaseApiController<KnowledgeBaseController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var labelMySpaces = await _mediator.Send(new GetKnowledgeBaseQuery());
            return Ok(labelMySpaces);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var labelMySpace = await _mediator.Send(new GetKnowledgeBaseByIdQuery() { Id = id });
            return Ok(labelMySpace);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateKnowledgeBaseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateKnowledgeBaseCommand command)
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
            return Ok(await _mediator.Send(new DeleteKnowledgeBaseCommand { Id = id }));
        }
    }
}