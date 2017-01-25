using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Celula : IComparable<Celula>
    {
        public int Linha { get; set; }

        public int Coluna { get; set; }

        public string Estilo { get; private set; }

        public int ColSpan { get; private set; }

        public int RowSpan { get; private set; }

        public string ObjetoInterno { get; private set; }

        public string Valor { get; private set; }

        public Celula(int linha, int coluna, string estilo)
        {
            this.Coluna = coluna;

            this.Linha = linha;

            this.Estilo = estilo;

            this.ColSpan = 1;

            this.RowSpan = 1;
        }

        public Celula(int linha, int coluna, string estilo, int colSpan, int rowSpan, string objetoInterno, string valor)
        {
            this.Coluna = coluna;

            this.Linha = linha;

            this.Estilo = estilo;

            this.ColSpan = colSpan;

            this.ObjetoInterno = objetoInterno;

            this.Valor = valor;

            this.RowSpan = rowSpan;
        }

        public void InformaColSpan(int colSpan)
        {
            this.ColSpan = colSpan;
        }

        public void InformaRowSpan(int rowSpan)
        {
            this.RowSpan = rowSpan;
        }

        public void InformaValor(string valor)
        {
            this.Valor = valor;
        }

        public void InformaObjetoInterno(string objetoInterno)
        {
            this.ObjetoInterno = objetoInterno;
        }

        public int CompareTo(Celula obj)
        {
            return Linha.CompareTo(obj.Linha);
        }

        public void AdicionaEstilo(string estilo)
        {
            this.Estilo += " " + estilo;
        }
    }
}
