using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Categories.Commands.Create;
using OnionArchitecture.Application.Features.Categories.Commands.Delete;
using OnionArchitecture.Application.Features.Categories.Commands.Update;
using OnionArchitecture.Application.Features.Categories.Queries.Get;
using OnionArchitecture.Application.Features.Categories.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class CategoryController : BaseApiController<CategoryController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoryQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return Ok(category);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCategoryCommand command)
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
            return Ok(await _mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}