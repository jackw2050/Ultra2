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
    public partial class DateTimeForm : Form
    {
        private DateTime tempDateTime = new DateTime();
        public DateTimeForm()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy     HH:mm:ss";



            dayLabel.Text = dateTimePicker1.Value.DayOfYear.ToString();

            var d = dateTimePicker1.Value.DayOfYear;
         //   Console.WriteLine(d);


            DateTime iDate;
            iDate = dateTimePicker1.Value;
            tempDateTime = iDate;

            //              add code here to set embedded processor date/ time
        }

        private void setDateTimeButton_Click(object sender, EventArgs e)
        {
            setDateTimeSuccessLabel.Text = "Set time successful";
            setDateTimeSuccessLabel.Visible = true;
            frmTerminal mainForm = new frmTerminal();
            mainForm.myDatetime = tempDateTime;
            mainForm.sendCmd("Update Date and Time");
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmTerminal mainForm = new frmTerminal();
            tempDateTime = DateTime.Now;
            textBox1.Text = Convert.ToString(DateTime.Now);
            dayLabel.Text = tempDateTime.DayOfYear.ToString();
            mainForm.myDatetime = tempDateTime;
            mainForm.sendCmd("Update Date and Time");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmTerminal mainForm = new frmTerminal();
            tempDateTime = DateTime.UtcNow;
            dayLabel.Text = tempDateTime.DayOfYear.ToString();
            textBox1.Text = Convert.ToString(DateTime.UtcNow);
            mainForm.myDatetime = tempDateTime;
            mainForm.sendCmd("Update Date and Time");
        }
    }
}
