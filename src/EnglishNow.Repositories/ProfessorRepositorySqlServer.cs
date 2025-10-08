using EnglishNow.Repositories.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public class ProfessorRepositorySqlServer : BaseRepository, IProfessorRepository
    {
        public ProfessorRepositorySqlServer(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Professor professor)
        {
            int? professorId = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO professor (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id); 
                                 SELECT SCOPE_IDENTITY()";

                var cmd = new SqlCommand(query, conn);

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
            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "UPDATE professor SET nome = @nome, email = @email WHERE professor_id = @professor_id";

                var cmd = new SqlCommand(query, conn);
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

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                    professor p INNER JOIN
                                    usuario u ON p.usuario_id = u.usuario_id
                                    ORDER BY
                                    p.professor_id";

                var cmd = new SqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var professor = new Professor
                        {
                            Id = (int)reader["professor_id"],
                            Nome = (string)reader["nome"],
                            Email = (string)reader["email"],
                            UsuarioId = (int)reader["usuario_id"],
                            Usuario = new Usuario
                            {
                                Id = (int)reader["usuario_id"],
                                Login = (string)reader["login"],
                                Senha = (string)reader["senha"]
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

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT p.professor_id, p.nome, p.email, u.usuario_id, u.login, u.senha FROM 
                                    professor p INNER JOIN
                                    usuario u ON p.usuario_id = u.usuario_id
                                    WHERE
                                    p.professor_id = @professor_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@professor_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Professor
                        {
                            Id = (int)reader["professor_id"],
                            Nome = (string)reader["nome"],
                            Email = (string)reader["email"],
                            UsuarioId = (int)reader["usuario_id"],
                            Usuario = new Usuario
                            {
                                Id = (int)reader["usuario_id"],
                                Login = (string)reader["login"],
                                Senha = (string)reader["senha"]
                            }
                        };
                    }
                }
            }

            return result;
        }

        public int? Apagar(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM professor WHERE professor_id = @professor_id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@professor_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}