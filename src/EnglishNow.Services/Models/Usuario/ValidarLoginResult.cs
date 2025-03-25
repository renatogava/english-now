using EnglishNow.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Models.Usuario
{
    public class ValidarLoginResult : BaseResult
    {
        public UsuarioResult? Usuario { get; set; }
    }

    public class UsuarioResult
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public Papel Papel { get; set; }
    }
}
