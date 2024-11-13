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

            var pedidos = await servicoPedido.ObterTodos();

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

            if (Request.Form["ActionType"] == "add")
            {
                
                if (model.PedidoProdutos.Any(p => p.Produto_Id == model.Produto_Id))
                {
                    ModelState.AddModelError("", $"Este produto já foi adicionado, caso necessite editar a quantidade remova o item e adicione novamente.");
                }
                else if (!ExisteProdutoEmEstoque(model.Produto_Id))
                {
                    ModelState.AddModelError("", $"Todas as unidades existentes deste produto já foram vendidas, ou produto não existe em estoque.");
                }
                else
                {
                    // Add the selected product to PedidoProdutos if no duplicates are found
                    if (model.Produto_Id > 0 && model.Quantidade > 0)
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

                TempData.Put("PedidoProdutos", model.PedidoProdutos);

                model.ClienteOptions = new SelectList(clientes, "Id", "Nome");
                model.ProdutoOptions = new SelectList(produtos, "Id", "Nome");

                return View("Create", model);
            }
            else
            {
                TempData.Remove("PedidoProdutos");
                return RedirectToAction("Index");
            }
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
