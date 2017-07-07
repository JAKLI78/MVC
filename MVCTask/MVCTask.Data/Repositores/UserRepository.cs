using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Repositores
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<string> AsyncGetFileUrl(int userId)
        {
            var user = await AsyncFindUser(userId);
            return user.FileUrl;
        }

        public Task<User> AsyncFindUser(int userId)
        {
            return Task.Run(() => FindById(userId));
        }
    }
}