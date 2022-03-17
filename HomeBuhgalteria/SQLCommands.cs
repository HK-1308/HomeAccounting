using System;

namespace WinFormsApp1
{
    public class SQLCommands
    {
        public static string GetAccountsByUserIdCommand(int userId)
        {
            return $"SELECT * FROM [Accounts] WHERE [UserId] = '{userId}'";
        }
        
        public static string GetUserByUserNameAndPasswordCommand(string userName, string password)
        {
            return $"SELECT * FROM [Users] WHERE [UserName] = '{userName}' AND [Password] = '{password}' ";
        }
        
        public static string AddNewUserByUserNameAndPasswordCommand(string userName, string password)
        {
            return $"INSERT INTO [Users] (Username,Password) VALUES ('{userName}','{password}')";
        }

        public static string AddDefaultAccountsByUserIdCommand(int userId)
        {
            return$"INSERT INTO [Accounts] (AccountName,UserId) VALUES ('Cash',{userId})";
        }
        
        public static string AddDefaultSettingsToAccountByAccountId(int accountId)
        {
            return $"INSERT INTO [AccountSettings] ([CurrencyId],[AccountId]) VALUES (1,{accountId})";
        }
        public static string AddDefaultSettingsToUserByUserId(int userId)
        {
            return $"INSERT INTO [UserSettings] (Theme,Language,UserId) VALUES ('White','ENG',{userId})";
        }
        
        public static string GetExpenceCategoriesCountCommand()
        {
            return "SELECT COUNT(*) AS [Count] FROM ExpenceCategories";
        }

        public static string GetMonthlySumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Expences.Expence) AS ALL_SUM"
                   + " FROM Expences " +
                   $"WHERE AccountId = {selectedAccountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year}";
        }

        public static string GetMonthlySummarizedExpensesByCategoryIdCommand(DateTime dateTime,int selectedAccountId,int categoryId)
        {
            return $"SELECT [ExpenceCategories].[ExpenceCategoryId], [CategoryName], SUM(Expences.Expence) AS CAT{categoryId}_SUM "
                   + "FROM ExpenceCategories " +
                   $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {selectedAccountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year} " +
                   $"WHERE ExpenceCategories.ExpenceCategoryId = {categoryId} " +
                   "GROUP BY [ExpenceCategories].[CategoryName], [ExpenceCategories].[ExpenceCategoryId]";
        }
        
        public static string GetYearlySumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Expences.Expence) AS ALL_SUM"
                   + " FROM Expences " +
                   $"WHERE AccountId = {selectedAccountId} AND YEAR(DateOfExpence) = {dateTime.Year}";
        }
        
        public static string GetYearlySummarizedExpensesByCategoryIdCommand(DateTime dateTime,int selectedAccountId, int categoryId)
        {
            return $"SELECT [ExpenceCategories].[ExpenceCategoryId], [CategoryName], SUM(Expences.Expence) AS CAT{categoryId}_SUM "
                   + "FROM ExpenceCategories " +
                   $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {selectedAccountId} AND YEAR(DateOfExpence) = {dateTime.Year} " +
                   $"WHERE ExpenceCategories.ExpenceCategoryId = {categoryId} " +
                   "GROUP BY [ExpenceCategories].[CategoryName], [ExpenceCategories].[ExpenceCategoryId]";
        }
    }
}