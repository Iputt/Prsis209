using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace ps.Web.Api.Extension
{
    /// <summary>
    /// 扩展路由，支持Session
    /// </summary>
    public class SessionRouteHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionRouteHandler(RouteData routeData) : base(routeData)
        {

        }
    }
}