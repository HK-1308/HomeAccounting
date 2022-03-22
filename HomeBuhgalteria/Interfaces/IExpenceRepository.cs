using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Interfaces
{
    public interface IExpenceRepository
    {
        public Task<int> GetCategoriesCount();
        public Task<decimal> GetMonthlySum(DateTime dateTime, int selectedAccountId);
        public Task<decimal> GetYearlySum(DateTime dateTime, int selectedAccountId);
        
        public Task<List<SummerizedExpensesByCategory>> GetYearlySumForEachCategory(DateTime dateTime,
            int selectedAccountId, int expenceCategoriesCount);

        public Task<List<SummerizedExpensesByCategory>> GetMonthlySumForEachCategory(DateTime dateTime,
            int selectedAccountId, int expenceCategoriesCount);
    }
}