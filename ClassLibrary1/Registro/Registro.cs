using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Registros
{
    public class Registro
    {
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }
        public double ValorProduto { get; set; }
        public int QuantidadeVendida { get; set; }
        public int CodigoVendedor { get; set; }

        private Produto.Produto produto;

        public Produto.Produto Produto
        {
            get { return produto; }
            set { produto = value; }
        }
    }
}
