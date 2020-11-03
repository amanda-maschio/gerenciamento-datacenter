using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ftec.ProjetosWeb.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public object ViewBag { get; private set; }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        private ActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
