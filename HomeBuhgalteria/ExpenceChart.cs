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
        
        private const string MONTHLY_FORMAT = "MMMM yyyy";
        
        private const string YEARLY_FORMAT = "yyyy";
        
        private const string DAYLY_FORMAT = "dd/MMM/yyyy";
        
        private string[] periods = new[] {"Month", "Day", "Year"};
        
        private DateTime dateTime;
        public ExpenceChart()
        {
            InitializeComponent();
            expenceController = new ExpenceController();
            dateTime = DateTime.Today;
        }

        private void SetCustomFormatForDatetimePicker(string format)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = format;
            dateTimePicker1.ShowUpDown = true;
        }

        /*private ExpenceChart(string accountId, TimePeriod timePeriod, DateTime dateTime)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] {"Month","Day","Year"});
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedItem = selectedPeriod;
            this.accountId = accountId;
            this.timePeriod = timePeriod;
            this.DateTime = dateTime;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }*/

        private async void ExpenceChart_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(periods);
            comboBox1.SelectedIndex = 0;
            SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
            await FillingAccountsList(CurrentUser.UserId);
            comboBox2.SelectedIndex = 0;
            
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Month":
                    await ShowMonthlyExpence();
                    break;
                case "Year":
                    await ShowYearlyExpence();
                    break;
                case "Day":
                    await ShowDailyExpence();
                    break;
                default:
                    await ShowMonthlyExpence();
                    break;
            }
        }

        private async Task ShowMonthlyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedMonthlyExpences = await expenceController.CollectMonthlyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedMonthlyExpence in summerizedMonthlyExpences)
            {
                listBox1.Items.Add($"{summerizedMonthlyExpence.CategoryName}   {summerizedMonthlyExpence.ExpencePersent}%");
            }
            label1.Text = "Monthly expences:";
        }

        private async Task ShowYearlyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedYearlyExpences = await expenceController.CollectYearlyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedYearlyExpence in summerizedYearlyExpences)
            {
                listBox1.Items.Add($"{summerizedYearlyExpence.CategoryName}   {summerizedYearlyExpence.ExpencePersent}%");
            }
            label1.Text = "Yearly expences:";
        }

        private async Task ShowDailyExpence()
        {
            List<SummerizedExpensesByCategory> summerizedDailyExpences = await expenceController.CollectDailyExpenceInfo(dateTime, selectedAccountId);
            foreach (var summerizedDailyExpence in summerizedDailyExpences)
            {
                listBox1.Items.Add($"{summerizedDailyExpence.CategoryName}   {summerizedDailyExpence.ExpencePersent}%");
            }
            label1.Text = "Daily expences:";

        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            //switch (comboBox1.SelectedItem.ToString())
            //{
            //    case "Month":
            //        await ShowMonthlyExpence();
            //        break;
            //    case "Year":
            //        await ShowYearlyExpence();
            //        break;
            //    case "Day":
            //        await ShowDailyExpence();
            //        break;
            //    default:
            //        await ShowMonthlyExpence();
            //        break;
            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async Task FillingAccountsList(int userId)
        {
            var accounts = await expenceController.GetUserAccounts(userId);
            foreach (var account in accounts)
            {
                comboBox2.Items.Add(account.AccountName);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
