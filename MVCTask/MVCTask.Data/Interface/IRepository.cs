using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCTask.Data.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(int id);        
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
        IQueryable<TEntity> Query();
    }
}