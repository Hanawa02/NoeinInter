using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class RetornoPadrao
    {
        public Mensagem MensagemRetorno { get; private set; }

        public object ObjetoRetorno { get; private set; }

        public RetornoPadrao(Mensagem mensagem, object objetoRetorno)
        {
            this.MensagemRetorno = mensagem;

            this.ObjetoRetorno = objetoRetorno;
        }
    }
}
