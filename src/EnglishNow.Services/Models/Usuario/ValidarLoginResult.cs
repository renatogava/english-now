using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Usuario
{
    public class ValidarLoginResult
    {
        public bool Sucesso { get; set; }

        public string? MensagemErro { get; set; }
    }
}
