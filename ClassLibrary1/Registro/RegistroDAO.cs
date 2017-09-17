using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Registros;
using System.Data.SqlClient;

namespace ClassLibrary1.Registros
{
    public class RegistroDAO : Conexao
    {
        public int Atualizar(Registro r)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_REGISTRO set
                                        CodigoProduto=@CodigoProduto,
                                        ValorProduto=@ValorProduto,
                                        QuantidadeVendida=@QuantidadeVendida
                                          where CodigoVenda=@CodigoVenda", cn);

                cmd.Parameters.AddWithValue("@CodigoVenda", r.CodigoVenda);
                cmd.Parameters.AddWithValue("@CodigoProduto", r.CodigoProduto);
                cmd.Parameters.AddWithValue("@ValorProduto", r.ValorProduto);
                cmd.Parameters.AddWithValue("@QuantidadeVendida", r.QuantidadeVendida);

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

        public int Deletar(int CodigoVenda)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"delete from TB_REGISTRO where CodigoVenda=@CodigoVenda", cn);
                cmd.Parameters.AddWithValue("@CodigoVenda", CodigoVenda);

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

        public List<Registro> ListarTodos()
        {
            try
            {
                AbrirConexao();

                cmd = new SqlCommand(@"select * from TB_REGISTRO", cn);
                dr = cmd.ExecuteReader();
                List<Registro> listRegistro = new List<Registro>();

                while (dr.Read())
                {
                    Registro r = new Registro();
                    r.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                    r.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    r.ValorProduto = Convert.ToDouble(dr["ValorProduto"]);
                    r.QuantidadeVendida = Convert.ToInt32(dr["QuantidadeVendida"]);
                    listRegistro.Add(r);
                }
                dr.Close();
                return listRegistro;

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

        public Registro Obter(int CodigoVenda)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_REGISTRO where CodigoVenda=@CodigoVenda", cn);
                cmd.Parameters.AddWithValue("@CodigoVenda", CodigoVenda);
                dr = cmd.ExecuteReader();

                Registro r = new Registro();
                if (dr.Read())
                {
                    r.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                    r.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    r.ValorProduto = Convert.ToDouble(dr["ValorProduto"]);
                    r.QuantidadeVendida = Convert.ToInt32(dr["QuantidadeVendida"]);
                }
                dr.Close();
                return r;
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


        public double precoVendaTot()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select ValorTotal from TB_VENDA", cn, trans);
                dr = cmd.ExecuteReader();
                double valorTotal = 0;
                List<double> listaValorTotal = new List<double>();
                while (dr.Read())
                {
                    valorTotal = valorTotal + (Convert.ToDouble(dr["ValorTotal"]));
                }
                return valorTotal;
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


