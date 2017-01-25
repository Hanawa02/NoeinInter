using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoeinModel
{
    public class BDTemporario
    {
        private static BDTemporario BancoDeDados { get; set; }

        private static List<Horario> Horarios { get; set; }

        private static List<Modalidade> ModalidadesCampeonato { get; set; }
        private BDTemporario()
        {
            Horarios = new List<Horario>();
            ModalidadesCampeonato = new List<Modalidade>();
            ModalidadesCampeonato.Add(new Modalidade(1, new Geral.ModalidadesBasicas(1, "Futsal"), new Geral.SexoModalidades(1, "Masculino"), false));
            ModalidadesCampeonato.Add(new Modalidade(2, new Geral.ModalidadesBasicas(2, "Vôlei"), new Geral.SexoModalidades(2, "Feminino"), true));
            ModalidadesCampeonato.Add(new Modalidade(3, new Geral.ModalidadesBasicas(3, "Handebol"), new Geral.SexoModalidades(2, "Feminino"), true));
            ModalidadesCampeonato.Add(new Modalidade(4, new Geral.ModalidadesBasicas(2, "Vôlei"), new Geral.SexoModalidades(1, "Masculino"), true));
            ModalidadesCampeonato.Add(new Modalidade(5, new Geral.ModalidadesBasicas(4, "Basquete"), new Geral.SexoModalidades(2, "Feminino"), true));
        }

        public static BDTemporario AcessaBD()
        {
            if (BancoDeDados == null)
            {
                BancoDeDados = new BDTemporario();
            }

            return BancoDeDados;
        }

        public Horario RetornaHorario(DateTime inicio, int intervalo)
        {
            var fim = inicio.AddMinutes(intervalo);

            if (Horarios == null || Horarios.Count == 0)
            {
                var horario = new Horario(1, inicio, intervalo);

                Horarios.Add(horario);

                return horario;
            } else
            {
                foreach (var item in Horarios)
                {
                    if (item.Inicio == inicio && item.Fim == fim)
                    {
                        return item;
                    }
                }
                var horario = new Horario(Horarios.Last().Codigo + 1, inicio, intervalo);

                Horarios.Add(horario);

                return horario;
            }
        }

        public Horario RetornaHorario(DateTime inicio, DateTime fim)
        {
            int intervalo = (int)fim.Subtract(inicio).TotalMinutes;

            return RetornaHorario(inicio, intervalo);
        }

        public List<Modalidade> RetornaModalidadesCampeonato()
        {
            var listaRetorno = new List<Modalidade>();

            listaRetorno.AddRange(ModalidadesCampeonato);

            return listaRetorno;
        }

        public List<Horario> RetornaListaHorarios()
        {
            return Horarios;
        }
    }
}
