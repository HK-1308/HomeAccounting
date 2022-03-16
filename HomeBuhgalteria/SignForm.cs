using System;
using System.Windows.Forms;
using WinFormsApp1.Controllers;

namespace WinFormsApp1
{
    public partial class SignInForm : Form
    {
        private const string FORGETED_PASSWORD_OR_USERNAME_MESSAGE = "You may forgot username or password!"; 
        private UserController userController; 
        public SignInForm()
        {
            InitializeComponent();
            userController = new UserController();
        }

        private void GoToRegister_Click(object sender, EventArgs e)
        {
            FormManager.OpenForm(new RegisterForm(),this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void SignIn_Click(object sender, EventArgs e)
        {
            
            if (await userController.IsRegistered(textBox1.Text,textBox2.Text))
            {
                FormManager.OpenForm(new ExpenceChart(),this);
            }
            else MessageBox.Show(FORGETED_PASSWORD_OR_USERNAME_MESSAGE);
        }


    }
}
