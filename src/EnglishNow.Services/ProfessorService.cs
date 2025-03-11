using EnglishNow.Repositories;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Mappings;
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

            var usuario = request.MapToUsuario();

            //inserir o usuário
            var usuarioId = _usuarioRepository.Inserir(usuario);

            if (!usuarioId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir o usuário";
                return result;
            }

            var professor = request.MapToProfessor(usuarioId.Value);

            //inserir o professor
            _professorRepository.Inserir(professor);

            result.Sucesso = true;

            return result;
        }
    }
}
