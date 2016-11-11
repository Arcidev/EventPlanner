using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BL
{
    public class UserFacadeTests : BaseFacadeTests
    {
        protected override void ConfigureFacade(IServiceCollection services)
        {
            services.AddTransient<IUserFacade, UserFacade>();
        }

        [Fact]
        public async Task TestUserFacade()
        {
            var userFacade = serviceProvider.GetRequiredService<IUserFacade>();
            var user = await userFacade.CreateOrGetUser("test@mail.sk");
            Assert.NotNull(user);
            Assert.Equal("test@mail.sk", user.Email);
        }
    }
}
