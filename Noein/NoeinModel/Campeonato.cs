using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

namespace NoeinModel
{
    public class Campeonato
    {
        public int IdCampeonato { get; private set; }

        public string DescricaoCampeonato { get; private set; }

        private List<Quadra> ListaDeQuadras { get; set; }

        //private List<ModalidadesBasicas> ListaModalidadesVisiveis { get; set; }

        public Campeonato(int idCampeonato, string descricaoCampeonato)
        {
            this.IdCampeonato = idCampeonato;
            this.DescricaoCampeonato = descricaoCampeonato;
        }

        public Campeonato(int idCampeonato, string descricaoCampeonato, List<Quadra> listaDeQuadras)
        {
            this.IdCampeonato = idCampeonato;
            this.DescricaoCampeonato = descricaoCampeonato;
            this.ListaDeQuadras.AddRange(listaDeQuadras);
        }
    }
}
