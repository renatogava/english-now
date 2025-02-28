using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    public class UsuarioController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
