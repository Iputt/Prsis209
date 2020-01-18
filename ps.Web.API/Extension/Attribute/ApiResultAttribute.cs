using ps.Web.Api.Application;
using ps.Web.Api.Extension;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;

namespace ps.Web.Api.Extension
{

    /// <summary>
    /// 不需要统一打包格式的方法添加该属性
    /// </summary>
    public class NoPackageResult : Attribute
    {

    }

    /// <summary>
    /// 返回结果封装
    /// </summary>
    public class ApiResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            //如果没有NoPackageResult属性的时候 封装返回结果
            var noPackage = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoPackageResult>();
            if (!noPackage.Any())
            {
                ApiResultModel result = new ApiResultModel();
                //当响应不为空的时候
                if (actionExecutedContext.ActionContext.Response != null)
                {
                    // 取得由 API 返回的状态代码
                    result.StatusCode = actionExecutedContext.ActionContext.Response.StatusCode;
                    //请求是否成功
                    result.IsSuccess = actionExecutedContext.ActionContext.Response.IsSuccessStatusCode;
                    result.ErrorMessage = "";
                    //当响应内容不为空的时候，将数据保存到Data中
                    //否则Data为空
                    if (actionExecutedContext.ActionContext.Response.Content != null)
                    {
                        result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                    }
                    else
                    {
                        result.Data = string.Empty;
                    }
                }
                else
                {
                    actionExecutedContext.ActionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    //状态码改为服务器错误
                    result.StatusCode = actionExecutedContext.ActionContext.Response.StatusCode;
                    result.IsSuccess = false;
                    result.ErrorMessage = actionExecutedContext.Exception.Message;
                    result.Data = string.Empty;
                }
                //结果转为自定义消息格式
                HttpResponseMessage httpResponseMessage = PackageMessage(result);
                // 重新封装回传格式
                actionExecutedContext.Response = httpResponseMessage;
            }
        }

        /// <summary>
        /// 将返回信息转换为字符串类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static HttpResponseMessage PackageMessage(ApiResultModel obj)
        {
            String str;
            //if (obj is String || obj is Char)//如果是字符串或字符直接返回
            //{
            //    str = obj.ToString();
            //}
            //else//否则序列为json字串
            //{
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            str = serializer.Serialize(obj);
            //}
            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = obj.StatusCode,
                Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json")
            };
            return result;
        }
    }
}