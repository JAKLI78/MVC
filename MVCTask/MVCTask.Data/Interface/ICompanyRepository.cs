using MVCTask.Data.Model;

namespace MVCTask.Data.Interface
{
    public interface ICompanyRepository:IRepository<Company>
    {
        string GetNameById(int companyId);
    }
}