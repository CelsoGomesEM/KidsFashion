using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using KidsFashion.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsFashion.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IMapper _mapper;

        public PedidoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var servicoPedido = new ServicoPedido();

            var pedidos = await servicoPedido.ObterTodosCompletoRastreamento();

            var retorno = _mapper.Map<List<PedidoViewModel>>(pedidos);

            return View("Listagem", retorno);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var servicoCliente = new ServicoCliente();
            var servicoProduto = new ServicoProduto();

            var clientes = await servicoCliente.ObterTodos();

            var produtos = await servicoProduto.ObterTodos();

            var vm = new PedidoViewModel()
            {
                ClienteOptions = new SelectList(clientes, "Id", "Nome"),
                ProdutoOptions = new SelectList(produtos, "Id", "Nome")
            };

            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(PedidoViewModel model)
        {
            var servicoProduto = new ServicoProduto();
            var servicoCliente = new ServicoCliente();

            var clientes = await servicoCliente.ObterTodos();
            var produtos = await servicoProduto.ObterTodos();

            model.PedidoProdutos = TempData.ContainsKey("PedidoProdutos")
                ? TempData.Get<List<PedidoProdutoViewModel>>("PedidoProdutos")
                : new List<PedidoProdutoViewModel>();

            var actionType = Request.Form["ActionType"];
            
            if (actionType == "add")
            {
                // Lógica de adicionar produto
                if (model.PedidoProdutos.Any(p => p.Produto_Id == model.Produto_Id))
                {
                    ModelState.AddModelError("", "Este produto já foi adicionado. Remova-o para editar.");
                }
                else if (!ExisteProdutoEmEstoque(model.Produto_Id))
                {
                    ModelState.AddModelError("", "Produto sem estoque disponível.");
                }
                else if (model.Produto_Id > 0 && model.Quantidade > 0)
                {
                    var produto = servicoProduto.ObterTodosCompletoRastreamento().Result
                        .FirstOrDefault(c => c.Id == model.Produto_Id);

                    var produtovm = _mapper.Map<ProdutoViewModel>(produto);

                    var pedidoprodutovm = new PedidoProdutoViewModel
                    {
                        Produto_Id = model.Produto_Id,
                        Produto = produtovm,
                        Quantidade = model.Quantidade
                    };

                    model.PedidoProdutos.Add(pedidoprodutovm);
                }
            }
            else if (actionType == "remove")
            {
                // Lógica de remover produto
                var produtoARemover = model.PedidoProdutos.FirstOrDefault(p => p.Produto_Id == model.Produto_Id);
               
                if (produtoARemover != null)
                {
                    model.PedidoProdutos.Remove(produtoARemover);
                }
            }
            else if (actionType == "save")
            {
                var servicoPedido = new ServicoPedido();
                var pedido = new Pedido();
                pedido.Cliente_Id = model.Cliente_Id;
                pedido.DataPedido = model.DataPedido;

                var idPedido = await servicoPedido.AdicionarRetornarID(pedido);

                var servicoPedidoProduto = new ServicoPedidoProduto();

                foreach (var item in model.PedidoProdutos)
                {
                    var pedidoItem = new PedidoProduto();
                    pedidoItem.Pedido_Id = idPedido;
                    pedidoItem.Produto_Id = item.Produto_Id;
                    pedidoItem.Quantidade = item.Quantidade;

                    await servicoPedidoProduto.Adicionar(pedidoItem);
                    AtualizarEstoqueAsync(pedidoItem.Produto_Id, pedidoItem.Quantidade);

                }

                // Lógica para salvar ou outra ação
                TempData.Remove("PedidoProdutos");

                return RedirectToAction("Index");
            }

            TempData.Put("PedidoProdutos", model.PedidoProdutos);

            model.ClienteOptions = new SelectList(clientes, "Id", "Nome");
            model.ProdutoOptions = new SelectList(produtos, "Id", "Nome");

            return View("Create", model);
        }

        private async Task AtualizarEstoqueAsync(int ProdutoId, int Quantidade)
        {
            //Atualizar Estoque
            var servicoEstoque = new ServicoEstoque();

            var produtoEmEstoque = servicoEstoque.ObterTodosFiltro(c => c.Produto_Id == ProdutoId).Result.FirstOrDefault();

            produtoEmEstoque.Quantidade -= Quantidade;

            await servicoEstoque.Atualizar(produtoEmEstoque);
        }

        private bool ExisteProdutoEmEstoque(int produto_Id)
        {
            var servicoEstoque = new ServicoEstoque();

            var estoqueProduto = servicoEstoque.ObterTodosFiltro(c => c.Produto_Id == produto_Id).Result.FirstOrDefault();

            if(estoqueProduto != null && estoqueProduto.Quantidade > 0)
                return true;

            return false;
        }
    }
}
