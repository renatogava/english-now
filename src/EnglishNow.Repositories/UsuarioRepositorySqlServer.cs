using EnglishNow.Repositories.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public class UsuarioRepositorySqlServer : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepositorySqlServer(string connectionString) : base(connectionString)
        {
        }

        public Usuario? ObterPorLogin(string login)
        {
            Usuario? usuario = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT usuario_id, login, senha, papel_id FROM usuario WHERE login = @login";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@login", login);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = (int)reader["usuario_id"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"],
                            PapelId = (int)reader["papel_id"]
                        };
                    }
                }
            }

            return usuario;
        }

        public int? Inserir(Usuario usuario)
        {
            int? usuarioId = null;

            using (var conn = new SqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO usuario (login, senha, papel_id) VALUES (@login, @senha, @papel_id); 
                                 SELECT SCOPE_IDENTITY()";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@papel_id", usuario.PapelId);

                conn.Open();

                usuarioId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return usuarioId;
        }

        public int? Atualizar(Usuario usuario)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "UPDATE usuario SET login = @login, senha = @senha WHERE usuario_id = @usuario_id";

                var cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@usuario_id", usuario.Id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public int? Apagar(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var query = "DELETE FROM usuario WHERE usuario_id = @usuario_id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario_id", id);

                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }
    }
}