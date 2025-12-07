using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Repositorio;

namespace ProjetoFinal.Controllers
{
    public class ConsultaClienteController : Controller
    {
        private readonly CadastroClienteRepositorio _repo;

        public ConsultaClienteController(CadastroClienteRepositorio repo)
        {
            _repo = repo;
        }

        public IActionResult ConsultaCliente()
        {
            var clientes = _repo.ListarClientes();
            return View(clientes);
        }
    }
}
