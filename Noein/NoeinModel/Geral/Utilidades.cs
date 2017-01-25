using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class Utilidades
    {
        public static List<DateTime> RetornaDatasSemHorario(List<Horario> horarios)
        {
            var listaDatas = new List<DateTime>();

            foreach( var item in horarios)
            {
                var dataSemHorario = item.Inicio.Subtract(item.Inicio.TimeOfDay);

                if (!listaDatas.Contains(dataSemHorario))
                {
                    listaDatas.Add(dataSemHorario);
                }
            }
            listaDatas.Sort();

            return listaDatas;
        }
        /// <summary>
        /// Retorna Os Horários, sendo a data 01/01/1993
        /// </summary>
        /// <param name="horarios"></param>
        /// <returns></returns>
        public static List<DateTime> RetornaHorarioSemDatas(List<Horario> horarios)
        {
            var listaDatas = new List<DateTime>();

            foreach (var item in horarios)
            {
                var horarioSemdata = new DateTime(1993, 1, 1, item.Inicio.Hour, item.Inicio.Minute, item.Inicio.Second);

                if (!listaDatas.Contains(horarioSemdata))
                {
                    listaDatas.Add(horarioSemdata);
                }
            }
            listaDatas.Sort();

            return listaDatas;
        }
    }
}
