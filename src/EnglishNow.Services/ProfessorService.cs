using EnglishNow.Repositories;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Models.Professor;

namespace EnglishNow.Services
{
    public interface IProfessorService
    {
        CriarProfessorResult Criar(CriarProfessorRequest request);
    }

    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProfessorService(IProfessorRepository professorRepository,
            IUsuarioRepository usuarioRepository)
        {
            _professorRepository = professorRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarProfessorResult Criar(CriarProfessorRequest request)
        {
            var result = new CriarProfessorResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null)
            {
                result.MensagemErro = "Esse usuário já existe";
                return result;
            }

            //inserir o usuário
            var usuarioId = _usuarioRepository.Inserir(new Repositories.Entities.Usuario
            {
                Login = request.Login,
                Senha = request.Senha,
                PapelId = (int)Papel.Professor
            });

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir o usuário";
                return result;
            }

            //inserir o professor
            _professorRepository.Inserir(new Repositories.Entities.Professor
            {
                Nome = request.Nome,
                Email = request.Email,
                UsuarioId = usuarioId.Value
            });

            result.Sucesso = true;

            return result;
        }
    }
}
