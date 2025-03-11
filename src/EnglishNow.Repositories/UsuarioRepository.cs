using EnglishNow.Repositories.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? ObterPorLogin(string login);

        int? Inserir(Usuario usuario);
    }

    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(string connectionString) : base(connectionString)
        {
        }

        public Usuario? ObterPorLogin(string login)
        {
            Usuario? usuario = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = "SELECT usuario_id, login, senha, papel_id FROM usuario WHERE login = @login";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("login", login);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = reader.GetInt32("usuario_id"),
                            Login = reader.GetString("login"),
                            Senha = reader.GetString("senha"),
                            PapelId = reader.GetInt32("papel_id")
                        };
                    }
                }
            }

            return usuario;
        }

        public int? Inserir(Usuario usuario)
        {
            int? usuarioId = null;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                string query = @"INSERT INTO usuario (login, senha, papel_id) VALUES (@login, @senha, @papel_id); 
                                 SELECT LAST_INSERT_ID()";

                var cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@login", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@papel_id", usuario.PapelId);

                conn.Open();

                usuarioId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return usuarioId;
        }
    }
}
