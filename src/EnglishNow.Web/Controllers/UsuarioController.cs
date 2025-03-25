using EnglishNow.Services;
using EnglishNow.Web.Models.Usuario;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnglishNow.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _usuarioService.ValidarLogin(model.Usuario!, model.Senha!);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, result.Usuario!.Login),
                new (ClaimTypes.Role, result.Usuario!.Papel.ToString()),
                new ("Id", result.Usuario!.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = model.LembrarMe
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Usuario");
        }
    }
}
