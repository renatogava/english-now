using EnglishNow.Web.Models.Erro;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    public class ErroController : Controller
    {
        public IActionResult Index()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var model = new ErroViewModel
            {
                MensagemErro = exceptionHandlerFeature == null ? "Erro inesperado" : exceptionHandlerFeature.Error.Message
            };

            return View(model);
        }
    }
}
