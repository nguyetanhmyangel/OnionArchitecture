using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Features.Reports.Commands.Create;
using OnionArchitecture.Application.Features.Reports.Commands.Delete;
using OnionArchitecture.Application.Features.Reports.Commands.Update;
using OnionArchitecture.Application.Features.Reports.Queries.Get;
using OnionArchitecture.Application.Features.Reports.Queries.GetById;

namespace OnionArchitecture.Api.Controllers.v1
{
    public class ReportController : BaseApiController<ReportController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _mediator.Send(new GetReportQuery());
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _mediator.Send(new GetReportByIdQuery() { Id = id });
            return Ok(report);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateReportCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateReportCommand command)
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
            return Ok(await _mediator.Send(new DeleteReportCommand { Id = id }));
        }
    }
}