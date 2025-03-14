using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Professor
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string? Email { get; set; }
    }
}
