using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Dominio
{
    public class Produto : EntidadeComId
    {
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public int Categoria_Id { get; set; } // FK para Endereco
        public Categoria Categoria { get; set; }
        public int Fornecedor_Id { get; set; } // FK para Endereco
        public Fornecedor Fornecedor { get; set; }
    }
}
