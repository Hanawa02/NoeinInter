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

        public bool PossuiCriterioProprioDeGeracaoDeClassificacaoInicial { get; private set; }

        public Dictionary<ITipoClassificacao, ICriterioGeracaoDeChaveDeClassificacao> Fases { get; set; }


        public Modalidade(int idModalidade, ModalidadesBasicas modalidadeBase, SexoModalidades sexoModalidade, bool possuiCriterioProprioDeGeracaoDeClassificacaoInicial)
        {
            this.IdModalidade = idModalidade;
            this.ModalidadeBase = modalidadeBase;
            this.SexoModalidade = sexoModalidade;
            this.DescricaoModalidade = ModalidadeBase.Descricao + " " + SexoModalidade.Descricao;
            this.PossuiCriterioProprioDeGeracaoDeClassificacaoInicial = possuiCriterioProprioDeGeracaoDeClassificacaoInicial;
        }
        
    }
}
