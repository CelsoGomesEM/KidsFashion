using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KidsFashion.Models
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public CategoriaViewModel Categoria { get; set; }

        // Para Fornecedor
        public int Fornecedor_Id { get; set; }
        public IEnumerable<SelectListItem> FornecedorOptions { get; set; }

        // Para Categoria
        public int Categoria_Id { get; set; }
        public IEnumerable<SelectListItem> CategoriaOptions { get; set; }

    }
}
