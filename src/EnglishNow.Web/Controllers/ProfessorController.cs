using Microsoft.AspNetCore.Mvc;
using EnglishNow.Web.Models.Professor;
using Microsoft.AspNetCore.Authorization;
using EnglishNow.Services;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Web.Mappings;

namespace EnglishNow.Web.Controllers
{
    [Route("professor")]
    [Authorize]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService) 
        {
            _professorService = professorService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //criar o professor
            var result = _professorService.Criar(model.MapToCriarProfessorRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
