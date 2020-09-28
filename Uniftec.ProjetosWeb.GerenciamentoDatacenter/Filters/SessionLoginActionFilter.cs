using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Filters
{
    public class SessionLoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session["Login"] == null)
                {
                    //verificando uma session de login
                    //caso não exista, redireciona para outra action
                    controller.HttpContext.Response.Redirect("/Login/Index");
                }
            }

        }
    }
}