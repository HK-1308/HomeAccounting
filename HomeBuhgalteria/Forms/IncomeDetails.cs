using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    //TODO: При сохранении криво сохраняет дату
    public partial class IncomeDetails : Form
    {
        private SqlDataReader sqlDataReader = null;
        private int incomeId = 0;
        public IncomeDetails()
        {
            InitializeComponent();
        }

        public IncomeDetails(int expenceId)
        {
            this.incomeId = expenceId;
            InitializeComponent();
        }

        private async void IncomeDetails_Load(object sender, EventArgs e)
        {
            comboBox3.Hide();
            comboBox4.Hide();

            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [Income], [Note], [DateOfIncome], [CategoryName], [AccountName]" +
                                                                 "FROM [Incomes] INNER JOIN [IncomeCategories] ON [IncomeCategories].[IncomeCategoryId] = [Incomes].[IncomeCategoryId]" +
                                                                 $"INNER JOIN [Accounts] ON [Accounts].[AccountId] = [Incomes].[AccountId] WHERE [IncomeId] = {incomeId}");
            await sqlDataReader.ReadAsync();
            textBox1.Text = Convert.ToString(sqlDataReader["Income"]);
            dateTimePicker1.Value = Convert.ToDateTime(sqlDataReader["DateOfIncome"]);
            textBox3.Text = Convert.ToString(sqlDataReader["Note"]);
            string category = Convert.ToString(sqlDataReader["CategoryName"]);
            string account = Convert.ToString(sqlDataReader["AccountName"]);
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT * FROM [Accounts] WHERE [UserId] = {CurrentUser.UserId}");
            while (await sqlDataReader.ReadAsync())
            {
                comboBox2.Items.Add(Convert.ToString(sqlDataReader["AccountName"]));
                comboBox4.Items.Add(Convert.ToString(sqlDataReader["AccountId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT * FROM [IncomeCategories]");
            while (await sqlDataReader.ReadAsync())
            {
                comboBox1.Items.Add(Convert.ToString(sqlDataReader["CategoryName"]));
                comboBox3.Items.Add(Convert.ToString(sqlDataReader["IncomeCategoryId"]));
            }
            sqlDataReader.Close();

            comboBox2.SelectedItem = account;
            comboBox1.SelectedItem = category;
            comboBox4.SelectedIndex = comboBox2.SelectedIndex;
            comboBox3.SelectedIndex = comboBox1.SelectedIndex;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var income = textBox1.Text.Replace(",", ".");
            await DbConnection.ExecuteNonQuerySqlCommand($"UPDATE [Incomes] SET [Note] = '{textBox3.Text}', [Income] = {income}, [DateOfIncome] = '{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Year} 0:00:00',"
                                                        + $"[IncomeCategoryId] = {comboBox3.Text}, [AccountId] = {comboBox4.Text} WHERE [IncomeId] = {incomeId}");
            MessageBox.Show("You save your changes");
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DbConnection.ExecuteNonQuerySqlCommand($"DELETE FROM [Incomes] WHERE [IncomeId] = {incomeId}");
            MessageBox.Show("You delete this expence");
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
                if (textBox1.Text.IndexOf(',') != -1)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = comboBox2.SelectedIndex;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
