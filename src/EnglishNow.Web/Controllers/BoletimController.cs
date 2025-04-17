using EnglishNow.Services;
using EnglishNow.Web.Mappings;
using EnglishNow.Web.Models.Boletim;
using Microsoft.AspNetCore.Mvc;

namespace EnglishNow.Web.Controllers
{
    [Route("boletim")]
    public class BoletimController : Controller
    {
        private readonly IBoletimService _boletimService;

        public BoletimController(IBoletimService boletimService) 
        {
            _boletimService = boletimService;
        }

        [Route("editar/{alunoId}/{turmaId}")]
        public IActionResult Editar(int alunoId, int turmaId)
        {
            var result = _boletimService.ObterBoletimPorAlunoTurma(alunoId, turmaId);

            if (result == null)
            {
                RedirectToAction("Editar", "Turma", new { id =  turmaId });
            }

            var model = result!.MapToEditarViewModel();

            model.PodeEditarBoletim = (User.IsInRole("Administrador") || User.IsInRole("Professor"));

            return View(model);
        }

        [HttpPost]
        [Route("editar/{alunoId}/{turmaId}")]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToAtualizarBoletimRequest();

            var result = _boletimService.Atualizar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                return View(model); 
            }

            return RedirectToAction("Editar", "Turma", new { id = model.TurmaId });
        }
    }
}
