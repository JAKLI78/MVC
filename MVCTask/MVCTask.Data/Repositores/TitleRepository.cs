using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Repositores
{
    public class TitleRepository : BaseRepository<Title>, ITitleRepository
    {
        public TitleRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<string> GetNamesByUserId(int userId)
        {
            var usersWithId = Get(c => c.Id == userId);
            if (usersWithId == null) return new List<string>(){""};            
            return usersWithId.
                Select(title => title.Name).ToList();
            
        }

        public IEnumerable<Title> GetUserTitles(int userId)
        {
            return Query().Where(t => t.UserId == userId);
        }
    }
}