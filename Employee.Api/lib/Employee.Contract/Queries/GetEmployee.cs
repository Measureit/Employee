using Middlink.Core.CQRS.Queries;
using System;

namespace Employee.Contract.Queries
{
    public record Get<TResult>(Guid Id): IQuery<TResult>;
}
