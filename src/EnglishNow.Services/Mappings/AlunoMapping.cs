using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Mappings
{
    public static class AlunoMapping
    {
        public static Aluno MapToAluno(this CriarAlunoRequest request, int usuarioId)
        {
            var aluno = new Aluno
            {
                Nome = request.Nome,
                Email = request.Email,
                UsuarioId = usuarioId
            };

            return aluno;
        }

        public static AlunoResult MapToAlunoResult(this Aluno aluno)
        {
            var result = new AlunoResult
            {
                Id = aluno.Id,
                UsuarioId = aluno.UsuarioId,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Login = aluno.Usuario?.Login,
                Senha = aluno.Usuario?.Senha
            };

            return result;
        }

        public static Aluno MapToAluno(this EditarAlunoRequest request)
        {
            var aluno = new Aluno
            {
                Id = request.Id,
                Nome = request.Nome,
                Email = request.Email
            };

            return aluno;
        }
    }
}
