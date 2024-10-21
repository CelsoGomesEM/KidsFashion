using KidsFashion.Dominio;
using KidsFashion.Persistencia.Repositorios;
using KidsFashion.Persistencia;
using KidsFashion.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Servicos.CadastrosBasicos
{
    public class ServicoProduto : ServicoAbstratoDeCadastro<Produto, RepositorioProduto, PersistContext>
    {
        public async Task RemoverEnderecoPorFornecedorId(long fornecedorId)
        {
            using (var repositorio = new RepositorioProduto())
            {
                await repositorio.RemoverEnderecoPorFornecedorId(fornecedorId);
            }
        }
    }
}
