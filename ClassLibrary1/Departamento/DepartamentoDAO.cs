using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Departamento
{
    public class DepartamentoDAO : Conexao, IDAO<Departamento>
    {
        public int Atualizar(Departamento d)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_DEPARTAMENTO set Nome=@Nome,
                        Sigla=@Sigla, Comissao=@Comissao, CodigoVendedor=@CodigoVendedor", cn);

                cmd.Parameters.AddWithValue("@CodigoDepartamento", d.CodigoDepartamento);
                cmd.Parameters.AddWithValue("@Nome", d.Nome);
                cmd.Parameters.AddWithValue("@Sigla", d.Sigla);
                cmd.Parameters.AddWithValue("@Comissao", d.Comissao);
                cmd.Parameters.AddWithValue("@CodigoVendedor", d.CodigoVendedor);

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

        public int Deletar(int CodigoDepartamento)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Delete TB_DEPARTAMENTO where
                    CodigoDepartamento=@CodigoDepartamento", cn);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);

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

        public int Inserir(Departamento d)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"insert into TB_DEPARTAMENTO (Nome, Sigla, Comissao, CodigoVendedor) values (@Nome, @Sigla, @Comissao, @CodigoVendedor)", cn);

                cmd.Parameters.AddWithValue("@Nome", d.Nome);
                cmd.Parameters.AddWithValue("@Sigla", d.Sigla);
                cmd.Parameters.AddWithValue("@Comissao", d.Comissao);
                cmd.Parameters.AddWithValue("@CodigoVendedor", d.CodigoVendedor);

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

        public List<Departamento> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_DEPARTAMENTO", cn);
                dr = cmd.ExecuteReader();

                List<Departamento> listaDepartamento = new List<Departamento>();

                while (dr.Read())
                {
                    Departamento d = new Departamento();
                    d.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    d.Nome = Convert.ToString(dr["Nome"]);
                    d.Sigla = Convert.ToString(dr["Sigla"]);
                    d.Comissao = Convert.ToInt32(dr["Comissao"]);
                    d.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);

                    listaDepartamento.Add(d);
                }
                dr.Close();
                return listaDepartamento;

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

        public Departamento Obter(int CodigoDepartamento)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_DEPARTAMENTO where
                            CodigoDepartamento = @CodigoDepartamento", cn);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);
                dr = cmd.ExecuteReader();

                Departamento d = new Departamento();
                if (dr.Read())
                {
                    d.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    d.Nome = Convert.ToString(dr["Nome"]);
                    d.Sigla = Convert.ToString(dr["Sigla"]);
                    d.Comissao = Convert.ToInt32(dr["Comissao"]);
                    d.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                }
                dr.Close();
                return d;

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

        //Listar os produtos por Departamento
        public List<Departamento> ListarPorSigla(String sigla)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * FROM TB_DEPARTAMENTO WHERE Sigla=@Sigla", cn);
                //    Departamento d1 = new Departamento();

                cmd.Parameters.AddWithValue("@Sigla", sigla);

                //cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);
                dr = cmd.ExecuteReader();

                List<Departamento> listaDepartamento = new List<Departamento>();

                while (dr.Read())
                {
                    Departamento d = new Departamento();

                    d.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                    d.Nome = Convert.ToString(dr["Nome"]);
                    d.Sigla = Convert.ToString(dr["Sigla"]);
                    d.Comissao = Convert.ToInt32(dr["Comissao"]);
                    if ((dr["CodigoVendedor"]) == DBNull.Value)
                    {
                        d.CodigoVendedor = 0;
                    }
                    else
                    {
                        d.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    }
                    if ((dr["DataAdmissao"]) == DBNull.Value)
                    {
                        //só pra não ficar nulo
                        d.DataAdmissao = new DateTime(1000, 10, 10);
                    }
                    else
                    {
                        d.DataAdmissao = Convert.ToDateTime(dr["DataAdmissao"]);
                    }
                    
                    listaDepartamento.Add(d);
                }
                dr.Close();

                cmd.CommandText = @"select * from TB_PRODUTO where CodigoDepartamento = @CodigoDepartamento";


                for (int i = 0; i < listaDepartamento.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CodigoDepartamento", listaDepartamento[i].CodigoDepartamento);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Produto.Produto p = new Produto.Produto();

                        p.CodigoProduto = Convert.ToInt32(dr["CodigoProduto"]);
                        p.EstoqueProduto = Convert.ToInt32(dr["EstoqueProduto"]);
                        p.PrecoProduto = Convert.ToDouble(dr["PrecoProduto"]);
                        p.NomeProduto = Convert.ToString(dr["NomeProduto"]);
                        p.CodigoCategoria = Convert.ToInt32(dr["CodigoCategoria"]);
                        p.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                        //Lista de produtos do departamento

                        listaDepartamento[i].ListaProduto.Add(p);
                    }
                    dr.Close();
                }


                return listaDepartamento;

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

        //verificar se o departamento já possui um chefe
        public Boolean jaTemChefe(int CodigoDepartamento)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select CodigoVendedor from TB_DEPARTAMENTO where 
                                            CodigoDepartamento=@CodigoDepartamento", cn);

                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((dr["CodigoVendedor"]) == DBNull.Value)
                    {
                        dr.Close();
                        return false;
                    }
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

        public int removerChefeDepartamento(int CodigoDepartamento)
        {
            try
            {
                AbrirConexao();
                AbrirTransacao();
                cmd = new SqlCommand(@"update TB_DEPARTAMENTO set CodigoVendedor = NULL,
                                                                  DataAdmissao = NULL
                                        where CodigoDepartamento=@CodigoDepartamento", cn, trans);

                // update senhas set situacao = 'F' where senha = v_senha;

                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);

                CommitarTransacao();
                return cmd.ExecuteNonQuery();
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

        public int InserirChefeDepartamento(int CodigoDepartamento, int CodigoVendedor, DateTime DataAdmissao)
        {
            try
            {

                if (jaTemChefe(CodigoDepartamento))
                {
                    throw new Exception("Este departamento já possui um chefe");
                }

                AbrirConexao();
                AbrirTransacao();


                cmd = new SqlCommand(@"update TB_DEPARTAMENTO set CodigoVendedor = @CodigoVendedor,
                                                                  DataAdmissao = @DataAdmissao 
                                        where CodigoDepartamento=@CodigoDepartamento", cn, trans);

                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);
                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor);
                cmd.Parameters.AddWithValue("@DataAdmissao", DataAdmissao);
                cmd.ExecuteScalar();

                /*cmd.CommandText = @"select CodigoVendedor from TB_DEPARTAMENTO where CodigoDepartamento=@CodigoDepartamento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);*/
                // dr = cmd.ExecuteReader();

                /* while (dr.Read())
                 {
                     if (!(dr["CodigoVendedor"]).Equals(DBNull.Value))
                     {
                         dr.Close();
                         throw new Exception("Esse departamento já possui um chefe");
                     }
                 }*/

                //dr.Close();

                Vendedor.Vendedor v = new Vendedor.Vendedor();
                Vendedor.VendedorDAO vDao = new Vendedor.VendedorDAO();

                cmd.CommandText = @"select * from TB_DEPARTAMENTO where CodigoDepartamento=@CodigoDepartamento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CodigoDepartamento", CodigoDepartamento);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                }
                dr.Close();


                v = vDao.Obter(v.CodigoVendedor);
                if ((v.Desligado == true))
                {
                    throw new Exception("Vendedor desligado da empresa");

                }
                if ((v.Graduado == false))
                {
                    throw new Exception("Vendedor não pode ser chefe pois não tem graduação em nível superior");
                }

                CommitarTransacao();
                return cmd.ExecuteNonQuery();
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
    }
}


