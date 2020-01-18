using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.Web.Api.Application
{
    /// <summary>
    /// 返回给前台数据的Dto
    /// </summary>
    public abstract class BasicDto
    {
    }
    /// <summary>
    /// 泛型集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasicDto<T> : BasicDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual T Id { get; set; }
    }
}
