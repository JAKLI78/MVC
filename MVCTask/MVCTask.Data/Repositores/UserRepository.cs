using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Repositores
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<string> GetFileUrlAsync(int userId)
        {
            var user = FindById(userId);
            return user.FileUrl;
        }

        public IEnumerable<User> GetUsersWithAllInfo()
        {
            return GetWithInclude(x => x.Company,x=>x.Titles);
        }

        public User FindUserInclude(int userId)
        {
            return Query().Include(x=>x.Titles).AsNoTracking().First(x => x.Id==userId);
        }

        public bool IsEmailInUse(string email,int userId)
        {
            var usersWithEmail = Query().Where(u => u.Email == email);
            if (usersWithEmail.Any())
            {
                if (usersWithEmail.First().Id == userId)
                {
                    return true;
                }
            }            
            return !usersWithEmail.Any();
        }

        public IEnumerable<User> GetUsersWithCompanyNames()
        {
            return GetWithInclude(x => x.Company);
        }
    }
}