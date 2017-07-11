using System;
using System.Collections.Generic;
using System.Linq;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ITitleRepository _titleRepository;
        private readonly IUserRepository _userRepository;

        public SearchService(IUserRepository userRepository, ICompanyRepository companyRepository,
            ITitleRepository titleRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository),
                                  $"{nameof(userRepository)} cannot be null.");
            ;
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository),
                                     $"{nameof(companyRepository)} cannot be null.");
            ;
            _titleRepository = titleRepository ?? throw new ArgumentNullException(nameof(titleRepository),
                                   $"{nameof(titleRepository)} cannot be null.");
            ;
        }

        public IEnumerable<User> FindUsers(string arg)
        {
            var result = new List<User>();
            var companys = _companyRepository.Get(c => c.Name.Contains(arg));
            if (companys.Any())
                foreach (var company in companys)
                    result.AddRange(_userRepository.Get(u => u.CompanyId == company.Id));
            var users = _userRepository.Get(u => u.Name.Contains(arg) | u.Surname.Contains(arg) |
                                                 u.BirthDate.ToString().Contains(arg));
            if (users.Any())
                foreach (var user in users)
                    if (!result.Exists(u => u.Id == user.Id))
                        result.Add(user);
            var titles = _titleRepository.Get(t => t.Name.Contains(arg));
            if (titles.Any())
                foreach (var title in titles)
                {
                    var user = _userRepository.FindById(title.UserId);
                    if (user != null)
                        if (!result.Exists(u => u.Id == user.Id))
                            result.Add(user);
                }
            return result;
        }
    }
}