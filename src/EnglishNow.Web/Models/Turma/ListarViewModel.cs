namespace EnglishNow.Web.Models.Turma
{
    public class ListarViewModel
    {
        public IList<TurmaViewModel>? Turmas { get; set; }

        public bool ExibirBotaoInserir { get; set; }

        public bool ExibirBotaoEditar { get; set; }

        public bool ExibirBotaoBoletim { get; set; }

        public int? AlunoId { get; set; }
    }

    public class TurmaViewModel
    {
        public int Id { get; set; }

        public required string Professor { get; set; }

        public required string SemestreAno { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }
    }
}
