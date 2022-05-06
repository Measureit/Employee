using Employee.Domain.Exceptions;
using Employee.Framework;
using System;

namespace Employee.Domain.EmployeeAggregate
{
    public class EmployeeEntity : Entity, IAggregateRoot
    {
        public RegistrationNumber Number { get; private set; }
        public Surname Surname { get; private set; }
        public Gender Gender { get; private set; }
        public EmployeeEntity(Guid id, RegistrationNumber number, Surname surname, Gender gender)
        {
            Id = id;
            Number = number is not null ? number : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
            Surname = surname is not null ? surname : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
            Gender = gender is not null ? gender : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
        }

        public EmployeeEntity Update(RegistrationNumber number, Surname surname, Gender gender)
        {

            Number = number;
            Surname = surname;
            Gender = gender;
            return this;
        }
    }
}
