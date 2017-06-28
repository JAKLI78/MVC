using System;
using System.Collections.Generic;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Class
{
    public class TestService:ITestServise
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITitleRepository _titleRepository;

        public TestService(IUserRepository userRepository,ICompanyRepository companyRepository,ITitleRepository titleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException("userRepository");
            _companyRepository = companyRepository ?? throw new ArgumentNullException("companyRepository");
            _titleRepository = titleRepository ?? throw new ArgumentNullException("titleRepository");
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

        public string GetCompanyNameById(int companyId)
        {
            return _companyRepository.GetNameById(companyId);
        }

        public IEnumerable<string> GetTitelsForUserById(int userId)
        {
            return _titleRepository.GetNamesByUserId(userId);
        }
    }
}
