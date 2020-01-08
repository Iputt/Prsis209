using ps.module.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.DAL
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbcontext()
        {
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new psdbEntities();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
