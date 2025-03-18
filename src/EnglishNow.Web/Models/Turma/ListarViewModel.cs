namespace EnglishNow.Web.Models.Turma
{
    public class ListarViewModel
    {
        public int Id { get; set; }

        public required string Professor { get; set; }

        public required string SemestreAno { get; set; }
        
        public required string Periodo { get; set; }

        public required string Nivel { get; set; }
    }
}
