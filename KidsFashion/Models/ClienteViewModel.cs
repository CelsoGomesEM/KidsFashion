using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KidsFashion.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "Telefone")]
        public string Contato { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        // Propriedade para armazenar o município selecionado (Id do município)
        [Required]
        [Display(Name = "Município")]
        public int MunicipioId { get; set; }

        // Propriedade para armazenar a lista de municípios disponíveis
        public IEnumerable<SelectListItem> MunicipioOptions { get; set; }
    }
}
