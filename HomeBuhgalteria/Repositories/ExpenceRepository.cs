using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WinFormsApp1.Interfaces;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public class ExpenceRepository: IExpenceRepository
    {
        public async Task<List<ExpenseCategory>> GetCategories()
        {
            List<ExpenseCategory> categories = new List<ExpenseCategory>();
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetExpenseCategories());
            do
            {
                ExpenseCategory category = new ExpenseCategory();
                category.ExpenseCategoryId = Convert.ToInt32(sqlDataReader["ExpenceCategoryId"]);
                category.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                categories.Add(category);
            } while (await sqlDataReader.ReadAsync());
            await DbConnection.CloseSqlConnection();
            return categories;
        }

        public async Task<int> GetCategoriesCount()
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetExpenceCategoriesCountCommand());
            int categoriesCount = Convert.ToInt32(sqlDataReader["Count"]);
            await DbConnection.CloseSqlConnection();
            return categoriesCount;
        }

        private SummerizedExpensesByCategory initializationOfSummerizedExpensesByCategory(SqlDataReader sqlDataReader, int categoryId)
        {
            SummerizedExpensesByCategory summerizedExpensesByCategory = new SummerizedExpensesByCategory();
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
            return summerizedExpensesByCategory;
        }
        
        public async Task<decimal> GetMonthlySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlyExpensesSumCommand(dateTime,selectedAccountId));
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
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlySummarizedExpensesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedExpensesByCategory summerizedExpensesByCategory =
                        initializationOfSummerizedExpensesByCategory(sqlDataReader,categoryId);
                    summerizedExpensesByCategories.Add(summerizedExpensesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedExpensesByCategories;
        }
        
        public async Task<decimal> GetYearlySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetYearlyExpensesSumCommand(dateTime,selectedAccountId));
            decimal yearlySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return yearlySum;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> GetYearlySumForEachCategory(DateTime dateTime,int selectedAccountId, int expenceCategoriesCount)
        {
            List<SummerizedExpensesByCategory>
                summerizedExpensesByCategories = new List<SummerizedExpensesByCategory>();
            for (int categoryId = 1; categoryId <= expenceCategoriesCount; categoryId++)
            {
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetYearlySummarizedExpensesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedExpensesByCategory summerizedExpensesByCategory =
                        initializationOfSummerizedExpensesByCategory(sqlDataReader,categoryId);
                    summerizedExpensesByCategories.Add(summerizedExpensesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedExpensesByCategories;
        }
        
        public async Task<decimal> GetDailySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetDailyExpensesSumCommand(dateTime,selectedAccountId));
            decimal dailySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return dailySum;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> GetDailySumForEachCategory(DateTime dateTime,int selectedAccountId, int expenceCategoriesCount)
        {
            List<SummerizedExpensesByCategory>
                summerizedExpensesByCategories = new List<SummerizedExpensesByCategory>();
            for (int categoryId = 1; categoryId <= expenceCategoriesCount; categoryId++)
            {
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetDailySummarizedExpensesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedExpensesByCategory summerizedExpensesByCategory =
                        initializationOfSummerizedExpensesByCategory(sqlDataReader,categoryId);
                    summerizedExpensesByCategories.Add(summerizedExpensesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedExpensesByCategories;
        }

        public async Task AddNewExpense(string expenceAmount,int ExpenseCategoryId,int AccountId, string note)
        {
            await DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(
                SQLCommands.AddNewExpenseCommand(expenceAmount, ExpenseCategoryId, AccountId,note));
            await DbConnection.CloseSqlConnection();
        }
    }
}