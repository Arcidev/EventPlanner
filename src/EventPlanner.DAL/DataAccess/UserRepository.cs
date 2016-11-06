using EventPlanner.DAL.Entities;

namespace EventPlanner.DAL.DataAccess
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base("users")
        {

        }
    }
}
