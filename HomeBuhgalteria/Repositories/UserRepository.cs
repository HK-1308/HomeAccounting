﻿using System;
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
            DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetUserByUserNameAndPasswordCommand(userName,password));
            if (sqlDataReader.HasRows)
            {
                searchedUser.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                searchedUser.Password = Convert.ToString(sqlDataReader["Password"]);
                searchedUser.UserName = Convert.ToString(sqlDataReader["UserName"]);
                DbConnection.CloseSqlConnection();
                return searchedUser;
            }
            DbConnection.CloseSqlConnection();
            return null;
        }

        public async Task<User> AddNewUserByNameAndPassword(string userName, string password)
        {
            DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddNewUserByUserNameAndPasswordCommand(userName,password));
            DbConnection.CloseSqlConnection();
            return await GetUserByNameAndPassword(userName, password);
            
        }
        public async Task AddDefaultAccountForUser(int userId)
        {
            DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultAccountsByUserIdCommand(userId));
            SqlDataReader sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetAccountsByUserIdCommand(userId));
            int newAccountId = Convert.ToInt32(sqlDataReader["AccountId"]);
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultSettingsToAccountByAccountId(newAccountId));
            DbConnection.CloseSqlConnection();
        }
        
        
        public async Task AddDefaultUserSettings(int userId)
        {
            DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(SQLCommands.AddDefaultSettingsToUserByUserId(userId));
            DbConnection.CloseSqlConnection();
        }
    }
}