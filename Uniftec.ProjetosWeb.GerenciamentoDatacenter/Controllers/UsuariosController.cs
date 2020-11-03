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
        // GET: Usuarios
        public ActionResult Index()
        {
            //Consumir API para obter a lista de clientes
            APIHttpClient clienteHttp = new APIHttpClient(@"https://localhost:44349/api/");
            var listaUsuario = clienteHttp.Get <List<Usuario>>("usuario");
            ViewBag.listaUsuario = listaUsuario;
            //List<Models.Usuario> usuarios;
            //usuarios = (List<Models.Usuario>)Session["usuarios"];
            //ViewBag.usuarios = usuarios;

            return View();
        }

        private object List<T>(string v)
        {
            throw new NotImplementedException();
        }

        public ActionResult Gerenciar()
        {

            return View();
        }

        public ActionResult Gravar(Models.Usuario usuario)
        {
            List<Models.Usuario> usuarios;
            usuarios = (List<Models.Usuario>)Session["usuarios"];

            usuarios.Add(usuario);

            Session["usuarios"] = usuarios;

            APIHttpClient clienteHttp = new APIHttpClient(@"https://localhost:44349/api/");
            var retorno = clienteHttp.Post<Usuario>("usuario", usuarios);
           // ViewBag.retorno = retorno.Message;

            return Json(new Mensagem()
            {
                MensagemErro = false,
                MensagemTexto = "Cadastro realizado com sucesso!"
            });

        }

        public ActionResult Alterar(Models.Usuario usuario)
        {
            List<Models.Usuario> usuarios;
            usuarios = (List<Models.Usuario>)Session["usuarios"];
     
            foreach (var user in usuarios)
            {
                if (user.Id == usuario.Id)
                {
                    user.PrimeiroNome = usuario.PrimeiroNome;
                    user.SegundoNome = usuario.SegundoNome;
                    user.Senha = usuario.Senha;
                    user.Email = usuario.Email;
                }
            }

            Session["usuarios"] = usuarios;

            return RedirectToAction("Gerenciar");
        }

        public ActionResult Editar()  
        {
            
            return View();
        }
    }
}