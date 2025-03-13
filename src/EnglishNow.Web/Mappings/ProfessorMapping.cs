using EnglishNow.Services.Models.Professor;
using EnglishNow.Web.Models.Professor;

namespace EnglishNow.Web.Mappings
{
    public static class ProfessorMapping
    {
        public static CriarProfessorRequest MapToCriarProfessorRequest(this CriarViewModel model)
        {
            var request = new CriarProfessorRequest
            {
                Login = model.Login!,
                Senha = model.Senha!,
                Nome = model.Nome!,
                Email = model.Email!
            };

            return request;
        }

        public static ListarViewModel MapToListarViewModel(this ProfessorResult model)
        {
            var viewModel = new ListarViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Login = model.Login!
            };

            return viewModel;
        }
    }
}
