using System;
using System.Windows.Forms;
using WinFormsApp1.Controllers;


namespace WinFormsApp1
{
    public partial class RegisterForm : Form
    {
        private const string SUCCESSFUL_REGISTRATION_MESSAGE = "Successfull registration";
        private const string FAILED_REGISTRATION_MESSAGE = "Application already has user with this username";
        private const string NOT_CONFIRMED_PASSWORD_MESSAGE = "Please repeat your password correctly";
        private UserController userController;
        public RegisterForm()
        {
            InitializeComponent();
            userController = new UserController();
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            FormManager.OpenNewFormWithClosingOldForm(new SignInForm(),this);
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            bool passwordConfirmed = textBox2.Text == textBox3.Text;
            if (passwordConfirmed)
            {
                if (!await userController.IsRegistered(textBox1.Text, textBox2.Text))
                {
                    await userController.AddNewUser(textBox1.Text, textBox2.Text);
                    MessageBox.Show(SUCCESSFUL_REGISTRATION_MESSAGE);
                    FormManager.OpenNewFormWithClosingOldForm(new SignInForm(), this);
                }
                else
                {
                    MessageBox.Show(FAILED_REGISTRATION_MESSAGE);
                }
            }
            else
            {
                MessageBox.Show(NOT_CONFIRMED_PASSWORD_MESSAGE);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
