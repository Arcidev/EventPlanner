using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.Entities;
using System.Threading.Tasks;

namespace Tests.DAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await TestEvents();
            }).Wait();
            
        }

        private static async Task TestEvents()
        {
            var entity = new Event()
            {
                AuthorId = "asdefg"
            };

            var repo = new EventRepository();
            await repo.AddAsync(entity);

            var eventsList = await repo.GetByAuthor("asdefg");
        }
    }
}
