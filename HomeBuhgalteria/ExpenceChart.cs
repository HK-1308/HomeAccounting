using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var accounts = await expenceController.GetUserAccounts(userId);
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
            mainListBox.Items.Clear();
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }

        private async void AccountComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAccountId = accountComboBox.SelectedIndex;
            mainListBox.Items.Clear();
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }
        

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            mainListBox.Items.Clear();
            dateTime = dateTimePicker.Value;
            await ShowExpensesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }

        private async Task ShowExpensesByPeriod(string timePeriod)
        {
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
    }
}
