using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Comments.Commands.Create;
using OnionArchitecture.Application.Features.Comments.Commands.Delete;
using OnionArchitecture.Application.Features.Comments.Commands.Update;
using OnionArchitecture.Application.Features.Comments.Queries.Get;
using OnionArchitecture.Application.Features.Comments.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class CommentController : BaseApiController<CommentController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _mediator.Send(new GetCommentQuery());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _mediator.Send(new GetCommentByIdQuery() { Id = id });
            return Ok(comment);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCommentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCommentCommand command)
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
            return Ok(await _mediator.Send(new DeleteCommentCommand { Id = id }));
        }
    }
}