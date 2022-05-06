using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Application.Services
{
    public class InMemoryRegistrationNumberGenerator: IRegistrationNumberGenerator
    {
        private int sequence = 0;
        public int GetNext() => ++sequence;
    }
}
