using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Repositorio;

namespace ProjetoFinal.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly CadastroFuncRepositorio _repo;
        public ConsultaController(CadastroFuncRepositorio repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Funcionario()
        {
            var funcionarios = _repo.ListarTodos();
            return View(funcionarios);
        }
    }
}
