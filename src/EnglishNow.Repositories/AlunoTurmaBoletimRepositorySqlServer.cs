using EnglishNow.Repositories.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public class AlunoTurmaBoletimRepositorySqlServer : BaseRepository, IAlunoTurmaBoletimRepository
    {
        public AlunoTurmaBoletimRepositorySqlServer(string connectionString) : base(connectionString) { }

        public int? Inserir(AlunoTurmaBoletim alunoTurmaBoletim)
        {
            int? alunoTurmaBoletimId = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO aluno_turma_boletim
                                (
                                nota_bim1_escrita,
                                nota_bim1_leitura,
                                nota_bim1_conversacao,
                                nota_bim1_final,
                                nota_bim2_leitura,
                                nota_bim2_escrita,
                                nota_bim2_conversacao,
                                nota_bim2_final,
                                nota_final_semestre,
                                faltas_semestre,
                                aluno_id,
                                turma_id)
                                VALUES
                                (
                                @nota_bim1_escrita,
                                @nota_bim1_leitura,
                                @nota_bim1_conversacao,
                                @nota_bim1_final,
                                @nota_bim2_leitura,
                                @nota_bim2_escrita,
                                @nota_bim2_conversacao,
                                @nota_bim2_final,
                                @nota_final_semestre,
                                @faltas_semestre,
                                @aluno_id,
                                @turma_id); 
                                 SELECT SCOPE_IDENTITY()";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nota_bim1_escrita", (object?)alunoTurmaBoletim.NotaBim1Escrita ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_leitura", (object?)alunoTurmaBoletim.NotaBim1Leitura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_conversacao", (object?)alunoTurmaBoletim.NotaBim1Conversacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_final", (object?)alunoTurmaBoletim.NotaBim1Final ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_leitura", (object?)alunoTurmaBoletim.NotaBim2Leitura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_escrita", (object?)alunoTurmaBoletim.NotaBim2Escrita ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_conversacao", (object?)alunoTurmaBoletim.NotaBim2Conversacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_final", (object?)alunoTurmaBoletim.NotaBim2Final ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_final_semestre", (object?)alunoTurmaBoletim.NotaFinalSemestre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@faltas_semestre", (object?)alunoTurmaBoletim.FaltasSemestre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@aluno_id", alunoTurmaBoletim.AlunoId);
                cmd.Parameters.AddWithValue("@turma_id", alunoTurmaBoletim.TurmaId);

                conn.Open();

                alunoTurmaBoletimId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return alunoTurmaBoletimId;
        }

        public int? Apagar(int alunoId, int turmaId)
        {
            int? affectedRows = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM aluno_turma_boletim WHERE aluno_id = @aluno_id AND turma_id = @turma_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@aluno_id", alunoId);
                cmd.Parameters.AddWithValue("@turma_id", turmaId);

                conn.Open();

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public AlunoTurmaBoletim? ObterPorAlunoTurma(int alunoId, int turmaId)
        {
            AlunoTurmaBoletim? result = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT 
		            atb.aluno_turma_boletim_id,
		            atb.nota_bim1_escrita,
		            atb.nota_bim1_leitura,
		            atb.nota_bim1_conversacao,
		            atb.nota_bim1_final,
		            atb.nota_bim2_leitura,
		            atb.nota_bim2_escrita,
		            atb.nota_bim2_conversacao,
		            atb.nota_bim2_final,
		            atb.nota_final_semestre,
		            atb.faltas_semestre,
		            atb.aluno_id,
		            atb.turma_id,
                    a.nome,
                    a.email,
                    t.ano,
                    t.semestre,
                    t.periodo,
                    t.nivel
		            FROM aluno_turma_boletim atb
                    INNER JOIN aluno a ON atb.aluno_id = a.aluno_id
		            INNER JOIN turma t ON atb.turma_id = t.turma_id
		            WHERE
		            atb.aluno_id = @aluno_id AND atb.turma_id = @turma_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@aluno_id", alunoId);
                cmd.Parameters.AddWithValue("@turma_id", turmaId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AlunoTurmaBoletim
                        {
                            Id = (int)reader["aluno_turma_boletim_id"],
                            NotaBim1Escrita = reader.GetDecimalOrNull("nota_bim1_escrita"),
                            NotaBim1Leitura = reader.GetDecimalOrNull("nota_bim1_leitura"),
                            NotaBim1Conversacao = reader.GetDecimalOrNull("nota_bim1_conversacao"),
                            NotaBim1Final = reader.GetDecimalOrNull("nota_bim1_final"),
                            NotaBim2Leitura = reader.GetDecimalOrNull("nota_bim2_leitura"),
                            NotaBim2Escrita = reader.GetDecimalOrNull("nota_bim2_escrita"),
                            NotaBim2Conversacao = reader.GetDecimalOrNull("nota_bim2_conversacao"),
                            NotaBim2Final = reader.GetDecimalOrNull("nota_bim2_final"),
                            NotaFinalSemestre = reader.GetDecimalOrNull("nota_final_semestre"),
                            FaltasSemestre = reader.GetInt32OrNull("faltas_semestre"),
                            AlunoId = (int)reader["aluno_id"],
                            TurmaId = (int)reader["turma_id"],
                            Aluno = new Aluno
                            {
                                Id = (int)reader["aluno_id"],
                                Nome = (string)reader["nome"],
                                Email = (string)reader["email"]
                            },
                            Turma = new Turma
                            {
                                Id = (int)reader["turma_id"],
                                Ano = (int)reader["ano"],
                                Semestre = (int)reader["semestre"],
                                Periodo = (string)reader["periodo"],
                                Nivel = (string)reader["nivel"]
                            }
                        };
                    }
                }
            }

            return result;
        }

        public int? Atualizar(AlunoTurmaBoletim alunoTurmaBoletim)
        {
            int? affectedRows = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"UPDATE aluno_turma_boletim
                                    SET
                                    nota_bim1_escrita = @nota_bim1_escrita,
                                    nota_bim1_leitura = @nota_bim1_leitura,
                                    nota_bim1_conversacao = @nota_bim1_conversacao,
                                    nota_bim1_final = @nota_bim1_final,
                                    nota_bim2_leitura = @nota_bim2_leitura,
                                    nota_bim2_escrita = @nota_bim2_escrita,
                                    nota_bim2_conversacao = @nota_bim2_conversacao,
                                    nota_bim2_final = @nota_bim2_final,
                                    nota_final_semestre = @nota_final_semestre,
                                    faltas_semestre = @faltas_semestre
                                    WHERE aluno_turma_boletim_id = @aluno_turma_boletim_id;";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@aluno_turma_boletim_id", alunoTurmaBoletim.Id);

                cmd.Parameters.AddWithValue("@nota_bim1_escrita", (object?)alunoTurmaBoletim.NotaBim1Escrita ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_leitura", (object?)alunoTurmaBoletim.NotaBim1Leitura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_conversacao", (object?)alunoTurmaBoletim.NotaBim1Conversacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim1_final", (object?)alunoTurmaBoletim.NotaBim1Final ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_leitura", (object?)alunoTurmaBoletim.NotaBim2Leitura ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_escrita", (object?)alunoTurmaBoletim.NotaBim2Escrita ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_conversacao", (object?)alunoTurmaBoletim.NotaBim2Conversacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_bim2_final", (object?)alunoTurmaBoletim.NotaBim2Final ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@nota_final_semestre", (object?)alunoTurmaBoletim.NotaFinalSemestre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@faltas_semestre", (object?)alunoTurmaBoletim.FaltasSemestre ?? DBNull.Value);

                conn.Open();

                affectedRows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }

            return affectedRows;
        }
    }
}