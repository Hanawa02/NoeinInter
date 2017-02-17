using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel;
using NoeinModel.Geral;
using NoeinService.DataBase;

namespace NoeinService
{
    public class ServicoDeQuadra
    {
        ServicoDBQuadra ServicoDB;
        public ServicoDeQuadra()
        {
            ServicoDB = new ServicoDBQuadra();
        }
        public RetornoPadrao CadastraQuadra(string descricaoQuadra, string localizacaoQuadra, List<string> listaModalidades)
        {
            //Função que pega o Id do Campeonato
            var Modalidades = new ServicoDBModalidades().RetornaModalidadesBasicas(1);

            var IdQuadra = ServicoDB.CadastraQuadra(1, descricaoQuadra, localizacaoQuadra, Modalidades);

            if (IdQuadra > 0)
            {
                return new RetornoPadrao(new Mensagem(0, "Sucesso", "Quadra cadastrada com sucesso!"), IdQuadra);
            }

            return new RetornoPadrao(new Mensagem(0, "Falha", "Não foi possível cadastrar a quadra, confira se os dados estão corretos!"), null);
        }
    }
}
