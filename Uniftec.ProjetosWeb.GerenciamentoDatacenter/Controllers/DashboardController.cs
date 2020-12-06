using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class DashboardController : Controller
    {

        private API.APIHttpClient clienteHttp;

        public DashboardController()
        {
            clienteHttp = new API.APIHttpClient("http://localhost:50474/api/");
        }

        public ActionResult Index(Guid id)
        {
            //Exibe os Dashboards do Servidor selecionado
            var servidor = clienteHttp.Get<Servidor>(string.Format(@"servidor/{0}", id.ToString()));
            return View(servidor);
        }


    }
}