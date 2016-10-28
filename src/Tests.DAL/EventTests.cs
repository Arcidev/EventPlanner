using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;
using Xunit;

namespace Tests.DAL
{
    public class EventTests
    {
        private EventRepository repository = new EventRepository();

        [Fact]
        public async Task TestCRUD()
        {
            var entity = new Event()
            {
                AuthorId = "TestCrud"
            };

            await repository.AddAsync(entity);
            Assert.NotNull(entity._id);

            var e = await repository.GetAsync(entity._id);
            Assert.NotNull(e);
            Assert.Equal(entity._id, e._id);
            Assert.Equal("TestCrud", e.AuthorId);

            var update = Builders<Event>.Update.Set(nameof(entity.AuthorId), "TestCrudUpdate");
            await repository.UpdateAsync(entity._id, update);
            e = await repository.GetAsync(entity._id);
            Assert.NotNull(e);
            Assert.Equal("TestCrudUpdate", e.AuthorId);

            await repository.DeleteAsync(entity._id);
            Assert.Null(await repository.GetAsync(entity._id));
        }
    }
}
