using Clean.Core;
using Clean.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {

        private IDbContext _context;
        private IDbSet<T> _entities;

        public EfRepository(IDbContext context)
        {
            this._context = context;

            this._entities = _context.Set<T>();
        }

       
        public void Delete(T item)
        {
            try
            {
                this._entities.Remove(item);
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException("item: "+ typeof(T));
            }
        }

        public IEnumerable<T> Get()
        {
            return this._entities.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, int amount = -1)
        {
            var result = this._entities.AsNoTracking();
           
            if (predicate != null)
                result = result.Where(predicate);
            if (amount > 0)
                result = result.Take(amount);

            return result.ToList();
        }

        public T GetById(int id)
        {
            return this._entities.Find(id);
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        private IQueryable<T> Include(Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this._entities.AsNoTracking();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IEnumerable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);

            return query.Where(predicate).ToList();
        }

        public void Insert(T item)
        {
            try
            {
                this._entities.Add(item);
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException("item: " + typeof(T));
            }
        }

        public void Update(T item)
        {
            try
            {
                this._context.Entry(item).State = EntityState.Modified; 
                this._context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ArgumentNullException("item: " + typeof(T));
            }
        }

        //цю властивість також непотрібно реалізовувати як і Table бо вона вертає нам DbSet з яким ми можемо поводитись так як  з Table
        //public IDbSet<T> Entities
        //{
        //    get
        //    {
        //        if (_entities == null)
        //            _entities = _context.Set<T>();

        //        return _entities;
        //    }
        //}

    }
}
