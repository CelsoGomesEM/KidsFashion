using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KidsFashion.Models
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Data Pedido")]
        public DateTime DataPedido { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public int Cliente_Id { get; set; }
        public IEnumerable<SelectListItem> ClienteOptions { get; set; }
        public ProdutoViewModel Produto { get; set; }
        public int Produto_Id { get; set; }
        public int Quantidade { get; set; }
        public IEnumerable<SelectListItem> ProdutoOptions { get; set; }
        public List<PedidoProdutoViewModel> PedidoProdutos { get; set; } = new List<PedidoProdutoViewModel>();
        public decimal ValorTotal { get; set; }

    }
}
