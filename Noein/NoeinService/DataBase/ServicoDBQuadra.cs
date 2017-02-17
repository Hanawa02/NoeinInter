using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoeinModel.Geral;

namespace NoeinService.DataBase
{
    public class ServicoDBQuadra
    {
        const int Nenhuma = 0;
        const int Erro = 0;
        const bool Sucesso = true;
        const bool Falha = false;

        SqlConnection Conexao;
        SqlDataReader Reader;

        public int CadastraQuadra(int IdCampeonato, string DescricaoQuadra, string LocalizacaoQuadra, List<ModalidadesBasicas> Modalidades )
        {
            int IdQuadra = 0;

            try {
                AbreConexao();
                
                SqlCommand sqlQuadra = new SqlCommand("INSERT INTO Quadras (IdCampeonato, DescricaoQuadra, Localizacao) OUTPUT Inserted.IdQuadra VALUES (" + IdCampeonato + ", " + DescricaoQuadra + ", " + LocalizacaoQuadra+ ")");

                SqlCommand sqlModalidadesDaQuadra = new SqlCommand("INSERT INTO Quadras (IdCampeonato, DescricaoQuadra, Localizacao) values (" + IdCampeonato + ", " + DescricaoQuadra + ", " + LocalizacaoQuadra + ")");

                sqlQuadra.Connection = Conexao;

                Reader = sqlQuadra.ExecuteReader();

                if (Reader.HasRows)
                {
                    Reader.Read();

                    IdQuadra = Reader.GetInt32(0);

                    var linhasAfetadasModalidadesDaQuadra = sqlModalidadesDaQuadra.ExecuteNonQuery();
                    
                    if (linhasAfetadasModalidadesDaQuadra > Nenhuma) {
                        return IdQuadra;
                    }
                }

                DeletaQuadra(IdCampeonato, IdQuadra);

                return Erro;
            }
            catch (Exception ex)
            {
                DeletaQuadra(IdCampeonato, IdQuadra);

                return Erro;
            }
            finally                
            {
                Reader.Close();
                FechaConexao();
            }
        }
        public bool DeletaQuadra(int IdCampeonato, int IdQuadra)
        {
            try
            {
                AbreConexao();

                SqlCommand sqlDesfazer = new SqlCommand("DELETE FROM Quadras WHERE IdCampeonato = " + IdCampeonato + " AND IdQuadra = " + IdQuadra);

                sqlDesfazer.Connection = Conexao;

                var linhasAfetadas = sqlDesfazer.ExecuteNonQuery();

                if (linhasAfetadas > Nenhuma)
                {
                    return Sucesso;
                }

                return Falha;
            }
            catch (Exception e )
            {
                return Falha;
            }
            finally
            {
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


/*
 SqlCommand sql = new SqlCommand("insert INTO Jogos (IdTime1, IdTime2, IdQuadra, ModalidadeJogo) values (" + time1.IdTime + ", " + time2.IdTime + ", " + quadraId + ", " + (int)modalidade + " )");

                sql.Connection = RetornaConexao();

                var linhasAfetadas = sql.ExecuteNonQuery();
*/