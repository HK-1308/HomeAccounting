using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GoToRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
            register.Activate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void SignIn_Click(object sender, EventArgs e)
        {
           
            var sqlDataReader = await DbConnection.ExecuteSqlCommand($"SELECT * FROM [Users] WHERE [UserName] = '{textBox1.Text}' AND [Password] = '{textBox2.Text}' ");
            if (sqlDataReader.HasRows)
            {
                await sqlDataReader.ReadAsync();
                StateClass.CurrentUserId = Convert.ToInt32(sqlDataReader["UserId"]);
                sqlDataReader.Close();
                ExpenceForm form = new ExpenceForm();
                this.Hide();
                form.Show();
            }
            //DIALOGSHOW
            else MessageBox.Show("You may forgot username or password!");
        }
    }
}
