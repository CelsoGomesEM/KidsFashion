using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Dominio
{
    public class Estoque : EntidadeComId
    {
        public DateTime DataEntrada { get; set; }
        public int Quantidade { get; set; }
        public int Produto_Id { get; set; } // FK para Produto
        public Produto Produto { get; set; }
    }
}
