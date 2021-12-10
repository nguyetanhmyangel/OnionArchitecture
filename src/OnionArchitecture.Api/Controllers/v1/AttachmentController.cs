using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Attachments.Commands.Create;
using OnionArchitecture.Application.Features.Attachments.Commands.Delete;
using OnionArchitecture.Application.Features.Attachments.Commands.Update;
using OnionArchitecture.Application.Features.Attachments.Queries.Get;
using OnionArchitecture.Application.Features.Attachments.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public class AttachmentController : BaseApiController<AttachmentController>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attachments = await _mediator.Send(new GetAttachmentQuery());
            return Ok(attachments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attachment = await _mediator.Send(new GetAttachmentByIdQuery() { Id = id });
            return Ok(attachment);
        }

        // POST api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateAttachmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAttachmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteAttachmentCommand { Id = id }));
        }
    }
}