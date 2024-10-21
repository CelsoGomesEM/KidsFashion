using KidsFashion.Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Persistencia.Repositorios 
{
    public class RepositorioProduto : RepositorioAbstratoCadastro<Produto, PersistContext>
    {
        protected override string Tabela => "Produto";

        public async Task RemoverEnderecoPorFornecedorId(long fornecedorId)
        {
            var sql = "DELETE FROM Endereco WHERE Id = (SELECT Endereco_Id FROM Fornecedor WHERE Id = @FornecedorId);";

            var parameters = new[]
            {
                new SqlParameter("@FornecedorId", fornecedorId)
            };

            await Contexto.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
