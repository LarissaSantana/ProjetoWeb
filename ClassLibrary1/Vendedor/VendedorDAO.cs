using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Vendedor;
using System.Data.SqlClient;

namespace ClassLibrary1.Vendedor
{
    public class VendedorDAO : Conexao, IDAO<Vendedor>
    {
        public int Atualizar(Vendedor v)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"update TB_VENDEDOR set
                                       CpfVendedor = @CpfVendedor,
                                       DataAdmissao =@DataAdmissao,
                                       Desligado = @Deligado,
                                       Graduado = @Graduado,
                                       Nome = @Nome,
                                       RG = @RG,
                                       CodigoDepartamento = @CodigoDepartamento", cn);

                cmd.Parameters.AddWithValue("@CpfVendedor", v.CpfVendedor);
                cmd.Parameters.AddWithValue("@DataAdmissao", v.DataAdmissao);
                cmd.Parameters.AddWithValue("@Desligado", v.Desligado);
                cmd.Parameters.AddWithValue("@Graduado", v.Graduado);
                cmd.Parameters.AddWithValue("@Nome", v.Nome);
                cmd.Parameters.AddWithValue("@RG", v.RG);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", v.CodigoDepartamento);

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

        public int Deletar(int CodigoVendedor)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"delete from TB_VENDEDOR where CodigoVendedor=
                                        @CodigoVendedor", cn);
                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor);

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

        public int Inserir(Vendedor v)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"insert into TB_VENDEDOR values 
                                        (@CpfVendedor, @DataAdmissao,@Desligado,
                                         @Graduado, @Nome, @RG,
                                         @CodigoDepartamento)", cn);

                //cmd.Parameters.AddWithValue("@CodigoVendedor", v.CodigoVendedor);
                cmd.Parameters.AddWithValue("@CpfVendedor", v.CpfVendedor);
                cmd.Parameters.AddWithValue("@DataAdmissao", v.DataAdmissao);
                cmd.Parameters.AddWithValue("@Desligado", v.Desligado);
                cmd.Parameters.AddWithValue("@Graduado", v.Graduado);
                cmd.Parameters.AddWithValue("@Nome", v.Nome);
                cmd.Parameters.AddWithValue("@RG", v.RG);
                cmd.Parameters.AddWithValue("@CodigoDepartamento", v.CodigoDepartamento);

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

        public List<Vendedor> ListarTodos()
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"select * from TB_VENDEDOR", cn);
                dr = cmd.ExecuteReader();
                List<Vendedor> listaVendedor = new List<Vendedor>();
                while (dr.Read())
                {
                    Vendedor v = new Vendedor();
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.CpfVendedor = Convert.ToString(dr["CpfVendedor"]);
                    v.DataAdmissao = Convert.ToDateTime(dr["DataAdmissao"]);
                    v.Desligado = Convert.ToBoolean(dr["Desligado"]);
                    v.Graduado = Convert.ToBoolean(dr["Graduado"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.Nome = Convert.ToString(dr["Nome"]);
                    v.RG = Convert.ToString(dr["RG"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);

                    listaVendedor.Add(v);
                }
                dr.Close();
                return listaVendedor;

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

        public Vendedor Obter(int CodigoVendedor)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * from TB_VENDEDOR where CodigoVendedor=@CodigoVendedor", cn);
                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor);

                dr = cmd.ExecuteReader();
                Vendedor v = new Vendedor();
                if (dr.Read())
                {
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.CpfVendedor = Convert.ToString(dr["CpfVendedor"]);
                    v.DataAdmissao = Convert.ToDateTime(dr["DataAdmissao"]);
                    v.Desligado = Convert.ToBoolean(dr["Desligado"]);
                    v.Graduado = Convert.ToBoolean(dr["Graduado"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.Nome = Convert.ToString(dr["Nome"]);
                    v.RG = Convert.ToString(dr["RG"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
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

        // verificar se o vendedor é chefe de um departamento e a partir de qual data ele assumiu essa chefia
        public List<Vendedor> VerificarVendedorChefe(int CodigoVendedor)
        {
            try
            {
                AbrirConexao();
                cmd = new SqlCommand(@"Select * FROM TB_VENDEDOR WHERE CodigoVendedor=@CodigoVendedor", cn);
               
                cmd.Parameters.AddWithValue("@CodigoVendedor", CodigoVendedor);
                dr = cmd.ExecuteReader();

                List<Vendedor> listaVendedor = new List<Vendedor>();

                while (dr.Read())
                {
                    Vendedor v = new Vendedor();

                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.CpfVendedor = Convert.ToString(dr["CpfVendedor"]);
                    v.DataAdmissao = Convert.ToDateTime(dr["DataAdmissao"]);
                    v.Desligado = Convert.ToBoolean(dr["Desligado"]);
                    v.Graduado = Convert.ToBoolean(dr["Graduado"]);
                    v.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                    v.Nome = Convert.ToString(dr["Nome"]);
                    v.RG = Convert.ToString(dr["RG"]);
                    v.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);

                    listaVendedor.Add(v);
                }
                dr.Close();

                cmd.CommandText = @"select * from TB_DEPARTAMENTO where CodigoVendedor = @CodigoVendedor";


                for (int i = 0; i < listaVendedor.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CodigoVendedor", listaVendedor[i].CodigoVendedor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Departamento.Departamento d = new Departamento.Departamento();

                        d.CodigoDepartamento = Convert.ToInt32(dr["CodigoDepartamento"]);
                        d.Nome = Convert.ToString(dr["Nome"]);
                        d.Sigla = Convert.ToString(dr["Sigla"]);
                        d.Comissao = Convert.ToInt32(dr["Comissao"]);
                        d.CodigoVendedor = Convert.ToInt32(dr["CodigoVendedor"]);
                        d.DataAdmissao = Convert.ToDateTime(dr["DataAdmissao"]);

                        //Lista de vendedores do departamento

                        listaVendedor[i].ListaDepartamento.Add(d);
                    }
                    dr.Close();
                }
                return listaVendedor;

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
