using EnglishNow.Repositories;
using EnglishNow.Services.Mappings;
using EnglishNow.Services.Models.Boletim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services
{
    public interface IBoletimService
    {
        BoletimResult? ObterBoletimPorAlunoTurma(int alunoId, int turmaId);
    }

    public class BoletimService : IBoletimService
    {
        private readonly IAlunoTurmaBoletimRepository _alunoTurmaBoletimRepository;

        public BoletimService(IAlunoTurmaBoletimRepository alunoTurmaBoletimRepository)
        {
            _alunoTurmaBoletimRepository = alunoTurmaBoletimRepository;
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
