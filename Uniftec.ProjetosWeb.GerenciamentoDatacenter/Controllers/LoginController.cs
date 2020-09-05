using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autenticar()
        {
            return View();
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }
    }
}