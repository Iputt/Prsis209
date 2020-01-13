using ps.module.DAL;
using ps.module.IDAL;

namespace ps.module.DALFactory
{
    class DbSession : IDbSession
    {
        #region all dal
        public IUserDal UserDal => StaticDalFactory.GetDal<UserDal>("User");

        public IAdjustDal AdjustDal => StaticDalFactory.GetDal<AdjustDal>("Adjust");
        #endregion

        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbcontext().SaveChanges();
        }
    }
}
