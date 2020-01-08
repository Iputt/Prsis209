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
        private static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssembly"];

        public static T GetDal<T>(string name) where T : class, new()
        {
            StringBuilder type = new StringBuilder();
            type.Append(assemblyName);
            type.Append(".");
            type.Append(name);
            return Assembly.Load(assemblyName).CreateInstance(type.ToString()) as T;
        }
    }
}
