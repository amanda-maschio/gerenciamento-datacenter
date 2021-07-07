using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models;

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
            var servidores = clienteHttp.Get<List<Servidor>>(@"servidor");
            return View(servidores);
        }

        public ActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ProcessarGravacaoPost(Servidor servidor)
        {
            if (ModelState.IsValid)
            {

                var retorno = clienteHttp.Post<Servidor>(@"servidor/", servidor);

                return RedirectToAction("Index", "Servidores");
            }
            else
            {
                return View("Index", servidor);
            }
        }

    }
}