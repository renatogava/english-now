using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public interface IAlunoRepository
    {
        int? Inserir(Aluno aluno);

        int? Atualizar(Aluno aluno);

        int? Apagar(int id);

        IList<Aluno> Listar();

        IList<Aluno> ListarPorTurma(int turmaId);

        IList<Aluno> ListarPorProfessor(int usuarioId);

        IList<Aluno> ListarPorAluno(int usuarioId);

        Aluno? ObterPorId(int id);

        Aluno? ObterPorUsuarioId(int usuarioId);
    }

    public class AlunoRepository : BaseRepository, IAlunoRepository
    {
        public AlunoRepository(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Aluno aluno)
        {
            int? alunoId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO aluno (nome, email, usuario_id) VALUES (@nome, @email, @usuario_id); 
                                 SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email);
                cmd.Parameters.AddWithValue("@usuario_id", aluno.UsuarioId);

                conn.Open();

                alunoId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return alunoId;
        }

        public int? Atualizar(Aluno aluno)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var query = "UPDATE aluno SET nome = @nome, email = @email WHERE aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", aluno.Nome);
                cmd.Parameters.AddWithValue("@email", aluno.Email);
                cmd.Parameters.AddWithValue("@aluno_id", aluno.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public IList<Aluno> Listar()
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM 
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id
                                    ORDER BY
                                    a.aluno_id";

                var cmd = new MySqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public IList<Aluno> ListarPorTurma(int turmaId)
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM 
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id INNER JOIN
                                    aluno_turma_boletim atb ON a.aluno_id = atb.aluno_id
                                    WHERE
                                    atb.turma_id = @turma_id
                                    ORDER BY
                                    a.aluno_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("turma_id", turmaId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public IList<Aluno> ListarPorProfessor(int usuarioId)
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT DISTINCT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM 
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id INNER JOIN
                                    aluno_turma_boletim atb ON a.aluno_id = atb.aluno_id INNER JOIN
                                    turma t ON t.turma_id = atb.turma_id INNER JOIN
                                    professor p ON t.professor_id = p.professor_id INNER JOIN
                                    usuario up ON up.usuario_id = p.usuario_id
                                    WHERE
                                    up.usuario_id = @usuario_id
                                    ORDER BY
                                    a.aluno_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("usuario_id", usuarioId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public IList<Aluno> ListarPorAluno(int usuarioId)
        {
            var result = new List<Aluno>();

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id
                                    WHERE
                                    u.usuario_id = @usuario_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("usuario_id", usuarioId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var aluno = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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

                        result.Add(aluno);
                    }
                }
            }

            return result;
        }

        public Aluno? ObterPorId(int id)
        {
            Aluno? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM 
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id
                                    WHERE
                                    a.aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("aluno_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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

        public Aluno? ObterPorUsuarioId(int usuarioId)
        {
            Aluno? result = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT a.aluno_id, a.nome, a.email, u.usuario_id, u.login, u.senha FROM 
                                    aluno a INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id
                                    WHERE
                                    u.usuario_id = @usuario_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("usuario_id", usuarioId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Aluno
                        {
                            Id = reader.GetInt32("aluno_id"),
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
                string query = "DELETE FROM aluno WHERE aluno_id = @aluno_id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aluno_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
