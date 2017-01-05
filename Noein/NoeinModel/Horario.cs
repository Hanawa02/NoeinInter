using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Horario
    {
        private DateTime Inicio { get; set; }

        private DateTime Fim { get; set; }


        public Horario(DateTime inicio, int intervaloEmMinutos)
        {
            this.Inicio = inicio;
            this.Fim = inicio.AddMinutes(intervaloEmMinutos);
        }

    }
}
