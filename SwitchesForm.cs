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
    public partial class SwitchesForm : Form
    {
        CalculateMarineData CMd = new CalculateMarineData();
        RelaySwitches RelaySwitches = new RelaySwitches();
        frmTerminal mainForm = new frmTerminal();
        ControlSwitches ControlSwitches = new ControlSwitches();

        public int set = 1;
        public int enable = 1;
        public int clear = 0;
        public int disable = 0;
        public SwitchesForm()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = { 0x01, 0x08, 0x09 };                                        //HexStringToByteArray(txtSendData.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // turn on 200 Hz
            // RelaySwitches.relay200Hz = enable;// set bit 0  high to turn on 200 Hz
            //  RelaySwitches.slew4 = enable;
            //   RelaySwitches.slew5 = enable;
            RelaySwitches.stepperMotorEnable = enable;

            //    RelaySwitches.RelaySwitchCalculate();// 0x80
            RelaySwitches.RelaySW = 0x80;// cmd 0
            mainForm.sendCmd("Send Relay Switches");           // 0 ----
            mainForm.sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
            mainForm.sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
            mainForm.sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----

            ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;

            mainForm.sendCmd("Send Control Switches");           // 1 ----
            mainForm.sendCmd("Send Control Switches");           // 1 ----
        }


    }
}