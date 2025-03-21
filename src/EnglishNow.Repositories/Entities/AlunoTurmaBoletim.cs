using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories.Entities
{
    public class AlunoTurmaBoletim
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }

        public int TurmaId { get; set; }

        public decimal? NotaBim1Escrita { get; set; }

        public decimal? NotaBim1Leitura { get; set; }

        public decimal? NotaBim1Conversacao { get; set; }

        public decimal? NotaBim1Final { get; set; }

        public decimal? NotaBim2Escrita { get; set; }

        public decimal? NotaBim2Leitura { get; set; }

        public decimal? NotaBim2Conversacao { get; set; }

        public decimal? NotaBim2Final { get; set; }

        public decimal? NotaFinalSemestre { get; set; }

        public int FaltasSemestre { get; set; }
    }
}
