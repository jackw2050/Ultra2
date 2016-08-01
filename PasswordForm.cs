using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class PasswordForm : Form
    {
        public static string passwordValid = null;
        frmTerminal fm = new frmTerminal();
       
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToString(userComboBox.SelectedItem) == "user")
            {
                if (passwordTextBox.Text == Properties.Settings.Default.userPassword)
                {
                    passwordValid = "userPasswordValid";
                    fm.userPasswordValid = true;
                }
                else
                {
                    passwordValid = "passwordInvalid";
                    fm.userPasswordValid = false;
                }

            }
            else if (Convert.ToString(userComboBox.SelectedItem) == "manager")
            {
                if (passwordTextBox.Text == Properties.Settings.Default.managerPassword)
                {
                    passwordValid = "managerPasswordValid";
                    fm.mgrPasswordValid = true;
                }
                else
                {
                    passwordValid = "passwordInvalid";
                    fm.mgrPasswordValid = false;
                }
            }
            else if (Convert.ToString( userComboBox.SelectedItem) ==  "ZLS")
            
                if (passwordTextBox.Text == Properties.Settings.Default.zlsPassword)
                {
                    passwordValid = "zlsPasswordValid";
                    fm.engPasswordValid = true;
                }
                else
                {
                    passwordValid = "passwordInvalid";
                    fm.engPasswordValid = false;
                }
            this.Hide();
        }
       

     
    }
}
