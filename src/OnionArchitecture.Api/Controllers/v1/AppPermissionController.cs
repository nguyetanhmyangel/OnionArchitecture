using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Privileges.Commands.Create;
using OnionArchitecture.Application.Features.Privileges.Commands.Delete;
using OnionArchitecture.Application.Features.Privileges.Commands.Update;
using OnionArchitecture.Application.Features.Privileges.Queries.Get;
using OnionArchitecture.Application.Features.Privileges.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class AppPermissionController : BaseApiController<AppPermissionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var privileges = await _mediator.Send(new GetPermissionQuery());
            return Ok(privileges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var privilege = await _mediator.Send(new GetPermissionByIdQuery() { Id = id });
            return Ok(privilege);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePermissionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePermissionCommand command)
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
            return Ok(await _mediator.Send(new DeletePermissionCommand { Id = id }));
        }
    }
}