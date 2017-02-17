using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class TipoDeModalidade
    {
        private int Codigo { get; set; }
        private string Descricao { get; set; }

        public static TipoDeModalidade Normal = new TipoDeModalidade(1, "Normal");
        public static TipoDeModalidade Paraesporte = new TipoDeModalidade(2, "Paraesporte");

        private TipoDeModalidade(int codigo, string descricao)
        {
            this.Codigo = codigo;

            this.Descricao = descricao;
        }

        public static string RetornaDescricao(TipoDeModalidade tipo)
        {
            return tipo.Descricao;
        }

        public static int RetornaCodigo(TipoDeModalidade tipo)
        {
            return tipo.Codigo;
        }
    }
}
