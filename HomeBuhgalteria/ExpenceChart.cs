using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class ExpenceChart : Form
    {
        private ExpenceController expenceController;
        
        private List<Account> accounts;


        private int selectedExpenseCategoryId;

        private int selectedAccountId;

        private int DEFAULT_INDEX = 0;
        
        private const string MONTHLY_FORMAT = "MMMM yyyy";
        
        private const string YEARLY_FORMAT = "yyyy";
        
        private const string DAILY_FORMAT = "dd/MMM/yyyy";
        
        private string[] periods = new[] {"Month", "Day", "Year"};
        
        private DateTime dateTime;
        public ExpenceChart()
        {
            InitializeComponent();
            expenceController = new ExpenceController();
            dateTime = DateTime.Today;
        }

        private async void ExpenceChart_Load(object sender, EventArgs e)
        {
            timePeriodComboBox.Items.AddRange(periods);
            SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
            await FillingAccountsList(CurrentUser.UserId);
            SetDefaultSelectedIndex();
            selectedAccountId = accounts.Find(u => u.AccountName == accountComboBox.Text).AccountId;
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }
        
        private void SetDefaultSelectedIndex()
        {
            timePeriodComboBox.SelectedIndexChanged -= TimePeriodComboBoxSelectedIndexChanged;
            accountComboBox.SelectedIndexChanged -= AccountComboBoxSelectedIndexChanged;
            timePeriodComboBox.SelectedIndex = DEFAULT_INDEX;
            accountComboBox.SelectedIndex = DEFAULT_INDEX;
            timePeriodComboBox.SelectedIndexChanged += TimePeriodComboBoxSelectedIndexChanged;
            accountComboBox.SelectedIndexChanged += AccountComboBoxSelectedIndexChanged;
        }
        private void SetCustomFormatForDatetimePicker(string format)
        {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = format;
            dateTimePicker.ShowUpDown = true;
        }
        
        private async Task FillingAccountsList(int userId)
        {
            accounts = await expenceController.GetUserAccounts(userId);
            foreach (var account in accounts)
            {
                accountComboBox.Items.Add(account.AccountName);
            }
        }

        private async Task ShowMonthlyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedMonthlyExpences = await expenceController.CollectMonthlyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedMonthlyExpence in summerizedMonthlyExpences)
            {
                mainListBox.Items.Add($"{summerizedMonthlyExpence.CategoryName}   {summerizedMonthlyExpence.ExpencePersent}%");
            }
            label1.Text = "Monthly expences:";
        }

        private async Task ShowYearlyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedYearlyExpences = await expenceController.CollectYearlyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedYearlyExpence in summerizedYearlyExpences)
            {
                mainListBox.Items.Add($"{summerizedYearlyExpence.CategoryName}   {summerizedYearlyExpence.ExpencePersent}%");
            }
            label1.Text = "Yearly expences:";
        }

        private async Task ShowDailyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedDailyExpences = await expenceController.CollectDailyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedDailyExpence in summerizedDailyExpences)
            {
                mainListBox.Items.Add($"{summerizedDailyExpence.CategoryName}   {summerizedDailyExpence.ExpencePersent}%");
            }
            label1.Text = "Daily expences:";

        }

        private async void TimePeriodComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }

        private async void AccountComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAccountId = accounts.Find(u => u.AccountName == accountComboBox.Text).AccountId;
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }
        

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTime = dateTimePicker.Value;
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }

        private async Task ShowExpensesByPeriod(string timePeriod)
        {
            mainListBox.Items.Clear();
            switch (timePeriod)
            {
                case "Month":
                    SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
                    await ShowMonthlyExpence();
                    break;
                case "Year":
                    SetCustomFormatForDatetimePicker(YEARLY_FORMAT);
                    await ShowYearlyExpence();
                    break;
                case "Day":
                    SetCustomFormatForDatetimePicker(DAILY_FORMAT);
                    await ShowDailyExpence();
                    break;
                default:
                    SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
                    await ShowMonthlyExpence();
                    break;
            }
        }

        private void mainListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedExpenseCategoryId = mainListBox.SelectedIndex+1;
        }

        private void expenseAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void expenseAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            var pressedKeyIsNumber = ((e.KeyChar >= '0') && (e.KeyChar <= '9'));
            if (pressedKeyIsNumber)
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (expenseAmount.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    addExpenseButton.Focus();
                return;
            }

            e.Handled = true;
        }

        private async void addExpenseButton_Click(object sender, EventArgs e)
        {
            if (expenseAmount.Text != null)
            {
                var expenceAmount = expenseAmount.Text.Replace(',','.');
                await expenceController.AddNewExpense(expenceAmount,selectedExpenseCategoryId,selectedAccountId, note.Text);
                await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
            }
        }
    }
}
