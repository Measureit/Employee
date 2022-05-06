using Middlink.Core.CQRS.Commands;
using Middlink.Core.CQRS.Events;
using System;

namespace Employee.Contract.Events
{
    public record EmployeeUpdated(Guid AggregateId) : IDomainEvent<Guid>;
}
