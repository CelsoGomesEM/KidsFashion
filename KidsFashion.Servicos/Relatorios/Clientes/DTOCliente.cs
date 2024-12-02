using KidsFashion.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.Relatorios.Clientes
{
    public class DTOCliente : IParametrosPDF
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Contato { get; set; }
    }
}
