using ps.Common;
using ps.Web.Api.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace ps.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            //获取配置配置项里允许跨域请求的地址,如果没有配置，默认取全部
            string allowCorsSite = AppConfig.GetSetting("allow_cors_site", "*");
            //头部消息
            string allowCorsHeader = AppConfig.GetSetting("allow_cors_header", "*");
            //方法
            string allowCorsMethod = AppConfig.GetSetting("allow_cors_method", "*");
            //跨域配置
            config.EnableCors(new EnableCorsAttribute(allowCorsSite, allowCorsHeader, allowCorsMethod));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //返回结果统一封装
            config.Filters.Add(new ApiResultAttribute());
            //是否 授权/登录 过滤
            config.Filters.Add(new SecurityFilterAttribute());
            //扩展开启Session
            RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new SessionControllerRouteHandler();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
