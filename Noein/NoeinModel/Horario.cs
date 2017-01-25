using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Horario
    {
        public int Codigo { get; private set; }

        public DateTime Inicio { get; private set; }

        public DateTime Fim { get; private set; }


        public Horario(int codigo, DateTime inicio, int intervaloEmMinutos)
        {
            this.Codigo = codigo;
            this.Inicio = inicio;
            this.Fim = inicio.AddMinutes(intervaloEmMinutos);
        }

        public Horario(int codigo, DateTime inicio, DateTime termino)
        {
            this.Codigo = codigo;
            this.Inicio = inicio;
            this.Fim = termino;
        }

        public override string ToString()
        {
            return Inicio.ToString("dd/MM/yyyy HH:mm") + "|" + Fim.ToString("dd/MM/yyyy HH:mm");
        }

        public override bool Equals(object obj)
        {
            var objetoConvertido = (Horario)obj;

            var retorno = this.Inicio == objetoConvertido.Inicio && this.Fim == objetoConvertido.Fim;

            return retorno;
        }

        public override int GetHashCode()
        {
            return Codigo;
        }
    }
}
