using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class ServidoresController : Controller
    {
        private API.APIHttpClient clienteHttp;

        public ServidoresController()
        {
            clienteHttp = new API.APIHttpClient("http://localhost:50474/api/");
        }

        public ActionResult Index()
        {
            //Exibir servidores cadastrados
            return View();
        }

        public ActionResult Cadastrar()
        {
            //Insere novos servidores
            return View();
        }

    }
}