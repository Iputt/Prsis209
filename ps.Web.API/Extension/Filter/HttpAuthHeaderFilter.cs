using ps.Web.Api.Extension;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace ps.Web.Api.App_Start
{
    /// <summary>
    /// swaggerUI 过滤条件
    /// 将验证消息 放到请求头部
    /// </summary>
    public class HttpAuthHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 需要授权认证的数据获取需要传递Token到后台
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            //var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
            //var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
            //var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            //不需要登录
            var noNeedLogin = apiDescription.ActionDescriptor.GetCustomAttributes<NoNeedLogin>().Any();
            //需要登录的时候添加sessionKey 与 Token作为参数
            if (!noNeedLogin)
            {
                //sessionKey
                operation.parameters.Add(new Parameter
                {
                    name = "sessionKey",
                    @in = "header",
                    description = "该字段对应当前登录用户在服务器上SessionKey",
                    required = true,
                    type = "string",
                    @default = "pISA+y33C4WscIUSeG3UGA=="

                });
                //token
                operation.parameters.Add(new Parameter
                {
                    name = "token",
                    @in = "header",
                    description = "该字段对应当前登录用户在服务器上的token",
                    required = false,
                    type = "string",
                });
            }
        }
    }
}