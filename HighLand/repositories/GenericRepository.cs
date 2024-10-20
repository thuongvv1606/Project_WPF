using HighLand.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighLand.repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HighLandContext _context = new HighLandContext();
        private readonly DbSet<T> _entities;

        public GenericRepository()
        {
            _entities = _context.Set<T>();
        }

        public T Get(Expression<Func<T, bool>> expression) => _entities.SingleOrDefault(expression);

        public IEnumerable<T> GetAll() => _entities.ToList();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression) => _entities.Where(expression).ToList();

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entities;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public T GetIncluding(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entities;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(expression);
        }
    }
}
