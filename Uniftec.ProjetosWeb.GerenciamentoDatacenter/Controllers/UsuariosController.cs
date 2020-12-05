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
            //Criar um novo usuário
            return View();
        }

        public ActionResult Gerenciar()
        {
            //Visualizar usuários cadastrados
            return View();
        }

        public ActionResult Editar()  
        {
            //Altera usuários cadastrados
            return View();
        }
    }
}