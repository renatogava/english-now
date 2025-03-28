﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnglishNow.Web.Models.Turma
{
    public class EditarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo Sempre é obrigatório.")]
        public int Semestre { get; set; }

        [Required(ErrorMessage = "O campo Professor é obrigatório.")]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = "O campo Nivel é obrigatório.")]
        public required string Nivel { get; set; }

        [Required(ErrorMessage = "O campo Período é obrigatório.")]
        public required string Periodo { get; set; }

        public List<SelectListItem>? Semestres { get; set; }

        public List<SelectListItem>? Professores { get; set; }

        public IList<AlunoTurmaViewModel>? AlunosTurma { get; set; }

        public IList<AlunoTurmaViewModel>? Alunos { get; set; }

        public bool PodeEditarApagarTurma { get; set; }
    }

    public class AlunoTurmaViewModel
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Login { get; set; }
    }
}
