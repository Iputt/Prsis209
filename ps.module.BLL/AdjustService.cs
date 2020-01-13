using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ps.module.IBLL;
using ps.module.Model;

namespace ps.module.BLL
{
    public class AdjustService : BaseService<ps_data_adjust>, IAdjustService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.AdjustDal;
        }
    }
}
