using System.Collections.Generic;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies();
        string GetCompanyNameById(int companyId);
    }
}