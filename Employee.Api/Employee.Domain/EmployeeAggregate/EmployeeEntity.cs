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
            Id = id != default ? id : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
            Number = number is not null ? number : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
            Surname = surname is not null ? surname : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
            Gender = gender is not null ? gender : throw new EmployeeException(Codes.IS_NOT_SPECIFIED);
        }

        public EmployeeEntity Update(Surname surname, Gender gender)
        {
            if(surname is null || gender is null) 
                throw new EmployeeException(Codes.IS_NOT_SPECIFIED);

            Surname = surname;
            Gender = gender;
            return this;
        }
    }
}
