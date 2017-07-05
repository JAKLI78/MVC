using System.Collections.Generic;
using MVCTask.Data.Model;

namespace MVCTask.Core.Interface
{
    public interface ISearchService
    {
        IEnumerable<User> FindUsers(string arg);
    }
}