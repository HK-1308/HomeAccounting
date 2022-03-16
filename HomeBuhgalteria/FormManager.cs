using System.Windows.Forms;

namespace WinFormsApp1
{
    public static class FormManager
    {
        public static void OpenForm(Form newForm, Form oldFrom)
        {
            oldFrom.Hide();
            newForm.Show();
            newForm.Activate();
        }
    }
}