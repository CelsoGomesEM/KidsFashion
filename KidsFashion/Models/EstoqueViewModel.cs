using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KidsFashion.Models
{
    public class EstoqueViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Data Entrada")]
        public DateTime DataEntrada { get; set; }
        public int Quantidade { get; set; }
        public ProdutoViewModel Produto { get; set; }
        // Para Produto
        public int Produto_Id { get; set; }
        public IEnumerable<SelectListItem> ProdutoOptions { get; set; }
    }
}
