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
    public partial class SerialPortForm : Form
    {
        public SerialPortForm()
        {
            InitializeComponent();
        }

        private void SerialPortForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmTerminal mainForm = new frmTerminal();
            mainForm.testDataPort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmTerminal mainForm = new frmTerminal();
            mainForm.testDataPort();
        }
    }
}
