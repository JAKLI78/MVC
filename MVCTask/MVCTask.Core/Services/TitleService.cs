using System;
using System.Collections.Generic;
using System.Linq;
using MVCTask.Core.Interface;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Services
{
    public class TitleService : ITitelsServise
    {
        private readonly ITitleRepository _titleRepository;


        public TitleService(
            ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository ??
                               throw new ArgumentNullException(nameof(titleRepository),
                                   $"{nameof(titleRepository)} cannot be null.");
        }


        public IEnumerable<string> GetTitelsByUserId(int userId)
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

        public void RemoveTitle(int userId, string title)
        {
            _titleRepository.Remove(_titleRepository.Get(t => (t.UserId == userId) & (t.Name == title)).First());
        }
    }
}