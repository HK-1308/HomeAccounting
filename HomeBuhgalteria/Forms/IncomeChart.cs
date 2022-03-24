using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class IncomeChart : Form
    {
        private IncomeController incomeController;

        private UserController userController;

        private List<Account> accounts;

        private List<IncomeCategory> categories;

        private int selectedIncomeCategoryId;

        private int selectedAccountId;

        private const int DEFAULT_INDEX = 0;

        private const string MONTHLY_FORMAT = "MMMM yyyy";

        private const string YEARLY_FORMAT = "yyyy";

        private const string DAILY_FORMAT = "dd/MMM/yyyy";

        private string[] periods = new[] { "Month", "Day", "Year" };

        private DataTable incomeDataTable;

        private DateTime dateTime;

        public IncomeChart()
        {
            InitializeComponent();
            incomeController = new IncomeController();
            userController = new UserController();
            dateTime = DateTime.Today;
            incomeDataTable = new DataTable();
        }

        private async void IncomeChart_Load(object sender, EventArgs e)
        {
            timePeriodComboBox.Items.AddRange(periods);
            SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
            await FillingAccountsList(CurrentUser.UserId);
            await FillingCategoriesList();
            CreateIncomeTable();
            SetDefaultSelectedIndex();
            selectedAccountId = accounts.Find(u => u.AccountName == accountComboBox.Text).AccountId;
            await ShowIncomesByPeriod(timePeriodComboBox.SelectedItem.ToString());

        }

        private void CreateIncomeTable()
        {
            incomeDataTable.Columns.Add("Category");
            incomeDataTable.Columns.Add("Income");
            incomeDataTable.Columns.Add("Share");
            dataGridView1.DataSource = incomeDataTable;
        }


        private void SetDefaultSelectedIndex()
        {
            timePeriodComboBox.SelectedIndexChanged -= timePeriodComboBox_SelectedIndexChanged;
            accountComboBox.SelectedIndexChanged -= accountComboBox_SelectedIndexChanged;
            timePeriodComboBox.SelectedIndex = DEFAULT_INDEX;
            accountComboBox.SelectedIndex = DEFAULT_INDEX;
            categoryComboBox.SelectedIndex = DEFAULT_INDEX;
            timePeriodComboBox.SelectedIndexChanged += timePeriodComboBox_SelectedIndexChanged;
            accountComboBox.SelectedIndexChanged += accountComboBox_SelectedIndexChanged;
        }

        private void SetCustomFormatForDatetimePicker(string format)
        {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = format;
            dateTimePicker.ShowUpDown = true;
        }

        private async Task FillingAccountsList(int userId)
        {
            accounts = await userController.GetUserAccounts(userId);
            foreach (var account in accounts)
            {
                accountComboBox.Items.Add(account.AccountName);
            }
        }

        private async Task FillingCategoriesList()
        {
            categories = await incomeController.GetCategories();
            foreach (var category in categories)
            {
                categoryComboBox.Items.Add(category.CategoryName);
            }
        }

        private async Task ShowIncomesByPeriod(string timePeriod)
        {
            incomeDataTable.Rows.Clear();
            switch (timePeriod)
            {
                case "Month":
                    SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
                    await ShowMonthlyIncome();
                    break;
                case "Year":
                    SetCustomFormatForDatetimePicker(YEARLY_FORMAT);
                    await ShowYearlyIncome();
                    break;
                case "Day":
                    SetCustomFormatForDatetimePicker(DAILY_FORMAT);
                    await ShowDailyIncome();
                    break;
                default:
                    SetCustomFormatForDatetimePicker(MONTHLY_FORMAT);
                    await ShowMonthlyIncome();
                    break;
            }
        }


        private async Task ShowMonthlyIncome()
        {
            List<SummerizedIncomesByCategory> summerizedMonthlyIncomesByCategory =
                await incomeController.CollectMonthlyIncomeInfo(dateTime, selectedAccountId);
            foreach (var summerizedMonthlyIncomeByCategory in summerizedMonthlyIncomesByCategory)
            {
                incomeDataTable.Rows.Add(summerizedMonthlyIncomeByCategory.CategoryName,
                    summerizedMonthlyIncomeByCategory.IncomeSum, $"{summerizedMonthlyIncomeByCategory.IncomePersent}%");
            }

            label1.Text = "Monthly incomes:";
        }

        private async Task ShowYearlyIncome()
        {
            List<SummerizedIncomesByCategory> summerizedYearlyIncomesByCategory =
                await incomeController.CollectYearlyIncomeInfo(dateTime, selectedAccountId);
            foreach (var summerizedYearlyIncomeByCategory in summerizedYearlyIncomesByCategory)
            {
                incomeDataTable.Rows.Add(summerizedYearlyIncomeByCategory.CategoryName,
                    summerizedYearlyIncomeByCategory.IncomeSum, $"{summerizedYearlyIncomeByCategory.IncomePersent}%");
            }

            label1.Text = "Yearly incomes:";
        }

        private async Task ShowDailyIncome()
        {
            List<SummerizedIncomesByCategory> summerizedDailyIncomesByCategory =
                await incomeController.CollectDailyIncomeInfo(dateTime, selectedAccountId);
            foreach (var summerizedDailyIncomeByCategory in summerizedDailyIncomesByCategory)
            {
                incomeDataTable.Rows.Add(summerizedDailyIncomeByCategory.CategoryName,
                    summerizedDailyIncomeByCategory.IncomeSum, $"{summerizedDailyIncomeByCategory.IncomePersent}%");
            }

            label1.Text = "Daily incomes:";

        }


        private void expensesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormManager.OpenNewFormWithClosingOldForm(new ExpenceChart(), this);
        }

        private async void accountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAccountId = accounts.Find(u => u.AccountName == accountComboBox.Text).AccountId;
            await ShowIncomesByPeriod(timePeriodComboBox.SelectedItem.ToString());

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIncomeCategoryId = categoryComboBox.SelectedIndex + 1;
        }

        private async void addIncomeButton_Click(object sender, EventArgs e)
        {
            if (incomeAmountTextBox.Text != null)
            {
                var incomeAmount = incomeAmountTextBox.Text.Replace(',', '.');
                await incomeController.AddNewIncome(incomeAmount, selectedIncomeCategoryId, selectedAccountId,
                    noteTextBox.Text);
                await ShowIncomesByPeriod(timePeriodComboBox.SelectedItem.ToString());
            }

        }

        private async void timePeriodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await ShowIncomesByPeriod(timePeriodComboBox.SelectedItem.ToString());
        }

        private async void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            dateTime = dateTimePicker.Value;
            await ShowIncomesByPeriod(timePeriodComboBox.SelectedItem.ToString());

        }

        private void incomeAmount_KeyPress(object sender, KeyPressEventArgs e)
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
                if (incomeAmountTextBox.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }

                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    addIncomeButton.Focus();
                return;
            }

            e.Handled = true;
        }

    }
}
