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
            var professores = _alunoService.Listar();

            var result = professores.Select(c => c.MapToListarViewModel()).ToList();

            return View(result);
        }

        [Route("editar/{id}")]
        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);

            var model = aluno?.MapToEditarViewModel();

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
