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

        public static string GetMonthlyExpensesSumCommand(DateTime dateTime, int selectedAccountId)
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
        
        public static string GetYearlyExpensesSumCommand(DateTime dateTime, int selectedAccountId)
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
        
        public static string GetDailyExpensesSumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Expences.Expence) AS ALL_SUM"
                   + " FROM Expences " +
                   $"WHERE AccountId = {selectedAccountId} AND YEAR(DateOfExpence) = {dateTime.Year} AND MONTH(DateOfExpence) = {dateTime.Month} AND DAY(DateOfExpence)={dateTime.Day}";
        }
        
        public static string GetDailySummarizedExpensesByCategoryIdCommand(DateTime dateTime,int selectedAccountId, int categoryId)
        {
            return $"SELECT [ExpenceCategories].[ExpenceCategoryId], [CategoryName], SUM(Expences.Expence) AS CAT{categoryId}_SUM "
                   + "FROM ExpenceCategories " +
                   $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {selectedAccountId} AND YEAR(DateOfExpence) = {dateTime.Year} AND MONTH(DateOfExpence) = {dateTime.Month} AND DAY(DateOfExpence)={dateTime.Day} " +
                   $"WHERE ExpenceCategories.ExpenceCategoryId = {categoryId} " +
                   "GROUP BY [ExpenceCategories].[CategoryName], [ExpenceCategories].[ExpenceCategoryId]";
        }
        
        public static string AddNewExpenseCommand(string expenceAmount,int ExpenseCategoryId,int AccountId, string note)
        {
            return $"INSERT INTO [Expences] ([DateOfExpence], [Note], [Expence], [AccountId], [ExpenceCategoryId]) VALUES (GETDATE(),'{note}',{expenceAmount},{AccountId},{ExpenseCategoryId})";
        }
        
        public static string GetExpenseCategories()
        {
            return "SELECT * FROM [ExpenceCategories]";
        }
        
        public static string GetIncomeCategories()
        {
            return "SELECT * FROM [IncomeCategories]";
        }
        
        public static string GetIncomeCategoriesCountCommand()
        {
            return "SELECT COUNT(*) AS [Count] FROM IncomeCategories";
        }
        
        public static string GetMonthlyIncomesSumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Incomes.Income) AS ALL_SUM"
                   + " FROM Incomes " +
                   $"WHERE AccountId = {selectedAccountId} AND MONTH(DateOfIncome) = {dateTime.Month} AND YEAR(DateOfIncome) = {dateTime.Year}";
        }

        public static string GetMonthlySummarizedIncomesByCategoryIdCommand(DateTime dateTime,int selectedAccountId,int categoryId)
        {
            return $"SELECT [IncomeCategories].[IncomeCategoryId], [CategoryName], SUM(Incomes.Income) AS CAT{categoryId}_SUM "
                   + "FROM IncomeCategories " +
                   $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {selectedAccountId} AND MONTH(DateOfIncome) = {dateTime.Month} AND YEAR(DateOfIncome) = {dateTime.Year} " +
                   $"WHERE IncomeCategories.IncomeCategoryId = {categoryId} " +
                   "GROUP BY [IncomeCategories].[CategoryName], [IncomeCategories].[IncomeCategoryId]";
        }
        
        public static string GetYearlyIncomesSumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Incomes.Income) AS ALL_SUM"
                   + " FROM Incomes " +
                   $"WHERE AccountId = {selectedAccountId} AND YEAR(DateOfIncome) = {dateTime.Year}";
        }
        
        public static string GetYearlySummarizedIncomesByCategoryIdCommand(DateTime dateTime,int selectedAccountId, int categoryId)
        {
            return $"SELECT [IncomeCategories].[IncomeCategoryId], [CategoryName], SUM(Incomes.Income) AS CAT{categoryId}_SUM "
                   + "FROM IncomeCategories " +
                   $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {selectedAccountId} AND YEAR(DateOfIncome) = {dateTime.Year} " +
                   $"WHERE IncomeCategories.IncomeCategoryId = {categoryId} " +
                   "GROUP BY [IncomeCategories].[CategoryName], [IncomeCategories].[IncomeCategoryId]";
        }
        
        public static string GetDailyIncomesSumCommand(DateTime dateTime, int selectedAccountId)
        {
            return "SELECT SUM(Incomes.Income) AS ALL_SUM"
                   + " FROM Incomes " +
                   $"WHERE AccountId = {selectedAccountId} AND YEAR(DateOfIncome) = {dateTime.Year} AND MONTH(DateOfIncome) = {dateTime.Month} AND DAY(DateOfIncome)={dateTime.Day}";
        }
        
        public static string GetDailySummarizedIncomesByCategoryIdCommand(DateTime dateTime,int selectedAccountId, int categoryId)
        {
            return $"SELECT [IncomeCategories].[IncomeCategoryId], [CategoryName], SUM(Incomes.Income) AS CAT{categoryId}_SUM "
                   + "FROM IncomeCategories " +
                   $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {selectedAccountId} AND YEAR(DateOfIncome) = {dateTime.Year} AND MONTH(DateOfIncome) = {dateTime.Month} AND DAY(DateOfIncome)={dateTime.Day} " +
                   $"WHERE IncomeCategories.IncomeCategoryId = {categoryId} " +
                   "GROUP BY [IncomeCategories].[CategoryName], [IncomeCategories].[IncomeCategoryId]";
        }
        
        public static string AddNewIncomeCommand(string incomeAmount,int incomeCategoryId,int AccountId, string note)
        {
            return $"INSERT INTO [Incomes] ([DateOfIncome], [Note], [Income], [AccountId], [IncomeCategoryId]) VALUES (GETDATE(),'{note}',{incomeAmount},{AccountId},{incomeCategoryId})";
        }
        
    }
}