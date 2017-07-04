using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        void CreateUser(string name, string surname, string email, string titels, DateTime birthDate, int companyId,
            string fileUrl);

        void UpdateUser(int userId, string name, string surname, string email, DateTime birthDate,
            int companyId,
            string fileUrl);

        void DeleteUser(int userId);
        Task<string> AsyncGetFileUrlById(int userId);
    }
}