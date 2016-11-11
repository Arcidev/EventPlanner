using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using EventPlanner.BL.DTO;
using System;
using System.Linq;

namespace Tests.BL
{
    public class EventFacadeTests : BaseFacadeTests
    {
        protected override void ConfigureFacade(IServiceCollection services)
        {
            services.AddTransient<IEventFacade, EventFacade>();
            services.AddTransient<IUserFacade, UserFacade>();
        }

        [Fact]
        public async Task TestGetEvent()
        {
            var userFacade = serviceProvider.GetRequiredService<IUserFacade>();
            var user = await userFacade.CreateOrGetUser("test@mail.sk");

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var events = await eventFacade.GetUserEvents(user.Id);
            Assert.NotNull(events);
        }

        [Fact]
        public async Task TestCreateEvent()
        {
            var userFacade = serviceProvider.GetRequiredService<IUserFacade>();
            var user = await userFacade.CreateOrGetUser("test@mail.sk");

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var e = new EventCreateDTO()
            {
                Times = new[] { DateTime.Now, DateTime.Now.Date },
                Places = new[] { new PlaceDTO() { X = 20, Y = 40}, new PlaceDTO() { X = 50, Y = 60} }
            };

            var eventDto = await eventFacade.CreateEvent(e, user.Id);
            Assert.NotNull(eventDto);

            var events = await eventFacade.GetUserEvents(user.Id);
            Assert.True(events.Any());
        }
    }
}
