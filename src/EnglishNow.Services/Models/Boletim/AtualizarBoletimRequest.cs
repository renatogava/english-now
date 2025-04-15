using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Boletim
{
    public class AtualizarBoletimRequest
    {
        public int BoletimId { get; set; }

        public decimal? NotaBim1Escrita { get; set; }

        public decimal? NotaBim1Leitura { get; set; }

        public decimal? NotaBim1Conversacao { get; set; }

        public decimal? NotaBim1Final { get; set; }

        public decimal? NotaBim2Escrita { get; set; }

        public decimal? NotaBim2Leitura { get; set; }

        public decimal? NotaBim2Conversacao { get; set; }

        public decimal? NotaBim2Final { get; set; }

        public decimal? NotaFinalSemestre { get; set; }

        public int? FaltasSemestre { get; set; }
    }
}
