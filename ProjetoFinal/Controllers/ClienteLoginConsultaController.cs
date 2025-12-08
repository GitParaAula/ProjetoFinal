using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Repositorio;

namespace ProjetoFinal.Controllers
{
    public class ClienteLoginConsultaController : Controller
    {
        private readonly LoginClienteRepositorio _repo;

        public ClienteLoginConsultaController(LoginClienteRepositorio repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult LoginCliente()
        {
            return View("~/Views/LoginCliente/LoginCliente.cshtml");
        }

        [HttpPost]
        public IActionResult LoginCliente(string usuario, string senha)
        {
            var cliente = _repo.BuscarCliente(usuario, senha);

            if (cliente == null)
            {
                TempData["Erro"] = "Nome ou senha incorretos.";
                return View("~/Views/LoginCliente/LoginCliente.cshtml");
            }

            return RedirectToAction("MostrarCliente", new { id = cliente.Codigo_Usuario });
        }

        public IActionResult MostrarCliente(int id)
        {
            // precisamos buscar o cliente de novo:
            var cliente = _repo.BuscarClientePorId(id);

            if (cliente == null)
            {
                TempData["Erro"] = "Cliente não encontrado.";
                return RedirectToAction("LoginCliente");
            }

            return View("~/Views/LoginCliente/MostrarCliente.cshtml", cliente);
        }
    }
}