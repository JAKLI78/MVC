using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ??
                              throw new ArgumentNullException(nameof(userRepository),
                                  $"{nameof(userRepository)} cannot be null.");
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

        public void CreateUser(string name, string surname, string email, string titels, DateTime birthDate,
            int companyId,
            string fileUrl)
        {
            var user = new User
            {
                Email = email,
                BirthDate = birthDate,
                CompanyId = companyId,
                FileUrl = fileUrl,
                Name = name,
                Surname = surname
            };
            _userRepository.Create(user);
        }

        public void UpdateUser(int userId, string name, string surname, string email, DateTime birthDate, int companyId,
            string fileUrl)
        {
            var user = new User
            {
                Id = userId,
                Email = email,
                BirthDate = birthDate,
                CompanyId = companyId,
                FileUrl = fileUrl,
                Name = name,
                Surname = surname
            };
            _userRepository.Update(user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Remove(_userRepository.FindById(userId));
        }

        public Task<string> AsyncGetFileUrlById(int userId)
        {
            return _userRepository.AsyncGetFileUrl(userId);
        }
    }
}