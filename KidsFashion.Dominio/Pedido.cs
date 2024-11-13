using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Dominio
{
    public class Pedido : EntidadeComId
    {
        public int Cliente_Id { get; set; } // FK para Cliente
        public Cliente Cliente { get; set; }
        public DateTime DataPedido { get; set; }
        public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }

}
