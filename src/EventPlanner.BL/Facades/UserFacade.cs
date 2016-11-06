using AutoMapper;
using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades
{
    public class UserFacade : IUserFacade
    {
        private UserRepository userRepository = new UserRepository();

        public async Task<UserDTO> CreateOrGetUser(string email)
        {
            var user = await userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                user = new User() { Email = email };
                await userRepository.AddAsync(user);
            }

            return Mapper.Map<UserDTO>(user);
        }
    }
}
