using ClassLibrary1.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Venda
{
    public class Venda
    {
        public int CodigoVenda { get; set; }
        public double ValorTotal { get; set; }
        public double ComissaoVenda { get; set; }
        public Boolean ComissaoPagou { get; set; }
        public DateTime DataVenda { get; set; }
        public int CodigoVendedor { get; set; }
        public int CodigoProduto { get; set; }
        public int CodigoDepartamento { get; set; }
        public int IdCliente { get; set; }
        public int QtdVendida { get; set; }
        private List<Venda> listaRelatorioVenda;
        public List<Venda> ListaRelatorioVenda
        {
            get { return listaRelatorioVenda; }
            set { listaRelatorioVenda = value; }
        }
        //lista de vendas ordenada por datas
        private List<Venda> listaVendaOrdenadaData;
        private List<Registro> listaRegistro;

        public List<Registro> ListaRegistro
        {
            get { return listaRegistro; }
            set { listaRegistro = value; }
        }

        public List<Venda> ListaVendaOrdenadaData
        {
            get { return listaVendaOrdenadaData; }
            set { listaVendaOrdenadaData = value; }
        }

        public Venda()
        {
            this.ListaVendaOrdenadaData = new List<Venda>();
            this.ListaRegistro = new List<Registro>();
            this.ListaRelatorioVenda = new List<Venda>();
        }

    }

}

