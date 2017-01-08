using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoeinModel;
using NoeinModel.Geral;

namespace Noein.Controllers
{
    public class NoeinController : Controller
    {
        public List<Quadra> ListaQuadras;
        public List<ModalidadesBasicas> ListaGeralModalidades;

        public NoeinController()
        {
            ListaQuadras = new List<Quadra>();
            ListaGeralModalidades = new List<ModalidadesBasicas>();
            ListaGeralModalidades.Add(new ModalidadesBasicas(1, "Futsal"));
            ListaGeralModalidades.Add(new ModalidadesBasicas(2, "Vôlei"));
            ListaGeralModalidades.Add(new ModalidadesBasicas(3, "Handebol"));
            ListaGeralModalidades.Add(new ModalidadesBasicas(4, "Basquete"));
            ListaGeralModalidades.Add(new ModalidadesBasicas(5, "Tênis"));
        }
        
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
            var listaModalidades = new List<ModalidadesBasicas>();            
            listaModalidades.AddRange(ListaGeralModalidades.FindAll(x => x.Codigo < 6));
            
            ListaQuadras.Add(new Quadra(1, "Quadra 1", "Ferreira Pacheco", listaModalidades));
            var listaModalidades2 = new List<ModalidadesBasicas>();
            listaModalidades2.Add(new ModalidadesBasicas(6, "Atletismo"));
            ListaQuadras.Add(new Quadra(2, "Pista de Atletismo", "UFG - Campus Samambaia", listaModalidades2));
            ListaQuadras.Add(new Quadra(3, "Piscina Olímpica", "Ferreira Pacheco", listaModalidades2));
            ViewBag.ModalidadesVisiveis = ListaGeralModalidades;
            return View("Quadras", ListaQuadras);
        }

        public JsonResult CadastraQuadra(string Descricao, string Localizacao, List<string> ListaModalidades)
        {
            var listaDeModalidades = ListaGeralModalidades.FindAll(x => ListaModalidades.Contains(x.Codigo.ToString()));

            var quadraAdicionada = new Quadra(ListaQuadras.Last().IdQuadra + 1, Descricao, Localizacao, listaDeModalidades);

            ListaQuadras.Add(quadraAdicionada);

            var retorno = new RetornoCadastro(new Mensagem(1, "Sucesso", "Cadastro realizado com sucesso!"), quadraAdicionada.IdQuadra);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}