using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Campeonato
    {
        public int IdCampeonato { get; private set; }
        public string DescricaoCampeonato { get; private set; }

        public Campeonato(int idCampeonato, string descricaoCampeonato)
        {
            this.IdCampeonato = idCampeonato;
            this.DescricaoCampeonato = descricaoCampeonato;
        }
    }
}
