using Employee.Domain.Exceptions;
using Employee.Framework;
using System.Collections.Generic;

namespace Employee.Domain.EmployeeAggregate
{
    public class Surname : ValueObject
    {
        public string Value { get; }
        public Surname(string input)
        {
            if(input.Length > 0 && input.Length > 50)
            {
                throw new EmployeeException(Codes.REGISTRATION_NUMBER_NOT_IN_RANGE);
            }

            Value = input;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
