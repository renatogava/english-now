using EnglishNow.Repositories;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Boletim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace EnglishNow.Services
{
    public interface IBoletimService
    {
        BoletimResult? ObterBoletimPorAlunoTurma(int alunoId, int turmaId);

        AtualizarBoletimResult Atualizar(AtualizarBoletimRequest request);
    }

    public class BoletimService : IBoletimService
    {
        private readonly IAlunoTurmaBoletimRepository _alunoTurmaBoletimRepository;

        public BoletimService(IAlunoTurmaBoletimRepository alunoTurmaBoletimRepository)
        {
            _alunoTurmaBoletimRepository = alunoTurmaBoletimRepository;
        }

        public AtualizarBoletimResult Atualizar(AtualizarBoletimRequest request)
        {
            var result = new AtualizarBoletimResult();

            var alunoTurmaBoletim = request.MapToAlunoTurmaBoletim();

            var affectedRows = _alunoTurmaBoletimRepository.Atualizar(alunoTurmaBoletim);

            if (!affectedRows.HasValue || affectedRows.Value == 0)
            {
                result.MensagemErro = "Erro ao atualizar o boletim.";
                return result;
            }

            result.Sucesso = true;

            return result;
        }

        public BoletimResult? ObterBoletimPorAlunoTurma(int alunoId, int turmaId)
        {
            var alunoTurmaBoletim = _alunoTurmaBoletimRepository.ObterPorAlunoTurma(alunoId, turmaId);

            if (alunoTurmaBoletim == null)
            {
                return null;
            }

            var result = alunoTurmaBoletim.MapToBoletimResult();

            return result;
        }
    }
}
