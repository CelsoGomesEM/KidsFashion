using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
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

            // Adiciona o produto temporário à lista de produtos
            if (model.Produto_Id > 0 && model.Quantidade > 0)
            {
                var produto = servicoProduto.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == model.Produto_Id).FirstOrDefault();

                var produtovm = _mapper.Map<ProdutoViewModel>(produto);

                var pedidoprodutovm = new PedidoProdutoViewModel
                {
                    Produto_Id = model.Produto_Id,
                    Produto = produtovm,
                    Quantidade = model.Quantidade
                };
                model.PedidoProdutos.Add(pedidoprodutovm);
            }

            // Recarrega opções de cliente e produto
            model.ClienteOptions = new SelectList(clientes, "Id", "Nome");
            model.ProdutoOptions = new SelectList(produtos, "Id", "Nome");

            // Redirecione para a lista de categorias após o sucesso
            return View("Create", model);

        }
    }
}
