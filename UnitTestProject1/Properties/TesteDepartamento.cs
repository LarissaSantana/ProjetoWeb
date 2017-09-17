using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Departamento;

namespace UnitTestProject1
{

    [TestClass]
    public class TesteDepartamento
    {
        Departamento departamento = new Departamento();
        DepartamentoDAO departamentoDAO = new DepartamentoDAO();


        [TestMethod]
        public void AtualizarDepartamento()
        {
            departamento.Nome = "Frios";
            departamento.Sigla = "FR";
            departamento.CodigoDepartamento = 2;
            departamento.Comissao = 20;
            departamento.CodigoVendedor = 1;
            departamentoDAO.Atualizar(departamento);
        }

        [TestMethod]
        public void DeletarDepartamento()
        {
            departamentoDAO.Deletar(1);
        }

        [TestMethod]
        public void InserirDepartamento()
        {
            departamento.Nome = "Papelaria";
            departamento.Sigla = "pp";
            departamento.Comissao = 10;
            departamento.CodigoVendedor = 1;
            departamentoDAO.Inserir(departamento);
        }

        [TestMethod]
        public void ListarDepartamentos()
        {
            departamentoDAO.ListarTodos();
        }

        [TestMethod]
        public void ObterDepartamento()
        {
            departamentoDAO.Obter(1);
        }

        [TestMethod]
        public void ListarPorSigla()
        {
            departamentoDAO.ListarPorSigla("FR");
        }

        [TestMethod]
        public void jaTemChefe()
        {
            departamentoDAO.jaTemChefe(10);
        }

        [TestMethod]
        public void removerChefe()
        {
            departamentoDAO.removerChefeDepartamento(2);
        }
        [TestMethod]
        public void inserirChefeDepartamento()
        {
            departamentoDAO.InserirChefeDepartamento(11, 5, new DateTime(2017, 10, 10));
        }
    }
}
