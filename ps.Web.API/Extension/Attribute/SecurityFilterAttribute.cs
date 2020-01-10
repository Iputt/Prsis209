using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ps.Web.Api.Extension
{
    /// <summary>
    /// 安全性过滤 
    /// </summary>
    public class SecurityFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            //是否需要登录
            bool needLogin = !actionContext.ActionDescriptor.GetCustomAttributes<NoNeedLogin>().Any();
            //sessionKey 
            //AES加密之后的字符串
            string sessionKey = HttpContext.Current.Request.Headers["sessionKey"].ToString();

            //检查是否有操作权限
            //

            #region 根据sessionKey验证当前用户是否处于登录状态 
            //sessionKey为空 || sessionKey不为空而且sessionId与sessionkey不一致
            if (needLogin && string.IsNullOrWhiteSpace(sessionKey))
            {
                //响应码设置为未认证身份 
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            #endregion
        }
    }
}