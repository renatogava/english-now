using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Repositories
{
    public static class RepositoryExtensions
    {
        public static decimal? GetDecimalOrNull(this MySqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (decimal)reader[name];
        }

        public static int? GetInt32OrNull(this MySqlDataReader reader, string name)
        {
            return reader[name] == DBNull.Value ? null : (int)reader[name];
        }
    }
}
