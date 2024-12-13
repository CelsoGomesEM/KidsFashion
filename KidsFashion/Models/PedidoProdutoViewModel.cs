using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;

namespace KidsFashion.Models
{
    public class PedidoProdutoViewModel
    {
        public int Pedido_Id { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public int Produto_Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

}
