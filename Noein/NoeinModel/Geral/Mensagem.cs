using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class Mensagem
    {
        public int IdMensagem { get; private set; }

        public string TituloMensagem { get; private set; }

        public StringBuilder CorpoMensagem { get; private set; }

        public string RetornoMensagens { get; private set; }

        public Mensagem(int idMensagem, string tituloMensagem, string mensagem)
        {
            this.IdMensagem = idMensagem;
            this.TituloMensagem = tituloMensagem;
            this.CorpoMensagem = new StringBuilder(mensagem);
            RetornoMensagens = CorpoMensagem.ToString();
        }

        public void AdicionaMensagem (string mensagem)
        {
            this.CorpoMensagem.AppendLine(mensagem);
            RetornoMensagens = CorpoMensagem.ToString();
        }
        
    }
}
