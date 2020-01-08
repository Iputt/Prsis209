using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.IDAL
{
    public interface IBaseDal<T> where T : class, new()
    {
        List<T> Query(Expression<Func<T, bool>> where);

        List<T> PageQuery<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc);

        T Add(T item);

        bool Update(T item);

        bool Delete(T item);
    }
}
