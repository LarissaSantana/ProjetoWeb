using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Registros;

namespace ClassLibrary1.Venda
{
    public class VendaDAO : Conexao, IDAO<Venda>
    {
        public int Atualizar(Venda v)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_VENDA set 
                                        ValorTotal = @ValorTotal,
                                        ComissaoVenda = @ComissaoVenda, 
                                        ComissaoPagou=@ComissaoPagou,
                                        DataVenda = @DataVenda,
                                        CodigoVendedor = @CodigoVendedor,
                                        CodigoProduto = @CodigoProduto,
                                        CodigoDepartamento = @CodigoDepartamento, 
                                        IdCliente = @IdCliente 
                                    where CodigoVenda = @CodigoVenda", cn);

                cmd.Parameters.AddWithValue("@CodigoVenda", v.CodigoVenda);
                cmd.Parameters.AddWithValue("@ValorTotal", v.ValorTotal);
                cmd.Parameters.AddWithValue("@ComissaoVenda", v.ComissaoVenda);
                cmd.Parameters.AddWithValue("@ComissaoPagou", v.ComissaoPagou);
                cmd.Parameters.AddWithValue("@DataVenda", v.DataVenda);
                cmd.Parameters.AddWithValue("@CodigoVendedor", v.CodigoVendedor);
                cmd.Parameters.AddWithValue("@CodigoProduto", v.CodigoProduto);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", v.CodigoDepartamento);
                cmd.Parameters.AddWithValue("@IdCliente", v.IdCliente);

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

        public int Deletar(int CodigoVenda)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"delete TB_REGISTRO where CodigoVenda=@CodigoVenda; 
                                        delete TB_VENDA where CodigoVenda=@CodigoVenda", cn);

                cmd.Parameters.AddWithValue("@CodigoVenda", CodigoVenda);
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


