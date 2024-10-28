using KidsFashion.Dominio;
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
    }
}
