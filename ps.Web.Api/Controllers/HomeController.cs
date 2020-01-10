using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ps.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 直接重定向到swagger 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Redirect("swagger");
        }
    }
}
