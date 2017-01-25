using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class Jogo
    {
        public int IdJogo { get; private set; }

        public Time Time1 { get; private set; }

        public Time Time2 { get; private set; }

        public Nullable<int> PontuacaoTime1 { get; private set; }

        public Nullable<int> PontuacaoTime2 { get; private set; }

        public Nullable<int> PontuacaoAdicionalTime1 { get; private set; }

        public Nullable<int> PontuacaoAdicionalTime2 { get; private set; }

        public Horario HorarioJogo { get; private set; }

        public int IdQuadra { get; private set; }

        public Modalidade ModalidadeJogo { get; private set; }

        public Jogo(int idJogo, Time time1, Time time2, Modalidade modalidadeJogo)
        {
            this.IdJogo = idJogo;

            this.Time1 = time1;

            this.Time2 = time2;

            this.ModalidadeJogo = modalidadeJogo;
        }

        public Jogo(int idJogo, Time time1, Time time2, Modalidade modalidadeJogo, int idQuadra, Horario horario)
        {
            this.IdJogo = idJogo;

            this.Time1 = time1;

            this.Time2 = time2;

            this.ModalidadeJogo = modalidadeJogo;

            this.IdQuadra = idQuadra;

            this.HorarioJogo = horario;
        }

        public Jogo(int idJogo, Time time1, Time time2, Modalidade modalidadeJogo, int idQuadra, Horario horario, int pontuacaoTime1, int pontuacaoTime2, Nullable<int> pontuacaoAdicionalTime1, Nullable<int> pontuacaoAdicionalTime2)
        {
            this.IdJogo = idJogo;

            this.Time1 = time1;

            this.Time2 = time2;

            this.ModalidadeJogo = modalidadeJogo;

            this.IdQuadra = idQuadra;

            this.HorarioJogo = horario;

            this.PontuacaoTime1 = pontuacaoTime1;

            this.PontuacaoTime2 = pontuacaoTime2;

            this.PontuacaoAdicionalTime1 = pontuacaoAdicionalTime1;

            this.PontuacaoAdicionalTime2 = pontuacaoAdicionalTime2;
        }

        public void InformaResultado (int pontuacaoTime1, int pontuacaoTime2, int pontuacaoAdicionalTime1, int pontuacaoAdicionalTime2)
        {
            this.PontuacaoTime1 = pontuacaoTime1;

            this.PontuacaoTime2 = pontuacaoTime2;

            this.PontuacaoAdicionalTime1 = pontuacaoAdicionalTime1;

            this.PontuacaoAdicionalTime2 = pontuacaoAdicionalTime2;
        }

        public Time RetornaTimeVencedor()
        {
            if (this.PontuacaoTime1 == null || this.PontuacaoTime2 == null)
            {
                return null;
            }
            else
            {
                if (this.PontuacaoTime1 > this.PontuacaoTime2)
                {
                    return this.Time1;
                }
                else
                {
                    if (this.PontuacaoTime1 < this.PontuacaoTime2)
                    {
                        return this.Time2;
                    } else
                    {
                        if (this.PontuacaoAdicionalTime1 > this.PontuacaoAdicionalTime2)
                        {
                            return this.Time1;
                        }
                        else
                        {
                            return this.Time2;
                        }
                    }
                }
            }
        }
    }
}
