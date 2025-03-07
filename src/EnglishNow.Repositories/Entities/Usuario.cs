using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Login { get; set; }

        public required string Senha { get; set; }

        public int PapelId { get; set; }
    }
}
