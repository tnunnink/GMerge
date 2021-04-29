using System.Linq;
using GalaxyMerge.Data.Abstractions;
using GalaxyMerge.Data.Entities;
using GalaxyMerge.Data.Helpers;

namespace GalaxyMerge.Data.Repositories
{
    public class UserRepository : Repository<UserProfile>, IUserRepository
    {
        public UserRepository(string connectionString) : base(ContextCreator.Create(connectionString))
        {
        }

        public UserProfile FindByName(string userName)
        {
            return Set.SingleOrDefault(x => x.UserName == userName);
        }
    }
}