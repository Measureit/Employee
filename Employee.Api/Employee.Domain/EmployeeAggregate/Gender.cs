using Employee.Domain.Exceptions;
using Employee.Framework;
using System;
using System.Collections.Generic;

namespace Employee.Domain.EmployeeAggregate
{
    public enum GenderEnum
    {
        Female = 0,
        Male = 1
    }
    public class Gender : ValueObject
    {
        public GenderEnum Value { get; }
        public Gender(int input)
        {
            if (!Enum.IsDefined(typeof(GenderEnum), input))
            {
                throw new EmployeeException(Codes.GENDER_NOT_IN_RANGE);
            }

            Value = (GenderEnum)input;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
