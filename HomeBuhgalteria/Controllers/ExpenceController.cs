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

        public List<Account> GetUserAccounts(int userId)
        {
            List<Account> accounts = expenceRepository.GetAccountsByUserId(userId);
            return accounts;
        }

        public async Task<List<SummerizedExpensesByCategory>> CollectMonthlyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int ExpenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal MonthlySum = await expenceRepository.GetMonthlySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetMonthlySumForEachCategory(dateTime,selectedAccountId,ExpenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                SummerizedExpense.ExpencePersent =
                    Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / MonthlySum * 100, 2);
            }
            return SummerizedExpenses;
        }
        
    }
}