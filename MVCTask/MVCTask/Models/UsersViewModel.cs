using PagedList;

namespace MVCTask.Models
{
    public class UsersViewModel
    {
        public PagedList<UserModel> UserModels { get; set; }
    }
}