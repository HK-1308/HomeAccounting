using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1.Models
{
    public class ExpenseForHistory
    {
        public int ExpenceId { get; set; }

        public string CategoryName { get; set; }

        public string AccountName { get; set; }

        public decimal Expense { get; set; }

        public DateTime DateOfExpense { get; set; }
    }
}
