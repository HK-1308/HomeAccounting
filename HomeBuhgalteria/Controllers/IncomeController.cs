using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    public class IncomeController
    {
         private IncomeRepository incomeRepository;

        public IncomeController()
        {
            incomeRepository = new IncomeRepository();
        }

        public async Task<List<IncomeCategory>> GetCategories()
        {
            List<IncomeCategory> categories = await incomeRepository.GetCategories();
            return categories;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> CollectMonthlyIncomeInfo(DateTime dateTime, int selectedAccountId)
        {
            int incomeCategoriesCount = await incomeRepository.GetCategoriesCount();
            decimal monthlySum = await incomeRepository.GetMonthlySum(dateTime, selectedAccountId);
            List<SummerizedIncomesByCategory> summerizedIncomes = await incomeRepository.GetMonthlySumForEachCategory(dateTime,selectedAccountId,incomeCategoriesCount);
            foreach (var summerizedIncome in summerizedIncomes)
            {
                summerizedIncome.IncomePersent = monthlySum != 0 ? Math.Round(Convert.ToDecimal(summerizedIncome.IncomeSum) / monthlySum * 100, 2) : 0;
            }
            return summerizedIncomes;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> CollectYearlyIncomeInfo(DateTime dateTime, int selectedAccountId)
        {
            int incomeCategoriesCount = await incomeRepository.GetCategoriesCount();
            decimal yearlySum = await incomeRepository.GetYearlySum(dateTime, selectedAccountId);
            List<SummerizedIncomesByCategory> summerizedIncomes = await incomeRepository.GetYearlySumForEachCategory(dateTime,selectedAccountId,incomeCategoriesCount);
            foreach (var summerizedIncome in summerizedIncomes)
            {
                summerizedIncome.IncomePersent = yearlySum != 0 ? Math.Round(Convert.ToDecimal(summerizedIncome.IncomeSum) / yearlySum * 100, 2) : 0;
            }
            return summerizedIncomes;
        }
        
        public async Task<List<SummerizedIncomesByCategory>> CollectDailyIncomeInfo(DateTime dateTime, int selectedAccountId)
        {
            int incomeCategoriesCount = await incomeRepository.GetCategoriesCount();
            decimal dailySum = await incomeRepository.GetDailySum(dateTime, selectedAccountId);
            List<SummerizedIncomesByCategory> summerizedIncomes = await incomeRepository.GetDailySumForEachCategory(dateTime,selectedAccountId,incomeCategoriesCount);
            foreach (var summerizedIncome in summerizedIncomes)
            {
                summerizedIncome.IncomePersent = dailySum != 0 ? Math.Round(Convert.ToDecimal(summerizedIncome.IncomeSum) / dailySum * 100, 2) : 0;
            }
            return summerizedIncomes;
        }
        
        public async Task AddNewIncome(string incomeAmount,int selectedIncomeCategoryId,int selectedAccountId, string note)
        {
            await incomeRepository.AddNewIncome(incomeAmount, selectedIncomeCategoryId, selectedAccountId,note);
        }
    }
}