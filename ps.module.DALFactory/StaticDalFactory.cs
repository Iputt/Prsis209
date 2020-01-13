using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ps.module.DALFactory
{
    public class StaticDalFactory
    {
        private static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

        public static T GetDal<T>(string name) where T : class, new()
        {
            var type = string.Format("{0}.{1}Dal", assemblyName, name);
            return Assembly.Load(assemblyName).CreateInstance(type) as T;
        }
    }
}
