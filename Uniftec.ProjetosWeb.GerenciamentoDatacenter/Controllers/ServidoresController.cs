using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class ServidoresController : Controller
    {
        // GET: Servidores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

    }
}