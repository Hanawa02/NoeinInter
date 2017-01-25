using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

namespace NoeinModel
{
    public class Time
    {
        public int IdTime { get; private set; }

        public string NomeTime { get; private set; }

        public int PontuacaoGeral { get; private set; }

        private Dictionary<Modalidade, int> ModalidadesParticipantes { get; set; }

        private List<AgendaTime> Agendas { get; set; }
                        
        public Time(int idTime, string nomeTime, int pontuacaoGeral, Dictionary<Modalidade, int> modalidadesParticipantes)
        {
            this.IdTime = idTime;
            this.NomeTime = nomeTime;
            this.PontuacaoGeral = pontuacaoGeral;
            this.Agendas = new List<AgendaTime>();
            this.ModalidadesParticipantes = new Dictionary<Modalidade, int>(modalidadesParticipantes);
            this.CriaAgendas();
        }

        public Time(int idTime, string nomeTime, int pontuacaoGeral, Dictionary<Modalidade, int> modalidadesParticipantes, List<AgendaTime> agendas)
        {
            this.IdTime = idTime;
            this.NomeTime = nomeTime;
            this.PontuacaoGeral = pontuacaoGeral;
            this.ModalidadesParticipantes = new Dictionary<Modalidade, int>(modalidadesParticipantes);
            this.Agendas = new List<AgendaTime>();
            this.Agendas.AddRange(agendas);
            this.CriaAgendas();
        }        
        
        public Dictionary<Modalidade, int> RetornaListaDeModalidade()
        {
            var listaRetorno = new Dictionary<Modalidade, int>(ModalidadesParticipantes);
            return listaRetorno;
        }
        public bool RemoveJogo(int idJogo)
        {
            bool retorno = false;

            Agendas.ForEach(x => retorno = retorno || x.RemoveJogo(idJogo));

            return retorno;
        }

        public bool InsereJogo(Horario horario, int idJogo, SexoModalidades sexoModalidade)
        {
            foreach (var item in Agendas) { 
                if (item.SexoAgenda.Equals(sexoModalidade))
                {
                    item.InsereJogo(horario, idJogo);
                    return true;
                }
            }

            return false;
        }

        public Dictionary<Horario, int> RetornaDicionarioHorarioJogo()
        {
            var dicionarioRetorno = new Dictionary<Horario, int>();

            foreach(var agenda in Agendas)
            {
                foreach(var jogo in agenda.RetornaDicionarioHorarioJogo())
                {
                    dicionarioRetorno.Add(jogo.Key, jogo.Value);
                }
            }

            return dicionarioRetorno;
        }
        
        private void CriaAgendas()
        {
            this.Agendas = new List<AgendaTime>();

            foreach(var x in this.ModalidadesParticipantes)
            {
                if (this.Agendas.Count == 0)
                {
                    this.Agendas.Add(new AgendaTime(x.Key.SexoModalidade));
                } else
                {
                    foreach (var agenda in this.Agendas)
                    {
                        if (agenda.SexoAgenda.Equals(x.Key.SexoModalidade))
                        {
                            break;
                        } else
                        {
                            if (agenda.SexoAgenda.Equals(this.Agendas.Last().SexoAgenda)) {
                                this.Agendas.Add(new AgendaTime(x.Key.SexoModalidade));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
