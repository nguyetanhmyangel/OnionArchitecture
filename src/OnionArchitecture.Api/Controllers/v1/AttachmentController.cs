using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Attachments.Commands.Create;
using OnionArchitecture.Application.Features.Attachments.Commands.Delete;
using OnionArchitecture.Application.Features.Attachments.Commands.Update;
using OnionArchitecture.Application.Features.Attachments.Queries.Get;
using OnionArchitecture.Application.Features.Attachments.Queries.GetById;
using OnionArchitecture.Application.Features.Attachments.Queries.GetByKnowledgeBaseId;

namespace OnionArchitecture.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public class AttachmentController : BaseApiController<AttachmentController>
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var attachments = await _mediator.Send(new GetAttachmentQuery());
        //    return Ok(attachments);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var attachment = await _mediator.Send(new GetAttachmentByIdQuery() { Id = id });
        //    return Ok(attachment);
        //}


        [HttpGet("{knowledgeBaseId}/attachments")]
        public async Task<IActionResult> GetByKnowledgeBaseId(int knowledgeBaseId)
        {
            var attachment = 
                await _mediator.Send(new GetAttachmentByKnowledgeBaseIdQuery() { KnowledgeBaseId = knowledgeBaseId });
            return Ok(attachment);
        }


        //[HttpPost]
        //public async Task<IActionResult> Post(CreateAttachmentCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, UpdateAttachmentCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await _mediator.Send(command));
        //}

        [HttpDelete("{knowledgeBaseId}/attachments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteAttachmentCommand { Id = id }));
        }
    }
}