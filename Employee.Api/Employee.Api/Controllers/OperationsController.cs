using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middlink.Core.CQRS.Dispatchers;
using Middlink.Core.CQRS.Operations;
using Middlink.Core.MessageBus;
using Middlink.MVC.Controllers;
using System;
using System.Threading.Tasks;

namespace DwsPortal.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class OperationsController : BaseController
    {
        private readonly IOperationsStorage _operationsStorage;

        public OperationsController(
          IPublisher busPublisher,
          IOperationsStorage operationsStorage,
          IQueryDispatcher queryDispatcher) : base(busPublisher, queryDispatcher)
        {
            _operationsStorage = operationsStorage;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> Get(Guid id)
            => Single(await _operationsStorage.GetAsync(id));
    }
}

