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
    public partial class ExpenceDetails : Form
    {
        private SqlDataReader sqlDataReader = null;
        private int expenceId = 0;
        public ExpenceDetails()
        {
            InitializeComponent();
        }

        public ExpenceDetails(int expenceId)
        {
            this.expenceId = expenceId;
            InitializeComponent();
        }



        private async void Details_Load(object sender, EventArgs e)
        {
            comboBox3.Hide();
            comboBox4.Hide();

            sqlDataReader = await DbConnection.ExecuteSqlCommand("SELECT [Expence], [Note], [DateOfExpence], [CategoryName], [AccountName]" +
                                                                 "FROM [Expences] INNER JOIN [ExpenceCategories] ON [ExpenceCategories].[ExpenceCategoryId] = [Expences].[ExpenceCategoryId]" +
                                                                 $"INNER JOIN [Accounts] ON [Accounts].[AccountId] = [Expences].[AccountId] WHERE [ExpenceId] = {expenceId}");
            await sqlDataReader.ReadAsync();
            textBox1.Text = Convert.ToString(sqlDataReader["Expence"]);
            dateTimePicker1.Value = Convert.ToDateTime(sqlDataReader["DateOfExpence"]);
            textBox3.Text = Convert.ToString(sqlDataReader["Note"]);
            string category = Convert.ToString(sqlDataReader["CategoryName"]);
            string account = Convert.ToString(sqlDataReader["AccountName"]);
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT * FROM [Accounts] WHERE [UserId] = {StateClass.CurrentUserId}");
            while (await sqlDataReader.ReadAsync())
            {
                comboBox2.Items.Add(Convert.ToString(sqlDataReader["AccountName"]));
                comboBox4.Items.Add(Convert.ToString(sqlDataReader["AccountId"]));
            }
            sqlDataReader.Close();

            sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT * FROM [ExpenceCategories]");
            while (await sqlDataReader.ReadAsync())
            {
                comboBox1.Items.Add(Convert.ToString(sqlDataReader["CategoryName"]));
                comboBox3.Items.Add(Convert.ToString(sqlDataReader["ExpenceCategoryId"]));
            }
            sqlDataReader.Close();

            comboBox2.SelectedItem = account;
            comboBox1.SelectedItem = category;
            comboBox4.SelectedIndex = comboBox2.SelectedIndex;
            comboBox3.SelectedIndex = comboBox1.SelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DbConnection.ExecuteNonQuerySqlCommand($"DELETE FROM Expences WHERE ExpenceId = {expenceId}");
            MessageBox.Show("You delete this expence");
            this.Close();
        }
        //10.02.2022 0:00:00
        private async void button1_Click(object sender, EventArgs e)
        {
            var expence = textBox1.Text.Replace(",", ".");
            await DbConnection.ExecuteNonQuerySqlCommand($"UPDATE [Expences] SET [Note] = '{textBox3.Text}', [Expence] = {expence}, [DateOfExpence] = '{dateTimePicker1.Value.Month}.{dateTimePicker1.Value.Day}.{dateTimePicker1.Value.Year} 0:00:00'," 
                                                        +$"[ExpenceCategoryId] = {comboBox3.Text}, [AccountId] = {comboBox4.Text} WHERE [ExpenceId] = {expenceId}");
            MessageBox.Show("You save your changes");
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = comboBox2.SelectedIndex;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
