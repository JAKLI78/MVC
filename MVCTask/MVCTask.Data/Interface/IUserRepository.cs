using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCTask.Data.Model;

namespace MVCTask.Data.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<string> GetFileUrlAsync(int userId);        
        IEnumerable<User> GetUsersWithCompanyNames();
        IEnumerable<User> GetUsersWithAllInfo();
        User FindUserInclude(int userId);
        bool IsEmailInUse(string email, int userId);
    }
}