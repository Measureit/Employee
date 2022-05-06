using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Application.Services
{
    public interface IRegistrationNumberGenerator
    {
        int GetNext();
    }
}
