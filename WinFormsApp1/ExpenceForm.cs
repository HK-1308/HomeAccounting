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
    public partial class ExpenceForm : Form
    {
        private SqlDataReader sqlDataReader = null;
        public ExpenceForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            dateTimePicker1.ValueChanged -= dateTimePicker1_ValueChanged;
            await ListBoxLoadAsync();
            await ComboBoxLoadAsync();
            //textBox1.Hide();
            listBox3.Hide();
            button2.Hide();
            button2.Enabled = false;
            listBox1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
        }


        private async Task ListBoxLoad()
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [CategoryName] FROM [ExpenceCategories]");
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



        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Hide();
            button2.Enabled = false;
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            sqlDataReader.Close();
            var selectedItem = listBox1.SelectedItem.ToString();
            
            //Получение ID категории затрат
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [ExpenceCategoryId] FROM [ExpenceCategories] WHERE [CategoryName] = '{selectedItem}'");
            await sqlDataReader.ReadAsync();
            var expenceCategoryId = Convert.ToInt32(sqlDataReader["ExpenceCategoryId"]);
            sqlDataReader.Close();
            //Получение ID аккаунта
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountId] FROM [Accounts] WHERE [AccountName] = '{comboBox1.SelectedItem}'");
            await sqlDataReader.ReadAsync();
            var accountId = Convert.ToInt32(sqlDataReader["AccountId"]);
            sqlDataReader.Close();
            textBox1.Text = Convert.ToString(accountId);
            //Заполнение списка iD затрат
            //заполнение списка затрат
            if (Convert.ToString(comboBox2.SelectedItem) == "Month") await FillingListboxesForMonth(accountId,expenceCategoryId);
            if (Convert.ToString(comboBox2.SelectedItem) == "Day") await FillingListboxesForDay(accountId, expenceCategoryId);
            if (Convert.ToString(comboBox2.SelectedItem) == "Year") await FillingListboxesForYear(accountId, expenceCategoryId);
            if (listBox2.Items.Count > 0) button2.Show();
        }

        #region Filling Listboxes
        private async Task FillingListboxesForMonth(int accountId, int expenceCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [ExpenceId] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND MONTH([DateOfExpence]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["ExpenceId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Expence],[DateOfExpence] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND MONTH([DateOfExpence]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Expence"] + "    " + sqlDataReader["DateOfExpence"]));
            }
            sqlDataReader.Close();
        }

        private async Task FillingListboxesForDay(int accountId, int expenceCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [ExpenceId] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND MONTH([DateOfExpence]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year} AND DAY([DateOfExpence]) = {dateTimePicker1.Value.Day}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["ExpenceId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Expence],[DateOfExpence] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND MONTH([DateOfExpence]) = {dateTimePicker1.Value.Month} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year} AND DAY([DateOfExpence]) = {dateTimePicker1.Value.Day} ");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Expence"] + "    " + sqlDataReader["DateOfExpence"]));
            }
            sqlDataReader.Close();
        }

        private async Task FillingListboxesForYear(int accountId, int expenceCategoryId)
        {
            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [ExpenceId] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox3.Items.Add(Convert.ToString(sqlDataReader["ExpenceId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [Expence],[DateOfExpence] FROM [Expences] WHERE [AccountId] = {accountId} AND [ExpenceCategoryId] = {expenceCategoryId} AND YEAR([DateOfExpence]) = {dateTimePicker1.Value.Year}");
            while (await sqlDataReader.ReadAsync())
            {
                listBox2.Items.Add(Convert.ToString(sqlDataReader["Expence"] + "    " + sqlDataReader["DateOfExpence"]));
            }
            sqlDataReader.Close();
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null)
            {
                var expence = textBox2.Text.Replace(",", ".");
                var note = textBox3.Text;
                //Получение ID категории трат
                var sqlReader = await DbConnection.ExecuteSqlCommand($"SELECT [ExpenceCategoryId] FROM [ExpenceCategories] WHERE [CategoryName] = '{Convert.ToString(listBox1.SelectedItem)}'");
                await sqlReader.ReadAsync();
                var expenceCategoryId = Convert.ToInt32(sqlReader["ExpenceCategoryId"]);
                sqlReader.Close();
                //Получение ID аккаунта трат
                sqlReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountId] FROM [Accounts] WHERE [AccountName] = '{Convert.ToString(comboBox1.SelectedItem)}' AND [UserId] = {StateClass.CurrentUserId}");
                await sqlReader.ReadAsync();
                var accountId = Convert.ToInt32(sqlReader["AccountId"]);
                sqlReader.Close();
                //Добавление затрат в базу
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [Expences] ([DateOfExpence], [Note], [Expence], [AccountId], [ExpenceCategoryId]) VALUES (GETDATE(),'{note}',{expence},{accountId},{expenceCategoryId})");
                MessageBox.Show("You add new expence!");
                listBox1_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExpenceDetails details = new ExpenceDetails(Convert.ToInt32(listBox3.SelectedItem));
            details.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            IncomeForm incomeForm = new IncomeForm();
            this.Close();
            incomeForm.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.SelectedIndex = listBox2.SelectedIndex;
            button2.Enabled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExpenceChart expenceChart = new ExpenceChart(textBox1.Text,comboBox2.Text,dateTimePicker1.Value);
            expenceChart.Show();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }


    
}
