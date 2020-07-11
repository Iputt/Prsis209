using ps.module.BLL;
using ps.module.IBLL;
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
            SpiderController spider = new SpiderController();
            return spider.Spider();
            //IUserService userService = new UserService();
            //var user = new module.Model.ps_sys_user()
            //{
            //    Id = new Guid(),
            //    nickName = "123"
            //};
            //userService.Add(user);
            //return null;
            //return Redirect("swagger");
        }
    }
}
