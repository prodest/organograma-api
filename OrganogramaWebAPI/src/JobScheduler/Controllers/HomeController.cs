using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobScheduler.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entrar()
        {
            return Redirect("/restrito");
        }

        public async Task<IActionResult> Sair()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");

            return RedirectToAction("Index");
        }
    }
}
