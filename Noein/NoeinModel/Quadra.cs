using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Quadra
    {
        public int IdQuadra { get; private set; }
        public string DescricaoQuadra { get; private set; }

        public string Localizacao { get; private set; }

        //private List<EnumModalidadesBasicas> {get;set;}
        //public EnumModalidadesBasicas {ge; private set;}

        public Quadra(int idQuadra, string descricaoQuadra, string localizacao)
        {
            this.IdQuadra = idQuadra;
            this.DescricaoQuadra = descricaoQuadra;
            this.Localizacao = localizacao;
        }
    }
}
