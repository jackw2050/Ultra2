using System;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class Switches : Form
    {
        private CalculateMarineData CMd = new CalculateMarineData();
        private RelaySwitches RelaySwitches = new RelaySwitches();

        private frmTerminal frmTerminal = new frmTerminal();
        private ControlSwitches ControlSwitches = new ControlSwitches();
       // private frmTerminal frmTerminal = new frmTerminal();
        private Comms Comms = new Comms();
        private ConfigData ConfigData = new ConfigData();

        public int set = 1;
        public int enable = 1;
        public int clear = 0;
        public int disable = 0;

        public Switches()
        {
            InitializeComponent();
        }

        private void DataForm_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] data = { 0x01, 0x08, 0x09 };  //HexStringToByteArray(txtSendData.Text);
            RelaySwitches.stepperMotorEnable(enable);
            
            //    RelaySwitches.RelaySwitchCalculate();// 0x80
            RelaySwitches.relaySW = 0x80;// cmd 0
            frmTerminal.sendCmd("Send Relay Switches");           // 0 ----
            frmTerminal.sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
            frmTerminal.sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
            frmTerminal.sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----

            ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;

            frmTerminal.sendCmd("Send Control Switches");           // 1 ----
            frmTerminal.sendCmd("Send Control Switches");           // 1 ----
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0xB1;// cmd 0
            frmTerminal.sendCmd("Send Relay Switches");           // 0 ----
            ControlSwitches.controlSw = 0x08; //ControlSwitches.RelayControlSW = 0x08;
            frmTerminal.sendCmd("Send Control Switches");           // 1 ----
        }
        private void button5_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x81;// cmd 0
            frmTerminal.sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ControlSwitches.controlSw = 0x08;// ControlSwitches.RelayControlSW = 0x08;
            frmTerminal.sendCmd("Send Control Switches");           // 1 ----
        }
        private void button7_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x80;// cmd 0
            frmTerminal.sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button8_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x81;// cmd 0
            frmTerminal.sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button9_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x83;// cmd 0
    //        frmTerminal.sendCmd("Send Relay Switches");// 0 ----
            ControlSwitches.TorqueMotor(enable);
            ControlSwitches.Alarm(enable);
            // ControlSwitches.controlSw = 0x09; // ControlSwitches.RelayControlSW = 0x09;
  //          frmTerminal.sendCmd("Send Control Switches");           // 1 ----
        }

        private void torqueMotorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if spring tension is enabled  alert("You must disable spring tension first");

            if (torqueMotorCheckBox.Checked == true)
            {
                RelaySwitches.relaySW = 0x83;// cmd 0
         //       frmTerminal.sendCmd("Send Relay Switches");// 0 ----
                ControlSwitches.TorqueMotor(enable);
                ControlSwitches.Alarm(enable);
                // ControlSwitches.controlSw = 0x09; // ControlSwitches.RelayControlSW = 0x09;
         //       frmTerminal.sendCmd("Send Control Switches");           // 1 ----
            }
            else
            {
                RelaySwitches.relaySW = 0x81;// cmd 0
                //Comms.sendCmd("Send Relay Switches");           // 0 ----
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void gyroCheckBox_CheckedChanged(object sender, EventArgs e)
        {


            if (gyroCheckBox.Checked == true)
            {
                
            }
            else
            {
                if (springTensionCheckBox.Checked == true)
                {
                    // alert Spring Tension must be disabled first
                }
                else if (torqueMotorCheckBox.Checked == true)
                {
                    // Torque Motor must be disabled first
                }
                else
                {
                    // disable gyro
                }
            }
        }

        private void springTensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionCheckBox.Checked == true)
            {
                
            }
            else
            {

            }
        }

        private void alarmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (alarmCheckBox.Checked == true)
            {
                
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

            byte[] data = { 0x01, 0x08, 0x09 };




          
            
          //  Log(LogMsgType.Outgoing, frmTerminal.ByteArrayToHexString(data) + "\n");
        }

        /*

                private void button5_Click(object sender, EventArgs e)
                {
                    RelaySwitches.RelaySW = 0xB1;// cmd 0
                    mainForm.sendCmd("Send Relay Switches");           // 0 ----
                    ControlSwitches.controlSw = 0x08; //ControlSwitches.RelayControlSW = 0x08;
                    mainForm.sendCmd("Send Control Switches");           // 1 ----
                }

                private void button6_Click(object sender, EventArgs e)
                {
                    ControlSwitches.controlSw = 0x08;// ControlSwitches.RelayControlSW = 0x08;
                    mainForm.sendCmd("Send Control Switches");           // 1 ----
                }

                private void button7_Click(object sender, EventArgs e)
                {
                    RelaySwitches.RelaySW = 0x80;// cmd 0
                    mainForm.sendCmd("Send Relay Switches");           // 0 ----
                }

                private void button8_Click(object sender, EventArgs e)
                {
                    RelaySwitches.RelaySW = 0x81;// cmd 0
                    mainForm.sendCmd("Send Relay Switches");           // 0 ----
                }

                private void button9_Click(object sender, EventArgs e)
                {
                    RelaySwitches.RelaySW = 0x83;// cmd 0
                    mainForm.sendCmd("Send Relay Switches");
                    // 0 ----
                    ControlSwitches.TorqueMotor(enable);
                    ControlSwitches.Alarm(enable);
                    // ControlSwitches.controlSw = 0x09; // ControlSwitches.RelayControlSW = 0x09;
                    mainForm.sendCmd("Send Control Switches");           // 1 ----
                }

                private void button10_Click(object sender, EventArgs e)
                {
                    RelaySwitches.RelaySW = 0x81;// cmd 0
                    mainForm.sendCmd("Send Relay Switches");           // 0 ----
                    ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
                    mainForm.sendCmd("Send Control Switches");           // 1 --
                }

                private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
                {
                }
         * */
    }
}