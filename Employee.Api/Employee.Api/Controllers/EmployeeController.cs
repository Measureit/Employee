using Employee.Contract.Commands;
using Employee.Contract.Queries;
using Employee.Domain.EmployeeAggregate;
using Microsoft.AspNetCore.Mvc;
using Middlink.Core.CQRS.Dispatchers;
using Middlink.Core.MessageBus;
using Middlink.MVC.Controllers;
using System;
using System.Threading.Tasks;

namespace Employee.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class EmployeeController : BaseController
    {
        public EmployeeController(
          IPublisher busPublisher,
          IQueryDispatcher queryDispatcher) : base(busPublisher, queryDispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEntity>> Get(Guid id)
         => Single(await QueryAsync(new Get<EmployeeEntity>(id)));


        [HttpPost]
        public Task<IActionResult> Create(CreateEmployee command)
        {
            var cmd = command with { AggregateId = Guid.NewGuid() };
            return SendAsync(cmd, resourceId: cmd.AggregateId, resource: "employee/get");
        }

        [HttpPut]
        public Task<IActionResult> Update(UpdateEmployee command)
        {
            return SendAsync(command, resourceId: command.AggregateId, resource: "employee/get");
        }
    }
}