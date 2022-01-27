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
    public partial class IncomeForm : Form
    {
        private SqlDataReader sqlDataReader = null;
        public IncomeForm()
        {
            InitializeComponent();
        }

        private async void IncomeForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged_1;
            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            dateTimePicker1.ValueChanged -= dateTimePicker1_ValueChanged_1;
            await ListBoxLoadAsync();
            await ComboBoxLoadAsync();
            listBox3.Hide();
            button2.Hide();
            button2.Enabled = false;
            listBox1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged_1;
        }


        private async Task ListBoxLoad()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [CategoryName] FROM [IncomeCategories]");
            while (await sqlDataReader.ReadAsync())
            {
                listBox1.Items.Add(Convert.ToString(sqlDataReader["CategoryName"]));
            }
            sqlDataReader.Close();
        }

        private async Task ListBoxLoadAsync()
        {
            await ListBoxLoad();
        }

        private async Task ComboBoxLoad()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountName] FROM [Accounts] WHERE [UserId] = {StateClass.CurrentUserId}");
            while (await sqlDataReader.ReadAsync())
            {
                comboBox1.Items.Add(Convert.ToString(sqlDataReader["AccountName"]));
            }
            sqlDataReader.Close();
            comboBox2.Items.Add("Day");
            comboBox2.Items.Add("Month");
            comboBox2.Items.Add("Year");
            comboBox2.SelectedItem = "Month";
        }

        private async Task ComboBoxLoadAsync()
        {
            await ComboBoxLoad();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                var income = textBox2.Text.Replace(",", ".");
                var note = textBox3.Text;
                //Получение ID категории трат
                var sqlReader = await DbConnection.ExecuteSqlCommand($"SELECT [IncomeCategoryId] FROM [IncomeCategories] WHERE [CategoryName] = '{Convert.ToString(listBox1.SelectedItem)}'");
                await sqlReader.ReadAsync();
                var expenceCategoryId = Convert.ToInt32(sqlReader["IncomeCategoryId"]);
                sqlReader.Close();
                //Получение ID аккаунта трат
                sqlReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountId] FROM [Accounts] WHERE [AccountName] = '{Convert.ToString(comboBox1.SelectedItem)}' AND [UserId] = {StateClass.CurrentUserId}");
                await sqlReader.ReadAsync();
                var accountId = Convert.ToInt32(sqlReader["AccountId"]);
                sqlReader.Close();
                //Добавление затрат в базу
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [Incomes] ([DateOfIncome], [Note], [Income], [AccountId], [IncomeCategotyId]) VALUES (GETDATE(),'{note}',{income},{accountId},{expenceCategoryId})");
                MessageBox.Show("You add new income!");
                listBox1_SelectedIndexChanged_1(this, new EventArgs());
            }
        }

        private async void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            button2.Hide();
            button2.Enabled = false;
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            sqlDataReader.Close();
            var selectedItem = listBox1.SelectedItem.ToString();

            //Получение ID категории затрат
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [IncomeCategoryId] FROM [IncomeCategories] WHERE [CategoryName] = '{selectedItem}'");
            await sqlDataReader.ReadAsync();
            var incomeCategoryId = Convert.ToInt32(sqlDataReader["IncomeCategoryId"]);
            sqlDataReader.Close();
            //Получение ID аккаунта
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountId] FROM [Accounts] WHERE [AccountName] = '{comboBox1.SelectedItem}'");
            await sqlDataReader.ReadAsync();
            var accountId = Convert.ToInt32(sqlDataReader["AccountId"]);
            sqlDataReader.Close();
            textBox1.Text = Convert.ToString(accountId);
            //Заполнение списка iD затрат
            //заполнение списка затрат
            if (Convert.ToString(comboBox2.SelectedItem) == "Month") await FillingListboxesForMonth(accountId, incomeCategoryId);
            if (Convert.ToString(comboBox2.SelectedItem) == "Day") await FillingListboxesForDay(accountId, incomeCategoryId);
            if (Convert.ToString(comboBox2.SelectedItem) == "Year") await FillingListboxesForYear(accountId, incomeCategoryId);
            if (listBox2.Items.Count > 0) button2.Show();
        }
        #region Filling Listboxes
        private async Task FillingListboxesForMonth(int accountId, int incomeCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [IncomeId] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] = {incomeCategoryId} AND MONTH([DateOfIncome]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["IncomeId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Income],[DateOfIncome] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] = {incomeCategoryId} AND MONTH([DateOfIncome]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Income"] + "    " + sqlDataReader["DateOfIncome"]));
            }
            sqlDataReader.Close();
        }

        private async Task FillingListboxesForDay(int accountId, int incomeCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [IncomeId] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] = {incomeCategoryId} AND MONTH([DateOfIncome]) =  {dateTimePicker1.Value.Month} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year} AND DAY([DateOfIncome]) = {dateTimePicker1.Value.Day}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["IncomeId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Income],[DateOfIncome] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] = {incomeCategoryId} AND MONTH([DateOfIncome]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year} AND DAY([DateOfIncome]) = {dateTimePicker1.Value.Day} ");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Income"] + "    " + sqlDataReader["DateOfIncome"]));
            }
            sqlDataReader.Close();
        }

        private async Task FillingListboxesForYear(int accountId, int incomeCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [IncomeId] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] = {incomeCategoryId} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["IncomeId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Income],[DateOfIncome] FROM [Incomes] WHERE [AccountId] = {accountId} AND [IncomeCategoryId] =  {incomeCategoryId} AND YEAR([DateOfIncome]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Income"] + "    " + sqlDataReader["DateOfIncome"]));
            }
            sqlDataReader.Close();
        }
        #endregion
        private void expencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpenceForm expenceForm = new ExpenceForm();
            this.Close();
            expenceForm.Show();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged_1(this, new EventArgs());
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged_1(this, new EventArgs());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if (e.KeyChar == ',')
            {
                if (textBox2.Text.IndexOf(',') != -1)
                {
                    e.Handled = true;
                }
                return;
            }

            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Enter)
                    button1.Focus();
                return;
            }

            e.Handled = true;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.SelectedIndex = listBox2.SelectedIndex;
            button2.Enabled = true;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IncomeDetails details = new IncomeDetails(Convert.ToInt32(listBox3.SelectedItem));
            details.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged_1(this, new EventArgs());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IncomeChart incomeChart = new IncomeChart(textBox1.Text, comboBox2.Text, dateTimePicker1.Value);
            incomeChart.Show();
        }
    }
}
