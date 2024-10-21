using AutoMapper;
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

    }
}
