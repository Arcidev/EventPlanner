using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.DAL
{
    public class EventTests
    {
        private EventRepository eventRepository = new EventRepository();
        private UserRepository userRepository = new UserRepository();

        [Fact]
        public async Task TestCRUD()
        {
            var entity = new Event()
            {
                AuthorId = "TestCrud",
                Places = new Place[] {
                    new Place(5, 7),
                    new Place(3, 4)
                },
                Times = new DateTime[]
                {
                    new DateTime()
                },
                Users = new ObjectId[]
                {
                    new ObjectId()
                }
            };

            await eventRepository.AddAsync(entity);
            Assert.NotNull(entity.Id);

            var e = await eventRepository.GetAsync(entity.Id);
            Assert.NotNull(e);
            Assert.Equal(entity.Id, e.Id);
            Assert.Equal("TestCrud", e.AuthorId);

            var update = Builders<Event>.Update.Set(nameof(entity.AuthorId), "TestCrudUpdate");
            await eventRepository.UpdateAsync(entity.Id, update);
            e = await eventRepository.GetAsync(entity.Id);
            Assert.NotNull(e);
            Assert.Equal("TestCrudUpdate", e.AuthorId);

            await eventRepository.DeleteAsync(entity.Id);
            Assert.Null(await eventRepository.GetAsync(entity.Id));
        }

    }
}
