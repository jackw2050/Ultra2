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
    public partial class UserDataForm : Form
    {
        public UserDataForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void screenFilterNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            frmTerminal.screenFilter = (int)screenFilterNumericUpDown.Value;
        }


        private void dataAquisitionModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmTerminal.dataAquisitionMode = dataAquisitionModeComboBox.SelectedText;
        }
    }
}
