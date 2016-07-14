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
    public partial class FileFormatForm : Form
    {
        public frmTerminal frmTerminal = new frmTerminal();
        public FileFormatForm()
        {
            InitializeComponent();
           
        }

        private void dateFormatRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton1.Checked == true)
            {
                frmTerminal.fileDateFormat = 1;
                UpdateFileNameText();
                //open text file to dump data
            }
        }

        private void dateFormatRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton2.Checked == true)
            {
                frmTerminal.fileDateFormat = 2;
                UpdateFileNameText();
            }
        }

        private void dateFormatRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (dateFormatRadioButton3.Checked == true)
            {
                frmTerminal.fileDateFormat = 3;
                UpdateFileNameText();
            }
        }

        private void customFileNameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.customFileNameRadioButton.Checked)
            {
                this.CustomNameTextBox.Visible = true;
                frmTerminal.fileDateFormat = 2;
            }
        }

        private void textRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (textRadioButton.Checked == true)
            {
                frmTerminal.fileType = "txt";
                UpdateFileNameText();
            }
        }

        private void commaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (commaRadioButton.Checked == true)
            {
                frmTerminal.fileType = "csv";
                UpdateFileNameText();
            }
        }

        private void tabRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tabRadioButton.Checked == true)
            {
                frmTerminal.fileType = "tsv";
                UpdateFileNameText();
            }
        }



        private void UpdateFileNameText()
        {
            DateTime now = DateTime.Now;
            if (frmTerminal.fileDateFormat == 1)
            {
                sampleFileNamelabel.Text = frmTerminal.meterNumber + "-" + frmTerminal.surveyName + "-" + now.ToString("yyyy-MMM-dd-HH-mm-ss") + "." + frmTerminal.fileType;
            }
            else if (frmTerminal.fileDateFormat == 2)
            {
                sampleFileNamelabel.Text = frmTerminal.meterNumber + "-" + frmTerminal.surveyName + "-" + now.ToString("yyyy-mm-dd-HH-mm-ss") + "." + frmTerminal.fileType;
            }
            else if (frmTerminal.fileDateFormat == 3)
            {
                sampleFileNamelabel.Text = frmTerminal.meterNumber + "-" + frmTerminal.surveyName + "-" + now.ToString("yyyy-dd-HH-mm-ss") + "." + frmTerminal.fileType;
            }
            else if (frmTerminal.fileDateFormat == 4)
            {
                sampleFileNamelabel.Text = CustomNameTextBox.Text + "." + frmTerminal.fileType;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
