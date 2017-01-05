using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Noein.Controllers
{
    public class NoeinController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;
            return View("Index");
        }
        #region Quadras
        public ActionResult Quadras()
        {
            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;
            /* Serviço que busca as quadras por campeonato */


            return View("Quadras");
        }

        #endregion
    }
}