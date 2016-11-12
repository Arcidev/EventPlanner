using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using EventPlanner.BL.DTO;
using System;
using System.Linq;
using System.Collections.Generic;

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
            var user = await GetUser("test@mail.sk");
            var eventDto = CreateEvent(user.Id);
            Assert.NotNull(eventDto);

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var events = await eventFacade.GetUserEvents(user.Id);
            Assert.True(events.Any());
        }

        [Fact]
        public async Task TestSignUpForEvent()
        {
            var user = await GetUser("test@mail.sk");
            var e = await CreateEvent(user.Id);

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            await eventFacade.SignUpForEvent(e.Id, user.Id, new UserEventDTO()
            {
                Choices = new Dictionary<PlaceDTO, IList<DateTime>>()
                {
                    { new PlaceDTO() { X = 20, Y = 40 }, new[] { DateTime.Now, DateTime.Now.Date } }
                }
            });

            var users = await eventFacade.GetUsersForEvent(e.Id);
            Assert.Equal(1, users.Count());
        }

        private async Task<UserDTO> GetUser(string mail)
        {
            var userFacade = serviceProvider.GetRequiredService<IUserFacade>();
            return await userFacade.CreateOrGetUser(mail);
        }

        private async Task<EventDTO> CreateEvent(string userId)
        {
            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var e = new EventCreateDTO()
            {
                Times = new[] { DateTime.Now, DateTime.Now.Date },
                Places = new[] { new PlaceDTO() { X = 20, Y = 40 }, new PlaceDTO() { X = 50, Y = 60 } }
            };
            
            return await eventFacade.CreateEvent(e, userId);
        }
    }
}
