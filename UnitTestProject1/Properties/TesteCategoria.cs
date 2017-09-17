using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Categoria;

namespace UnitTestProject1
{
    [TestClass]
    public class TesteCategoria
    {
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        Categoria categoria = new Categoria();

        [TestMethod]
        public void TestarInsercao()
        {
            categoria.Nome = " fff";
            categoriaDAO.Inserir(categoria);

        }


        [TestMethod]
        public void TestarDelecao()
        {
            categoriaDAO.Deletar(5);

        }
        [TestMethod]
        public void TestarAtualizar()
        {
            categoria.Nome = "teste";
            categoria.CodigoCategoria = 8;
            categoriaDAO.Atualizar(categoria);
        }

        [TestMethod]
        public void ListarTodos()
        {
            categoriaDAO.ListarTodos();
        }
        
        [TestMethod]
        public void ObterPorId()
        {
            categoriaDAO.Obter(1);
        }

    }
}
