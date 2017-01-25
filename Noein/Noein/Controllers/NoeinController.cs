using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoeinModel;
using NoeinModel.Geral;
using NoeinModel.Interfaces;

namespace Noein.Controllers
{
    public class NoeinController : Controller
    {
        public static List<Quadra> ListaQuadras;
        public static List<Time> ListaTimes;
        public static List<ModalidadesBasicas> ListaGeralModalidades;
        public static List<ITipoClassificacao> chaves;

        public NoeinController()
        {
            if (ListaQuadras == null)
            {
                ListaQuadras = new List<Quadra>();
            }
            if (ListaGeralModalidades == null)
            {
                ListaGeralModalidades = new List<ModalidadesBasicas>();
                ListaGeralModalidades.Add(new ModalidadesBasicas(1, "Futsal"));
                ListaGeralModalidades.Add(new ModalidadesBasicas(2, "Vôlei"));
                ListaGeralModalidades.Add(new ModalidadesBasicas(3, "Handebol"));
                ListaGeralModalidades.Add(new ModalidadesBasicas(4, "Basquete"));
                ListaGeralModalidades.Add(new ModalidadesBasicas(5, "Tênis"));
            }
            if (ListaTimes == null)
            {
                ListaTimes = new List<Time>();
            }

            if(chaves==null)
            {
                chaves = new List<ITipoClassificacao>();
            }
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
            //Lista Quadras do campeonato
            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;
            /* Serviço que busca as quadras por campeonato */
            var listaModalidades = new List<ModalidadesBasicas>();            
            listaModalidades.AddRange(ListaGeralModalidades.FindAll(x => x.Codigo < 6));
            var listaModalidades2 = new List<ModalidadesBasicas>();
            listaModalidades2.Add(new ModalidadesBasicas(6, "Atletismo"));
            if (ListaQuadras.Count == 0)
            {
                ListaQuadras.Add(new Quadra(1, "Quadra 1", "Ferreira Pacheco", listaModalidades));
                ListaQuadras.Add(new Quadra(2, "Pista de Atletismo", "UFG - Campus Samambaia", listaModalidades2));
                ListaQuadras.Add(new Quadra(3, "Piscina Olímpica", "Ferreira Pacheco", listaModalidades2));

            }
            ViewBag.ModalidadesVisiveis = ListaGeralModalidades;
            return View("Quadras", ListaQuadras);
        }

