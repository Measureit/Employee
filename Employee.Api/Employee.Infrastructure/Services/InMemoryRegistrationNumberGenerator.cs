using System.Threading.Tasks;

namespace Employee.Application.Services
{
    public class InMemoryRegistrationNumberGenerator: IRegistrationNumberGenerator
    {
        private int sequence = 0;
        public Task<int> GetNextAsync() => Task.FromResult(++sequence);
    }
}
