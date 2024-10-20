using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighLand.repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T GetIncluding(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
    }
}
