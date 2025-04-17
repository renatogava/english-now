using EnglishNow.Repositories;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services
{
    public interface ITurmaService
    {
        CriarTurmaResult Criar(CriarTurmaRequest request);

        EditarTurmaResult Editar(EditarTurmaRequest request);

        AssociarAlunoTurmaResult AssociarAlunoTurma(int alunoId, int turmaId);

        AssociarAlunoTurmaResult DesassociarAlunoTurma(int alunoId, int turmaId);

        ExcluirTurmaResult Excluir(int id);

        IList<TurmaResult> Listar();

        IList<TurmaResult> ListarPorProfessor(int usuarioId);

        IList<TurmaResult> ListarPorAluno(int usuarioId);

        TurmaResult? ObterPorId(int id);
    }

    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAlunoTurmaBoletimRepository _alunoTurmaBoletimRepository;

        public TurmaService(
            ITurmaRepository turmaRepository,
            IAlunoTurmaBoletimRepository alunoTurmaBoletimRepository)
        {
            _turmaRepository = turmaRepository;
            _alunoTurmaBoletimRepository = alunoTurmaBoletimRepository;
        }

        public CriarTurmaResult Criar(CriarTurmaRequest request)
        {
            var result = new CriarTurmaResult();

            var turma = request.MapToTurma();

            //inserir a turma
            var turmaId = _turmaRepository.Inserir(turma);

            if (turmaId <= 0)
            {
                result.MensagemErro = "Não foi possível inserir a turma";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public EditarTurmaResult Editar(EditarTurmaRequest request)
        {
            var result = new EditarTurmaResult();

            var turma = request.MapToTurma();

            var affectedRows = _turmaRepository.Atualizar(turma);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível atualizar a turma";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public AssociarAlunoTurmaResult AssociarAlunoTurma(int alunoId, int turmaId)
        {
            var result = new AssociarAlunoTurmaResult();

            var alunoTurmaBoletim = new AlunoTurmaBoletim
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };

            var alunoTurma = _alunoTurmaBoletimRepository.ObterPorAlunoTurma(alunoId, turmaId);

            if (alunoTurma != null)
            {
                result.MensagemErro = "O aluno já pertence a essa turma";
                return result;
            }

            var affectedRows = _alunoTurmaBoletimRepository.Inserir(alunoTurmaBoletim);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível associar o aluno à turma";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public AssociarAlunoTurmaResult DesassociarAlunoTurma(int alunoId, int turmaId)
        {
            var result = new AssociarAlunoTurmaResult();

            var alunoTurma = _alunoTurmaBoletimRepository.ObterPorAlunoTurma(alunoId, turmaId);

            if (alunoTurma == null)
            {
                result.MensagemErro = "O aluno não pertence a essa turma";
                return result;
            }

            var affectedRows = _alunoTurmaBoletimRepository.Apagar(alunoId, turmaId);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível desassociar o aluno à turma";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public ExcluirTurmaResult Excluir(int id)
        {
            var result = new ExcluirTurmaResult();

            var turma = _turmaRepository.ObterPorId(id);

            if (turma == null)
            {
                result.MensagemErro = "Turma não existe";

                return result;
            }

            var affectedRows = _turmaRepository.Apagar(id);

            if (affectedRows == 0)
            {
                result.MensagemErro = "Não foi possível excluir a turma";

                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public IList<TurmaResult> Listar()
        {
            var turmas = _turmaRepository.Listar();

            var result = turmas.Select(c => c.MapToTurmaResult()).ToList();

            return result;
        }

        public IList<TurmaResult> ListarPorProfessor(int usuarioId)
        {
            var turmas = _turmaRepository.ListarPorProfessor(usuarioId);

            var result = turmas.Select(c => c.MapToTurmaResult()).ToList();

            return result;
        }

        public IList<TurmaResult> ListarPorAluno(int usuarioId)
        {
            var turmas = _turmaRepository.ListarPorAluno(usuarioId);

            var result = turmas.Select(c => c.MapToTurmaResult()).ToList();

            return result;
        }

        public TurmaResult? ObterPorId(int id)
        {
            var turma = _turmaRepository.ObterPorId(id);

            if (turma == null)
                return null;

            var result = turma.MapToTurmaResult();

            return result;
        }
    }
}
