using EnglishNow.Repositories.Entities;
using EnglishNow.Services.Models.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services.Mappings
{
    public static class TurmaMapping
    {
        public static Turma MapToTurma(this CriarTurmaRequest request)
        {
            var turma = new Turma
            {
                Ano = request.Ano,
                Semestre = request.Semestre,
                Nivel = request.Nivel,
                Periodo = request.Periodo,
                ProfessorId = request.ProfessorId
            };

            return turma;
        }

        public static TurmaResult MapToTurmaResult(this Turma turma)
        {
            var result = new TurmaResult
            {
                Id = turma.Id,
                Ano = turma.Ano,
                Semestre = turma.Semestre,
                Nivel = turma.Nivel,
                Periodo = turma.Periodo,
                ProfessorId = turma.ProfessorId,
                ProfessorNome = turma.Professor?.Nome
            };

            return result;
        }

        public static Turma MapToTurma(this EditarTurmaRequest request)
        {
            var turma = new Turma
            {
                Id = request.Id,
                Ano = request.Ano,
                Semestre = request.Semestre,
                Nivel = request.Nivel,
                Periodo = request.Periodo,
                ProfessorId = request.ProfessorId
            };

            return turma;
        }

    }
}
