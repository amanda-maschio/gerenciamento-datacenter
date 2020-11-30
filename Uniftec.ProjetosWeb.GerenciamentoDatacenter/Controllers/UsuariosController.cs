using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.API;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Gerenciar()
        {

            return View();
        }

        public ActionResult Gravar(Models.Usuario usuario)
        {

            return View();
        }

        public ActionResult Editar()  
        {
            
            return View();
        }
    }
}