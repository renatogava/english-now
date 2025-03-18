using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Turma
{
    public class CriarTurmaRequest
    {
        public required int Ano { get; set; }

        public required int Semestre { get; set; }

        public required int ProfessorId { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }
    }
}
