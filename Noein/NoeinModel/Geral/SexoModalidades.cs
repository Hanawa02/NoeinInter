using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class SexoModalidades
    {
        private int Codigo {get;set;}
        private string Descricao { get; set; }

        public static SexoModalidades Masculino = new SexoModalidades(1, "Masculino");
        public static SexoModalidades Feminino = new SexoModalidades(2, "Feminino");
        public static SexoModalidades Misto = new SexoModalidades(3, "Misto");

        private SexoModalidades(int codigo, string descricao)
        {
            this.Codigo = codigo;

            this.Descricao = descricao;
        }   

        public static string RetornaDescricao(SexoModalidades sexo)
        {
            return sexo.Descricao;
        }

        public static int RetornaCodigo(SexoModalidades sexo)
        {
            return sexo.Codigo;
        }
    }
}