        public JsonResult CadastraQuadra(string Descricao, string Localizacao, List<string> ListaModalidades)
        {
            //Adiciona Quadra ao campeonato
            var listaDeModalidades = ListaGeralModalidades.FindAll(x => ListaModalidades.Contains(x.Codigo.ToString()));

            var quadraAdicionada = new Quadra((ListaQuadras.Last().IdQuadra + 1), Descricao, Localizacao, listaDeModalidades);

            ListaQuadras.Add(quadraAdicionada);

            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Cadastro realizado com sucesso!"), quadraAdicionada.IdQuadra);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveQuadra(string IdQuadra)
        {
            //Remove quadra do campeonato
            var quadraRemover = ListaQuadras.Find(x => x.IdQuadra.ToString() == IdQuadra);

            ListaQuadras.Remove(quadraRemover);

            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Quadra removida com sucesso!"), null);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaListaHorarioDaQuadra(string IdQuadra)
        {
            //Retorna horários da quadra
            var quadra = ListaQuadras.Find(x => x.IdQuadra.ToString() == IdQuadra);
            
            var retorno = new RetornoPadrao(new Mensagem(1, "", ""), Conversor.DicionarioHorarioDisponivelParaObjetoJQuery(quadra.RetornaDicionarioHorarioJogo()));

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CadastraHorarioDisponivelDaQuadra(string IdQuadra, string DataInicio, string DataTermino, string Intervalo)
        {
            //Cadastra horário disponível para a quadra
            var quadra = ListaQuadras.Find(x => x.IdQuadra.ToString() == IdQuadra);
            
            var DtInicio = Conversor.ConverteDataJQueryParaDateTime(DataInicio);
            var DtAuxiliar = DtInicio;
            var DtFim = Conversor.ConverteDataJQueryParaDateTime(DataTermino);
            int intervalo;
            int.TryParse(Intervalo, out intervalo);

            while(DtAuxiliar < DtFim)
            {
                var horario = BDTemporario.AcessaBD().RetornaHorario(DtAuxiliar, intervalo);
                quadra.CadastraHorarioDisponivel(horario);
                DtAuxiliar = DtAuxiliar.AddMinutes(intervalo);
            }
            
            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Horário cadastrado com sucesso!"), Conversor.DicionarioHorarioDisponivelParaObjetoJQuery(quadra.RetornaDicionarioHorarioJogo()));

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult RemoveHorarioDaQuadra(string IdQuadra, string DataInicio, string DataTermino)
        {
            //Cadastra horário disponível da quadra
            Mensagem MensagemDeRetorno;

            var quadra = ListaQuadras.Find(x => x.IdQuadra.ToString() == IdQuadra);

            var horario = BDTemporario.AcessaBD().RetornaHorario(Conversor.ConverteDataFormatadaParaDateTime(DataInicio), Conversor.ConverteDataFormatadaParaDateTime(DataTermino));

            var excluiuHorario = quadra.RemoveHorário(horario);

            if (excluiuHorario)
            {
                MensagemDeRetorno = new Mensagem(1, "Sucesso", "Horário removido com sucesso!");
            }
            else
            {
                MensagemDeRetorno = new Mensagem(1, "Falha", "Não foi possível remover o horário!");
            }

            var retorno = new RetornoPadrao(MensagemDeRetorno, Conversor.DicionarioHorarioDisponivelParaObjetoJQuery(quadra.RetornaDicionarioHorarioJogo()));

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Times

        public ActionResult Times()
        {
            //Lista times do campeonato
            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;
            /* Serviço que busca as times por campeonato */
            var modalidades = BDTemporario.AcessaBD().RetornaModalidadesCampeonato();
            var dicionarioU = new Dictionary<Modalidade, int>();
            var dicionarioM = new Dictionary<Modalidade, int>();
            var dicionarioD = new Dictionary<Modalidade, int>();
            var dicionarioE = new Dictionary<Modalidade, int>();
            modalidades.ForEach(x =>
            {
                if(x.IdModalidade < 3)
                {
                    dicionarioU.Add(x, x.IdModalidade + 2);
                    dicionarioE.Add(x, x.IdModalidade + 2);
                }
                if (x.IdModalidade == 3)
                {
                    dicionarioM.Add(x, x.IdModalidade + 3);
                }
                if (x.IdModalidade > 3)
                {
                    dicionarioD.Add(x, x.IdModalidade + 1);
                }
            });

            if (ListaTimes.Count == 0)
            {
                var unif = new Time(2, "Unificada", 10, dicionarioU);
                unif.InsereJogo(new Horario(99, new DateTime(2017, 01, 15, 16, 00, 00), 30), 8, new SexoModalidades(2, "Feminino"));
                ListaTimes.Add(unif);
                ListaTimes.Add(new Time(1, "Medicina", 2, dicionarioM));
                ListaTimes.Add(new Time(3, "Direito", 8, dicionarioD));
                ListaTimes.Add(new Time(4, "Elétrica", 0, dicionarioE));
            }
            ViewBag.Modalidades = modalidades;

            return View("Times", ListaTimes);
        }
        
        public JsonResult CadastraTime(string Nome, List<string> ListaModalidades)
        {            
            //Adiciona Time no campeonato
            //Adiciona Time nas Modalidades do campeonato
            var listaDeModalidades = new Dictionary<Modalidade, int>();
            ListaModalidades.ForEach(x => {
                var itens = x.Split('|');
                var modalidade = BDTemporario.AcessaBD().
                                    RetornaModalidadesCampeonato().
                                    Find(y => y.IdModalidade.ToString() == itens[0]);
                int itemCriterio;
                int.TryParse(itens[1], out itemCriterio);
                listaDeModalidades.Add(modalidade, itemCriterio);
            });
            
            var timeAdicionado = new Time((ListaTimes.Last().IdTime + 1), Nome, 0, listaDeModalidades);

            ListaTimes.Add(timeAdicionado);

            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Cadastro realizado com sucesso!"), timeAdicionado.IdTime);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveTime(string IdTime)
        {
            //Adiciona Time no campeonato
            //Remove Time nas Modalidades do campeonato
            var timeRemover = ListaTimes.Find(x => x.IdTime.ToString() == IdTime);

            ListaTimes.Remove(timeRemover);

            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Quadra removida com sucesso!"), null);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetornaListaJogosTime(string IdTime)
        {
            var time = ListaTimes.Find(x => x.IdTime.ToString() == IdTime);

            var retorno = new RetornoPadrao(new Mensagem(1, "", ""), Conversor.DicionarioHorarioDisponivelParaObjetoJQuery(time.RetornaDicionarioHorarioJogo()));

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        #region Fases De Clasificação

        public ActionResult FasesDeClassificacao()
        {
            //Lista Fases de Classificacao

            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;
            
            return View("FasesDeClassificacao", BDTemporario.AcessaBD().RetornaModalidadesCampeonato());
        }

        public PartialViewResult FasesDeClassificacaoPartial(string IdModalidade)
        {
            //Serviço que retorna Fases de classificação da modalidade

            if (chaves.Count == 0)
            {
                chaves.Add(new TipoClassificacao_Chave(ListaTimes));
            }

            return PartialView("FasesDeClassificacaoPartial", chaves);
        }
        #endregion

        #region Horários Jogos
        public ActionResult HorarioJogos()
        {
            //Apresenta Tabela de Jogos por Horário
            ViewBag.NomeCampeonato = "Inter";
            ViewBag.NomeUsuario = "Laura Caroline";
            ViewBag.IdUsuario = 128;


            ListaQuadras.ElementAt(0).CadastraHorarioDisponivel(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 08, 15, 00), 30));
            ListaQuadras.ElementAt(0).CadastraHorarioDisponivel(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 08, 45, 00), 30));
            ListaQuadras.ElementAt(1).CadastraHorarioDisponivel(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 19, 09, 15, 00), 30));
            ListaQuadras.ElementAt(1).CadastraHorarioDisponivel(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 09, 45, 00), 30));
            ListaQuadras.ElementAt(2).CadastraHorarioDisponivel(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 10, 45, 00), 30));

