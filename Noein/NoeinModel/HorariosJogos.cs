using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

namespace NoeinModel
{
    public class HorariosJogos
    {
        public MapaDeCelulas MapaCelulas { get; private set; }

        private List<Quadra> Quadras { get; set; }

        private List<Horario> Horarios { get; set; }

        public HorariosJogos(List<Quadra> quadras, List<Jogo> TodosOsJogos)
        {
            this.Quadras = new List<Quadra>(quadras);

            PreencheListaHorarios();

            MapaCelulas = GeraMapa(TodosOsJogos);
        }

        public void PreencheListaHorarios()
        {
            if (this.Horarios == null)
            {
                this.Horarios = new List<Horario>();
            }

            foreach(var quadra in this.Quadras)
            {
                foreach(var horario in quadra.RetornaDicionarioHorarioJogo().Keys)
                {
                    if (!Horarios.Contains(horario)) {
                        this.Horarios.Add(horario);
                    }
                }
            }
        }


        public MapaDeCelulas GeraMapa(List<Jogo> jogos)
        {
            int Linhas = 0;
            int Colunas = 0;
            int ColunasData;
            int LinhasJogo;
            var Celulas = new List<Celula>();

            //Adiciona Linhas em Branco referentes à coluna de horários e as linhas de data e quadra
            Celulas.Add(new Celula(1, 1, "celulaVaziaData", 1, 1, "", ""));
            Celulas.Add(new Celula(2, 1, "celulaVaziaQuadra", 1, 1, "", ""));
            Linhas += 2;
            Colunas++;

            var listaDatas = Utilidades.RetornaDatasSemHorario(this.Horarios);

            var listaHorarios = Utilidades.RetornaHorarioSemDatas(this.Horarios);

            //Adiciona Coluna de Horários
            foreach (var item in listaHorarios)
            {
                Celulas.Add(new Celula(Linhas + 1, 1, "celulaHorario", 1, 1, item.Hour + ":" + item.Minute, item.Hour + ":" + item.Minute));

                Linhas++;
            }

            ColunasData = Colunas + 1;

            //Adiciona Linha de Datas e Quadras
            foreach (var item in listaDatas)
            {
                var listaQuadras = new List<Quadra>();
                foreach (var quadra in this.Quadras)
                {
                    if (Utilidades.RetornaDatasSemHorario(quadra.RetornaDicionarioHorarioJogo().Keys.ToList()).Contains(item)) {
                        listaQuadras.Add(quadra);
                    }
                }
                
                Celulas.Add(new Celula(1, ColunasData, "celulaData", listaQuadras.Count, 1, item.ToString("dd-MM-yyyy"), item.ToString("dd-MM-yyyy")));
                ColunasData++;

                foreach (var itemQuadra in listaQuadras)
                {
                    LinhasJogo = 3;

                    Celulas.Add(new Celula(2, Colunas + 1, "celulaQuadra", 1, 1, itemQuadra.DescricaoQuadra, itemQuadra.IdQuadra.ToString()));

                    var rowspan = 0;

                    foreach (var itemHorario in listaHorarios)
                    {
                        if (rowspan > 1)
                        {
                            rowspan--;

                            continue;                            
                        }
                        var dataFull = new DateTime(item.Year, item.Month, item.Day, itemHorario.Hour, itemHorario.Minute, 0);

                        var jogo = jogos.Find(x => x.HorarioJogo.Inicio == dataFull && x.IdQuadra == itemQuadra.IdQuadra);

                        if (jogo != null)
                        {
                            rowspan = 1;
                            
                            var Index = listaHorarios.IndexOf(itemHorario) + 1;

                            if (listaHorarios.Count > Index)
                            {
                                var dataFullIndex = new DateTime(item.Year, item.Month, item.Day, listaHorarios[Index].Hour, listaHorarios[Index].Minute, 0);

                                while (jogo.HorarioJogo.Fim > dataFullIndex)
                                {
                                    rowspan++;
                                    Index++;
                                    if (listaHorarios.Count > Index)
                                    {
                                        dataFullIndex = new DateTime(item.Year, item.Month, item.Day, listaHorarios[Index].Hour, listaHorarios[Index].Minute, 0);
                                    } else
                                    {
                                        break;
                                    }
                                }
                            }
                            //serviço que retorna nome dos times pra jogar na celula
                            Celulas.Add(new Celula(LinhasJogo, Colunas + 1, "celulaJogo", 1, rowspan, jogo.IdJogo + " - " + jogo.ModalidadeJogo.DescricaoModalidade + "|" + jogo.Time1.NomeTime + " x " + jogo.Time2.NomeTime, jogo.IdJogo.ToString()));

                            LinhasJogo += rowspan;
                        }
                        else
                        {
                            Celulas.Add(new Celula(LinhasJogo, Colunas + 1, "HorarioSemJogo"));
                            LinhasJogo++;
                        }
                    }

                    Colunas++;
                }
            }
            var MapaRetorno = new MapaDeCelulas(Linhas, Colunas);

            Celulas.ForEach(x => MapaRetorno.AdicionaCelula(x));

            return MapaRetorno;
        }
    }
}
