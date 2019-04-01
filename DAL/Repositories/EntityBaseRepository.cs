using DAL.Context;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories
{
    public class EntityBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationContext _context;

        public EntityBaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
        }

        public void Delete(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                try
                {
                    _context.Set<TEntity>().Attach(item);
                }
                catch (Exception) { }
            }
            _context.Set<TEntity>().Remove(item);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Set<TEntity>().Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
