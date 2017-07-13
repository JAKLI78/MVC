using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateUser(User user)
        {            
            _userRepository.Create(user);
        }

        public void UpdateUser(User user)
        {            
            _userRepository.Update(user);
        }

        public bool isUserExist(int userId)
        {
            return _userRepository.Query().Where(u => u.Id == userId).Any();
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Remove(_userRepository.FindById(userId));
        }

        public User FindUserById(int userId)
        {
            return _userRepository.FindById(userId);
        }

        public User FindUserByIdWithTitles(int userId)
        {
            return _userRepository.FindUserInclude(userId);
        }

        public Task<string> GetFileUriByIdAsync(int userId)
        {
            return _userRepository.GetFileUrlAsync(userId);
        }

        public IEnumerable<User> GetUsersWithCompany()
        {
            return _userRepository.GetUsersWithCompanyNames();
        }

        public IEnumerable<User> GetUsersWithAllInfo()
        {
            return _userRepository.GetUsersWithAllInfo();
        }
    }
}