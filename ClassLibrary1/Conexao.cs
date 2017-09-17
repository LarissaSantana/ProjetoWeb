using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.Configuration;

namespace ClassLibrary1
{
    public class Conexao
    {
        protected SqlConnection cn;
        protected SqlCommand cmd;
        protected SqlDataReader dr;
        protected SqlTransaction trans;

        protected void AbrirConexao()
        {
            try
            {
                String strCon = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                cn = new SqlConnection(strCon);
                cn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void FecharConexao()
        {
            try
            {
                cn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        protected void AbrirTransacao()
        {
            this.trans = this.cn.BeginTransaction();
        }

        protected void CommitarTransacao()
        {
            this.trans.Commit();
        }

        protected void RollbackTransacao()
        {
            this.trans.Rollback();
        }
    }
}
