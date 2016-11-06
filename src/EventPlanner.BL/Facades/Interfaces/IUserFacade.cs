using EventPlanner.BL.DTO;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades.Interfaces
{
    public interface IUserFacade
    {
        Task<UserDTO> CreateOrGetUser(string email);
    }
}
