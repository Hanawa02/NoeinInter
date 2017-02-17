using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel;
using NoeinModel.Geral;

namespace NoeinService.DataBase
{
    public class ServicoDBModalidades
    {
        SqlConnection Conexao;
        SqlDataReader Reader;

        public List<ModalidadesBasicas> RetornaModalidadesBasicas(int IdCampeonato)
        {
            try
            {
                var Retorno = new List<ModalidadesBasicas>();

                AbreConexao();

                SqlCommand sqlListaModalidades = new SqlCommand("Select MV.IdModalidadeBasica, MB.Descricao From ModalidadesBasicas MB, ModalidadesVisiveis MV WHERE MV.IdCampeonato = " + IdCampeonato + "AND MV.IdModalidadeBasica = MB.IdModalidadeBasica");

                sqlListaModalidades.Connection = Conexao;

                Reader = sqlListaModalidades.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Retorno.Add(new ModalidadesBasicas(Reader.GetInt32(0), Reader.GetString(1)));
                    }
                }

                if (Retorno.Count > 0)
                {
                    return Retorno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                Reader.Close();
                FechaConexao();
            }
        }
        private void AbreConexao()
        {
            if (Conexao == null)
            {
                Conexao = new SqlConnection("Data Source=NEWBRYANT\\SQLSERVER;Initial Catalog=Noein;Integrated Security=True;MultipleActiveResultSets=True;ApplicationIntent=ReadWrite; Max Pool Size = 999;");
            }

            Conexao.Open();
        }

        private void FechaConexao()
        {
            Conexao.Close();
            Conexao.Dispose();
        }
    }
}
