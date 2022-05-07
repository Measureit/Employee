using Bogus;
using Employee.Application.Handlers.Commands;
using Employee.Application.Services;
using Employee.Contract.Commands;
using Employee.Contract.Events;
using Employee.Domain.EmployeeAggregate;
using Middlink.Core;
using Middlink.Core.MessageBus;
using Middlink.Core.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Domain.UnitTest.Application.Handlers.Commands
{
    public class EmployeeCommandHandlerUnitTest
    {
        public static IEnumerable<object[]> CorrectSetOfDataCommands =>
            new List<object[]>
            {
                new object[] { new CreateEmployee(Guid.NewGuid(), new Faker().Random.Chars(count: 1).ToString(), 0) },
            };

        [Theory]
        [MemberData(nameof(CorrectSetOfDataCommands))]
        public async Task HandleCreateEmployeeCommand_CorrectParemeters_EmployeeCreated(CreateEmployee command)
        {
            // Arrange
            var publisher =  new Mock<IPublisher>();
            var repository = new Mock<IRepository<EmployeeEntity>>();
            var sequenceGenerator = new Mock<IRegistrationNumberGenerator>();
            var ctx = new Mock<ICorrelationContext>();
            var handler = new EmployeeCommandHandler(publisher.Object, repository.Object, sequenceGenerator.Object);
            sequenceGenerator.Setup(r => r.GetNextAsync()).ReturnsAsync(1);

            // Act
            await handler.HandleAsync(command, ctx.Object);

            // Asset
            publisher.Verify(foo => foo.PublishAsync(It.IsAny<EmployeeCreated>(), ctx.Object), Times.Once());
            repository.Verify(foo => foo.AddAsync(It.IsAny<EmployeeEntity>()), Times.Once());
        }
    }
}
