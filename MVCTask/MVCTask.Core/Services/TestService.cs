using System;
using System.Collections.Generic;
using System.Linq;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class TestService : ITestServise
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ITitleRepository _titleRepository;
        private readonly IUserRepository _userRepository;

        public TestService(IUserRepository userRepository, ICompanyRepository companyRepository,
            ITitleRepository titleRepository)
        {
            _userRepository = userRepository ??
                              throw new ArgumentNullException(nameof(userRepository),
                                  $"{nameof(userRepository)} cannot be null.");
            _companyRepository = companyRepository ??
                                 throw new ArgumentNullException(nameof(companyRepository),
                                     $"{nameof(companyRepository)} cannot be null.");
            _titleRepository = titleRepository ??
                               throw new ArgumentNullException(nameof(titleRepository),
                                   $"{nameof(titleRepository)} cannot be null.");
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

        public IEnumerable<Company> GetCompanies()
        {
            return _companyRepository.Get();
        }

        public void CreateUser(string Name, string Surname, string Email, string titels, DateTime birthDate,
            int companyId, string fileUrl)
        {
            var user = new User
            {
                Email = Email,
                BirthDate = birthDate,
                CompanyId = companyId,
                FileUrl = fileUrl,
                Name = Name,
                Surname = Surname
            };

            _userRepository.Create(user);

            if (titels.Length > 0)
            {
                var titless = titels.Split('/');
                foreach (var s in titless)
                    if (s.Length > 0)
                    {
                        var userId = _userRepository.Get().First(u => u.Email == Email).Id;
                        _titleRepository.Create(new Title {Name = s, UserId = userId});
                    }
            }
        }

        public void UpdateUser(int userId, string Name, string Surname, string Email, string titels, DateTime birthDate,
            int companyId,
            string fileUrl)
        {
            var user = new User
            {
                Id = userId,
                Email = Email,
                BirthDate = birthDate,
                CompanyId = companyId,
                FileUrl = fileUrl,
                Name = Name,
                Surname = Surname
            };

            _userRepository.Update(user);
        }

        public void DeleteUser(int userId)
        {
            foreach (var title in _titleRepository.Get(t => t.UserId == userId))
                _titleRepository.Remove(title);
            _userRepository.Remove(_userRepository.FindById(userId));
        }
    }
}