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

        public async Task<List<Account>> GetUserAccounts(int userId)
        {
            List<Account> accounts = await expenceRepository.GetAccountsByUserId(userId);
            return accounts;
        }

        public async Task<List<SummerizedExpensesByCategory>> CollectMonthlyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int ExpenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal MonthlySum = await expenceRepository.GetMonthlySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetMonthlySumForEachCategory(dateTime,selectedAccountId,ExpenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                if (MonthlySum != 0)
                {
                    SummerizedExpense.ExpencePersent =
                        Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / MonthlySum * 100, 2);
                }
                else
                {
                    SummerizedExpense.ExpencePersent = 0;
                }
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
                if (yearlySum != 0)
                {
                    SummerizedExpense.ExpencePersent =
                        Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / yearlySum * 100, 2);
                }
                else
                {
                    SummerizedExpense.ExpencePersent = 0;
                }
            }
            return SummerizedExpenses;
        }
        
        public async Task<List<SummerizedExpensesByCategory>> CollectDailyExpenceInfo(DateTime dateTime, int selectedAccountId)
        {
            int ExpenceCategoriesCount = await expenceRepository.GetCategoriesCount();
            decimal MonthlySum = await expenceRepository.GetMonthlySum(dateTime, selectedAccountId);
            List<SummerizedExpensesByCategory> SummerizedExpenses = await expenceRepository.GetMonthlySumForEachCategory(dateTime,selectedAccountId,ExpenceCategoriesCount);
            foreach (var SummerizedExpense in SummerizedExpenses)
            {
                if (MonthlySum != 0)
                {
                    SummerizedExpense.ExpencePersent =
                        Math.Round(Convert.ToDecimal(SummerizedExpense.ExpenceSum) / MonthlySum * 100, 2);
                }
                else
                {
                    SummerizedExpense.ExpencePersent = 0;
                }
            }
            return SummerizedExpenses;
        }
        
    }
}