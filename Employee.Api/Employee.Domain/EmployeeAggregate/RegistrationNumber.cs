using Employee.Domain.Exceptions;
using Employee.Framework;
using System.Collections.Generic;

namespace Employee.Domain.EmployeeAggregate
{
    public class RegistrationNumber : ValueObject
    {
        public string Value { get; }
        public RegistrationNumber(int sequence)
        {
            var input = sequence.ToString();

            if (input.Length > 8)
            {
                throw new EmployeeException(Codes.REGISTRATION_NUMBER_NOT_IN_RANGE);
            }
            Value = input.PadLeft(8, '0');
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
