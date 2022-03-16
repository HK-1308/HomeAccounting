using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    static class DbConnection
    {
        private static string connectionString = @"Data Source=CMDB-80829;Initial Catalog=BUGALTERIA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True";
        private static SqlConnection sqlConnection;
        public static async void OpenSqlConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
        }

        public static async void CloseSqlConnection()
        {
            await sqlConnection.CloseAsync();
        }
        
        public static async Task<SqlDataReader> ExecuteSqlCommand(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlDataReader.ReadAsync();
            return sqlDataReader;
        }

        public static async Task ExecuteNonQuerySqlCommand(string command)
        {
            var com = new SqlCommand(command, sqlConnection);
            await com.ExecuteNonQueryAsync();
        }
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
