using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories.Entities
{
    public class Professor
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public int UsuarioId { get; set; }
    }
}
