using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Produto;

namespace UnitTestProject1.Properties
{
    /// <summary>
    /// Descrição resumida para TestProduto
    /// </summary>
    [TestClass]
    public class TestProduto
    {
        ProdutoDAO ProdutoDAO = new ProdutoDAO();
        Produto Produto = new Produto();

        [TestMethod]
        public void TestarInsercao()
        {
            Produto.EstoqueProduto = 0;
            Produto.PrecoProduto = 500;
            Produto.NomeProduto = "Borracha";
            Produto.CodigoCategoria = 1;
            Produto.CodigoDepartamento = 2;

            ProdutoDAO.Inserir(Produto);

        }


        [TestMethod]
        public void TestarDelecao()
        {
            ProdutoDAO.Deletar(0);
        }
        [TestMethod]
        public void TestarAtualizar()
        {
            Produto.CodigoProduto = 25;
            Produto.EstoqueProduto = 1000;
            Produto.CodigoDepartamento = 2;
            Produto.CodigoCategoria = 1;
            Produto.PrecoProduto = 1;
            Produto.NomeProduto = "Borracha";
           
            ProdutoDAO.Atualizar(Produto);
        }

        [TestMethod]
        public void ListarTodosProdutos()
        {
            ProdutoDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterProdutoPorId()
        {
            ProdutoDAO.Obter(4);
        }

        [TestMethod]
        public void testarSeTemEstoque()
        {
            ProdutoDAO.temEstoque(26);
        }
        [TestMethod]
        public void testarRetornoEstoque()
        {
            ProdutoDAO.qtdEstoque(23);
        }
    }
}
