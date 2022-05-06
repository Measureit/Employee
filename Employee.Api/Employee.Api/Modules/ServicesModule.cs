using Autofac;
using Employee.Application.Services;

namespace Employee.Api.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryRegistrationNumberGenerator>()
                .As<IRegistrationNumberGenerator>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
