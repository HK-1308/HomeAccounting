using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class ExpenceController
    {
        private ExpenceRepository expenceRepository;

        public ExpenceController()
        {
            expenceRepository = new ExpenceRepository();
        }

        public async Task<List<ExpenseCategory>> GetCategories()
        {
            List<ExpenseCategory> categories = await expenceRepository.GetCategories();
            return categories;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> CollectMonthlyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int expenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal monthlySum = await expenceRepository.GetMonthlySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetMonthlySumForEachCategory(dateTime,selectedAccountId,expenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                SummerizedExpense.ExpencePersent = monthlySum != 0 ? Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / monthlySum * 100, 2) : 0;
            }
            return SummerizedExpenses;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> CollectYearlyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int expenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal yearlySum = await expenceRepository.GetYearlySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetYearlySumForEachCategory(dateTime,selectedAccountId,expenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                SummerizedExpense.ExpencePersent = yearlySum != 0 ? Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / yearlySum * 100, 2) : 0;
            }
            return SummerizedExpenses;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> CollectDailyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int expenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal dailySum = await expenceRepository.GetDailySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetDailySumForEachCategory(dateTime,selectedAccountId,expenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                SummerizedExpense.ExpencePersent = dailySum != 0 ? Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / dailySum * 100, 2) : 0;
            }
            return SummerizedExpenses;
        }
        
        public async Task AddNewExpense(string expenceAmount,int selectedExpenseCategoryId,int selectedAccountId, string note)
        {
            await expenceRepository.AddNewExpense(expenceAmount, selectedExpenseCategoryId, selectedAccountId,note);
        }
        
    }
}