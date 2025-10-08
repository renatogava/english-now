using EnglishNow.Repositories.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public class TurmaRepositorySqlServer : BaseRepository, ITurmaRepository
    {
        public TurmaRepositorySqlServer(string connectionString) : base(connectionString)
        {
        }

        public int? Inserir(Turma turma)
        {
            int? turmaId = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO turma (ano, semestre, periodo, nivel, professor_id) VALUES (@ano, @semestre, @periodo, @nivel, @professor_id); 
                                 SELECT SCOPE_IDENTITY()";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ano", turma.Ano);
                cmd.Parameters.AddWithValue("@semestre", turma.Semestre);
                cmd.Parameters.AddWithValue("@periodo", turma.Periodo);
                cmd.Parameters.AddWithValue("@nivel", turma.Nivel);
                cmd.Parameters.AddWithValue("@professor_id", turma.ProfessorId);

                conn.Open();

                turmaId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return turmaId;
        }

        public int? Atualizar(Turma turma)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "UPDATE turma SET ano = @ano, semestre = @semestre, periodo = @periodo, nivel = @nivel, professor_id = @professor_id WHERE turma_id = @turma_id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ano", turma.Ano);
                cmd.Parameters.AddWithValue("@semestre", turma.Semestre);
                cmd.Parameters.AddWithValue("@periodo", turma.Periodo);
                cmd.Parameters.AddWithValue("@nivel", turma.Nivel);
                cmd.Parameters.AddWithValue("@professor_id", turma.ProfessorId);
                cmd.Parameters.AddWithValue("@turma_id", turma.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public IList<Turma> Listar()
        {
            var result = new List<Turma>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT t.turma_id, t.semestre, t.ano, t.periodo, t.nivel, t.professor_id, p.nome, p.email FROM
                                    turma t INNER JOIN
                                    professor p ON t.professor_id = p.professor_id
                                    ORDER BY
                                    t.ano, t.semestre";

                var cmd = new SqlCommand(query, conn);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var turma = new Turma
                        {
                            Id = (int)reader["turma_id"],
                            Ano = (int)reader["ano"],
                            Semestre = (int)reader["semestre"],
                            Periodo = (string)reader["periodo"],
                            Nivel = (string)reader["nivel"],
                            ProfessorId = (int)reader["professor_id"],
                            Professor = new Professor
                            {
                                Id = (int)reader["professor_id"],
                                Nome = (string)reader["nome"],
                                Email = (string)reader["email"]
                            }
                        };

                        result.Add(turma);
                    }
                }
            }

            return result;
        }

        public IList<Turma> ListarPorProfessor(int usuarioId)
        {
            var result = new List<Turma>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT t.turma_id, t.semestre, t.ano, t.periodo, t.nivel, t.professor_id, p.nome, p.email FROM
                                    turma t INNER JOIN
                                    professor p ON t.professor_id = p.professor_id INNER JOIN
                                    usuario u ON p.usuario_id = u.usuario_id
                                    WHERE
                                    u.usuario_id = @usuario_id
                                    ORDER BY
                                    t.ano, t.semestre";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var turma = new Turma
                        {
                            Id = (int)reader["turma_id"],
                            Ano = (int)reader["ano"],
                            Semestre = (int)reader["semestre"],
                            Periodo = (string)reader["periodo"],
                            Nivel = (string)reader["nivel"],
                            ProfessorId = (int)reader["professor_id"],
                            Professor = new Professor
                            {
                                Id = (int)reader["professor_id"],
                                Nome = (string)reader["nome"],
                                Email = (string)reader["email"]
                            }
                        };

                        result.Add(turma);
                    }
                }
            }

            return result;
        }

        public IList<Turma> ListarPorAluno(int usuarioId)
        {
            var result = new List<Turma>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT t.turma_id, t.ano, t.semestre, t.periodo, t.nivel, p.professor_id, p.nome, p.email FROM
                                    aluno_turma_boletim atb INNER JOIN
                                    turma t ON atb.turma_id = t.turma_id INNER JOIN
                                    professor p ON t.professor_id = p.professor_id INNER JOIN
                                    aluno a ON atb.aluno_id = a.aluno_id INNER JOIN
                                    usuario u ON a.usuario_id = u.usuario_id
                                    WHERE
                                    u.usuario_id = @usuario_id
                                    ORDER BY
                                    t.ano, t.semestre";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@usuario_id", usuarioId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var turma = new Turma
                        {
                            Id = (int)reader["turma_id"],
                            Ano = (int)reader["ano"],
                            Semestre = (int)reader["semestre"],
                            Periodo = (string)reader["periodo"],
                            Nivel = (string)reader["nivel"],
                            ProfessorId = (int)reader["professor_id"],
                            Professor = new Professor
                            {
                                Id = (int)reader["professor_id"],
                                Nome = (string)reader["nome"],
                                Email = (string)reader["email"]
                            }
                        };

                        result.Add(turma);
                    }
                }
            }

            return result;
        }

        public Turma? ObterPorId(int id)
        {
            Turma? result = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT t.turma_id, t.semestre, t.ano, t.periodo, t.nivel, t.professor_id, p.nome, p.email FROM
                                    turma t INNER JOIN
                                    professor p ON t.professor_id = p.professor_id
                                    WHERE
                                    t.turma_id = @turma_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@turma_id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new Turma
                        {
                            Id = (int)reader["turma_id"],
                            Ano = (int)reader["ano"],
                            Semestre = (int)reader["semestre"],
                            Periodo = (string)reader["periodo"],
                            Nivel = (string)reader["nivel"],
                            ProfessorId = (int)reader["professor_id"],
                            Professor = new Professor
                            {
                                Id = (int)reader["professor_id"],
                                Nome = (string)reader["nome"],
                                Email = (string)reader["email"]
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
                string query = "DELETE FROM turma WHERE turma_id = @turma_id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@turma_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}