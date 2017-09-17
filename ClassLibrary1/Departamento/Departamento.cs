using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Produto;
using ClassLibrary1.Vendedor;

namespace ClassLibrary1.Departamento
{
    public class Departamento
    {
        public Int32 CodigoDepartamento { get; set; }
        public String Nome { get; set; }
        public String Sigla { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Int32 Comissao { get; set; }
        public Int32 CodigoVendedor { get; set; }

        private List<Produto.Produto> listaProduto;
        private List<Venda.Venda> listaVenda;

        //variável chefe do tipo Vendedor
        private Vendedor.Vendedor chefe;
        private Vendedor.VendedorDAO daoVendedor = new VendedorDAO();
        //private Int32 CodigoChefe;


        //Retorna o chefe do departamento
        public Vendedor.Vendedor Chefe
        {
            get
            {
                if (this.chefe == null)
                {
                    this.chefe = daoVendedor.Obter(CodigoVendedor);
                }
                return this.chefe;
            }
            set
            {
                this.chefe = value;
            }
        }

        public List<Venda.Venda> ListaVenda
        {
            get { return listaVenda; }
            set { listaVenda = value; }
        }

        public List<Produto.Produto> ListaProduto
        {
            get { return listaProduto; }
            set { listaProduto = value; }
        }

        public Departamento()
        {
            this.ListaProduto = new List<Produto.Produto>();
            this.listaVenda = new List<Venda.Venda>();
        }
    }
}
