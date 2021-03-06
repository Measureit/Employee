using Employee.Application.Services;
using Employee.Contract.Commands;
using Employee.Contract.Events;
using Employee.Domain.EmployeeAggregate;
using Middlink.Core;
using Middlink.Core.CQRS.Handlers;
using Middlink.Core.MessageBus;
using Middlink.Core.Storage;
using System.Threading.Tasks;

namespace Employee.Application.Handlers.Commands
{
    public class EmployeeCommandHandler :
        ICommandHandler<CreateEmployee>,
        ICommandHandler<UpdateEmployee>
    {
        private readonly IRepository<EmployeeEntity> _repository;
        private readonly IPublisher _publisher;
        private readonly IRegistrationNumberGenerator _sequenceGenerator;
        public EmployeeCommandHandler(IPublisher publisher, IRepository<EmployeeEntity> repository, IRegistrationNumberGenerator sequenceGenerator)
        {
            _repository = repository;
            _publisher = publisher;
            _sequenceGenerator = sequenceGenerator;
        }

        public async Task HandleAsync(CreateEmployee command, ICorrelationContext context)
        {
            var sequence = await _sequenceGenerator.GetNextAsync();
            var aggregate = new EmployeeEntity(command.AggregateId, new RegistrationNumber(sequence), new Surname(command.Surname), new Gender(command.Gender));
            await _repository.AddAsync(aggregate);
            await _publisher.PublishAsync(new EmployeeCreated(aggregate.Id), context);
        }

        public async Task HandleAsync(UpdateEmployee command, ICorrelationContext context)
        {
            var aggregate = await _repository.GetAsync(command.AggregateId);
            aggregate.Update(new Surname(command.Surname), new Gender(command.Gender));
            await _repository.UpdateAsync(aggregate);
            await _publisher.PublishAsync(new EmployeeUpdated(aggregate.Id), context);
        }
    }
}
