using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Privileges.Commands.Create;
using OnionArchitecture.Application.Features.Privileges.Commands.Delete;
using OnionArchitecture.Application.Features.Privileges.Commands.Update;
using OnionArchitecture.Application.Features.Privileges.Queries.Get;
using OnionArchitecture.Application.Features.Privileges.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class PrivilegeController : BaseApiController<PrivilegeController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var privileges = await _mediator.Send(new GetPrivilegeQuery());
            return Ok(privileges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var privilege = await _mediator.Send(new GetPrivilegeByIdQuery() { Id = id });
            return Ok(privilege);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePrivilegeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePrivilegeCommand command)
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
            return Ok(await _mediator.Send(new DeletePrivilegeCommand { Id = id }));
        }
    }
}