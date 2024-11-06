using KidsFashion.Models;
using Microsoft.AspNetCore.Mvc;

namespace KidsFashion.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            var vm = new LoginViewModel();

            return View("Login", vm);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simulação de autenticação (substitua por lógica real)
            if (username == "admin" && password == "admin")
            {
                // Autenticação bem-sucedida - redireciona para a página inicial
                return RedirectToAction("Index", "Home");
            }

            // Exibir mensagem de erro em caso de falha de autenticação
            ViewBag.Error = "Usuário ou senha incorretos.";
            return View();
        }

        public IActionResult Logout()
        {
            // Lógica para logout (opcional)
            var vm = new LoginViewModel();

            return View("Login", vm);
        }
    }
}
