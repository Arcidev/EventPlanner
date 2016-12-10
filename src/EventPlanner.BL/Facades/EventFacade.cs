using AutoMapper;
using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.DAL.DataAccess.Interfaces;
using EventPlanner.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventPlanner.BL.Facades
{
    public class EventFacade : IEventFacade
    {
        private IEventRepository eventRepository;
        private IUserRepository userRepository;

        public async Task<IEnumerable<EventDTO>> GetInvitedToEvents(string userId)
        {
            var user = await GetUser(userId);
            var filter = Builders<Event>.Filter.Where(e=> e.SenderList.Any( s=> s == user.Email));

            var events = await eventRepository.FindAsync(filter);
            return Mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public EventFacade(IEventRepository eventRepository, IUserRepository userRepository)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
        }

        public async Task<IList<PlaceDTO>> GetEventPlaces(string eventId)
        {
            var outputEvent = await eventRepository.GetAsync(ObjectId.Parse(eventId));
            if (outputEvent == null)
                throw new ArgumentException("Event does not exist");

            return Mapper.Map<IList<PlaceDTO>>(outputEvent.Places);
        }

        public async Task<IEnumerable<EventDTO>> GetUserEvents(string userId)
        {
            var user = await GetUser(userId);
            if (!user.CreatedEvents?.Any() ?? true)
                return new List<EventDTO>();

            var filter = Builders<Event>.Filter.Eq((x => x.Id), user.CreatedEvents.First());
            foreach (var eventId in user.CreatedEvents.Skip(1))
                filter |= Builders<Event>.Filter.Eq((x => x.Id), eventId);

            var events = await eventRepository.FindAsync(filter);
            return Mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task<EventDTO> CreateEvent(EventCreateDTO e, string userId)
        {
            var user = await GetUser(userId);
            var entity = Mapper.Map<Event>(e);
            await eventRepository.AddAsync(entity);

            UpdateDefinition<User> update;
            if (user.CreatedEvents != null)
                update = Builders<User>.Update.AddToSet(x => x.CreatedEvents, entity.Id);
            else
                update = Builders<User>.Update.Set(x => x.CreatedEvents, new[] { entity.Id });

            await userRepository.UpdateAsync(user.Id, update);
            return Mapper.Map<EventDTO>(entity);
        }

        public async Task<EventDTO> GetEvent(string id)
        {
            var entity = await eventRepository.GetAsync(ObjectId.Parse(id));
            if (entity == null)
                throw new ArgumentException("Event does not exist");

            return Mapper.Map<EventDTO>(entity);
        }

        public async Task SignUpForEvent(string eventId, string userId, UserEventDTO choices)
        {
            var user = await GetUser(userId);
            var eventObjectId = ObjectId.Parse(eventId);
            var e = await eventRepository.GetAsync(eventObjectId);

            var entity = Mapper.Map<UserEvent>(choices);
            UpdateDefinition<Event> update;
            if (e.UserChoices != null)
            {
                e.UserChoices[user.Email] = entity;
                update = Builders<Event>.Update.Set(x => x.UserChoices, e.UserChoices);
            }
            else
                update = Builders<Event>.Update.Set(x => x.UserChoices, new Dictionary<string, UserEvent>() { { user.Email, entity } });

            await eventRepository.UpdateAsync(eventObjectId, update);
        }


        public async Task<IEnumerable<UserDTO>> GetUsersForEvent(string eventId)
        {
            var eventObjectId = ObjectId.Parse(eventId);
            var e = await GetEvent(eventId);
            var filter = Builders<User>.Filter.Where(x => e.UserChoices.Keys.Contains(x.Email));
            var users = await userRepository.FindAsync(filter);
            return Mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task EditEvent(string eventId, EventCreateDTO data)
        {
            var eventObjectId = ObjectId.Parse(eventId);
            var editEvent = await GetEvent(eventId);

            UpdateDefinition<Event> update;
            var places = Mapper.Map<IList<Place>>(data.Places);
            update = Builders<Event>.Update
                .Set(x => x.Name, data.Name)
                .Set(x => x.Description, data.Description)
                .AddToSetEach(x => x.Times, data.Times);
            if (editEvent.UserChoices.Keys.Count == 0)
            {
                update.Set(x => x.Places, places);
            }
            await eventRepository.UpdateAsync(eventObjectId, update);
        }

        private async Task<User> GetUser(string userId)
        {
            var user = await userRepository.GetAsync(ObjectId.Parse(userId));
            if (user == null)
                throw new ArgumentException("User does not exist");

            return user;
        }
    }
}
