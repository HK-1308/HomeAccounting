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
    public partial class IncomeChart : Form
    {
        private SqlDataReader sqlDataReader = null;

        private string accountId = "";

        private string periodItem = "";

        private DateTime DateTime;
        public IncomeChart()
        {
            InitializeComponent();
        }

        public IncomeChart(string accountId, string periodItem, DateTime dateTime)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Month", "Day", "Year" });
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedItem = periodItem;
            this.accountId = accountId;
            this.periodItem = periodItem;
            this.DateTime = dateTime;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private async void IncomeChart_Load(object sender, EventArgs e)
        {
            if (periodItem == "Month") MonthlyRoutine();
            if (periodItem == "Year") YearRoutine();
            if (periodItem == "Day") DayRoutine();
        }

        private async void MonthlyRoutine()
        {
            await CountMonthSum();
            await sqlDataReader.ReadAsync();
            decimal sum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            sqlDataReader.Close();
            listBox1FillMonthly(sum);
            label1.Text = "Monthly incomes:";
        }

        private async void YearRoutine()
        {
            await CountYearSum();
            await sqlDataReader.ReadAsync();
            decimal sum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            sqlDataReader.Close();
            listBox1FillYearly(sum);
            label1.Text = "Yearly incomes:";
        }

        private async void DayRoutine()
        {
            await CountDaySum();
            await sqlDataReader.ReadAsync();
            decimal sum = sqlDataReader["ALL_SUM"] is DBNull ? 0 : Convert.ToDecimal(sqlDataReader["ALL_SUM"]);
            sqlDataReader.Close();
            listBox1FillDayly(sum);
            label1.Text = "Dayly incomes:";
        }

        private async Task CountMonthSum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Incomes.Income) AS ALL_SUM"
                                                                + " FROM Incomes " +
                                                                $"WHERE AccountId = {accountId} AND MONTH([DateOfIncome]) = {DateTime.Month} AND YEAR([DateOfIncome]) = {DateTime.Year}");
        }

        private async Task CountYearSum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Incomes.Income) AS ALL_SUM"
                                                    + " FROM Incomes " +
                                                    $"WHERE AccountId = {accountId} AND YEAR(DateOfIncome) = {DateTime.Year}");
        }

        private async Task CountDaySum()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT SUM(Incomes.Income) AS ALL_SUM"
                                                    + " FROM Incomes " +
                                                    $"WHERE AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day}");
        }

        #region LISTBOXFILLING
        private async void listBox1FillMonthly(decimal sum)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT1_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 1 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT1_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT1_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100}%");
            sqlDataReader.Close();
            /////////////////////
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT2_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 2 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT2_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT2_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT2_SUM"]) / sum * 100}%");
            sqlDataReader.Close();
            /////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT3_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 3 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT3_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT3_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT3_SUM"]) / sum * 100}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT4_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 4 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT4_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT4_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT4_SUM"]) / sum * 100}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT5_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 5 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT5_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT5_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT5_SUM"]) / sum * 100}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT6_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 6 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT6_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT6_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT6_SUM"]) / sum * 100}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT8_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 8 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT7_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT7_SUM"])}  {Convert.ToDecimal(sqlDataReader["CAT7_SUM"]) / sum * 100}%");
            //sqlDataReader.Close();
        }

        private async void listBox1FillYearly(decimal sum)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT1_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 1 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT1_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT1_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            sqlDataReader.Close();
            /////////////////////
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT2_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 2 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT2_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT2_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT2_SUM"]) / sum * 100, 2)}%");
            sqlDataReader.Close();
            /////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT3_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 3 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT3_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT3_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT3_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT4_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 4 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT4_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT4_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT4_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT5_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 5 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT5_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT5_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT5_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT6_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 6 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT6_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT6_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT6_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT7_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId}  AND YEAR(DateOfIncome) = {DateTime.Year} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 7 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT7_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT7_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT7_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
        }

        private async void listBox1FillDayly(decimal sum)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT1_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 1 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT1_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT1_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            /////////////////////
            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT2_SUM "
                                                                + "FROM IncomeCategories " +
                                                                $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
                                                                "WHERE IncomeCategories.IncomeCategoryId = 2 " +
                                                                "GROUP BY IncomeCategories.[CategoryName] ");
            await sqlDataReader.ReadAsync();
            if (sqlDataReader["CAT2_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT2_SUM"])} {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            sqlDataReader.Close();
            /////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT3_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 3 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT3_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT3_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT4_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 4 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT4_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT4_SUM"])} {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT5_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 5 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT5_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT5_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT6_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 6 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT6_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT6_SUM"])}  {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
            ///////////////////////
            //sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [CategoryName],SUM(Incomes.Income) AS CAT7_SUM "
            //                                                    + "FROM IncomeCategories " +
            //                                                    $"LEFT JOIN Incomes ON IncomeCategories.IncomeCategoryId = Incomes.IncomeCategoryId AND AccountId = {accountId} AND MONTH(DateOfIncome) = {DateTime.Month} AND YEAR(DateOfIncome) = {DateTime.Year} AND DAY(DateOfIncome) = {DateTime.Day} " +
            //                                                    "WHERE IncomeCategories.IncomeCategoryId = 7 " +
            //                                                    "GROUP BY IncomeCategories.[CategoryName] ");
            //await sqlDataReader.ReadAsync();
            //if (sqlDataReader["CAT7_SUM"] is DBNull) listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  0  0%");
            //else listBox1.Items.Add($"{Convert.ToString(sqlDataReader["CategoryName"])}  {Convert.ToDecimal(sqlDataReader["CAT7_SUM"])} {Math.Round(Convert.ToDecimal(sqlDataReader["CAT1_SUM"]) / sum * 100, 2)}%");
            //sqlDataReader.Close();
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (Convert.ToString(comboBox1.SelectedItem) == "Month") MonthlyRoutine();
            if (Convert.ToString(comboBox1.SelectedItem) == "Year") YearRoutine();
            if (Convert.ToString(comboBox1.SelectedItem) == "Day") DayRoutine();
        }
    }
}
