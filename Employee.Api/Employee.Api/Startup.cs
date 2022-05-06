using Autofac;
using Employee.Api.Options;
using Employee.Application.Handlers.Queries;
using Employee.Contract.Commands;
using Employee.Contract.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Middlink.Core.CQRS.Events;
using Middlink.CQRS.Autofac.Extensions;
using Middlink.CQRS.Operations.SignalR.Extensions;
using Middlink.Extensions;
using Middlink.MessageBus.InMemory.Autofac;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Employee.Api
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-ResourceId", "X-Total-Count" };
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyModules(Assembly.GetEntryAssembly());
            containerBuilder.AddInMemoryMessageBus();
            containerBuilder.AddCQRS(new[] {
                typeof(EmployeeQueryHandler).Assembly
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOperations();

            services.AddMvcCore()
            .AddDataAnnotations()
            .AddApiExplorer()
            .AddJsonOptions(o =>
            {
                var t = o.JsonSerializerOptions.DefaultIgnoreCondition;
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                    cors
                    .AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials()
                       .WithExposedHeaders(Headers));
            });

            AddSignalR(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMPLOYEE API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMPLOYEE API V1");
            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<OperationsHub>($"/operations");
                endpoints.MapControllers();
            });

            app.UseCQRS((b) =>
            {
                b
                //EMPLOYEE
                .SubscribeCommand<CreateEmployee>(onError: (c, e) => new RejectedEvent<Guid>(c.AggregateId, e.Message, e.Code))
                .SubscribeCommand<UpdateEmployee>(onError: (c, e) => new RejectedEvent<Guid>(c.AggregateId, e.Message, e.Code))
                //GENERAL
                .SubscribeAllEvents(new[] {
                    typeof(EmployeeCreated).Assembly
                })
                .SubscribeEvent<RejectedEvent<Guid>>();
            });
        }

        private void AddSignalR(IServiceCollection services)
        {
            var options = Configuration.GetOptions<SignalrOptions>("signalr");
            services
             .AddSingleton(options)
             .AddSignalR().AddJsonProtocol();
        }
    }
}