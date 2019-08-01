using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Clean.Core.Data
{
    public interface IRepository<T> where T: BaseEntity
    {
        void Insert(T item);

        T GetById(int id);

        void Update(T item);

        void Delete(T item);

        IEnumerable<T> Get();

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, int amount = -1);

        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        
    }
}
