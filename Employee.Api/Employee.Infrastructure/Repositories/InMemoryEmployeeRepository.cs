using Employee.Domain.EmployeeAggregate;
using Middlink.Core.CQRS.Queries;
using Middlink.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public class InMemoryEmployeeRepository : IRepository<EmployeeEntity>
    {
        private readonly IDictionary<Guid, EmployeeEntity> _storage = new Dictionary<Guid, EmployeeEntity>();
        public Task AddAsync(EmployeeEntity entity)
        {
            _storage.Add(entity.Id, entity);
            return Task.CompletedTask;
        }

        public Task AddManyAsync(IEnumerable<EmployeeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<EmployeeEntity>> BrowseAsync<TQuery>(Expression<Func<EmployeeEntity, bool>> predicate, TQuery query) where TQuery : PagedQueryBase
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> ExistsAsync(Expression<Func<EmployeeEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<EmployeeEntity>> FindAsync(Expression<Func<EmployeeEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<EmployeeEntity>> FindAsync(Expression<Func<EmployeeEntity, bool>> predicate, Expression<Func<EmployeeEntity, object>> orderSelector)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<EmployeeEntity>> FindAsync(Expression<Func<EmployeeEntity, bool>> predicate, Expression<Func<EmployeeEntity, object>> orderSelector, int limit)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<EmployeeEntity>> FindAsync(Expression<Func<EmployeeEntity, bool>> predicate, int limit)
        {
            throw new NotImplementedException();
        }

        public ValueTask<EmployeeEntity> GetAsync(Guid id)
            => new ValueTask<EmployeeEntity>(Task.FromResult(_storage[id]));

        public ValueTask<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> predicate)
        {
            var expression = predicate.Compile();
            return new ValueTask<EmployeeEntity>(Task.FromResult(_storage.Values.Single(x => expression(x))));
        }

        public Task UpdateAsync(EmployeeEntity entity)
        {
            _storage[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }
}
