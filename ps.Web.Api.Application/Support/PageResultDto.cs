using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.Web.Api.Application
{
    /// <summary>
    /// 前台返回单个结果的通用ClassDto
    /// </summary>
    public class PageResultDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResultDto()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary> 
        /// <param name="obj">数据</param>
        /// <param name="state">是否成功</param>
        /// <param name="info">提示信息</param>
        public PageResultDto(ApiObject data, bool state = true, string info = "")
        {
            State = state;
            Info = info;
            Data = data;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Info { get; set; }

        public ApiObject Data { get; set; }
    }

    /// <summary>
    /// 前台返回单个结果的通用ClassDto(泛型类)
    /// </summary>
    public abstract class PageResultDto<T> where T : BasicDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResultDto()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary> 
        /// <param name="data">数据</param>
        /// <param name="state">是否成功</param>
        /// <param name="info">提示信息</param>
        public PageResultDto(T data, bool state = true, string info = "")
        {
            State = state;
            Info = info;
            Data = data;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
