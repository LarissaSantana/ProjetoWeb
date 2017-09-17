using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Vendedor;

namespace UnitTestProject1.Properties
{
    [TestClass]
    public class TesteVendedor
    {
        Vendedor Vendedor = new Vendedor();
        VendedorDAO VendedorDAO = new VendedorDAO();

        //
        [TestMethod]
        public void AtualizarVendedor()
        {
            Vendedor.CodigoVendedor = 1;
            Vendedor.CpfVendedor = "0005228";
            Vendedor.DataAdmissao = new DateTime(2009, 02, 01);
            Vendedor.Desligado = false;
            Vendedor.Graduado = false;
            Vendedor.Nome = "Lucas";
            Vendedor.RG = "44445";
            Vendedor.CodigoDepartamento = 2;
        }

        [TestMethod]
        public void DeletarVendedor()
        {
            VendedorDAO.Deletar(4);
        }

        [TestMethod]
        public void InserirVendedor()
        {

            Vendedor.CpfVendedor = "000558";
            Vendedor.DataAdmissao = new DateTime(2009,02,01);
            Vendedor.Desligado = false;
            Vendedor.Graduado = false;
            Vendedor.Nome = "José";
            Vendedor.RG = "44445";
            Vendedor.CodigoDepartamento = 2;
            
            VendedorDAO.Inserir(Vendedor);
        }

        [TestMethod]
        public void ListarVendedores()
        {
            VendedorDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterVendedor()
        {
            VendedorDAO.Obter(1);
        }
        // verificar se o vendedor é chefe de um departamento e a partir de qual data ele assumiu essa chefia
        [TestMethod]
        public void verificarVendedorChefe()
        {
            VendedorDAO.VerificarVendedorChefe(1);
        }
    }
}
