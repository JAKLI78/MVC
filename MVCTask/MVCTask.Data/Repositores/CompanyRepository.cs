using System;
using System.Data.Entity;
using System.Linq;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Repositores
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {
        }

        public string GetNameById(int companyId)
        {
            try
            {
                var result = Get(c => c.Id == companyId).First().Name;
                return result;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}