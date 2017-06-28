using System.Collections.Generic;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface ITestServise
    {
        IEnumerable<User> GetUsers();
        string GetCompanyNameById(int companyId);
        IEnumerable<string> GetTitelsForUserById(int titleId);
    }
}