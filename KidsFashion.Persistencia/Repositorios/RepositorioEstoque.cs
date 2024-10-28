using KidsFashion.Dominio;
using KidsFashion.Persistencia.Extensoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Persistencia.Repositorios
{
    public class RepositorioEstoque : RepositorioAbstratoCadastro<Estoque, PersistContext>
    {
        protected override string Tabela => "Estoque";

        public override async Task<IEnumerable<Estoque>> ObterTodosCompletoRastreamento()
        {
            return await DbSet
               .Rastrear(true)
               .Include(m => m.Produto)
               .ToListAsync();
        }
    }
}
