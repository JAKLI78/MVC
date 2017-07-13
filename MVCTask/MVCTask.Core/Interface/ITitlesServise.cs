using System.Collections.Generic;

namespace MVCTask.Core.Interface
{
    public interface ITitlesServise
    {
        IEnumerable<string> GetTitelsByUserId(int titleId);
        void CreateTitle(string title, int userId);
        void RemoveTitle(int userId);
        void UpdateUserTitles(int userId, ICollection<string> titleNames);
    }
}