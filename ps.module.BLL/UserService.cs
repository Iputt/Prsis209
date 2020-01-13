using ps.module.IBLL;
using ps.module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.BLL
{
    public class UserService : BaseService<ps_sys_user>, IUserService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserDal;
        }
    }
}
