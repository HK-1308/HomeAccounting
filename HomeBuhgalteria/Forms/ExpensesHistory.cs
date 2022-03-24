using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models;

namespace WinFormsApp1.Forms
{
    public partial class ExpensesHistory : Form
    {
        ExpenceController expenceController;

        List<ExpenseForHistory> expensesForHistory;

        public ExpensesHistory()
        {
            InitializeComponent();
        }

        private async void ExpensesHistory_Load(object sender, EventArgs e)
        {
            expensesForHistory = await expenceController.CollectInfoForExpensesHistory();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
