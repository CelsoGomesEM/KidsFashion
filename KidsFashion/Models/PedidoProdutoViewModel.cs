using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsFashion.Models
{
    public class PedidoProdutoViewModel
    {
        public PedidoViewModel Pedido { get; set; }
        public int Pedido_Id { get; set; }

        public ProdutoViewModel Produto { get; set; }
        public int Produto_Id { get; set; }

        public int Quantidade { get; set; }
    }

}
