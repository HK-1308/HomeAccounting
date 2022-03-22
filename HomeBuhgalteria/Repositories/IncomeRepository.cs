using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public class IncomeRepository
    {
        public async Task<List<IncomeCategory>> GetCategories()
        {
            List<IncomeCategory> categories = new List<IncomeCategory>();
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetIncomeCategories());
            do
            {
                IncomeCategory category = new IncomeCategory();
                category.IncomeCategoryId = Convert.ToInt32(sqlDataReader["IncomeCategoryId"]);
                category.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
                categories.Add(category);
            } while (await sqlDataReader.ReadAsync());
            await DbConnection.CloseSqlConnection();
            return categories;
        }

        public async Task<int> GetCategoriesCount()
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetIncomeCategoriesCountCommand());
            int categoriesCount = Convert.ToInt32(sqlDataReader["Count"]);
            await DbConnection.CloseSqlConnection();
            return categoriesCount;
        }

        private SummerizedIncomesByCategory initializationOfSummerizedIncomesByCategory(SqlDataReader sqlDataReader, int categoryId)
        {
            SummerizedIncomesByCategory summerizedIncomesByCategory = new SummerizedIncomesByCategory();
            summerizedIncomesByCategory.IncomeCategoryId = Convert.ToInt32(sqlDataReader["incomeCategoryId"]);
            summerizedIncomesByCategory.CategoryName = Convert.ToString(sqlDataReader["CategoryName"]);
            if (!(sqlDataReader[$"CAT{categoryId}_SUM"] is DBNull))
            {
                summerizedIncomesByCategory.IncomeSum = Convert.ToDecimal(sqlDataReader[$"CAT{categoryId}_SUM"]);
            }
            else
            {
                summerizedIncomesByCategory.IncomeSum = 0;
            }
            return summerizedIncomesByCategory;
        }
        
        public async Task<decimal> GetMonthlySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlyIncomesSumCommand(dateTime,selectedAccountId));
            decimal monthlySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return monthlySum;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> GetMonthlySumForEachCategory(DateTime dateTime,int selectedAccountId, int incomeCategoriesCount)
        {
            List<SummerizedIncomesByCategory>
                summerizedincomesByCategories = new List<SummerizedIncomesByCategory>();
            for (int categoryId = 1; categoryId <= incomeCategoriesCount; categoryId++)
            {
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetMonthlySummarizedIncomesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedIncomesByCategory summerizedIncomesByCategory =
                        initializationOfSummerizedIncomesByCategory(sqlDataReader,categoryId);
                    summerizedincomesByCategories.Add(summerizedIncomesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedincomesByCategories;
        }
        
        public async Task<decimal> GetYearlySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetYearlyIncomesSumCommand(dateTime,selectedAccountId));
            decimal yearlySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return yearlySum;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> GetYearlySumForEachCategory(DateTime dateTime,int selectedAccountId, int incomeCategoriesCount)
        {
            List<SummerizedIncomesByCategory>
                summerizedIncomesByCategories = new List<SummerizedIncomesByCategory>();
            for (int categoryId = 1; categoryId <= incomeCategoriesCount; categoryId++)
            {
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetYearlySummarizedIncomesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedIncomesByCategory summerizedIncomesByCategory =
                        initializationOfSummerizedIncomesByCategory(sqlDataReader,categoryId);
                    summerizedIncomesByCategories.Add(summerizedIncomesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedIncomesByCategories;
        }
        
        public async Task<decimal> GetDailySum(DateTime dateTime, int selectedAccountId)
        {
            await DbConnection.OpenSqlConnection();
            var sqlDataReader = await DbConnection.ExecuteSqlCommand(SQLCommands.GetDailyIncomesSumCommand(dateTime,selectedAccountId));
            decimal dailySum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            await DbConnection.CloseSqlConnection();
            return dailySum;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> GetDailySumForEachCategory(DateTime dateTime,int selectedAccountId, int incomeCategoriesCount)
        {
            List<SummerizedIncomesByCategory>
                summerizedIncomesByCategories = new List<SummerizedIncomesByCategory>();
            for (int categoryId = 1; categoryId <= incomeCategoriesCount; categoryId++)
            {
                await DbConnection.OpenSqlConnection();
                var sqlDataReader = 
                    await DbConnection.ExecuteSqlCommand(SQLCommands.GetDailySummarizedIncomesByCategoryIdCommand(dateTime, selectedAccountId, categoryId));
                if (sqlDataReader.HasRows)
                {
                    SummerizedIncomesByCategory summerizedIncomesByCategory =
                        initializationOfSummerizedIncomesByCategory(sqlDataReader,categoryId);
                    summerizedIncomesByCategories.Add(summerizedIncomesByCategory);
                }
                await DbConnection.CloseSqlConnection();
            }
            return summerizedIncomesByCategories;
        }

        public async Task AddNewIncome(string incomeAmount,int incomeCategoryId,int AccountId, string note)
        {
            await DbConnection.OpenSqlConnection();
            await DbConnection.ExecuteNonQuerySqlCommand(
                SQLCommands.AddNewIncomeCommand(incomeAmount, incomeCategoryId, AccountId,note));
            await DbConnection.CloseSqlConnection();
        }
    }
}