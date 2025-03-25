using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Models.Aluno;
using EnglishNow.Services.Models.Professor;
using EnglishNow.Services.Models.Usuario;

namespace EnglishNow.Services.Mappings
{
    public static class UsuarioMapping
    {
        public static Usuario MapToUsuario(this CriarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this EditarProfessorRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login,
                Senha = request.Senha
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this CriarAlunoRequest request)
        {
            var usuario = new Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Aluno
            };

            return usuario;
        }

        public static Usuario MapToUsuario(this EditarAlunoRequest request)
        {
            var usuario = new Usuario
            {
                Id = request.UsuarioId,
                Login = request.Login,
                Senha = request.Senha
            };

            return usuario;
        }

        public static UsuarioResult MapToUsuarioResult(this Usuario usuario)
        {
            var usuarioResult = new UsuarioResult
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Papel = (Papel)usuario.PapelId
            };

            return usuarioResult;
        }
    }
}
