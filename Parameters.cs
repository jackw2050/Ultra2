using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class Parameters : Form
    {
        public Parameters()
        {
            InitializeComponent();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void crossDampingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossDampingCheckBox.Checked)
            {
                crossDampingTextBox.Enabled = true;
            }
            else
            {
                crossDampingTextBox.Enabled = false;
            }
        }

        private void crossPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossPeriodCheckBox.Checked)
            {
                crossPeriodTextBox.Enabled = true;
            }
            else
            {
                crossPeriodTextBox.Enabled = false;
            }
        }

        private void crossGainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossGainCheckBox.Checked)
            {
                crossGainTextBox.Enabled = true;
            }
            else
            {
                crossGainTextBox.Enabled = false;
            }
        }

        private void crossLeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossLeadCheckBox.Checked)
            {
                crossLeadTextBox.Enabled = true;
            }
            else
            {
                crossLeadTextBox.Enabled = false;
            }
        }

        private void crossCompFactor4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompFactor4CheckBox.Checked)
            {
                crossCompFactor4TextBox.Enabled = true;
            }
            else
            {
                crossCompFactor4TextBox.Enabled = false;
            }
        }

        private void crossCompPhase4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompPhase4CheckBox.Checked)
            {
                crossCompPhase4TextBox.Enabled = true;
            }
            else
            {
                crossCompPhase4TextBox.Enabled = false;
            }
        }

        private void crossCompFactor16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompFactor16CheckBox.Checked)
            {
                crossCompFactor16TextBox.Enabled = true;
            }
            else
            {
                crossCompFactor16TextBox.Enabled = false;
            }
        }

        private void crossCompPhase16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompPhase16CheckBox.Checked)
            {
                crossCompPhase16TextBox.Enabled = true;
            }
            else
            {
                crossCompPhase16TextBox.Enabled = false;
            }
        }

        private void longPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longPeriodCheckBox.Checked)
            {
                longPeriodTextBox.Enabled = true;
            }
            else
            {
                longPeriodTextBox.Enabled = false;
            }
        }

        private void longDampingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longDampingCheckBox.Checked)
            {
                longDampingTextBox.Enabled = true;
            }
            else
            {
                longDampingTextBox.Enabled = false;
            }
        }

        private void longGainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longGainCheckBox.Checked )
            {
                longGainTextBox.Enabled = true;
            }
            else
            {
                longGainTextBox.Enabled = false;
            }
        }

        private void longLeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longLeadCheckBox.Checked)
            {
                longLeadTextBox.Enabled = true;
            }
            else
            {
                longLeadTextBox.Enabled = false;
            }
        }

        private void longCompFactor4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompFactor4CheckBox.Checked)
            {
                longCompFactor4TextBox.Enabled = true;
            }
            else
            {
                longCompFactor4TextBox.Enabled = false;
            }
        }

        private void longCompPhase4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompPhase4CheckBox.Checked)
            {
                longCompPhase4TextBox.Enabled = true;
            }
            else
            {
                longCompPhase4TextBox.Enabled = false;
            }
        }

        private void longCompFactor16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompFactor16CheckBox.Checked)
            {
                longCompFactor16TextBox.Enabled = true;
            }
            else
            {
                longCompFactor16TextBox.Enabled = false;
            }
        }

        private void longCompPhase16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompPhase16CheckBox.Checked)
            {
                longCompPhase16TextBox.Enabled = true;
            }
            else
            {
                longCompPhase16TextBox.Enabled = false;
            }
        }
    }
}
