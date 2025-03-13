using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Professor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Mappings
{
    public static class ProfessorMapping
    {
        public static Professor MapToProfessor(this CriarProfessorRequest request, int usuarioId)
        {
            var professor = new Professor
            {
                Nome = request.Nome,
                Email = request.Email,
                UsuarioId = usuarioId
            };

            return professor;
        }

        public static ProfessorResult MapToProfessorResult(this Professor professor)
        {
            var result = new ProfessorResult
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                Login = professor.Usuario?.Login,
                Senha = professor.Usuario?.Senha
            };

            return result;
        }
    }
}
