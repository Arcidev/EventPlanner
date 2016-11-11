using EventPlanner.DAL.Entities;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
