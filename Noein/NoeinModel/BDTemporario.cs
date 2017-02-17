using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

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
            ModalidadesCampeonato.Add(new Modalidade(1, new ModalidadesBasicas(1, "Futsal"), SexoModalidades.Masculino, TipoDeModalidade.Normal, 16, 3, 0, ""));
            ModalidadesCampeonato.Add(new Modalidade(2, new ModalidadesBasicas(2, "Vôlei"), SexoModalidades.Feminino, TipoDeModalidade.Normal, 8, 2, 0, ""));
            ModalidadesCampeonato.Add(new Modalidade(3, new ModalidadesBasicas(3, "Handebol"), SexoModalidades.Feminino, TipoDeModalidade.Normal, 32, 7, 0, ""));
            ModalidadesCampeonato.Add(new Modalidade(4, new ModalidadesBasicas(2, "Vôlei"), SexoModalidades.Masculino, TipoDeModalidade.Paraesporte, 8, 2, 0, ""));
            ModalidadesCampeonato.Add(new Modalidade(5, new ModalidadesBasicas(4, "Basquete"), SexoModalidades.Feminino, TipoDeModalidade.Normal, 16, 20, 0, ""));
            ModalidadesCampeonato.Add(new Modalidade(6, new ModalidadesBasicas(5, "Atletismo"), SexoModalidades.Misto, TipoDeModalidade.Normal, 50, 0, 0, ""));
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
