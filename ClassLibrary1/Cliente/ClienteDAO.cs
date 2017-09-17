using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Cliente
{
    public class ClienteDAO : Conexao, IDAO<Cliente>
    {
        public int Atualizar(Cliente c)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_CLIENTE set Nome=@Nome where
                    Cpf=@Cpf", cn);

                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Cpf", c.Cpf);

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

        public int Deletar(int Cpf)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"delete TB_Cliente where Cpf=@Cpf", cn);

                cmd.Parameters.AddWithValue("@Cpf", Cpf);
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

        public int Inserir(Cliente c)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"insert into TB_CLIENTE (Cpf, Nome) values (@Cpf, @Nome)", cn);

                cmd.Parameters.AddWithValue("@Cpf", c.Cpf);
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

        public List<Cliente> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_CLIENTE", cn);
                dr = cmd.ExecuteReader();
                List<Cliente> listaCliente = new List<Cliente>();
                while (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                    c.Cpf = Convert.ToString(dr["Cpf"]);
                    c.Nome = Convert.ToString(dr["Nome"]);

                    listaCliente.Add(c);
                }
                dr.Close();
                return listaCliente;
             
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

        public Cliente Obter(int IdCliente)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * from TB_CLIENTE where
                    IdCliente=@IdCliente", cn);
                
                cmd.Parameters.AddWithValue("@IdCliente", IdCliente);
                SqlDataReader dr = cmd.ExecuteReader();

                Cliente c = null;
                if (dr.Read())
                {
                    c = new Cliente();
                    c.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                    c.Nome = Convert.ToString(dr["Nome"]);
                    c.Cpf = Convert.ToString(dr["Cpf"]);
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
