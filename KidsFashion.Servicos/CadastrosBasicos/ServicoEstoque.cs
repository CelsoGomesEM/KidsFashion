using KidsFashion.Dominio;
using KidsFashion.Persistencia;
using KidsFashion.Persistencia.Repositorios;
using KidsFashion.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.CadastrosBasicos
{
    public class ServicoEstoque : ServicoAbstratoDeCadastro<Estoque, RepositorioEstoque, PersistContext>
    {

    }
}
