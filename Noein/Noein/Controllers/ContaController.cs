using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoeinModel;
using NoeinModel.Geral;

namespace Noein.Controllers
{
    public class ContaController : Controller
    {
        public ActionResult RedirecionarParaLogin1()
        {            
            return RedirectToAction("Login1");
        }
        // GET: Conta
        public ActionResult Login1()
        {
            return View();
        }
        //Passar uma list de campeonatos que o usuário tem acesso e o nome do Usuário
        public ActionResult Login2(List<Campeonato> CampeonatosUsuario, string NomeUsuario)
        {
            ViewBag.NomeUsuario = NomeUsuario;
            CampeonatosUsuario = new List<Campeonato>();
            CampeonatosUsuario.Add(new Campeonato(1, "Inter"));
            return View("Login2", CampeonatosUsuario);
        }
        public ActionResult RealizarLogin(string Email, string Senha)
        {
            //Chama serviço de Login
            /* A ser Removido depois da criação do serviço */
            var Retorno = new Usuario(1, "Laura Caroline");
            /* Fim */

            if (Retorno != null) {
                //Chama serviço que lista Campeonatos por usuário
                /* A ser Removido depois da criação do serviço */
                var ListaCampeonatos = new List<Campeonato>();
                ListaCampeonatos.Add(new Campeonato(1, "Inter"));
                ListaCampeonatos.Add(new Campeonato(2, "Pré-Inter"));
                /* Fim */

                var mensagemRetorno = new Mensagem(1, "Sucesso", "Redirecionando para a seleção de Campeonato!");

                return Json(new { mensagemRetorno , ListaCampeonatos, Retorno.NomeUsuario }, JsonRequestBehavior.AllowGet);
            }

            /* A ser Removido depois da criação do serviço */
            return Json(new Mensagem(1, "Falha", "E-mail e/ou senha inválidos! Tente Novamente!"), JsonRequestBehavior.AllowGet);
            /* Fim */
        }
    }
}

/* A ser Removido depois da criação do serviço */
/* Fim */
