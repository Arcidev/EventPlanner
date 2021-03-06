﻿using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using EventPlanner.BL.DTO;
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;

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
            var eventDto = await CreateEvent(user.Id);
            Assert.NotNull(eventDto);

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var events = await eventFacade.GetUserEvents(user.Id);
            Assert.True(events.Any());
        }

        [Fact]
        public async Task TestSignUpForEvent()
        {
            var user = await GetUser("testing@mail.sk");
            var e = await CreateEvent(user.Id);

            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            await eventFacade.SignUpForEvent(e.Id, user.Id, new UserEventDTO()
            {
                Choices = new Dictionary<int, int[]>()
                {
                    { 1, new[] { 1, 2 } }
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

        private async Task<EventDTO> CreateEvent(string userId, List<PlaceDTO> places = null, List<DateTime> dates = null)
        {
            var eventFacade = serviceProvider.GetRequiredService<IEventFacade>();
            var e = new EventCreateDTO()
            {
                Times = new[] { DateTime.UtcNow, DateTime.UtcNow.Date },
                Places = new[] { new PlaceDTO() { X = 20, Y = 40 }, new PlaceDTO() { X = 50, Y = 60 } }
            };
            if (places != null)
            { 
                e.Places = e.Places.Concat(places).ToList();
            }
            
            if (dates != null)
            {
                e.Times = e.Times.Concat(dates).ToList();
            }
            
            return await eventFacade.CreateEvent(e, userId);
        }
    }
}
