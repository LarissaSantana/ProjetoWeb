using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Venda;
using ClassLibrary1.Registros;
using ClassLibrary1.Produto;

namespace UnitTestProject1
{
    [TestClass]
    public class TesteVenda
    {
        Venda Venda = new Venda();
        VendaDAO VendaDAO = new VendaDAO();


        Produto produto = new Produto();
        ProdutoDAO pDao = new ProdutoDAO();

        [TestMethod]
        public void AtualizarVenda()
        {
            Venda.CodigoVenda = 4;
            Venda.ValorTotal = 30;
            Venda.ComissaoVenda = 102;
            Venda.ComissaoPagou = true;
            Venda.DataVenda = new DateTime(2008, 2, 1);
            Venda.CodigoVendedor = 1;
            Venda.CodigoProduto = 4;
            Venda.CodigoDepartamento = 2;
            Venda.IdCliente = 1;

            VendaDAO.Atualizar(Venda);
        }

        [TestMethod]
        public void DeletarVenda()
        {
            VendaDAO.Deletar(1059);
        }

        [TestMethod]
        public void InserirVenda()
        {
            Venda = new Venda();
            //Venda.ValorTotal = 0;
            // Venda.ComissaoVenda = 30;
            Venda.ComissaoPagou = false;
            Venda.DataVenda = new DateTime(2011, 02, 05);
            Venda.CodigoVendedor = 5;
            Venda.CodigoProduto = 25;
            // Venda.CodigoDepartamento = 2;
            Venda.IdCliente = 1;
            Venda.QtdVendida = 500;

            produto = pDao.Obter(Venda.CodigoProduto);

            Registro registro = new Registro();
            registro.CodigoProduto = produto.CodigoProduto;
            registro.ValorProduto = produto.PrecoProduto;
            registro.QuantidadeVendida = Venda.QtdVendida;
            registro.CodigoVendedor = Venda.CodigoVendedor;
            registro.Produto = produto;

            Venda.ListaRegistro.Add(registro);

            VendaDAO.Inserir(Venda);
        }

        [TestMethod]
        public void ListarVendas()
        {
            VendaDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterVenda()
        {
            VendaDAO.Obter(4);
        }

        [TestMethod]
        public void ListaOrdenadaData()
        {
            VendaDAO.ObterVendaPorData(5, new DateTime(2011, 02, 05), new DateTime(2004, 08, 10));
        }
        [TestMethod]
        public void ListarVendaPorData()
        {
            VendaDAO.RelatorioVendas();
        }

    }
}
