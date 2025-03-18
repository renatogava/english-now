using EnglishNow.Services;
using EnglishNow.Web.Mappings;
using EnglishNow.Web.Models.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishNow.Web.Controllers
{
    [Route("turma")]
    [Authorize]
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turmaService;
        private readonly IProfessorService _professorService;

        public TurmaController(ITurmaService turmaService, IProfessorService professorService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
        }

        [Route("criar")]
        public IActionResult Criar()
        {
            var model = new CriarViewModel();

            model.Semestres = ObterListaSemestres();

            model.Professores = ObterListaProfessores();

            return View(model);
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //criar a turma
            var result = _turmaService.Criar(model.MapToCriarTurmaRequest());

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("listar")]
        public IActionResult Listar()
        {
            var turmas = _turmaService.Listar();

            var result = turmas.Select(c => c.MapToListarViewModel()).ToList();

            return View(result);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var turma = _turmaService.ObterPorId(id);

            if (turma == null)
            {
                return RedirectToAction("Listar");
            }

            var model = turma.MapToEditarViewModel();

            model.Semestres = ObterListaSemestres();

            model.Professores = ObterListaProfessores();

            return View(model);
        }

        [Route("editar/{id}")]
        [HttpPost]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToEditarTurmaRequest();

            var result = _turmaService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("excluir/{id}")]
        [HttpPost]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _turmaService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }

        private List<SelectListItem> ObterListaSemestres()
        {
            return new List<SelectListItem>
            {
                new SelectListItem("1° Semestre", "1"),
                new SelectListItem("2° Semestre", "2")
            };
        }

        private List<SelectListItem> ObterListaProfessores()
        {
            return _professorService.Listar()
                .Select(p => new SelectListItem(p.Nome, p.Id.ToString())).ToList();
        }
    }
}