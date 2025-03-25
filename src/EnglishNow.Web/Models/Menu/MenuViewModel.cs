namespace EnglishNow.Web.Models.Menu
{
    public class MenuViewModel
    {
        public Menu Ativo { get; set; }

        public bool MenuProfessorVisivel { get; set; }
    }

    public enum Menu
    {
        Home,
        Professor,
        Aluno,
        Turma
    }
}
