namespace EnglishNow.Web.Models.Aluno
{
    public class ListarViewModel
    {
        public IList<AlunoViewModel>? Alunos { get; set; }

        public bool ExibirBotoesEdicao { get; set; }
    }

    public class AlunoViewModel
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Login { get; set; }
    }
}
