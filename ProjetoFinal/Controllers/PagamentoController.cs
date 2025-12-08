using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Repositorio;

namespace ProjetoFinal.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly PagamentoRepositorio _repositorio;

        public PagamentoController(PagamentoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View(); // Abre a view para cadastro
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Cadastrar(Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                _repositorio.CadastrarPagamento(pagamento);

                // Redireciona para a view de confirmação
                return View("~/Views/ConfirmarPagamento/ConfirmarPagamento.cshtml");
            }

            return View(pagamento);
        }
    }
}