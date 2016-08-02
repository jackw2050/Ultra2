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
                    passwordValid = "PasswordValid";
                    PasswordsClass.userPasswordValid = true;
                }
                else
                {
                    MessageBox.Show("The password you entered is invalid.", "Critical Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    passwordValid = "passwordInvalid";
                    PasswordsClass.userPasswordValid = false;
                }

            }
            else if (Convert.ToString(userComboBox.SelectedItem) == "manager")
            {
                if (passwordTextBox.Text == Properties.Settings.Default.managerPassword)
                {
                    passwordValid = "PasswordValid";
                    PasswordsClass.mgrPasswordValid = true;
                }
                else
                {
                    MessageBox.Show("The password you entered is invalid.", "Critical Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    passwordValid = "passwordInvalid";
                    PasswordsClass.mgrPasswordValid = false;
                }
            }
            else if (Convert.ToString( userComboBox.SelectedItem) ==  "ZLS")
            
                if (passwordTextBox.Text == Properties.Settings.Default.zlsPassword)
                {
                    passwordValid = "PasswordValid";
                    PasswordsClass.engPasswordValid = true;
                }
                else
                {
                    MessageBox.Show("The password you entered is invalid.", "Critical Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    passwordValid = "passwordInvalid";
                    PasswordsClass.engPasswordValid = false;
                }
            if (passwordValid == "PasswordValid")
            {
                this.Hide();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passwordValid = "passwordInvalid";
            this.Hide();
        }
    }
}
