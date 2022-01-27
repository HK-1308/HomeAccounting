using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ExpenceChart : Form
    {
        private SqlDataReader sqlDataReader = null;

        private string accountId = "";

        private string periodItem = "";

        private DateTime DateTime;
        public ExpenceChart()
        {
            InitializeComponent();
        }

        public ExpenceChart(string accountId, string periodItem, DateTime dateTime)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] {"Month","Day","Year"});
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedItem = periodItem;
            this.accountId = accountId;
            this.periodItem = periodItem;
            this.DateTime = dateTime;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private async void ExpenceChart_Load(object sender, EventArgs e)
        {
            if (periodItem == "Month") MonthlyRoutine();
            if (periodItem == "Year") YearRoutine();
            if (periodItem == "Day") DayRoutine();
        }

        private async void MonthlyRoutine()
        {
            decimal sum = await CountMonthSum();
            int expenceCategoriesCount = await GetExpenceCategoryCount();
            sqlDataReader.Close();
            listBox1FillMonthly(sum, expenceCategoriesCount);
            label1.Text = "Monthly expences:";
        }

        private async void YearRoutine()
        {
            decimal sum  = await CountYearSum();
            int expenceCategoriesCount = await GetExpenceCategoryCount();
            sqlDataReader.Close();
            listBox1FillYearly(sum, expenceCategoriesCount);
            label1.Text = "Yearly expences:";
        }

        private async void DayRoutine()
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
                                                                $"WHERE AccountId = {accountId} AND MONTH(DateOfExpence) = {DateTime.Month} AND YEAR(DateOfExpence) = {DateTime.Year}");
            await sqlDataReader.ReadAsync();
            return sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
        }

        private async Task<decimal> CountYearSum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Expences.Expence) AS ALL_SUM"
                                                                + " FROM Expences " +
                                                                $"WHERE AccountId = {accountId} AND YEAR(DateOfExpence) = {DateTime.Year}");
            await sqlDataReader.ReadAsync();
            return sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
        }

        private async Task<decimal> CountDaySum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Expences.Expence) AS ALL_SUM"
                                                                + " FROM Expences " +
                                                                $"WHERE AccountId = {accountId} AND MONTH(DateOfExpence) = {DateTime.Month} AND YEAR(DateOfExpence) = {DateTime.Year} AND DAY(DateOfExpence) = {DateTime.Day}");
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
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND MONTH(DateOfExpence) = {DateTime.Month} AND YEAR(DateOfExpence) = {DateTime.Year} " +
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
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND YEAR(DateOfExpence) = {DateTime.Year} " +
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
                                                                    $"LEFT JOIN Expences ON ExpenceCategories.ExpenceCategoryId = Expences.ExpenceCategoryId AND AccountId = {accountId} AND MONTH(DateOfExpence) = {DateTime.Month} AND YEAR(DateOfExpence) = {DateTime.Year} AND DAY(DateOfExpence) = {DateTime.Day}  " +
                                                                    $"WHERE ExpenceCategories.ExpenceCategoryId = {i} " +
                                                                    "GROUP BY ExpenceCategories.[CategoryName] ");
                await sqlDataReader.ReadAsync();
                if (sqlDataReader[$"CAT{i}_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
                else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader[$"CAT{i}_SUM"]) / sum * 100, 2)}%");
                sqlDataReader.Close();
            }
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (Convert.ToString(comboBox1.SelectedItem) == "Month") MonthlyRoutine();
            if (Convert.ToString(comboBox1.SelectedItem) == "Year") YearRoutine();
            if (Convert.ToString(comboBox1.SelectedItem) == "Day") DayRoutine();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
