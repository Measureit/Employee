using Middlink.Core.CQRS.Events;
using System;

namespace Employee.Contract.Events
{
    public record EmployeeCreated(Guid AggregateId) : IDomainEvent<Guid>;
}
