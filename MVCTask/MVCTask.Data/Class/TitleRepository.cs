using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Data.Class
{
    public class TitleRepository:BaseRepository<Title>,ITitleRepository
    {
        public TitleRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<string> GetNamesByUserId(int userId)
        {
            try
            {
                
                
                return Get(t => t.UserId == userId).Select(title => title.Name).ToList();

            }
            catch (Exception e)
            {
                return new List<string>();
                
            }
        }
    }
}
