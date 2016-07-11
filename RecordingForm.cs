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
    public partial class RecordingForm : Form
    {
        public RecordingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dailyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dailyComboBox.SelectedText == "At Other Time")
            {
                refreshTimeComboBox.Visible = true;
            }
        }

        private void refreshTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (refreshTimeComboBox.SelectedText == "At Other Time")
            {
                dailyRefreshPanel.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            dailyRefreshPanel.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = true;
            dailyComboBox.Visible = false;
            refreshTimeComboBox.Visible = false;
            dailyRefreshPanel.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            hourlyComboBox.Visible = false;
            dailyComboBox.Visible = true;
            refreshTimeComboBox.Visible = false;
            dailyRefreshPanel.Visible = false;
        }
    }
}
