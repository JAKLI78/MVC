using System.Collections.Generic;

namespace MVCTask.Core.Interface
{
    public interface ITitelsServise
    {
        IEnumerable<string> GetTitelsByUserId(int titleId);
        void CreateTitle(string titel, int userId);
        void RemoveTitle(int userId);
        void RemoveTitle(int userId, string title);
    }
}