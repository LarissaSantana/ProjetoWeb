using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Produto
{
    public class ProdutoDAO : Conexao, IDAO<Produto>
    {
        public int Atualizar(Produto p)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_PRODUTO set 
                                        EstoqueProduto=@EstoqueProduto,
                                        PrecoProduto=@PrecoProduto,
                                        NomeProduto=@NomeProduto 
                                        where CodigoProduto = @CodigoProduto", cn);

                cmd.Parameters.AddWithValue("@CodigoProduto", p.CodigoProduto);
                cmd.Parameters.AddWithValue("@EstoqueProduto", p.EstoqueProduto);
                cmd.Parameters.AddWithValue("@PrecoProduto", p.PrecoProduto);
                cmd.Parameters.AddWithValue("@NomeProduto", p.NomeProduto);
                /*cmd.Parameters.AddWithValue("@CodigoCategoria", p.CodigoCategoria);
                 cmd.Parameters.AddWithValue("@CodigoDepartamento", p.CodigoDepartamento);*/

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

        public int Deletar(int CodigoProduto)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"DELETE TB_VENDA where
                                        CodigoProduto=@CodigoProduto; DELETE TB_REGISTRO where
                                        CodigoProduto=@CodigoProduto; DELETE TB_PRODUTO where
                                        CodigoProduto=@CodigoProduto", cn);

                cmd.Parameters.AddWithValue("@CodigoProduto", CodigoProduto);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int Inserir(Produto p)
        {
            try
            {
                AbrirConexao();

                cmd = new SqlCommand(@"insert into TB_PRODUTO
                           (EstoqueProduto, PrecoProduto, NomeProduto, CodigoCategoria,
                             CodigoDepartamento) values
                            (@EstoqueProduto, @PrecoProduto, @NomeProduto, @CodigoCategoria,
                                @CodigoDepartamento)", cn);

                //cmd.Parameters.AddWithValue("@CodigoProduto", p.CodigoProduto);
                cmd.Parameters.AddWithValue("@EstoqueProduto", p.EstoqueProduto);
                cmd.Parameters.AddWithValue("@PrecoProduto", p.PrecoProduto);
                cmd.Parameters.AddWithValue("@NomeProduto", p.NomeProduto);
                cmd.Parameters.AddWithValue("@CodigoCategoria", p.CodigoCategoria);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", p.CodigoDepartamento);

                p.ListaProduto.Add(p);
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

        public List<Produto> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_PRODUTO", cn);
                dr = cmd.ExecuteReader();
                List<Produto> listaProduto = new List<Produto>();
                while (dr.Read())
                {
                    Produto p = new Produto();
                    p.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    p.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                    p.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    p.EstoqueProduto = Convert.ToInt32(dr["EstoqueProduto"]);
                    p.NomeProduto = Convert.ToString(dr["NomeProduto"]);
                    p.PrecoProduto = Convert.ToDouble(dr["PrecoProduto"]);

                    listaProduto.Add(p);
                }
                dr.Close();
                return listaProduto;

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

        public Produto Obter(int CodigoProduto)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * from TB_PRODUTO where
                    CodigoProduto=@CodigoProduto", cn);

                cmd.Parameters.AddWithValue("@CodigoProduto", CodigoProduto);
                SqlDataReader dr = cmd.ExecuteReader();

                Produto p = null;
                if (dr.Read())
                {
                    p = new Produto();
                    p.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    p.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                    p.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    p.EstoqueProduto = Convert.ToInt32(dr["EstoqueProduto"]);
                    p.NomeProduto = Convert.ToString(dr["NomeProduto"]);
                    p.PrecoProduto = Convert.ToDouble(dr["PrecoProduto"]);
                }
                dr.Close();
                return p;

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

        public Boolean temEstoque(int CodigoProduto)
        {
            try
            {
                AbrirConexao();
                int estoque = 0;
                cmd = new SqlCommand(@"Select EstoqueProduto from TB_PRODUTO where CodigoProduto=@CodigoProduto", cn);
                cmd.Parameters.AddWithValue("@CodigoProduto", CodigoProduto);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    estoque = Convert.ToInt32(dr["EstoqueProduto"]);
                }
                if (estoque <= 0)
                {
                    dr.Close();
                    return false;
                }
                dr.Close();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int qtdEstoque(int CodigoProduto)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select EstoqueProduto from TB_PRODUTO where 
                                            CodigoProduto=@CodigoProduto", cn);
                cmd.Parameters.AddWithValue("@CodigoProduto", CodigoProduto);
                dr = cmd.ExecuteReader();
                int qtdEstoque = 0;
                while (dr.Read())
                {
                    qtdEstoque = Convert.ToInt32(dr["EstoqueProduto"]);
                }
                dr.Close();
                return qtdEstoque;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

    }
}
