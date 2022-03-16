using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models;

namespace WinFormsApp1.Interfaces
{
    public interface IExpenceRepository
    {
        public List<Account> GetAccountsByUserId(int userId);
        public Task<int> GetCategoriesCount();
        public Task<decimal> GetMonthlySum(DateTime dateTime, int selectedAccountId);
    }
}