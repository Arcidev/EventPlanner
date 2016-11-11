using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.DAL
{
    public class EventTests
    {
        private EventRepository eventRepository = new EventRepository();

        [Fact]
        public async Task TestCRUD()
        {
            var entity = new Event()
            {
                Places = new[] {
                    new Place(5, 7),
                    new Place(3, 4)
                },
                Times = new[]
                {
                    new DateTime()
                },
                SenderList = new[]
                {
                    "randomEmail"
                }
            };

            await eventRepository.AddAsync(entity);
            Assert.NotNull(entity.Id);

            var e = await eventRepository.GetAsync(entity.Id);
            Assert.NotNull(e);
            Assert.Equal(entity.Id, e.Id);
            Assert.Equal(new List<string> { "randomEmail" }, e.SenderList);

            var update = Builders<Event>.Update.Set(x => x.SenderList, new List<string> { "TestCrudUpdate" });
            await eventRepository.UpdateAsync(entity.Id, update);
            e = await eventRepository.GetAsync(entity.Id);
            Assert.NotNull(e);
            Assert.Equal(new List<string> { "TestCrudUpdate" }, e.SenderList);

            await eventRepository.DeleteAsync(entity.Id);
            Assert.Null(await eventRepository.GetAsync(entity.Id));
        }

    }
}
