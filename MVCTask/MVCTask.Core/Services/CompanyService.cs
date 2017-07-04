using System;
using System.Collections.Generic;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ??
                                 throw new ArgumentNullException(nameof(companyRepository),
                                     $"{nameof(companyRepository)} cannot be null.");
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companyRepository.Get();
        }

        public string GetCompanyNameById(int companyId)
        {
            return _companyRepository.GetNameById(companyId);
        }
    }
}