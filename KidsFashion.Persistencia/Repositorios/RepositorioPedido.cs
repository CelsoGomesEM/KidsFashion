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
    public class RepositorioPedido : RepositorioAbstratoCadastro<Pedido, PersistContext>
    {
        protected override string Tabela => "Pedido";

        public override async Task<IEnumerable<Pedido>> ObterTodosCompletoRastreamento()
        {
            return await DbSet
                .Rastrear(true)
                    .Include(c => c.Cliente)
                .ToListAsync();
        }
    }
}
