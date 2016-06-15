using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;



namespace SerialPortTerminal
{
    public partial class DataForm : Form
    {
        CalculateMarineData CMd = new CalculateMarineData();
        

        public DataForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
        }


    }
}