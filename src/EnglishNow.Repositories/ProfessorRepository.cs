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

        IList<Professor> Listar();
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

        public IList<Professor> Listar()
        {
            var result = new List<Professor>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                    professor p INNER JOIN
                                    usuario u ON p.professor_id = u.usuario_id
                                    ORDER BY
                                    p.professor_id";

                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var professor = new Professor
                        {
                            Id = reader.GetInt32("professor_id"),
                            Nome = reader.GetString("nome"),
                            Email = reader.GetString("email"),
                            UsuarioId = reader.GetInt32("usuario_id"),
                            Usuario = new Usuario
                            {
                                Id = reader.GetInt32("usuario_id"),
                                Login = reader.GetString("login"),
                                Senha = reader.GetString("senha")
                            }
                        };

                        result.Add(professor);
                    }
                }
            }

            return result;
        }
    }
}
