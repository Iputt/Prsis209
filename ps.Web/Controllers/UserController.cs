using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ps.module.BLL;
using ps.module.IBLL;

namespace ps.Web.Controllers
{
    public class UserController : Controller
    {
        readonly IUserService UserService = new UserService();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public void AddProcess()
        {
            var t = UserService.Query(u => u.gender == "1").FirstOrDefault();
        }
    }
}