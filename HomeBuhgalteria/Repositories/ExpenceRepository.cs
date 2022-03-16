using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public class ExpenceRepository
    {
        public async Task<List<Account>> GetAccountsByUserId(int userId)
        {
            List<Account> accounts = new List<Account>();
            DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetAccountsByUserIdCommand(userId));
            while (await sqlDataReader.ReadAsync())
            {
                Account account = new Account();
                account.AccountId = Convert.ToInt32(sqlDataReader["AccountId"]);
                account.AccountName = Convert.ToString(sqlDataReader["AccountName"]);
                account.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                accounts.Add(account);
            }
            DbConnection.CloseSqlConnection();
            return accounts;
        }
    }
}