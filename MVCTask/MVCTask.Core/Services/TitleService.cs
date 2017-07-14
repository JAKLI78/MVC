using System;
using System.Collections.Generic;
using System.Linq;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class TitleService : ITitlesServise
    {
        private readonly ITitleRepository _titleRepository;

        public TitleService(
            ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository ??
                               throw new ArgumentNullException(nameof(titleRepository),
                                   $"{nameof(titleRepository)} cannot be null.");
        }

        public IEnumerable<string> GetTitlesByUserId(int userId)
        {
            return _titleRepository.GetNamesByUserId(userId);
        }

        public void CreateTitle(string title, int userId)
        {
            _titleRepository.Create(new Title {Name = title, UserId = userId});
        }

        public void RemoveTitle(int userId)
        {
            _titleRepository.Remove(_titleRepository.Get(t => t.UserId == userId).First());
        }

        public void UpdateUserTitles(int userId, ICollection<string> titleNames)
        {
            var currentUserTitles = _titleRepository.GetUserTitles(userId);
            var tmpTitles = new List<string>(titleNames);
            var titlesToDelete = new List<Title>();
            foreach (var currentUserTitle in currentUserTitles)
            {
                                
                if (!titleNames.Contains(currentUserTitle.Name))
                {
                    titlesToDelete.Add(currentUserTitle);
                }
                else
                {
                    tmpTitles.Remove(currentUserTitle.Name);                        
                }                                   
            }
            if (tmpTitles.Any())
            {
                foreach (var titleName in tmpTitles)
                {
                    if (titleName.Length > 0)
                    {
                        CreateTitle(titleName, userId);
                    }
                }                    
            }
            if (titlesToDelete.Any())
            {
                foreach (var title in titlesToDelete)
                {
                    _titleRepository.Remove(title);
                }
            }
        }
    }
}