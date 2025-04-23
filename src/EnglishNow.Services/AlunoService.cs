using EnglishNow.Repositories;
using EnglishNow.Services.Enums;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Aluno;

namespace EnglishNow.Services
{
    public interface IAlunoService
    {
        CriarAlunoResult Criar(CriarAlunoRequest request);

        EditarAlunoResult Editar(EditarAlunoRequest request);

        ExcluirAlunoResult Excluir(int id);

        IList<AlunoResult> Listar();

        IList<AlunoResult> ListarPorTurma(int turmaId);

        IList<AlunoResult> ListarPorProfessor(int usuarioId);

        IList<AlunoResult> ListarPorAluno(int usuarioId);

        AlunoResult? ObterPorId(int id);

        AlunoResult? ObterPorUsuarioId(int usuarioId);
    }

    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AlunoService(IAlunoRepository alunoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _alunoRepository = alunoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public CriarAlunoResult Criar(CriarAlunoRequest request)
        {
            var result = new CriarAlunoResult();

            if (request == null)
            {
                result.MensagemErro = "Objeto CriarAlunoRequest obrigatório";
                return result;
            }

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

            var aluno = request.MapToAluno(usuarioId.Value);

            //inserir o aluno
            var alunoId = _alunoRepository.Inserir(aluno);

            if (!alunoId.HasValue)
            {
                result.MensagemErro = "Erro ao inserir o aluno";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public EditarAlunoResult Editar(EditarAlunoRequest request)
        {
            var result = new EditarAlunoResult();

            var usuarioExistente = _usuarioRepository.ObterPorLogin(request.Login);

            if (usuarioExistente != null && usuarioExistente.Id != request.UsuarioId)
            {
                result.MensagemErro = "Já existe outro usuário com esse login";

                return result;
            }

            var aluno = request.MapToAluno();

            var affectedRows = _alunoRepository.Atualizar(aluno);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o aluno";
                return result;
            }

            var usuario = request.MapToUsuario();

            affectedRows = _usuarioRepository.Atualizar(usuario);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar o usuário";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirAlunoResult Excluir(int id)
        {
            var result = new ExcluirAlunoResult();

            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
            {
                result.MensagemErro = "Aluno não existe";

                return result;
            }

            var affectedRows = _alunoRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o aluno";

                return result;
            }

            affectedRows = _usuarioRepository.Apagar(aluno.UsuarioId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir o usuário";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<AlunoResult> Listar()
        {
            var alunos = _alunoRepository.Listar();

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorTurma(int turmaId)
        {
            var alunos = _alunoRepository.ListarPorTurma(turmaId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorProfessor(int usuarioId)
        {
            var alunos = _alunoRepository.ListarPorProfessor(usuarioId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public IList<AlunoResult> ListarPorAluno(int usuarioId)
        {
            var alunos = _alunoRepository.ListarPorAluno(usuarioId);

            var result = alunos.Select(c => c.MapToAlunoResult()).ToList();

            return result;
        }

        public AlunoResult? ObterPorId(int id)
        {
            var aluno = _alunoRepository.ObterPorId(id);

            if (aluno == null)
                return null;

            var result = aluno.MapToAlunoResult();

            return result;
        }

        public AlunoResult? ObterPorUsuarioId(int usuarioId)
        {
            var aluno = _alunoRepository.ObterPorUsuarioId(usuarioId);

            if (aluno == null)
                return null;

            var result = aluno.MapToAlunoResult();

            return result;
        }
    }
}
