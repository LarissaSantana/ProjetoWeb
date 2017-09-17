using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Produto
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public int EstoqueProduto { get; set; }
        public double PrecoProduto { get; set; }
        public String NomeProduto { get; set; }
        public int CodigoCategoria { get; set; }
        public int CodigoDepartamento { get; set; }
        private List<Produto> listaProduto;

        public List<Produto> ListaProduto
        {
            get { return listaProduto; }
            set { listaProduto = value; }

        }

        public Produto()
        {
            this.listaProduto = new List<Produto>();
        }

    }
}
