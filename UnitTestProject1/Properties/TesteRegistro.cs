using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Registros;

namespace UnitTestProject1.Properties
{
    [TestClass]
    public class TesteRegistro
    {
        Registro registro = new Registro();
        RegistroDAO registroDAO = new RegistroDAO();


        [TestMethod]
        public void AtualizarRegistro()
        {
            registro.CodigoVenda = 7;
            registro.CodigoProduto = 4;
            registro.ValorProduto = 10;
            registro.QuantidadeVendida = 20;
            registroDAO.Atualizar(registro);
        }

        [TestMethod]
        public void DeletarRegistro()
        {
            registroDAO.Deletar(2);
        }

        [TestMethod]
        public void ListarRegistros()
        {
            registroDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterRegistro()
        {
            registroDAO.Obter(1);
        }

        [TestMethod]
        public void PrecoTot()
        {
            registroDAO.precoVendaTot();
        }
    }
}
