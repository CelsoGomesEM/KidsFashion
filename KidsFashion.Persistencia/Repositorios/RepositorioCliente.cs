using KidsFashion.Dominio;
using KidsFashion.Persistencia.Extensoes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsFashion.Persistencia.Repositorios
{
    public class RepositorioCliente : RepositorioAbstratoCadastro<Cliente, PersistContext>
    {
        protected override string Tabela => "Cliente";

        public override async Task<IEnumerable<Cliente>> ObterTodosCompletoRastreamento()
        {
            return await DbSet
                .Rastrear(true)
                .Include(m => m.Endereco)
                 .ThenInclude(m => m.Municipio)
                .ToListAsync();
        }

        public async Task RemoverEnderecoPorClienteId(long clienteId)
        {
            var sql = "DELETE FROM Endereco WHERE Id = (SELECT Endereco_Id FROM Cliente WHERE Id = @ClienteId);";

            var parameters = new[]
            {
                new SqlParameter("@ClienteId", clienteId)
            };

            await Contexto.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
