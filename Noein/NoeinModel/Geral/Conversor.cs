using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class Conversor
    {
        public static List<string> DicionarioHorarioDisponivelParaObjetoJQuery(Dictionary<Horario, int> Dicionario)
        {
            var listaRetorno = new List<string>();

            foreach (var item in Dicionario)
            {
                if (item.Value == 0)
                {
                    listaRetorno.Add(item.Key.ToString() + "|Disponível");
                }
                else
                {
                    listaRetorno.Add(item.Key.ToString() + "| Jogo: " + item.Value);
                }
            }

            return listaRetorno;
        }

        public static DateTime ConverteDataJQueryParaDateTime(string DataJquery)
        {
            int Ano;
            int Mes;
            int Dia;
            int Hora;
            int Minutos;
            
            int.TryParse(DataJquery.Substring(0, 4), out Ano);
            int.TryParse(DataJquery.Substring(5, 2), out Mes);
            int.TryParse(DataJquery.Substring(8, 2), out Dia);
            int.TryParse(DataJquery.Substring(11, 2), out Hora);
            int.TryParse(DataJquery.Substring(14, 2), out Minutos);

            return new DateTime(Ano, Mes, Dia, Hora, Minutos, 0);

        }

        public static DateTime ConverteDataFormatadaParaDateTime(string DataJquery)
        {
            int Ano;
            int Mes;
            int Dia;
            int Hora;
            int Minutos;

            int.TryParse(DataJquery.Substring(0, 2), out Dia);
            int.TryParse(DataJquery.Substring(3, 2), out Mes);
            int.TryParse(DataJquery.Substring(6, 4), out Ano);
            int.TryParse(DataJquery.Substring(11, 2), out Hora);
            int.TryParse(DataJquery.Substring(14, 2), out Minutos);

            return new DateTime(Ano, Mes, Dia, Hora, Minutos, 0);
        }
    }
}
