using ps.module.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.DAL
{
    public class BaseDal<T> where T : class, new()
    {
        public psdbEntities Db => (psdbEntities)DbContextFactory.GetCurrentDbcontext();

        private DbSet<T> _dbset;

        public BaseDal()
        {
            _dbset = Db.Set<T>();
        }

        public List<T> Query(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public List<T> PageQuery<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> where, Expression<Func<T, S>> orderby, bool isAsc)
        {
            total = _dbset.Where(where).Count();
            if (isAsc)
            {
                return _dbset
                    .Where(where)
                    .OrderBy(orderby)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                return _dbset
                    .Where(where)
                    .OrderByDescending(orderby)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList();
            }
        }

        public T Add(T item)
        {
            _dbset.Add(item);
            return item;
        }

        public bool Update(T item)
        {
            Db.Entry(item).State = EntityState.Modified;
            return true;
        }

        public bool Delete(T item)
        {
            Db.Entry(item).State = EntityState.Deleted;
            return true;
        }
    }
}
