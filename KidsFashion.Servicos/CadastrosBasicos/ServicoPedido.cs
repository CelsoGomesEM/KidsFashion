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
    public class ServicoPedido : ServicoAbstratoDeCadastro<Pedido, RepositorioPedido, PersistContext>
    {
        public async Task RemoverPedidoComAtualizacaoEstoqueAsync(long pedidoId)
        {
            using (var repositorio = new RepositorioPedido())
            {
                await repositorio.RemoverPedidoComAtualizacaoEstoqueAsync(pedidoId);
            }
        }

        public async Task RemoverItensPedidoComAtualizacaoEstoqueAsync(int pedidoId)
        {
            using (var repositorio = new RepositorioPedido())
            {
                await repositorio.RemoverItensPedidoComAtualizacaoEstoqueAsync(pedidoId);
            }
        }
    }
}
