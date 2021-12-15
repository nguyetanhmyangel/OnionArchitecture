using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.AppPermissions.Commands.Create;
using OnionArchitecture.Application.Features.AppPermissions.Commands.Delete;
using OnionArchitecture.Application.Features.AppPermissions.Commands.Update;
using OnionArchitecture.Application.Features.AppPermissions.Queries.Get;
using OnionArchitecture.Application.Features.AppPermissions.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class AppPermissionController : BaseApiController<AppPermissionController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var privileges = await _mediator.Send(new GetAppPermissionQuery());
            return Ok(privileges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var privilege = await _mediator.Send(new GetAppPermissionByIdQuery() { Id = id });
            return Ok(privilege);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateAppPermissionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAppPermissionCommand command)
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
            return Ok(await _mediator.Send(new DeleteAppPermissionCommand { Id = id }));
        }
    }
}