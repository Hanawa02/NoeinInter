using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class RetornoCadastro
    {
        public Mensagem MensagemRetorno { get; private set; }

        public int IdObjetoCadastrado { get; private set; }

        public RetornoCadastro(Mensagem mensagem, int idObjetoCadastrado)
        {
            this.MensagemRetorno = mensagem;
            
            this.IdObjetoCadastrado = idObjetoCadastrado;
        }
    }
}
