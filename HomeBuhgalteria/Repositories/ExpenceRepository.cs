using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Interfaces;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public class ExpenceRepository: IExpenceRepository
    {
        public async Task<List<Account>> GetAccountsByUserId(int userId)
        {
            List<Account> accounts = new List<Account>();
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetAccountsByUserIdCommand(userId));
            while (await sqlDataReader.ReadAsync())
            {
                Account account = new Account();
                account.AccountId = Convert.ToInt32(sqlDataReader["AccountId"]);
                account.AccountName = Convert.ToString(sqlDataReader["AccountName"]);
                account.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                accounts.Add(account);
            }
            await DbConnection.CloseSqlConnection();
            return accounts;
        }

        public async Task<int> GetCategoriesCount()
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetExpenceCategoriesCountCommand());
            int categoriesCount = Convert.ToInt32(sqlDataReader["Count"]);
            await DbConnection.CloseSqlConnection();
            return categoriesCount;
        }

        public async Task<decimal> GetMonthlySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlySumCommand(dateTime,selectedAccountId));
            decimal monthlySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return monthlySum;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> GetMonthlySumForEachCategory(DateTime dateTime,int selectedAccountId, int expenceCategoriesCount)
        {
            List<SummerizedExpensesByCategory>
                summerizedExpensesByCategories = new List<SummerizedExpensesByCategory>();
            for (int categoryId = 1; categoryId <= expenceCategoriesCount; categoryId++)
            {
                SummerizedExpensesByCategory summerizedExpensesByCategory = new SummerizedExpensesByCategory();
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlySummarizedExpensesByCategoryIdCommand(dateTime,selectedAccountId,
                        expenceCategoriesCount,categoryId));
                if (sqlDataReader.HasRows)
                {
                    summerizedExpensesByCategory.ExpenceCategoryId = Convert.ToInt32(sqlDataReader["ExpenceCategoryId"]);
                    summerizedExpensesByCategory.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                    if (!(sqlDataReader[$"CAT{categoryId}_SUM"] is DBNull))
                    {
                        summerizedExpensesByCategory.ExpenceSum = Convert.ToDecimal(sqlDataReader[$"CAT{categoryId}_SUM"]);
                    }
                    else
                    {
                        summerizedExpensesByCategory.ExpenceSum = 0;
                    }
                    summerizedExpensesByCategories.Add(summerizedExpensesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedExpensesByCategories;
        }
    }
}