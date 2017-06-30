using System;
using System.Collections.Generic;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface ITestServise
    {
        IEnumerable<User> GetUsers();
        string GetCompanyNameById(int companyId);
        IEnumerable<string> GetTitelsForUserById(int titleId);
        IEnumerable<Company> GetCompanies();

        void CreateUser(string name, string surname, string email, string titels, DateTime birthDate, int companyId,
            string fileUrl);

        void UpdateUser(int userId, string name, string surname, string email, string titels, DateTime birthDate,
            int companyId,
            string fileUrl);

        void DeleteUser(int userId);
    }
}