        public int Inserir(Venda v)
        {
            try
            {
                AbrirConexao();
                AbrirTransacao();
                //Query de inserção
                cmd = new SqlCommand(@"insert into TB_VENDA (ComissaoPagou,
                                         DataVenda, CodigoVendedor, CodigoProduto,
                                         IdCliente, QtdVendida) values 
                                        (@ComissaoPagou,
                                         @DataVenda, @CodigoVendedor, @CodigoProduto,
                                         @IdCliente, @QtdVendida);select @@Identity", cn, trans);

                cmd.Parameters.AddWithValue("@ComissaoPagou", v.ComissaoPagou);
                cmd.Parameters.AddWithValue("@DataVenda", v.DataVenda);
                cmd.Parameters.AddWithValue("@CodigoVendedor", v.CodigoVendedor);
                cmd.Parameters.AddWithValue("@CodigoProduto", v.CodigoProduto);
                cmd.Parameters.AddWithValue("@IdCliente", v.IdCliente);
                cmd.Parameters.AddWithValue("@QtdVendida", v.QtdVendida);
                v.CodigoVenda = Convert.ToInt32(cmd.ExecuteScalar());

                //adicionar o ValorTotal da venda, CalcularComissao e pegar o CodigoDoDepartamento
                cmd.CommandText = @"select PrecoProduto, CodigoDepartamento from TB_PRODUTO where CodigoProduto=@CodigoProduto";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodigoProduto", v.CodigoProduto);
                SqlDataReader dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    v.ValorTotal = Convert.ToDouble(dr2["PrecoProduto"]) * (v.QtdVendida);
                    v.CodigoDepartamento = Convert.ToInt32(dr2["CodigoDepartamento"]);
                }
                dr2.Close();

                cmd.CommandText = @"select Comissao from TB_DEPARTAMENTO where CodigoDepartamento=
                                                @CodigoDepartamento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodigoDepartamento", v.CodigoDepartamento);
                SqlDataReader dr3 = cmd.ExecuteReader();
                while (dr3.Read())
                {
                    if (v.ComissaoPagou == true)
                    {
                        v.ComissaoVenda = (((v.ValorTotal) * (Convert.ToDouble(dr3["Comissao"]))) / 100);
                    }
                    else
                    {
                        v.ComissaoVenda = 0;
                    }
                }
                dr3.Close();

                foreach (Registro registro in v.ListaRegistro)
                {
                    //verificar se tem estoque disponível para aquela venda
                    if (registro.Produto.EstoqueProduto >= registro.QuantidadeVendida)
                    {
                        cmd.CommandText = @"insert into TB_REGISTRO (CodigoVenda, CodigoProduto, ValorProduto,
                                    QuantidadeVendida, CodigoVendedor) values (@CodigoVenda, @CodigoProduto, @ValorProduto,
                                        @QuantidadeVendida, @CodigoVendedor)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CodigoVenda", v.CodigoVenda);
                        cmd.Parameters.AddWithValue("@CodigoProduto", registro.CodigoProduto);
                        cmd.Parameters.AddWithValue("@ValorProduto", registro.ValorProduto);
                        cmd.Parameters.AddWithValue("@QuantidadeVendida", registro.QuantidadeVendida);
                        cmd.Parameters.AddWithValue("@CodigoVendedor", registro.CodigoVendedor);
                        cmd.ExecuteNonQuery();

                        //decrementar o Estoque de produtos na TB_PRODUTO
                        cmd.CommandText = @"select * from TB_PRODUTO where CodigoProduto=
                                              @CodigoProduto";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CodigoProduto", registro.CodigoProduto);
                        dr = cmd.ExecuteReader();

                        Produto.Produto p = new Produto.Produto();
                        while (dr.Read())
                        {
                            p.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                            p.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                            p.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                            p.EstoqueProduto = ((Convert.ToInt32(dr["EstoqueProduto"]) - (registro.QuantidadeVendida)));
                            p.NomeProduto = Convert.ToString(dr["NomeProduto"]);
                            p.PrecoProduto = Convert.ToDouble(dr["PrecoProduto"]);
                        }
                        Produto.ProdutoDAO pDao = new Produto.ProdutoDAO();
                        pDao.Atualizar(p);
                        dr.Close();
                    }
                    else
                    {
                        throw new Exception("Produto indisponível");
                    }
                }
                CommitarTransacao();
                Atualizar(v);
                return 1;
            }

            catch (Exception e)
            {
                RollbackTransacao();
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }


        public List<Venda> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_VENDA", cn);
                dr = cmd.ExecuteReader();
                List<Venda> listaVenda = new List<Venda>();
                while (dr.Read())
                {
                    Venda v = new Venda();
                    v.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                    v.ValorTotal = Convert.ToInt32(dr["ValorTotal"]);
                    v.ComissaoVenda = Convert.ToInt32(dr["ComissaoVenda"]);
                    v.ComissaoPagou = Convert.ToBoolean(dr["ComissaoPagou"]);
                    v.DataVenda = Convert.ToDateTime(dr["DataVenda"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    v.IdCliente = Convert.ToInt32(dr["IdCliente"]);

                    listaVenda.Add(v);
                }
                dr.Close();
                return listaVenda;

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

        public Venda Obter(int CodigoVenda)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * from TB_VENDA where
                    CodigoVenda=@CodigoVenda", cn);

                cmd.Parameters.AddWithValue("@CodigoVenda", CodigoVenda);
                dr = cmd.ExecuteReader();

                Venda v = null;
                if (dr.Read())
                {
                    v = new Venda();
                    v.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                    v.ValorTotal = Convert.ToInt32(dr["ValorTotal"]);
                    v.ComissaoVenda = Convert.ToInt32(dr["ComissaoVenda"]);
                    v.ComissaoPagou = Convert.ToBoolean(dr["ComissaoPagou"]);
                    v.DataVenda = Convert.ToDateTime(dr["DataVenda"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    v.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                }
                dr.Close();
                return v;

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

        //Questão 03 da apostila - Listar as vendas de um determinado vendedor em um intervalo de datas
        public List<Venda> ObterVendaPorData(int CodigoVendedor, DateTime data1, DateTime data2)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_VENDA WHERE CodigoVendedor=@CodigoVendedor ORDER BY DataVenda", cn);

                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor);
                dr = cmd.ExecuteReader();
                //List<Venda> listaOrdenada = new List<Venda>();
                Venda venda = new Venda();
                while (dr.Read())
                {
                    Venda v = new Venda();
                    v.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                    v.ValorTotal = Convert.ToDouble(dr["ValorTotal"]);
                    v.ComissaoPagou = Convert.ToBoolean(dr["ComissaoPagou"]);
                    v.DataVenda = Convert.ToDateTime(dr["DataVenda"]);
                    v.ComissaoVenda = Convert.ToDouble(dr["ComissaoVenda"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    v.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);

                    venda.ListaVendaOrdenadaData.Add(v);
                }
                List<Venda> listaVendaProv = new List<Venda>();
                //a data1 tem que ser menor que a data2
                if (data1 > data2)
                {
                    DateTime aux = data1;
                    data1 = data2;
                    data2 = aux;
                }
                for (int i = 0; i < venda.ListaVendaOrdenadaData.Count; i++)
                {
                    //pesquisar a primeira data e pegar o index
                    //pesquisar a segunda data e pegar o index
                    //imprimir as datas que compreendem os dois index

                    if (data1 != data2)

                    {
                        if ((venda.ListaVendaOrdenadaData[i].DataVenda).Equals(data1))
                        {
                            listaVendaProv.Add(venda.ListaVendaOrdenadaData[i]);
                        }
                        if ((venda.ListaVendaOrdenadaData[i].DataVenda).Equals(data2))
                        {
                            listaVendaProv.Add(venda.ListaVendaOrdenadaData[i]);
                        }

                    }
                    //Se as duas datas forem iguais quer dizer que a lista só precisa receber valores de um dia, uma só vez.
                    else
                    {
                        if ((venda.ListaVendaOrdenadaData[i].DataVenda).Equals(data1))
                        {
                            listaVendaProv.Add(venda.ListaVendaOrdenadaData[i]);
                        }
                    }
                }
                dr.Close();
                return listaVendaProv;
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



        //Questão 07 - relatorio de vendas efetuadas por data
        public List<Venda> RelatorioVendas()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select distinct DataVenda from TB_VENDA", cn);
                dr = cmd.ExecuteReader();

                List<Venda> listaDataVenda = new List<Venda>();

                while (dr.Read())
                {
                    Venda v = new Venda();

                    v.DataVenda = Convert.ToDateTime(dr["DataVenda"]);
                    listaDataVenda.Add(v);

                }
                dr.Close();

                cmd.CommandText = @"select * from TB_VENDA where DataVenda = @DataVenda";


                for (int i = 0; i < listaDataVenda.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@DataVenda", listaDataVenda[i].DataVenda);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Venda v1 = new Venda();
                        v1.CodigoVenda = Convert.ToInt32(dr["CodigoVenda"]);
                        v1.ValorTotal = Convert.ToInt32(dr["ValorTotal"]);
                        v1.ComissaoVenda = Convert.ToInt32(dr["ComissaoVenda"]);
                        v1.ComissaoPagou = Convert.ToBoolean(dr["ComissaoPagou"]);
                        v1.DataVenda = Convert.ToDateTime(dr["DataVenda"]);
                        v1.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                        v1.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                        v1.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                        v1.IdCliente = Convert.ToInt32(dr["IdCliente"]);


                        listaDataVenda[i].ListaRelatorioVenda.Add(v1);
                    }
                    dr.Close();
                }

                return listaDataVenda;

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
