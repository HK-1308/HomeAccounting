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
        private static SqlConnection sqlConnection = new SqlConnection(connectionString);
        public static async Task OpenSqlConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            { await sqlConnection.OpenAsync(); }
        }

        public static async Task CloseSqlConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            { await sqlConnection.CloseAsync(); }
        }
        
        public static async Task<SqlDataReader> ExecuteSqlCommand(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            await sqlDataReader.ReadAsync();
            return sqlDataReader;
        }
        
        public static SqlDataReader ExecuteSqlCommandSync(string command)
        {
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            var sqlDataReader =  sqlCommand.ExecuteReader();
            //удалил sqlDataReader.Read(); так как из-за этого пропускалась одна строка при выборке
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
