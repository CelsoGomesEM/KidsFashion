using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsFashion.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly IMapper _mapper;

        public ProdutoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var servicoProduto = new ServicoProduto();

            var produtos = await servicoProduto.ObterTodos();

            var retorno = _mapper.Map<List<ProdutoViewModel>>(produtos);

            return View("Listagem", retorno);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var servicoFornecedor = new ServicoFornecedor();
            var servicoCategoria = new ServicoCategoria();

            var fornecedores = await servicoFornecedor.ObterTodos();
            var categorias = await servicoCategoria.ObterTodos();

            var vm = new ProdutoViewModel
            {
                FornecedorOptions = new SelectList(fornecedores, "Id", "Nome"),
                CategoriaOptions = new SelectList(categorias, "Id", "Descricao")
            };

            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ProdutoViewModel model)
        {
            var servicoProduto = new ServicoProduto();

            var produto = new Produto();
            produto.Descricao = model.Descricao;
            produto.Nome = model.Nome;
            produto.Quantidade = model.Quantidade;
            produto.Categoria_Id = model.Categoria_Id;
            produto.Fornecedor_Id = model.Fornecedor_Id;

            await servicoProduto.Adicionar(produto);

            // Redirecione para a lista de categorias após o sucesso
            return RedirectToAction("Index");

        }

        // Processa o envio do formulário de edição
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var servicoProduto = new ServicoProduto();
            var servicoFornecedor = new ServicoFornecedor();
            var servicoCategoria = new ServicoCategoria();

            var produtoEdit = servicoProduto.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            var produtoVm = _mapper.Map<ProdutoViewModel>(produtoEdit);

            produtoVm.Fornecedor_Id = produtoVm.Fornecedor_Id;

            produtoVm.FornecedorOptions = servicoFornecedor.ObterTodos().Result.Select(te => new SelectListItem
            {
                Value = te.Id.ToString(),
                Text = te.Nome,
                Selected = te.Id == produtoVm.Fornecedor_Id
            }).ToList();

            produtoVm.Categoria_Id = produtoVm.Categoria_Id;

            produtoVm.CategoriaOptions = servicoCategoria.ObterTodos().Result.Select(te => new SelectListItem
            {
                Value = te.Id.ToString(),
                Text = te.Descricao,
                Selected = te.Id == produtoVm.Categoria_Id
            }).ToList();

            return View("Edit", produtoVm);
        }

        // Processa o envio do formulário de edição
        [HttpPost]
        public async Task<IActionResult> SubmitEdit(ProdutoViewModel model)
        {
            var servicoProduto = new ServicoProduto();

            var produtoEdit = servicoProduto.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == model.Id).FirstOrDefault();

            var produto = _mapper.Map<Produto>(model);

            produtoEdit.Descricao = produto.Descricao;
            produtoEdit.Nome = produto.Nome;
            produtoEdit.Quantidade = produto.Quantidade;
            produtoEdit.Categoria_Id = produto.Categoria_Id;
            produtoEdit.Fornecedor_Id = produto.Fornecedor_Id;

            await servicoProduto.Atualizar(produtoEdit);

            return RedirectToAction("Index");
        }

        // Ação para excluir uma categoria
        [HttpPost]
        public async Task<IActionResult> Excluir(long id)
        {
            var servicoProduto = new ServicoProduto();

            var produto = servicoProduto.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            await servicoProduto.Remover(produto);

            return RedirectToAction("Index");
        }

    }
}
