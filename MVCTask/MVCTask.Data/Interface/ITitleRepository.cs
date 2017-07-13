using System.Collections.Generic;
using MVCTask.Data.Model;

namespace MVCTask.Data.Interface
{
    public interface ITitleRepository : IRepository<Title>
    {
        IEnumerable<string> GetNamesByUserId(int userId);
        IEnumerable<Title> GetUserTitels(int userId);
    }
}