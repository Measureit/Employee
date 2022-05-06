using Autofac;
using Employee.Domain.EmployeeAggregate;
using Employee.Infrastructure.Repositories;
using Middlink.Core.Storage;

namespace Employee.Api.Modules
{
    public class StoragesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEmployeeRepository>()
                .As<IRepository<EmployeeEntity>>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
