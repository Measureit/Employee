using Employee.Contract.Queries;
using Employee.Domain.EmployeeAggregate;
using Middlink.Core.CQRS.Handlers;
using Middlink.Core.Storage;
using System.Threading.Tasks;

namespace Employee.Application.Handlers.Queries
{
    public class EmployeeQueryHandler : IQueryHandler<Get<EmployeeEntity>, EmployeeEntity>
    {
        private readonly IRepository<EmployeeEntity> _repository;
        public EmployeeQueryHandler(IRepository<EmployeeEntity> repository)
        {
            _repository = repository;
        }

        public Task<EmployeeEntity> HandleAsync(Get<EmployeeEntity> query)
            => _repository
                .GetAsync(query.Id)
                .AsTask();
    }
}
