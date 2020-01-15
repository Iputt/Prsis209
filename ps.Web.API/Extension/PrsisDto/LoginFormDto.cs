using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ps.Web.Api.Extension
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginFormDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
    }
}