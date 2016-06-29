using System;
using System.Threading;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class AutoStartForm : Form
    {
        
        public Boolean completed = false;
        private MeterStatus MeterStatus = new MeterStatus();
        private Boolean crossFogNotReady = true;
        private Boolean longFogNotReady = true;
        private Boolean heaterNotReady = true;
        public int heaterWaitOptions = 0;// 0 - wait for heater, 1 - continue with startup without heater, 2 - Cancel startup

        public AutoStartForm()
        {
            InitializeComponent();
        }

        private void radioButtonWait_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonWait.Checked == true)
            {
                heaterWaitOptions = 1;
            }
        }

        private void radioButtonContinue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonContinue.Checked == true)
            {
                heaterWaitOptions = 2;
            }
        }

        private void radioButtonCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancel.Checked == true)
            {
                heaterWaitOptions = 3;
            }
        }

        public void FogCheck()
        {
            Thread.Sleep(3000);// wait 1 sec
            while ((crossFogNotReady) & (longFogNotReady) & (heaterNotReady))
            {
                crossGyroStatusLabel.Text = "Not Ready...";
                // Check Cross FOG status
                if (MeterStatus.xGyro_Fog == 0)
                {
                    crossFogNotReady = false;
                }
                else
                {
                    crossFogNotReady = true;
                }

                // Check Long FOG status
                if (MeterStatus.xGyro_Fog == 0)
                {
                    longFogNotReady = false;
                }
                else
                {
                    longFogNotReady = true;
                }

                // Check Heater status
                if (MeterStatus.meterHeater == 0)
                {
                    heaterNotReady = false;
                }
                else
                {
                    heaterNotReady = true;
                }

                if (heaterWaitOptions == 2 | heaterWaitOptions == 3)
                {
                    break;
                }

               // Thread.Sleep(3000);// wait 1 sec
            }// while
           // Thread.Sleep(3000);// wait 1 sec
            completed = true;
            Console.WriteLine("autostart loaded");
           // this.Hide();
        }

        private void AutoStartForm_Load(object sender, EventArgs e)
        {
            FogCheck();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FogCheck();
        }
    }
}