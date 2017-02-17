using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class ItemEnum
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public ItemEnum(int codigo, string descricao)
        {
            this.Codigo = codigo;

            this.Descricao = descricao;
        }
    }
}
