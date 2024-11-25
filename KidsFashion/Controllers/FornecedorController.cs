using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace KidsFashion.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IMapper _mapper;

        public FornecedorController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var servicoMunicipio = new ServicoMunicipio();

            var municipios = await servicoMunicipio.ObterTodos();

            // Ordena os municípios por UF e Nome
            var municipiosOrdenados = municipios
                .OrderBy(m => m.UF)
                .ThenBy(m => m.Nome)
                .Select(m => new
                {
                    Id = m.Id,
                    Nome = $"{m.Nome} - {m.UF}"
                })
                .ToList();

            var vm = new FornecedorViewModel
            {
                MunicipioOptions = new SelectList(municipiosOrdenados, "Id", "Nome")
            };

            return View("Create", vm);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var servicoFornecedor = new ServicoFornecedor();

            var fornecedores = await servicoFornecedor.ObterTodos();

            var retorno = _mapper.Map<List<FornecedorViewModel>>(fornecedores);

            return View("Listagem", retorno);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(FornecedorViewModel model)
        {
            var servicoFornecedor = new ServicoFornecedor();

            var fornecedor = new Fornecedor();
            fornecedor.Nome = model.Nome;
            fornecedor.CPF_CNPJ = model.CPF_CNPJ;
            fornecedor.Contato = model.Contato;
            fornecedor.Endereco = new Endereco();
            fornecedor.Endereco.Logradouro = model.Endereco.Logradouro;
            fornecedor.Endereco.Numero = model.Endereco.Numero;
            fornecedor.Endereco.Complemento = model.Endereco.Complemento;
            fornecedor.Endereco.Bairro = model.Endereco.Bairro;
            fornecedor.Endereco.Municipio = null;
            fornecedor.Endereco.Municipio_Id = model.Endereco.Municipio.Id;

            await servicoFornecedor.Adicionar(fornecedor);

            // Redirecione para a lista de categorias após o sucesso
            return RedirectToAction("Index");

        }

        // Processa o envio do formulário de edição
        [HttpPost]
        public async Task<IActionResult> SubmitEdit(FornecedorViewModel model)
        {
            var servicoFornecedor = new ServicoFornecedor();

            var fornecedorEdit = servicoFornecedor.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == model.Id).FirstOrDefault();

            var fornecedor = _mapper.Map<Fornecedor>(model);

            fornecedorEdit.Nome = fornecedor.Nome;
            fornecedorEdit.CPF_CNPJ = fornecedor.CPF_CNPJ;
            fornecedorEdit.Contato = fornecedor.Contato;
            fornecedorEdit.Endereco.Logradouro = fornecedor.Endereco.Logradouro;
            fornecedorEdit.Endereco.Numero = fornecedor.Endereco.Numero;
            fornecedorEdit.Endereco.Complemento = fornecedor.Endereco.Complemento;
            fornecedorEdit.Endereco.Bairro = fornecedor.Endereco.Bairro;
            fornecedorEdit.Endereco.Municipio_Id = fornecedor.Endereco.Municipio_Id;

            await servicoFornecedor.Atualizar(fornecedorEdit);

            return RedirectToAction("Index");
        }

        // Ação para excluir uma categoria
        [HttpPost]
        public async Task<IActionResult> Excluir(long id)
        {
            var servicoFornecedor = new ServicoFornecedor();

            var fornecedor = servicoFornecedor.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            await servicoFornecedor.RemoverEnderecoPorFornecedorId(fornecedor.Id.Value);

            return RedirectToAction("Index");
        }

        // Processa o envio do formulário de edição
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var servicoFornecedor = new ServicoFornecedor();
            var servicoMunicipio = new ServicoMunicipio();

            var fornecedorEdit = servicoFornecedor.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            var fornecedorVm = _mapper.Map<FornecedorViewModel>(fornecedorEdit);

            fornecedorVm.MunicipioId = fornecedorEdit.Endereco.Municipio.Id.Value;

            var municipios = await servicoMunicipio.ObterTodos();

            // Ordena os municípios por UF e Nome
            var municipiosOrdenados = municipios
                .OrderBy(m => m.UF)
                .ThenBy(m => m.Nome)
                .Select(m => new
                {
                    Id = m.Id,
                    Nome = $"{m.Nome} - {m.UF}"
                })
                .ToList();

            fornecedorVm.MunicipioOptions = municipiosOrdenados.Select(te => new SelectListItem
            {
                Value = te.Id.ToString(),
                Text = te.Nome,
                Selected = te.Id == fornecedorEdit.Endereco.Municipio.Id.Value
            }).ToList();

            return View("Edit", fornecedorVm);
        }
    }
}
