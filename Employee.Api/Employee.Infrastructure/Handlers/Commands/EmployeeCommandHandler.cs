using Employee.Contract.Commands;
using Employee.Contract.Events;
using Employee.Domain.EmployeeAggregate;
using Middlink.Core;
using Middlink.Core.CQRS.Handlers;
using Middlink.Core.MessageBus;
using Middlink.Core.Storage;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Handlers.Commands
{
    public class EmployeeCommandHandler : 
        ICommandHandler<CreateEmployee>,
        ICommandHandler<UpdateEmployee>
    {
        private readonly IRepository<EmployeeEntity> _repository;
        private readonly IPublisher _publisher;
        public EmployeeCommandHandler(IPublisher publisher, IRepository<EmployeeEntity> repository)
        {
            _publisher = publisher;
            _repository = repository;
        }

        public async Task HandleAsync(CreateEmployee command, ICorrelationContext context)
        {
            var aggregate = new EmployeeEntity(command.AggregateId, RegistrationNumber.From(command.RegistrationNumber), Surname.From(command.Surname), Gender.From(command.Gender));
            await _repository.AddAsync(aggregate);
            await _publisher.PublishAsync(new EmployeeCreated(aggregate.Id), context);
        }

        public async Task HandleAsync(UpdateEmployee command, ICorrelationContext context)
        {
            var aggregate = await _repository.GetAsync(command.AggregateId);
            aggregate.Update(RegistrationNumber.From(command.RegistrationNumber), Surname.From(command.Surname), Gender.From(command.Gender));
            await _repository.UpdateAsync(aggregate);
            await _publisher.PublishAsync(new EmployeeUpdated(aggregate.Id), context);
        }
    }
}
