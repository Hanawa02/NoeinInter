using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

namespace NoeinModel
{
    public class Quadra
    {
        public int IdQuadra { get; private set; }

        public string DescricaoQuadra { get; private set; }

        public string Localizacao { get; private set; }

        private Dictionary<Horario, int> DicionarioHorarioJogo { get; set; }

        private List<ModalidadesBasicas> ListaModalidades { get; set; }
        
        public Quadra(int idQuadra, string descricaoQuadra, string localizacao, List<ModalidadesBasicas> modalidades)
        {
            this.IdQuadra = idQuadra;
            this.DescricaoQuadra = descricaoQuadra;
            this.Localizacao = localizacao;
            this.ListaModalidades = new List<ModalidadesBasicas>();
            this.ListaModalidades.AddRange(modalidades);
        }

        public Quadra(int idQuadra, string descricaoQuadra, string localizacao, List<ModalidadesBasicas> modalidades, Dictionary<Horario, int> dicionarioHorariosJogos)
        {
            this.IdQuadra = idQuadra;
            this.DescricaoQuadra = descricaoQuadra;
            this.Localizacao = localizacao;
            this.ListaModalidades = new List<ModalidadesBasicas>();
            this.ListaModalidades.AddRange(modalidades);
            this.DicionarioHorarioJogo = new Dictionary<Horario, int>(dicionarioHorariosJogos);
        }

        public List<ModalidadesBasicas> RetornaListaDeModalidade()
        {
            var listaRetorno = new List<ModalidadesBasicas>();

            listaRetorno.AddRange(this.ListaModalidades);

            return listaRetorno;
        }

    }
}
