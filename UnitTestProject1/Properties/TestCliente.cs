using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Cliente;

namespace UnitTestProject1.Properties
{
    /// <summary>
    /// Descrição resumida para UnitTest1
    /// </summary>
    [TestClass]
    public class TestCliente
    {
        ClienteDAO clienteDAO = new ClienteDAO();
        Cliente cliente = new Cliente();

        [TestMethod]
        public void TestarInsercao()
        {
            cliente.Nome = "fff";
            cliente.Cpf = "123";
            clienteDAO.Inserir(cliente);

        }


        [TestMethod]
        public void TestarDelecao()
        {
            clienteDAO.Deletar(5);

        }
        [TestMethod]
        public void TestarAtualizar()
        {
            cliente.Nome = "teste";
            cliente.Cpf = "8";
            clienteDAO.Atualizar(cliente);
        }

        [TestMethod]
        public void ListarTodos()
        {
            clienteDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterClientePorId()
        {
            clienteDAO.Obter(1);
        }

    }
}
