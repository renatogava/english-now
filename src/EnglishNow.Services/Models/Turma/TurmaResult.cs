using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Turma
{
    public class TurmaResult
    {
        public int Id { get; set; }

        public int Ano { get; set; }

        public int Semestre { get; set; }

        public int ProfessorId { get; set; }

        public required string ProfessorNome { get; set; }

        public required string Periodo { get; set; }

        public required string Nivel { get; set; }
    }
}
