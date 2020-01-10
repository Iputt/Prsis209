using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;

namespace ps.Web.Api.Extension
{
    /// <summary>
    /// 扩展路由，支持Session
    /// </summary>
    public class SessionControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        { 
            return new SessionRouteHandler(requestContext.RouteData);
        }
    }
}