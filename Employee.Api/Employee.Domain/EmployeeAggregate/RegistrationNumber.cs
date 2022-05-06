using Employee.Domain.Exceptions;
using Employee.Framework;
using System.Collections.Generic;

namespace Employee.Domain.EmployeeAggregate
{
    public class RegistrationNumber : ValueObject
    {
        public string Value { get; }
        public static RegistrationNumber From(string input)
        {
            if(input.Length > 8)
            {
                throw new EmployeeException(Codes.REGISTRATION_NUMBER_NOT_IN_RANGE);
            }

            return new RegistrationNumber(input.PadLeft(8, '0'));
        }
        private RegistrationNumber(string value) => (Value) = (value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
