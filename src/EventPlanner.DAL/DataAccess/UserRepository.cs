using EventPlanner.DAL.Entities;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base("users") { }

        public async Task<User> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq((x => x.Email), email);
            return (await FindAsync(filter)).FirstOrDefault();
        }
    }
}
