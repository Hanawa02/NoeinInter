using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel.Geral
{
    public class AgendaTime
    {
        private Dictionary<Horario, int> DicionarioHorarioJogo { get; set; }

        public SexoModalidades SexoAgenda { get; private set; }

        public AgendaTime(SexoModalidades sexoAgenda)
        {
            this.SexoAgenda = sexoAgenda;

            this.DicionarioHorarioJogo = new Dictionary<Horario, int>();
        }

        public AgendaTime(SexoModalidades sexoAgenda, Dictionary<Horario, int> horarioJogos)
        {
            this.SexoAgenda = sexoAgenda;

            this.DicionarioHorarioJogo = new Dictionary<Horario, int>(horarioJogos);
            
        }

        public Dictionary<Horario, int> RetornaDicionarioHorarioJogo()
        {
            var DicionarioRetorno = new Dictionary<Horario, int>();


            foreach (var item in DicionarioHorarioJogo)
            {
                DicionarioRetorno.Add(item.Key, item.Value);
            }

            return DicionarioRetorno;
        }

        public bool RemoveJogo(int idJogo)
        {
            if (this.DicionarioHorarioJogo.ContainsValue(idJogo))
            {
                foreach (var item in this.DicionarioHorarioJogo)
                {
                    this.DicionarioHorarioJogo.Remove(this.DicionarioHorarioJogo.First(x => x.Value == idJogo).Key);

                    return true;
                }
            }

            return false;
        }

        public bool InsereJogo(Horario horario, int idJogo)
        {
            if (!this.DicionarioHorarioJogo.ContainsKey(horario))
            {
                this.DicionarioHorarioJogo[horario] = idJogo;

                return true;
            }

            return false;
        }
    }
}
