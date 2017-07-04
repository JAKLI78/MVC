using System.Threading.Tasks;
using MVCTask.Data.Model;

namespace MVCTask.Data.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> AsyncGetFileUrl(int userId);
        Task<User> AsyncFindUser(int userId);
    }
}