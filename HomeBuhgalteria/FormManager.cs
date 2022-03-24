using System.Windows.Forms;

namespace WinFormsApp1
{
    public static class FormManager
    {
        public static void OpenForm(Form newForm)
        {
            newForm.Show();
            newForm.Activate();
        }

        public static void OpenNewFormWithClosingOldForm(Form newForm, Form oldFrom)
        {
            oldFrom.Close();
            newForm.Show();
            newForm.Activate();
        }

        public static void OpenNewFormWithHidingOldForm(Form newForm, Form oldFrom)
        {
            oldFrom.Hide();
            newForm.Show();
            newForm.Activate();
        }
    }
}