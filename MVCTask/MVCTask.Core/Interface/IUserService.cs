using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        void CreateUser(User user);

        void UpdateUser(User user);
        bool isUserExist(int userId);
        void DeleteUser(int userId);
        User FindUserById(int userId);
        User FindUserByIdWithTitles(int userId);
        Task<string> GetFileUriByIdAsync(int userId);
        IEnumerable<User> GetUsersWithCompany();
        IEnumerable<User> GetUsersWithAllInfo();

    }
}