using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using KidsFashion.Servicos.Relatorios.Categorias;
using KidsFashion.Servicos.Relatorios.Clientes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KidsFashion.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;

        public CategoriaController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var _servicoCategoria = new ServicoCategoria();

            var categorias = await _servicoCategoria.ObterTodos();

            var retorno = _mapper.Map<List<CategoriaViewModel>>(categorias);

            return View("Listagem", retorno);
        }

        public IActionResult Create()
        {
            var vm = new CategoriaViewModel();

            return View("Create", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(CategoriaViewModel model)
        {
            var _servicoCategoria = new ServicoCategoria();

            // Mapeie o CategoriaViewModel para o modelo de domínio se necessário
            var categoria = new Categoria
            {
                Descricao = model.Descricao
            };

            // Use o serviço para adicionar a categoria
            await _servicoCategoria.Adicionar(categoria);

            // Redirecione para a lista de categorias após o sucesso
            return RedirectToAction("Index");

        }

        // Processa o envio do formulário de edição
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var servicoCategoria = new ServicoCategoria();

            var categoriaEdit = servicoCategoria.ObterTodosFiltro(c => c.Id == id).Result.FirstOrDefault();

            var categoriaVm = _mapper.Map<CategoriaViewModel>(categoriaEdit);

            return View("Edit", categoriaVm);
        }

        // Processa o envio do formulário de edição
        [HttpPost]
        public async Task<IActionResult> SubmitEdit(CategoriaViewModel model)
        {
            var servicoCategoria = new ServicoCategoria();

            var categoria = _mapper.Map<Categoria>(model);
            
            await servicoCategoria.Atualizar(categoria);
            
            return RedirectToAction("Index");
        }

        // Ação para excluir uma categoria
        [HttpPost]
        public async Task<IActionResult> Excluir(long id)
        {
            var servicoCategoria = new ServicoCategoria();

            var categoriaRemover = servicoCategoria.ObterTodosFiltro(c => c.Id == id).Result.FirstOrDefault();

            await servicoCategoria.Remover(categoriaRemover);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Imprimir()
        {
            MemoryStream relatorioStream = new MemoryStream();

            var gerador = new RelatorioCategoriaGenerator();

            relatorioStream = await gerador.GerarRelatorioPDF();

            Response.Headers.Add("Content-Disposition", "attachment; filename=Categorias.pdf");
            return File(relatorioStream.ToArray(), "application/pdf");
        }
    }
}
