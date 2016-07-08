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
        public DateTimeForm()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy     HH:mm:ss";



            dayOfYearLabel.Text = "Day  " + dateTimePicker1.Value.DayOfYear.ToString();

            var d = dateTimePicker1.Value.DayOfYear;
            Console.WriteLine(d);


            DateTime iDate;
            iDate = dateTimePicker1.Value;
            DateTime tempDateTime = iDate;

            //              add code here to set embedded processor date/ time
        }

        private void setDateTimeButton_Click(object sender, EventArgs e)
        {
            setDateTimeSuccessLabel.Text = "Set time successful";
            setDateTimeSuccessLabel.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
