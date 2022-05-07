using Employee.Domain.EmployeeAggregate;
using Employee.Domain.Exceptions;
using Xunit;

namespace Employee.Domain.UnitTest.Domain.EmployeeAggregate
{
    public class RegistrationNumberUnitTest
    {
        [Theory]
        [InlineData(1, "00000001")]
        [InlineData(2, "00000002")]
        [InlineData(10000001, "10000001")]
        public void CreateRegistrationNumber_CorrectParemeters_RegistrationNumberCreated(int number, string expected)
        {
            // Arrange

            // Act
            var registrationNumber = new RegistrationNumber(number);

            // Asset
            Assert.Equal(registrationNumber.Value, expected);
        }

        [Theory]
        [InlineData(100000010)]
        [InlineData(-1)]
        public void CreateEmployee_IncorrectParemeters_ThrowNotInRangeException(int number)
        {
            // Arrange

            // Act
            var ex = Assert.Throws<EmployeeException>(() => new RegistrationNumber(number));

            // Asset
            Assert.Equal(Codes.REGISTRATION_NUMBER_NOT_IN_RANGE, ex.Code);
        }
    }
}