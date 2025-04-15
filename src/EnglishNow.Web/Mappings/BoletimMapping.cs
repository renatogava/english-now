using EnglishNow.Services.Models.Boletim;
using EnglishNow.Web.Models.Boletim;

namespace EnglishNow.Web.Mappings
{
    public static class BoletimMapping
    {
        public static EditarViewModel MapToEditarViewModel(this BoletimResult model)
        {
            var viewModel = new EditarViewModel
            {
                BoletimId = model.BoletimId,
                TurmaId = model.TurmaId,
                NomeAluno = model.NomeAluno,
                DescricaoTurma = $"{model.SemestreTurma}° Semestre/{model.AnoTurma} - {model.NivelTurma}, {model.PeriodoTurma}",
                NotaBim1Escrita = model.NotaBim1Escrita,
                NotaBim1Leitura = model.NotaBim1Leitura,
                NotaBim1Conversacao = model.NotaBim1Conversacao,
                NotaBim1Final = model.NotaBim1Final,
                NotaBim2Escrita = model.NotaBim2Escrita,
                NotaBim2Leitura = model.NotaBim2Leitura,
                NotaBim2Conversacao = model.NotaBim2Conversacao,
                NotaBim2Final = model.NotaBim2Final,
                NotaFinalSemestre = model.NotaFinalSemestre,
                FaltasSemestre = model.FaltasSemestre
            };

            return viewModel;
        }

        public static AtualizarBoletimRequest MapToAtualizarBoletimRequest(this EditarViewModel model)
        {
            var request = new AtualizarBoletimRequest
            {
                BoletimId = model.BoletimId,
                NotaBim1Escrita = model.NotaBim1Escrita,
                NotaBim1Leitura = model.NotaBim1Leitura,
                NotaBim1Conversacao = model.NotaBim1Conversacao,
                NotaBim1Final = model.NotaBim1Final,
                NotaBim2Escrita = model.NotaBim2Escrita,
                NotaBim2Leitura = model.NotaBim2Leitura,
                NotaBim2Conversacao = model.NotaBim2Conversacao,
                NotaBim2Final = model.NotaBim2Final,
                NotaFinalSemestre = model.NotaFinalSemestre,
                FaltasSemestre = model.FaltasSemestre
            };
            return request;
        }
    }
}
