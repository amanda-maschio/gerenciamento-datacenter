﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uniftec.ProjetosWeb.GerenciamentoDatacenter.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            //Exibe os Dashboards do Servidor selecionado
            return View();
        }


    }
}