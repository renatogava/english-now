using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public interface IProfessorRepository
    {
        int? Inserir(Professor professor);
    }

    public class ProfessorRepository : BaseRepository, IProfessorRepository
    {
        public ProfessorRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Professor professor)
        {
            int? professorId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO professor (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id); 
                                 SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", professor.Nome);
                cmd.Parameters.AddWithValue("@email", professor.Email);
                cmd.Parameters.AddWithValue("@usuario_id", professor.UsuarioId);

                conn.Open();

                professorId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return professorId;
        }
    }
}
