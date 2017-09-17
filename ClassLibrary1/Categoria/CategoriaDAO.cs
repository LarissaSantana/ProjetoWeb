using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.Data.SqlClient;
using System.Configuration;

namespace ClassLibrary1.Categoria
{
    public class CategoriaDAO : Conexao, IDAO<Categoria>
    {
        //cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C: \Users\PC - DELL\Documents\visual studio 2017\WebSites\LojaDepartamento\App_Data\Database.mdf;Integrated Security=True");

        public int Atualizar(Categoria c)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Update TB_CATEGORIA set Nome = @Nome where CodigoCategoria = @CodigoCategoria", cn);

                cmd.Parameters.AddWithValue("@CodigoCategoria", c.CodigoCategoria);
                cmd.Parameters.AddWithValue("@Nome", c.Nome);

                return cmd.ExecuteNonQuery();

            }

            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            finally
            {
                FecharConexao();
            }

        }

        public int Deletar(int CodigoCategoria)
        {
            try
            {
                AbrirConexao();
                //Valor do Banco - Valor do Parâmetro
                cmd = new SqlCommand(@"Delete from TB_CATEGORIA where CodigoCategoria=@CodigoCategoria", cn);
                cmd.Parameters.AddWithValue("@CodigoCategoria", CodigoCategoria);
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                FecharConexao();
            }

        }

        public int Inserir(Categoria c)
        {
            try
            {
                AbrirConexao();
                /*var sqlQuery = string.Format("insert into TB_CATEGORIA values ('{0}')", c.Nome);
                cmd = new SqlCommand(sqlQuery, cn);*/

                cmd = new SqlCommand("INSERT INTO TB_CATEGORIA (Nome)" +
                    " values (@Nome) ", cn);
                //cmd.Parameters.AddWithValue("@CodigoCategoria", c.CodigoCategoria);
                cmd.Parameters.AddWithValue("@Nome", c.Nome);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            finally
            {
                FecharConexao();
            }
        }


        public List<Categoria> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * from TB_CATEGORIA", cn);
                dr = cmd.ExecuteReader();

                List<Categoria> listaCategoria = new List<Categoria>(); //lista vazia
                while (dr.Read())
                {
                    Categoria c = new Categoria();
                    c.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                    c.Nome = Convert.ToString(dr["Nome"]);

                    listaCategoria.Add(c);
                }
                dr.Close();
                return listaCategoria;
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                FecharConexao();
            }

        }

        public Categoria Obter(int CodigoCategoria)
        {
            try
            {
                AbrirConexao();

                cmd = new SqlCommand(@"select * from TB_CATEGORIA where CodigoCategoria
                = @CodigoCategoria", cn);

                cmd.Parameters.AddWithValue("@CodigoCategoria", CodigoCategoria);

                SqlDataReader dr = cmd.ExecuteReader();

                Categoria c = null;
                if (dr.Read())
                {
                    c = new Categoria();
                    c.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                    c.Nome = Convert.ToString(dr["Nome"]);
                }
                dr.Close();
                return c;
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                FecharConexao();
            }


        }
    }
}
