using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Aluno
{
    public class AlunoResult
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public string? Login { get; set; }

        public string? Senha { get; set; }
    }
}
