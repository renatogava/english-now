using Microsoft.AspNetCore.Mvc;
using EnglishNow.Web.Models.Aluno;
using Microsoft.AspNetCore.Authorization;
using EnglishNow.Services;
using EnglishNow.Services.Models.Aluno;
using EnglishNow.Web.Mappings;

namespace EnglishNow.Web.Controllers
{
    [Route("aluno")]
    [Authorize]
    public class AlunoController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService) 
        {
            _alunoService = alunoService;
        }

        [Route("criar")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Criar()
        {
            return View();
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

            //criar o professor
            var result = _alunoService.Criar(model.MapToCriarAlunoRequest());

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
            IList<AlunoResult>? alunos = null;
            
            if (User.IsInRole("Administrador"))
            {
                alunos = _alunoService.Listar();
            }
            else if (User.IsInRole("Professor"))
            {
                var usuarioId = Convert.ToInt32(User.FindFirst("Id")?.Value);

                alunos = _alunoService.ListarPorProfessor(usuarioId);
            }

            var model = new ListarViewModel
            {
                Alunos = alunos?.Select(c => c.MapToAlunoViewModel()).ToList(),
                ExibirBotoesEdicao = User.IsInRole("Administrador")
            };

            return View(model);
        }

        [Route("editar/{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);

            var model = aluno?.MapToEditarViewModel();

            return View(model);
        }

        [Route("editar/{id}")]
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Editar(EditarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = model.MapToEditarAlunoRequest();

            var result = _alunoService.Editar(request);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);
                
                return View(model);
            }

            return RedirectToAction("Listar");
        }

        [Route("excluir/{id}")]
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Excluir(EditarViewModel model)
        {
            var result = _alunoService.Excluir(model.Id);

            if (!result.Sucesso)
            {
                ModelState.AddModelError(string.Empty, result.MensagemErro!);

                return View(model);
            }

            return RedirectToAction("Listar");
        }
    }
}
