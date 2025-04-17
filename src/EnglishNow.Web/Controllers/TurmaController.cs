using EnglishNow.Services;
using EnglishNow.Services.Models.Aluno;
using EnglishNow.Services.Models.Turma;
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
        private readonly IAlunoService _alunoService;

        public TurmaController(
            ITurmaService turmaService, 
            IProfessorService professorService,
            IAlunoService alunoService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
            _alunoService = alunoService;
        }

        [Route("criar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Criar()
        {
            var model = new CriarViewModel();

            model.Semestres = ObterListaSemestres();

            model.Professores = ObterListaProfessores();

            return View(model);
        }

        [HttpPost]
        [Route("criar")]
        [Authorize(Roles = "Administrador")]
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
            IList<TurmaResult>? turmas = null;

            var usuarioId = Convert.ToInt32(User.FindFirst("Id")?.Value);

            AlunoResult? aluno = null;

            if (User.IsInRole("Administrador"))
            {
                turmas = _turmaService.Listar();
            }
            else if (User.IsInRole("Professor"))
            {
                turmas = _turmaService.ListarPorProfessor(usuarioId);
            }
            else if (User.IsInRole("Aluno"))
            {
                turmas = _turmaService.ListarPorAluno(usuarioId);

                aluno = _alunoService.ObterPorUsuarioId(usuarioId);
            }

            var result = new ListarViewModel
            {
                Turmas = turmas?.Select(c => c.MapToTurmaViewModel()).ToList(),
                ExibirBotaoInserir = User.IsInRole("Administrador"),
                ExibirBotaoEditar = User.IsInRole("Administrador") || User.IsInRole("Professor"),
                ExibirBotaoBoletim = User.IsInRole("Aluno"),
                AlunoId = aluno?.Id
            };

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

            model.AlunosTurma = _alunoService.ListarPorTurma(id)
                .Select(c => c.MapToAlunoTurmaViewModel())
                .ToList();

            model.Alunos = _alunoService.Listar()
                .Select(c => c.MapToAlunoTurmaViewModel())
                .ToList();

            model.Semestres = ObterListaSemestres();

            model.Professores = ObterListaProfessores();

            model.PodeEditarApagarTurma = User.IsInRole("Administrador");

            return View(model);
        }

        [Route("editar/{id}")]
        [HttpPost]
        [Authorize(Roles = "Administrador")]
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

        [HttpPost]
        [Route("associarAlunos")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AssociarAlunos(int turmaId)
        {
            foreach (var formItem in Request.Form)
            {
                if (formItem.Key.StartsWith("aluno_"))
                {
                    var alunoId = int.Parse(formItem.Key.Split("_")[1]);

                    _turmaService.AssociarAlunoTurma(alunoId, turmaId);
                }
            }

            return RedirectToAction("Editar", "Turma", new { id = turmaId });
        }

        [HttpPost]
        [Route("desassociarAluno")]
        [Authorize(Roles = "Administrador")]
        public IActionResult DesassociarAluno(int alunoId, int turmaId)
        {
            _turmaService.DesassociarAlunoTurma(alunoId, turmaId);

            return RedirectToAction("Editar", "Turma", new { id = turmaId });
        }

        [HttpPost]
        [Route("excluir/{id}")]
        [Authorize(Roles = "Administrador")]
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