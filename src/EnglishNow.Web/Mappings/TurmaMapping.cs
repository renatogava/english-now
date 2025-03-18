using EnglishNow.Services.Models.Turma;
using EnglishNow.Web.Models.Turma;

namespace EnglishNow.Web.Mappings
{
    public static class TurmaMapping
    {
        public static CriarTurmaRequest MapToCriarTurmaRequest(this CriarViewModel model)
        {
            var request = new CriarTurmaRequest
            {
                Ano = model.Ano!.Value,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel!,
                Periodo = model.Periodo!
            };

            return request;
        }

        public static ListarViewModel MapToListarViewModel(this TurmaResult model)
        {
            var viewModel = new ListarViewModel
            {
                Id = model.Id,
                SemestreAno = $"{model.Semestre}° Semestre/{model.Ano}",
                Professor = model.ProfessorNome,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return viewModel;
        }

        public static EditarViewModel MapToEditarViewModel(this TurmaResult model)
        {
            var viewModel = new EditarViewModel
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return viewModel;
        }

        public static EditarTurmaRequest MapToEditarTurmaRequest(this EditarViewModel model)
        {
            var request = new EditarTurmaRequest
            {
                Id = model.Id,
                Ano = model.Ano,
                Semestre = model.Semestre,
                ProfessorId = model.ProfessorId,
                Nivel = model.Nivel,
                Periodo = model.Periodo
            };

            return request;
        }
    }
}
