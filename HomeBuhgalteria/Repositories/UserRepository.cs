using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WinFormsApp1.Interfaces;
using WinFormsApp1.Models;

namespace WinFormsApp1.Repositories
{
    
    public class UserRepository: IUserRepository
    {
        public async Task<User> GetUserByNameAndPassword(string userName, string password)
        {
            User searchedUser = new User();
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetUserByUserNameAndPasswordCommand(userName,password));
            if (sqlDataReader.HasRows)
            {
                searchedUser.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                searchedUser.Password = Convert.ToString(sqlDataReader["Password"]);
                searchedUser.UserName = Convert.ToString(sqlDataReader["UserName"]);
                await DbConnection.CloseSqlConnection();
                return searchedUser;
            }
            else
            {
                await DbConnection.CloseSqlConnection();
            }
            return null;
        }
        
        public async Task<List<Account>> GetAccountsByUserId(int userId)
        {
            List<Account> accounts = new List<Account>();
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetAccountsByUserIdCommand(userId));
            do
            {
                Account account = new Account();
                account.AccountId = Convert.ToInt32(sqlDataReader["AccountId"]);
                account.AccountName = Convert.ToString(sqlDataReader["AccountName"]);
                account.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                accounts.Add(account);
            } while (await sqlDataReader.ReadAsync());
            await DbConnection.CloseSqlConnection();
            return accounts;
        }

        public async Task<User> AddNewUserByNameAndPassword(string userName, string password)
        {
            await DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddNewUserByUserNameAndPasswordCommand(userName,password));
            await DbConnection.CloseSqlConnection();
            return await GetUserByNameAndPassword(userName, password);
            
        }
        public async Task AddDefaultAccountForUser(int userId)
        {
            await DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultAccountsByUserIdCommand(userId));
            SqlDataReader sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetAccountsByUserIdCommand(userId));
            int newAccountId = Convert.ToInt32(sqlDataReader["AccountId"]);
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultSettingsToAccountByAccountId(newAccountId));
            await DbConnection.CloseSqlConnection();
        }
        
        
        public async Task AddDefaultUserSettings(int userId)
        {
            await DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultSettingsToUserByUserId(userId));
            await DbConnection.CloseSqlConnection();
        }
    }
}