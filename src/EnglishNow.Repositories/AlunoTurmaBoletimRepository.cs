using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public interface IAlunoTurmaBoletimRepository
    {
        int? Inserir(AlunoTurmaBoletim alunoTurmaBoletim);

        int? Apagar(int alunoId, int turmaId);

        AlunoTurmaBoletim? ObterPorAlunoTurma(int alunoId, int turmaId);
    }

    public class AlunoTurmaBoletimRepository : BaseRepository, IAlunoTurmaBoletimRepository
    {
        public AlunoTurmaBoletimRepository(string connectionString) : base(connectionString) { }

        public int? Inserir(AlunoTurmaBoletim alunoTurmaBoletim)
        {
            int? alunoTurmaBoletimId = null;

            using (var conn = new MySqlConnection(ConnectionString))
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
                                 SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nota_bim1_escrita", alunoTurmaBoletim.NotaBim1Escrita);
                cmd.Parameters.AddWithValue("@nota_bim1_leitura", alunoTurmaBoletim.NotaBim1Leitura);
                cmd.Parameters.AddWithValue("@nota_bim1_conversacao", alunoTurmaBoletim.NotaBim1Conversacao);
                cmd.Parameters.AddWithValue("@nota_bim1_final", alunoTurmaBoletim.NotaBim1Final);
                cmd.Parameters.AddWithValue("@nota_bim2_leitura", alunoTurmaBoletim.NotaBim2Leitura);
                cmd.Parameters.AddWithValue("@nota_bim2_escrita", alunoTurmaBoletim.NotaBim2Escrita);
                cmd.Parameters.AddWithValue("@nota_bim2_conversacao", alunoTurmaBoletim.NotaBim2Conversacao);
                cmd.Parameters.AddWithValue("@nota_bim2_final", alunoTurmaBoletim.NotaBim2Final);
                cmd.Parameters.AddWithValue("@nota_final_semestre", alunoTurmaBoletim.NotaFinalSemestre);
                cmd.Parameters.AddWithValue("@faltas_semestre", alunoTurmaBoletim.FaltasSemestre);
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

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "DELETE FROM aluno_turma_boletim WHERE aluno_id = @aluno_id AND turma_id = @turma_id";

                var cmd = new MySqlCommand(query, conn);

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

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"SELECT 
                                    aluno_turma_boletim_id,
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
                                    turma_id
                                    FROM aluno_turma_boletim
                                    WHERE
                                    aluno_id = @aluno_id AND turma_id = @turma_id";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@aluno_id", alunoId);
                cmd.Parameters.AddWithValue("@turma_id", turmaId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new AlunoTurmaBoletim
                        {
                            Id = reader.GetInt32("aluno_turma_boletim_id"),
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
                            AlunoId = reader.GetInt32("aluno_id"),
                            TurmaId = reader.GetInt32("turma_id")
                        };
                    }
                }
            }

            return result;
        }
    }
}
