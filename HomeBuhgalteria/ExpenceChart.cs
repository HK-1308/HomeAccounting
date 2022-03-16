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
            comboBox1.Items.AddRange(periods);
            comboBox1.SelectedIndex = 0;
            SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
            FillingAccountsList(CurrentUser.UserId);
            comboBox2.SelectedIndex = 0;
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
                listBox1.Items.Add($"{summerizedMonthlyExpence.CategoryName}   {summerizedMonthlyExpence.ExpencePersent}");
            }
            label1.Text = "Monthly expences:";
        }

        private async Task ShowYearlyExpence()
        {
            decimal sum  = await CountYearSum();
            int expenceCategoriesCount = await GetExpenceCategoryCount();
            sqlDataReader.Close();
            listBox1FillYearly(sum, expenceCategoriesCount);
            label1.Text = "Yearly expences:";
        }

        private async Task ShowDailyExpence()
        {
            decimal sum  = await CountDaySum();
            int expenceCategoriesCount = await GetExpenceCategoryCount();
            sqlDataReader.Close();
            listBox1FillDayly(sum, expenceCategoriesCount);
            label1.Text = "Dayly expences:";
        }

        private async Task<int> GetExpenceCategoryCount()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT COUNT(*) AS C FROM ExpenceCategories");
            await sqlDataReader.ReadAsync();          
            int count = Convert.ToInt32(sqlDataReader["C"]);
            sqlDataReader.Close();
            return count;
        }

        private async Task<decimal>  CountMonthSum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Expences.Expence) AS ALL_SUM"
                                                                + " FROM Expences " +
                                                                $"WHERE AccountId = {accountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year}");
            await sqlDataReader.ReadAsync();
            return sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
        }

        private async Task<decimal> CountYearSum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Expences.Expence) AS ALL_SUM"
                                                                + " FROM Expences " +
                                                                $"WHERE AccountId = {accountId} AND YEAR(DateOfExpence) = {dateTime.Year}");
            await sqlDataReader.ReadAsync();
            return sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
        }

        private async Task<decimal> CountDaySum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Expences.Expence) AS ALL_SUM"
                                                                + " FROM Expences " +
                                                                $"WHERE AccountId = {accountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year} AND DAY(DateOfExpence) = {dateTime.Day}");
            await sqlDataReader.ReadAsync();
            return sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
        }

        #region LISTBOXFILLING
        private async void listBox1FillMonthly(decimal sum,int categoriesCount)
        {
            for (int i = 1; i <= categoriesCount; i++)
            {
                sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [CategoryName],SUM(Expences.Expence) AS CAT{i}_SUM "
                                                                    + "FROM ExpenceCategories " +
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year} " +
                                                                    $"WHERE ExpenceCategories.ExpenceCategoryId = {i} " +
                                                                    "GROUP BY ExpenceCategories.[CategoryName] ");
                await sqlDataReader.ReadAsync();
                if (sqlDataReader[$"CAT{i}_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
                else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"]) / sum * 100,2)}%");
                sqlDataReader.Close();
            }
        }

        private async void listBox1FillYearly(decimal sum, int categoriesCount)
        {
            for (int i = 1; i <= categoriesCount; i++)
            {
                sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [CategoryName],SUM(Expences.Expence) AS CAT{i}_SUM "
                                                                    + "FROM ExpenceCategories " +
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND YEAR(DateOfExpence) = {dateTime.Year} " +
                                                                    $"WHERE ExpenceCategories.ExpenceCategoryId = {i} " +
                                                                    "GROUP BY ExpenceCategories.[CategoryName] ");
                await sqlDataReader.ReadAsync();
                if (sqlDataReader[$"CAT{i}_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
                else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"]) / sum * 100, 2)}%");
                sqlDataReader.Close();
            }
        }

        private async void listBox1FillDayly(decimal sum, int categoriesCount)
        {
            for (int i = 1; i <= categoriesCount; i++)
            {
                sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [CategoryName],SUM(Expences.Expence) AS CAT{i}_SUM "
                                                                    + "FROM ExpenceCategories " +
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND MONTH(DateOfExpence) = {dateTime.Month} AND YEAR(DateOfExpence) = {dateTime.Year} AND DAY(DateOfExpence) = {dateTime.Day}  " +
                                                                    $"WHERE ExpenceCategories.ExpenceCategoryId = {i} " +
                                                                    "GROUP BY ExpenceCategories.[CategoryName] ");
                await sqlDataReader.ReadAsync();
                if (sqlDataReader[$"CAT{i}_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
                else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"]) / sum * 100, 2)}%");
                sqlDataReader.Close();
            }
        }

        #endregion

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillingAccountsList(int userId)
        {
            var accounts = expenceController.GetUserAccounts(userId);
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
