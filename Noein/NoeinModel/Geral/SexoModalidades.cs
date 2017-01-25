using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class SexoModalidades
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public SexoModalidades(int codigo, string descricao)
        {
            this.Codigo = codigo;

            this.Descricao = descricao;
        }

        public override bool Equals(object obj)
        {
            var objetoConvertido = (SexoModalidades)obj;

            return this.Codigo == objetoConvertido.Codigo ;
        }

        public override int GetHashCode()
        {
            return this.Codigo;
        }
    }
}
