using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ps.Web.Api.Extension
{
    /// <summary>
    /// 验证信息存储对象
    /// </summary>
    public class VerifyDto : BasicDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// sessionKey
        /// </summary>
        public string SessionKey { get; set; }
    }
}