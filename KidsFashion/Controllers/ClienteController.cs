using AutoMapper;
using KidsFashion.Dominio;
using KidsFashion.Models;
using KidsFashion.Servicos.CadastrosBasicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsFashion.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;

        public ClienteController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var servicoMunicipio = new ServicoMunicipio();

            var municipios = await servicoMunicipio.ObterTodos();

            var vm = new ClienteViewModel
            {
                MunicipioOptions = new SelectList(municipios, "Id", "Nome")
            };

            return View("Create", vm);
        }

        public async Task<IActionResult> IndexAsync()
        {
            var servicoCliente = new ServicoCliente();

            var clientes = await servicoCliente.ObterTodos();

            var retorno = _mapper.Map<List<ClienteViewModel>>(clientes);

            return View("Listagem", retorno);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ClienteViewModel model)
        {
            var servicoCliente = new ServicoCliente();

            var cliente = new Cliente();
            cliente.Nome = model.Nome;
            cliente.CPF = model.CPF;
            cliente.Contato = model.Contato;
            cliente.Endereco = new Endereco();
            cliente.Endereco.Logradouro = model.Endereco.Logradouro;
            cliente.Endereco.Numero = model.Endereco.Numero;
            cliente.Endereco.Complemento = model.Endereco.Complemento;
            cliente.Endereco.Bairro = model.Endereco.Bairro;
            cliente.Endereco.Municipio = null;
            cliente.Endereco.Municipio_Id = model.Endereco.Municipio.Id;

            await servicoCliente.Adicionar(cliente);

            // Redirecione para a lista de categorias após o sucesso
            return RedirectToAction("Index");

        }

        // Processa o envio do formulário de edição
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var servicoCliente = new ServicoCliente();
            var servicoMunicipio = new ServicoMunicipio();

            var clienteEdit = servicoCliente.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            var clienteVm = _mapper.Map<ClienteViewModel>(clienteEdit);

            clienteVm.MunicipioId = clienteEdit.Endereco_Id;

            clienteVm.MunicipioOptions = servicoMunicipio.ObterTodos().Result.Select(te => new SelectListItem
            {
                Value = te.Id.ToString(),
                Text = te.Nome,
                Selected = te.Id == clienteEdit.Endereco_Id
            }).ToList();

            return View("Edit", clienteVm);
        }

        // Processa o envio do formulário de edição
        [HttpPost]
        public async Task<IActionResult> SubmitEdit(ClienteViewModel model)
        {
            var servicoCliente = new ServicoCliente();

            var clientEdit = servicoCliente.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == model.Id).FirstOrDefault();

            var cliente = _mapper.Map<Cliente>(model);

            clientEdit.Nome = cliente.Nome;
            clientEdit.CPF = cliente.CPF;
            clientEdit.Contato = cliente.Contato;
            clientEdit.Endereco.Logradouro = cliente.Endereco.Logradouro;
            clientEdit.Endereco.Numero = cliente.Endereco.Numero;
            clientEdit.Endereco.Complemento = cliente.Endereco.Complemento;
            clientEdit.Endereco.Bairro = cliente.Endereco.Bairro;
            clientEdit.Endereco.Municipio = null;
            clientEdit.Endereco.Municipio_Id = cliente.Endereco.Municipio.Id.Value;

            await servicoCliente.Atualizar(clientEdit);

            return RedirectToAction("Index");
        }

        // Ação para excluir uma categoria
        [HttpPost]
        public async Task<IActionResult> Excluir(long id)
        {
            var servicoCliente = new ServicoCliente();

            var cliente = servicoCliente.ObterTodosCompletoRastreamento().Result.Where(c => c.Id == id).FirstOrDefault();

            await servicoCliente.RemoverEnderecoPorClienteId(cliente.Id.Value);

            return RedirectToAction("Index");
        }

    }
}
