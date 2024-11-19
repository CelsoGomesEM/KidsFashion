using KidsFashion.Dominio;
using KidsFashion.Persistencia.Extensoes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        public async Task RemoverPedidoComAtualizacaoEstoqueAsync(long pedidoId)
        {
            using (var transaction = await Contexto.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Buscar os itens do pedido
                    var buscarItensSql = @" SELECT pp.Produto_Id, pp.Quantidade 
                                            FROM PedidoProduto pp
                                            WHERE pp.Pedido_Id = @PedidoId";

                    var itensPedido = new List<(int ProdutoId, int Quantidade)>();

                    using (var command = Contexto.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = buscarItensSql;
                        command.Transaction = transaction.GetDbTransaction();
                        command.Parameters.Add(new SqlParameter("@PedidoId", pedidoId));

                        await Contexto.Database.OpenConnectionAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                itensPedido.Add((
                                    ProdutoId: reader.GetInt32(0),
                                    Quantidade: reader.GetInt32(1)
                                ));
                            }
                        }
                    }

                    // 2. Remover os itens da tabela PedidoProduto
                    var removerPedidoProdutoSql = @"DELETE FROM PedidoProduto 
                                                    WHERE Pedido_Id = @PedidoId";

                    await Contexto.Database.ExecuteSqlRawAsync(removerPedidoProdutoSql,
                        new SqlParameter("@PedidoId", pedidoId));

                    // 3. Remover o pedido da tabela Pedido
                    var removerPedidoSql = @"DELETE FROM Pedido 
                                             WHERE Id = @PedidoId";

                    await Contexto.Database.ExecuteSqlRawAsync(removerPedidoSql,
                        new SqlParameter("@PedidoId", pedidoId));

                    // 4. Atualizar o estoque
                    foreach (var (produtoId, quantidade) in itensPedido)
                    {
                        var atualizarEstoqueSql = @"UPDATE Estoque
                                                    SET Quantidade = Quantidade + @Quantidade
                                                    WHERE Produto_Id = @ProdutoId";

                        await Contexto.Database.ExecuteSqlRawAsync(atualizarEstoqueSql,
                            new SqlParameter("@Quantidade", quantidade),
                            new SqlParameter("@ProdutoId", produtoId));
                    }

                    // Confirmar a transação
                    await transaction.CommitAsync();
                }
                catch
                {
                    // Reverter a transação em caso de erro
                    await transaction.RollbackAsync();
                    throw;
                }
                finally
                {
                    await Contexto.Database.CloseConnectionAsync();
                }
            }
        }

    }
}
