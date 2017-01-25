using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Interfaces;

namespace NoeinModel
{
    public class TipoClassificacao_Chave : ITipoClassificacao
    {
        public MapaDeCelulas MapaCelulas { get; private set; }

        private List<Etapa> Etapas { get; set; }
        
        public TipoClassificacao_Chave(List<Time> TimesInicial)
        {
            GeraMapaDeCelulas(TimesInicial);
        }
                
        private void GeraMapaDeCelulas(List<Time> TimesInicial)
        {
            var Linhas = TimesInicial.Count * 2 - 1;

            var QuantidadeDeEtapas = RetornaQuantidadeEtapas(TimesInicial.Count);

            var Colunas = (QuantidadeDeEtapas * 2) -1;

            var Mapas = GeraMapaBasico(Linhas, Colunas);

            PopulaMapa(1, TimesInicial, Mapas);

            this.MapaCelulas = new MapaDeCelulas(Linhas, Colunas);

            Mapas.ForEach(x => MapaCelulas.AdicionaCelula(x));
        }

        //Ainda não leva em conta quantidade de times impar
        private int RetornaQuantidadeEtapas(int quantidadeTimes)
        {
            int Etapas = 0;

            while (quantidadeTimes != 1)
            {
                Etapas++;

                quantidadeTimes = quantidadeTimes / 2;
            }

            Etapas++;

            return Etapas;
        }

        public List<Celula> GeraMapaBasico(int Linhas, int Colunas)
        {
            var Mapas = new List<Celula>();

            int definidorDeTime = 1;

            int linha;

            int coluna;

            bool UpDown = true;

            int indiceMultiplicador = 2;


            for (coluna = 1; coluna <= Colunas; coluna += 2)
            {
                for (linha = 1; linha <= Linhas; linha++)
                {
                    if (definidorDeTime == linha)
                    {
                        if (coluna % 2 != 0)
                        {
                            Mapas.Add(new Celula(linha, coluna, "celulaTime"));

                            if (indiceMultiplicador > 4)
                            {
                                var inicio = linha - (indiceMultiplicador / 4) + 1;

                                var fim = (indiceMultiplicador / 2) + inicio - 2;

                                for (int k = inicio; k <= fim; k++)
                                {
                                    Mapas.Add(new Celula(k, coluna - 1, "celulaRight"));
                                }
                            }
                            if (UpDown)
                            {
                                Mapas.Add(new Celula(linha, coluna + 1, "celulaUp"));

                                UpDown = false;
                            }
                            else
                            {
                                Mapas.Add(new Celula(linha, coluna + 1, "celulaDown"));

                                UpDown = true;
                            }

                        }

                        definidorDeTime += indiceMultiplicador;
                    }
                }

                if (definidorDeTime > (Linhas + 1))
                {
                    definidorDeTime = indiceMultiplicador;

                    indiceMultiplicador = indiceMultiplicador * 2;
                }
            }
            return Mapas;
        }

        public void PopulaMapa(int etapa, List<Time> TimesEtapa, List<Celula> Mapas)
        {
            var valorColunaEtapa = (2 * etapa) - 1;

            var celulasTime = Mapas.FindAll(x => x.Estilo == "celulaTime" && x.Coluna == valorColunaEtapa);

            celulasTime.Sort();

            for (int i = 0; i < TimesEtapa.Count; i++)
            {
                celulasTime[i].InformaValor(TimesEtapa[i].IdTime.ToString());

                celulasTime[i].InformaObjetoInterno(TimesEtapa[i].NomeTime);
            }
        }

        public MapaDeCelulas RetornaMapa()
        {
            return this.MapaCelulas;
        }

        /*public MapaChaveDeClassificacao(Dictionary<int, List<Time>> DicionarioTimes, int QuantidadeEtapas)
        {
            Linhas = (DicionarioTimes[1].Count * 2) - 1;

            Colunas = (QuantidadeEtapas * 2) - 1;

            Mapas = new List<Celula>();

            GeraMapaBasico();

            foreach (var item in DicionarioTimes)
            {
                PopulaMapa(item.Key, item.Value);
            }
        }
        */
        
    }
}
