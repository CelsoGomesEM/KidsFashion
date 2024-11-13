using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Dominio
{
    public class PedidoProduto : EntidadeComId
    {
        public int Pedido_Id { get; set; }
        public Pedido Pedido { get; set; }
        public int Produto_Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; } // Quantidade do produto no pedido
    }
}
