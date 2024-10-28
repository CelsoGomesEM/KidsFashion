using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsFashion.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IMapper _mapper;

        public EstoqueController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var servicoProduto = new ServicoProduto();

            var produtos = await servicoProduto.ObterTodos();

            var vm = new EstoqueViewModel
            {
                ProdutoOptions = new SelectList(produtos, "Id", "Nome")
            };

            return View("Create", vm);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var servicoEstoque = new ServicoEstoque();

            var estoques = await servicoEstoque.ObterTodosCompletoRastreamento();

            var retorno = _mapper.Map<List<EstoqueViewModel>>(estoques);

            return View("Listagem", retorno);
        }


        [HttpPost]
        public async Task<IActionResult> Submit(EstoqueViewModel model)
        {
            var servicoEstoque = new ServicoEstoque();

            var estoque = new Estoque();
            estoque.Quantidade = model.Quantidade;
            estoque.DataEntrada = model.DataEntrada;
            estoque.Produto_Id = model.Produto.Id;

            await servicoEstoque.Adicionar(estoque);

            // Redirecione para a lista de categorias após o sucesso
            return RedirectToAction("Index");

        }

        // Processa o envio do formulário de edição
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var servicoEstoque = new ServicoEstoque();
            var servicoProduto = new ServicoProduto();

            var servicoMunicipio = new ServicoMunicipio();

            var estoqueEdit = servicoEstoque.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            var estoqueVm = _mapper.Map<EstoqueViewModel>(estoqueEdit);

            estoqueVm.Produto_Id = estoqueEdit.Produto_Id;

            estoqueVm.ProdutoOptions = servicoProduto.ObterTodos().Result.Select(te => new SelectListItem
            {
                Value = te.Id.ToString(),
                Text = te.Nome,
                Selected = te.Id == estoqueEdit.Produto_Id
            }).ToList();

            return View("Edit", estoqueVm);
        }

        // Processa o envio do formulário de edição
        [HttpPost]
        public async Task<IActionResult> SubmitEdit(EstoqueViewModel model)
        {
            var servicoEstoque = new ServicoEstoque();

            var estoqueEdit = servicoEstoque.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == model.Id).FirstOrDefault();
            estoqueEdit.Quantidade = model.Quantidade;
            estoqueEdit.DataEntrada = model.DataEntrada;

            await servicoEstoque.Atualizar(estoqueEdit);

            return RedirectToAction("Index");
        }

        // Ação para excluir uma categoria
        [HttpPost]
        public async Task<IActionResult> Excluir(long id)
        {
            var servicoEstoque = new ServicoEstoque();

            var estoqueRemover = servicoEstoque.ObterTodosFiltro(c => c.Id == id).Result.FirstOrDefault();

            await servicoEstoque.Remover(estoqueRemover);

            return RedirectToAction("Index");
        }
    }
}
