using System.Threading.Tasks;

namespace Employee.Application.Services
{
    public interface IRegistrationNumberGenerator
    {
        Task<int> GetNextAsync();
    }
}
