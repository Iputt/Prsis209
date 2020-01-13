using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ps.module.IBLL;
using ps.module.IDAL;
using ps.module.DALFactory;
using System.Linq.Expressions;

namespace ps.module.BLL
{
    public abstract class BaseService<T> where T : class, new()
    {
        public IBaseDal<T> CurrentDal { set; get; }

        public IDbSession DbSession => DbSessionFactory.GetCurrentDbSession();

        public BaseService()
        {
            SetCurrentDal();
        }

        public abstract void SetCurrentDal();

        #region crud
        public List<T> Query(Expression<Func<T, bool>> where)
        {
            return CurrentDal.Query(where);
        }

        public List<T> PageQuery<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
        {
            return CurrentDal.PageQuery(pageSize, pageIndex, out total, where, orderBy, isAsc);
        }

        public T Add(T item)
        {
            CurrentDal.Add(item);
            DbSession.SaveChanges();
            return item;
        }

        public bool Update(T item)
        {
            CurrentDal.Update(item);
            return DbSession.SaveChanges() > 0;
        }

        public bool Delete(T item)
        {
            CurrentDal.Delete(item);
            return DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}