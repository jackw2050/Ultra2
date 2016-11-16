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
    public partial class AutoStart : Form
    {
        frmTerminal frmTerminal = new frmTerminal();
        Data Data = new Data();
        public Single springTensionTarget = -999;
        public AutoStart()
        {
            InitializeComponent();
            springTensionTargetNumericTextBoxAutostart.Text = Convert.ToString( Data.SpringTension );
        }

        private void button1_Click(object sender, EventArgs e)
        {
             springTensionTarget = Convert.ToSingle(springTensionTargetNumericTextBoxAutostart.Text);
            // validate spring tenstion
            if (springTensionTarget < 0 | springTensionTarget > frmTerminal.springTensionMax)
            {
                MessageBox.Show("Invalid Spring Tension Entered\nEnter value between 0 and " + frmTerminal.springTensionMax,
                        "Critical Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
            else
            {
                frmTerminal.springTensionValid = true;
                this.Hide();
            }
        }
    }
}
