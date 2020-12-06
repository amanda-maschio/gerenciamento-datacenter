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

        private API.APIHttpClient clienteHttp;

        public UsuariosController()
        {
            clienteHttp = new API.APIHttpClient("http://localhost:50474/api/");
        }

        public ActionResult Index()
        {
            var servidores = clienteHttp.Get<List<Servidor>>(@"servidor");
            return View(servidores);
        }

        public ActionResult Gerenciar()
        {
            //Visualizar usuários cadastrados
            var usuarios = clienteHttp.Get<List<Usuario>>(@"usuario");
            return View(usuarios);
        }

        public ActionResult Editar(Guid id)  
        {
            //Altera usuários cadastrados
            var usuario = clienteHttp.Get<Usuario>(string.Format(@"usuario/{0}", id.ToString()));
            ViewBag.usuario = usuario;

            var servidores = clienteHttp.Get<List<Servidor>>(@"servidor");
            return View(servidores);
        }

        [HttpPost]
        public ActionResult ProcessarGravacaoPost(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.ListaServidores = new List<Servidor>();
                Servidor servidor = new Servidor();
                usuario.ListaServidores.Add(servidor);
                //pegar os valores do select multiple colocar num array, depois passar por parametro
                //pegar essa lista e colocar dentro do usuario.ListaServidores
                var id = clienteHttp.Post<Usuario>(@"usuario/", usuario);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", usuario);
            }
        }

        [HttpPost]
        public ActionResult ProcessarUpdatePost(Usuario usuario)
        {
            usuario.ListaServidores = new List<Servidor>();

            var id = clienteHttp.Put<Usuario>(@"usuario/", usuario.Id, usuario);

            return RedirectToAction("Index");
        }
    }
}