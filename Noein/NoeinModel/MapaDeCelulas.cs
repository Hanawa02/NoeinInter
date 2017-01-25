using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Interfaces;

namespace NoeinModel
{
    public class MapaDeCelulas
    {
        public int Linhas { get; private set; }

        public int Colunas { get; private set; }

        public List<Celula> Mapas { get; set; }

        public MapaDeCelulas(int linhas, int colunas)
        {
            this.Linhas = linhas;

            this.Colunas = colunas;

            this.Mapas = new List<Celula>();
        }

        public bool AdicionaCelula(Celula celula)
        {
            if (this.Mapas == null)
            {
                return false;
            }
            else
            {
                if (this.Mapas.Count == 0)
                {
                    this.Mapas.Add(celula);

                    return true;
                }
                else
                {
                    foreach (var item in this.Mapas)
                    {
                        if (item.Linha == celula.Linha && item.Coluna == celula.Coluna)
                        {
                            return false;
                        }
                    }

                    this.Mapas.Add(celula);

                    return true;
                }
            }
        }

        public bool RemoveCelula(Celula celula)
        {
            foreach (var item in this.Mapas)
            {
                if (item.Linha == celula.Linha && item.Coluna == celula.Coluna)
                {
                    this.Mapas.Remove(item);

                    return true;
                }
            }

            return false;
        }

        public Celula RetornaCelula(int linha, int coluna)
        {
            return Mapas.Find(x => x.Linha == linha && x.Coluna == coluna);
        }
    }
}
