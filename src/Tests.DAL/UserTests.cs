using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;
using Xunit;

namespace Tests.DAL
{
    public class UserTests
    {
        private UserRepository userRepository = new UserRepository();

        [Fact]
        public async Task TestCRUD()
        {
            var entity = new User()
            {
                Email = "test@mail.sk"
            };

            await userRepository.AddAsync(entity);
            Assert.NotNull(entity.Id);

            var user = await userRepository.GetAsync(entity.Id);
            Assert.NotNull(user);
            Assert.Equal(entity.Id, user.Id);
            Assert.Equal("test@mail.sk", user.Email);

            var update = Builders<User>.Update.Set(x => x.Email, "test2@mail.sk");
            await userRepository.UpdateAsync(entity.Id, update);
            user = await userRepository.GetAsync(entity.Id);
            Assert.NotNull(user);
            Assert.Equal("test2@mail.sk", user.Email);

            await userRepository.DeleteAsync(entity.Id);
            Assert.Null(await userRepository.GetAsync(entity.Id));
        }
    }
}
