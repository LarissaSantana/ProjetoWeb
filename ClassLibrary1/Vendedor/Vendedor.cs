using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Vendedor
{
    public class Vendedor
    {
        public int CodigoVendedor { get; set; }
        public String CpfVendedor { get; set; }
        public DateTime DataAdmissao { get; set; }
        public Boolean Desligado { get; set; }
        public Boolean Graduado { get; set; }
        public String Nome { get; set; }
        public String RG { get; set; }
        public int CodigoDepartamento { get; set; }

        private List<Departamento.Departamento> listaDepartamento;
        public List<Departamento.Departamento> ListaDepartamento
        {
            get { return listaDepartamento; }
            set { listaDepartamento = value; }
        }
        public Vendedor()
        {
            this.ListaDepartamento = new List<Departamento.Departamento>();
        }

    }
}
