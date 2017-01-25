using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Etapa
    {
        public int CodigoEtapa { get; private set; }

        private List<Time> Times { get; set; }

        private List<Jogo> Jogos { get; set; }

        public Etapa(int codigoEtapa, List<Time> ListaDeTimesOrdenada)
        {
            this.CodigoEtapa = codigoEtapa;
            this.Times = new List<Time>(ListaDeTimesOrdenada);
            this.GeraJogos();
        }


        private void GeraJogos()
        {
            if(Jogos == null)
            {
                this.Jogos = new List<Jogo>();
            }

            for (int contador = 0; contador < this.Times.Count; contador++)
            {
                //Serviço que cria jogo, tem que ser serviço por conta do 
            }
        }
    }
}
