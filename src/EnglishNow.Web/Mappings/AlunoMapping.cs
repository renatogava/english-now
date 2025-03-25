using EnglishNow.Services.Models.Aluno;
using EnglishNow.Web.Models.Aluno;

namespace EnglishNow.Web.Mappings
{
    public static class AlunoMapping
    {
        public static CriarAlunoRequest MapToCriarAlunoRequest(this CriarViewModel model)
        {
            var request = new CriarAlunoRequest
            {
                Login = model.Login!,
                Senha = model.Senha!,
                Nome = model.Nome!,
                Email = model.Email!
            };

            return request;
        }

        public static AlunoViewModel MapToAlunoViewModel(this AlunoResult model)
        {
            var viewModel = new AlunoViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login!
            };

            return viewModel;
        }

        public static Models.Turma.AlunoTurmaViewModel MapToAlunoTurmaViewModel(this AlunoResult model)
        {
            var viewModel = new Models.Turma.AlunoTurmaViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login!
            };

            return viewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this AlunoResult model)
        {
            var viewModel = new EditarViewModel
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login!,
                Senha = model.Senha!
            };

            return viewModel;
        }

        public static EditarAlunoRequest MapToEditarAlunoRequest(this EditarViewModel model)
        {
            var request = new EditarAlunoRequest
            {
                Id = model.Id,
                UsuarioId = model.UsuarioId,
                Nome = model.Nome!,
                Email = model.Email!,
                Login = model.Login!,
                Senha = model.Senha!
            };

            return request;
        }


    }
}
