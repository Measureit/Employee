using Bogus;
using Employee.Domain.EmployeeAggregate;
using Employee.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Employee.Domain.UnitTest.Domain.EmployeeAggregate
{
    public class EmployeeUnitTest
    {
        public static IEnumerable<object[]> CorrectSetOfData =>
            new List<object[]>
            {
                new object[] { Guid.NewGuid(), new RegistrationNumber(1), new Surname(new Faker().Random.Chars(count: 1).ToString()), new Gender(0) },
                new object[] { Guid.NewGuid(), new RegistrationNumber(2), new Surname(new Faker().Random.Chars(count: 1).ToString()), new Gender(1) },
                new object[] { Guid.NewGuid(), new RegistrationNumber(3), new Surname(new Faker().Random.Chars(count: 10).ToString()), new Gender(0) },
                new object[] { Guid.NewGuid(), new RegistrationNumber(4), new Surname(new Faker().Random.Chars(count: 10).ToString()), new Gender(1) },
                new object[] { Guid.NewGuid(), new RegistrationNumber(5), new Surname(new Faker().Random.Chars(count: 50).ToString()), new Gender(0) },
                new object[] { Guid.NewGuid(), new RegistrationNumber(6), new Surname(new Faker().Random.Chars(count: 50).ToString()), new Gender(1) }
            };

        public static IEnumerable<object[]> IncorrectSetOfData =>
            new List<object[]>
            {
                        new object[] { Guid.Empty, new RegistrationNumber(1), new Surname(new Faker().Random.Chars(count: 1).ToString()), new Gender(0) },
                        new object[] { Guid.NewGuid(), null, new Surname(new Faker().Random.Chars(count: 1).ToString()), new Gender(1) },
                        new object[] { Guid.NewGuid(), new RegistrationNumber(3), null, new Gender(0) },
                        new object[] { Guid.NewGuid(), new RegistrationNumber(4), new Surname(new Faker().Random.Chars(count: 10).ToString()), null }
            };

        [Theory]
        [MemberData(nameof(CorrectSetOfData))]
        public void CreateEmployee_CorrectParemeters_EmployeeCreated(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange

            // Act
            var aggregate = new EmployeeEntity(id, number, surname, gender);

            // Asset
            Assert.Equal(aggregate.Id, id);
            Assert.Equal(aggregate.Number, number);
            Assert.Equal(aggregate.Surname, surname);
            Assert.Equal(aggregate.Gender, gender);
        }

        [Theory]
        [MemberData(nameof(IncorrectSetOfData))]
        public void CreateEmployee_IncorrectParemeters_ThrowNotSpecifiedException(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange

            // Act
            var ex = Assert.Throws<EmployeeException>(() => new EmployeeEntity(id, number, surname, gender));

            // Asset
            Assert.Equal(Codes.IS_NOT_SPECIFIED, ex.Code);
        }

        [Theory]
        [MemberData(nameof(CorrectSetOfData))]
        public void UpdateEmployee_ChangeSurname_SurnameUpdated(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange
            var aggregate = new EmployeeEntity(id, number, surname, gender);
            var newSurname = new Surname(new Faker().Random.Chars(count: 10).ToString());

            // Act
            aggregate.Update(newSurname, aggregate.Gender);

            // Asset
            Assert.Equal(aggregate.Surname, newSurname);
        }

        [Theory]
        [MemberData(nameof(CorrectSetOfData))]
        public void UpdateEmployee_ChangeGender_GenderUpdated(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange
            var aggregate = new EmployeeEntity(id, number, surname, gender);
            var newGender = new Gender(
                new Faker()
                .Random
                .CollectionItem(Enum.GetValues(typeof(GenderEnum))
                    .Cast<int>()
                    .Except(new[] { (int)gender.Value })
                    .ToList()));

            // Act
            aggregate.Update(aggregate.Surname, newGender);

            // Asset
            Assert.Equal(aggregate.Gender, newGender);
        }


        [Theory]
        [MemberData(nameof(CorrectSetOfData))]
        public void UpdateEmployee_ResetSurname_ThrowNotSpecifiedException(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange
            var aggregate = new EmployeeEntity(id, number, surname, gender);

            // Act
            var ex = Assert.Throws<EmployeeException>(() => aggregate.Update(null, aggregate.Gender));

            // Asset
            Assert.Equal(Codes.IS_NOT_SPECIFIED, ex.Code);
        }

        [Theory]
        [MemberData(nameof(CorrectSetOfData))]
        public void UpdateEmployee_ResetGender_ThrowNotSpecifiedException(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            // Arrange
            var aggregate = new EmployeeEntity(id, number, surname, gender);

            // Act
            var ex = Assert.Throws<EmployeeException>(() => aggregate.Update(aggregate.Surname, null));

            // Asset
            Assert.Equal(Codes.IS_NOT_SPECIFIED, ex.Code);
        }
    }
}