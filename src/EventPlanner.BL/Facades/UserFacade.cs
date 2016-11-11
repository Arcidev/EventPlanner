using AutoMapper;
using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.DAL.DataAccess.Interfaces;
using EventPlanner.DAL.Entities;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades
{
    public class UserFacade : IUserFacade
    {
        private IUserRepository userRepository;

        public UserFacade(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

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
