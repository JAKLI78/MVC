using System.Collections.Generic;

namespace MVCTask.Models
{
    public class UsersViewModel
    {
        public List<UserModel> UserModels { get; set; }
        public string Search { get; set; }
    }
}