            var ListaJogos = new List<Jogo>();
            ListaJogos.Add(new Jogo(1, ListaTimes.Find(x => x.IdTime == 2), ListaTimes.Find(x => x.IdTime == 1), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[3], ListaQuadras.ElementAt(0).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 08, 15, 00), 60)));
            ListaJogos.Add(new Jogo(2, ListaTimes.Find(x => x.IdTime == 3), ListaTimes.Find(x => x.IdTime == 4), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[2], ListaQuadras.ElementAt(1).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 19, 09, 15, 00), 30)));
            ListaJogos.Add(new Jogo(3, ListaTimes.Find(x => x.IdTime == 2), ListaTimes.Find(x => x.IdTime == 3), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[1], ListaQuadras.ElementAt(1).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 09, 45, 00), 30)));

            ListaQuadras.ElementAt(0).InsereJogo(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 08, 15, 00), 60), 1);
            ListaQuadras.ElementAt(1).InsereJogo(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 19, 09, 15, 00), 30), 2);
            ListaQuadras.ElementAt(1).InsereJogo(BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 09, 45, 00), 30), 3);

            var Retorno = new HorariosJogos(ListaQuadras, ListaJogos);

            return View("HorarioJogos", Retorno.MapaCelulas);
        }
        #endregion

        #region Resultados
        public ActionResult Resultados()
        {
            var ListaJogos = new List<Jogo>();
            var jogo1 = new Jogo(1, ListaTimes.Find(x => x.IdTime == 2), ListaTimes.Find(x => x.IdTime == 1), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[3], ListaQuadras.ElementAt(0).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 08, 15, 00), 60), 8, 2, null, null);
            var jogo2 = new Jogo(2, ListaTimes.Find(x => x.IdTime == 3), ListaTimes.Find(x => x.IdTime == 4), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[2], ListaQuadras.ElementAt(1).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 19, 09, 15, 00), 30), 1, 1, 3, 2);
            var jogo3 = new Jogo(3, ListaTimes.Find(x => x.IdTime == 2), ListaTimes.Find(x => x.IdTime == 3), BDTemporario.AcessaBD().RetornaModalidadesCampeonato()[1], ListaQuadras.ElementAt(1).IdQuadra, BDTemporario.AcessaBD().RetornaHorario(new DateTime(2017, 06, 18, 09, 45, 00), 30));
            ListaJogos.Add(jogo1);
            ListaJogos.Add(jogo2);
            ListaJogos.Add(jogo3);
            

            return View("Resultados", ListaJogos);
        }

        public JsonResult SalvaResultados(List<string> ListaResultados)
        {
            var retorno = new RetornoPadrao(new Mensagem(1, "Sucesso", "Resultados salvos com sucesso!"), null);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}