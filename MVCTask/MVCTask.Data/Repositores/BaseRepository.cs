using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MVCTask.Data.Interface;

namespace MVCTask.Data.Repositores
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context),
                $"{nameof(context)} cannot be null.");
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity item)
        {
            var newItem =_dbSet.Add(item);
            _context.SaveChanges();
            return newItem;
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }        

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Remove(TEntity item)
        {            
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();            
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }
    }
}