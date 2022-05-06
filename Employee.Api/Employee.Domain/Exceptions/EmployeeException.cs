using System;

namespace Employee.Domain.Exceptions
{
    public class EmployeeException : Exception
    {
        public string Code { get; }

        public EmployeeException()
        {
        }

        public EmployeeException(string code)
        {
            Code = code;
        }

        public EmployeeException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public EmployeeException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public EmployeeException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public EmployeeException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
