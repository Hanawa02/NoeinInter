using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;
using NoeinModel.Interfaces;

namespace NoeinModel
{
    public class Modalidade
    {
        public int IdModalidade { get; private set; }
        
        public string DescricaoModalidade { get; private set; }

        public ModalidadesBasicas ModalidadeBase { get; private set; }

        public SexoModalidades SexoModalidade { get; private set; }

        public TipoDeModalidade TipoModalidade { get; private set; }

        public int MaximoDeTimes { get; private set; }

        public int IdCampeonato { get; private set; }

        public int QuantidadeDeTimesInscritos { get; private set; }

        public string Divisao { get; private set; }

        public int PontuacaoPadraoW { get; private set; }

        public int PontuacaoPadraoO { get; private set; }

        public Dictionary<ITipoClassificacao, bool> Fases { get; set; }


        public Modalidade(int idModalidade, ModalidadesBasicas modalidadeBase, SexoModalidades sexoModalidade, TipoDeModalidade tipoModalidade, int maximoDeTimes, int pontuacaoPadraoW, int pontuacaoPadraoO, string divisao)
        {
            this.IdModalidade = idModalidade;
            this.ModalidadeBase = modalidadeBase;
            this.SexoModalidade = sexoModalidade;
            this.TipoModalidade = tipoModalidade;
            this.MaximoDeTimes = maximoDeTimes;
            this.PontuacaoPadraoW = pontuacaoPadraoW;
            this.PontuacaoPadraoO = pontuacaoPadraoO;
            this.Divisao = divisao;
            this.DescricaoModalidade = ModalidadeBase.Descricao + " " + SexoModalidades.RetornaDescricao(SexoModalidade);
            this.Fases = new Dictionary<ITipoClassificacao, bool>();
        }
        
    }
}
