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
            this.DicionarioHorarioJogo = new Dictionary<Horario, int>();
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

        public Dictionary<Horario, int> RetornaDicionarioHorarioJogo()
        {
            var DicionarioRetorno = new Dictionary<Horario, int>();


            foreach( var item in DicionarioHorarioJogo)
            {
                DicionarioRetorno.Add(item.Key, item.Value);
            }

            return DicionarioRetorno;
        }

        public bool CadastraHorarioDisponivel(Horario horario)
        {
            if (!this.DicionarioHorarioJogo.ContainsKey(horario)) { 

                this.DicionarioHorarioJogo.Add(horario, 0);

                return true;
            }

            return false;         
        }

        public bool RemoveHorário(Horario horario)
        {
            if (this.DicionarioHorarioJogo.ContainsKey(horario))
            {
                if (this.DicionarioHorarioJogo[horario] == 0)
                {
                    this.DicionarioHorarioJogo.Remove(horario);

                    return true;
                }
            }
            
            return false;
        }

        public bool InsereJogo(Horario horario, int idJogo)
        {
            if (this.DicionarioHorarioJogo.ContainsKey(horario))
            {
                this.DicionarioHorarioJogo[horario] = idJogo;

                return true;
            }

            return false;
        }
    }
}
