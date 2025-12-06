using Microsoft.AspNetCore.Mvc;

namespace ProjetoFinal.Controllers
{
    public class EscolhaLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EscolhaLogin()
        {
            return View();
        }
    }
}
