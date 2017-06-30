using System.Data.Entity;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Repositores
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}