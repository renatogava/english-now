using EnglishNow.Repositories;
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

        ExcluirTurmaResult Excluir(int id);

        IList<TurmaResult> Listar();

        TurmaResult? ObterPorId(int id);
    }

    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
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
