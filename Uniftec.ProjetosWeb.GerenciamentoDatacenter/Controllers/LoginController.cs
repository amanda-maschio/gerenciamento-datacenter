using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uniftec.ProjetosWeb.GerenciamentoDatacenter.Models;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class LoginController : Controller
    {

        private API.APIHttpClient clienteHttp;

        public LoginController()
        {
            clienteHttp = new API.APIHttpClient("http://localhost:50474/api/");
        }

        public ActionResult Index()
        {
            //Informar dados de login para entrar no sistema
            return View();
        }

        public ActionResult RecuperarSenha()
        {
            //Tela para recuperar senha
            return View();
        }

        public ActionResult Autenticar(Cliente cliente)
        {
            try
            {
                clienteHttp.AuthenticationPost(cliente.Email, cliente.Password);
                //Redireciona para a pagina principal
                return RedirectToAction("index", "Home");
            }
            catch (Exception e)
            {
                //Trata a falha da autenticacao;
                return RedirectToAction("Index");
            }
        }
    }
}