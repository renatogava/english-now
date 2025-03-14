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

        int? Atualizar(Professor professor);

        int? Apagar(int id);

        IList<Professor> Listar();

        Professor? ObterPorId(int id);
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

        public int? Atualizar(Professor professor)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "UPDATE professor SET nome = @nome, email = @email WHERE professor_id = @professor_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", professor.Nome);
                cmd.Parameters.AddWithValue("@email", professor.Email);
                cmd.Parameters.AddWithValue("@professor_id", professor.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }


        public IList<Professor> Listar()
        {
            var result = new List<Professor>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                    professor p INNER JOIN
                                    usuario u ON p.usuario_id = u.usuario_id
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

        public Professor? ObterPorId(int id)
        {
            Professor? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                    professor p INNER JOIN
                                    usuario u ON p.usuario_id = u.usuario_id
                                    WHERE
                                    p.professor_id = @professor_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("professor_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Professor
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
                    }
                }
            }

            return result;
        }

        public int? Apagar(int id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "DELETE FROM professor WHERE professor_id = @professor_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@professor_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
