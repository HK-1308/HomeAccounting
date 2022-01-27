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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [UserName] FROM [Users] WHERE [UserName] = '{textBox1.Text}' ");
            if (textBox2.Text == textBox2.Text && !sqlDataReader.HasRows)
            {
                sqlDataReader.Close();
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [Users] (Username,Password) VALUES ('{textBox1.Text}','{textBox2.Text}')");

                //Получение ID нового пользователя
                sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [UserId] FROM [Users] WHERE [UserName] = '{textBox1.Text}' ");
                await sqlDataReader.ReadAsync();
                int userId = Convert.ToInt32(sqlDataReader["UserId"]);
                sqlDataReader.Close();

                //Создание аккаунта по умолчанию
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [Accounts] (AccountName,UserId) VALUES ('Cash',{userId})");
                //Создание настроек пользователя по умолчанию
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [UserSettings] (Theme,Language,UserId) VALUES ('White','ENG',{userId})");

                //Получение ID аккаунта
                sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT [AccountId] FROM [Accounts] WHERE [UserId] = {userId}");
                await sqlDataReader.ReadAsync();
                int accountId = Convert.ToInt32(sqlDataReader["UserId"]);
                sqlDataReader.Close();

                //Создание настроек аккаунта по умолчанию
                await DbConnection.ExecuteNonQuerySqlCommand($"INSERT INTO [AccountSettings] ([CurrencyId],[AccountId]) VALUES (1,{accountId})");

                MessageBox.Show("Successfull registration");
                Form1 form = new Form1();
                this.Hide();
                form.Show();
            }
            MessageBox.Show("Application already has user with this username");
            sqlDataReader.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
