using Employee.Domain.Exceptions;
using Employee.Framework;
using System.Collections.Generic;

namespace Employee.Domain.EmployeeAggregate
{
    public class Surname : ValueObject
    {
        public string Value { get; }
        public static Surname From(string input)
        {
            if(input.Length > 0 && input.Length > 50)
            {
                throw new EmployeeException(Codes.REGISTRATION_NUMBER_NOT_IN_RANGE);
            }

            return new Surname(input);
        }
        private Surname(string value) => (Value) = (value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
