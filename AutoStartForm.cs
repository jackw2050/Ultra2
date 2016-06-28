using System;
using System.Threading;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class AutoStartForm : Form
    {
        public string status = "Not Ready";
        public Boolean completed = false;
        private MeterStatus MeterStatus = new MeterStatus();
        Boolean crossFogReady = false;
        Boolean longFogReady = false;
        Boolean heaterReady = false;
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
                heaterWaitOptions = 1;
            }
        }

        private void radioButtonCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancel.Checked == true)
            {
                heaterWaitOptions = 2;
            }
        }


        private void FogCheck()
        {
            Thread.Sleep(3000);


            while ((crossFogReady == false) & (longFogReady == false) & (heaterReady == false))
            {



                // Check Cross FOG status
                if (MeterStatus.xGyro_Fog == 0)
                {
                    crossFogReady = true;
                    crossGyroStatusLabel.Text = "Ready";
                    crossGyroStatusLabel.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    crossFogReady = false;
                    crossGyroStatusLabel.Text = "Not Ready";
                    crossGyroStatusLabel.ForeColor = System.Drawing.Color.Red;
                }

                // Check Long FOG status
                if (MeterStatus.xGyro_Fog == 0)
                {
                    longFogReady = true;
                    longGyroStatusLabel.Text = "Ready";
                    longGyroStatusLabel.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    longFogReady = false;
                    longGyroStatusLabel.Text = "Not Ready";
                    longGyroStatusLabel.ForeColor = System.Drawing.Color.Red;
                }

                // Check Heater status
                if (MeterStatus.xGyro_Fog == 0)
                {
                    heaterReady = true;
                    heaterStatusLabel.Text = "Ready";
                    heaterStatusLabel.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    heaterReady = false;
                    heaterStatusLabel.Text = "Not Ready";
                    heaterStatusLabel.ForeColor = System.Drawing.Color.Red;
                }


                if (heaterWaitOptions == 2)
                {
                    break;
                }

                Thread.Sleep(1000);// wait 1 sec
            }// while
            completed = true;
            Console.WriteLine("autostart loaded");
        }



        private void AutoStartForm_Load(object sender, EventArgs e)
        {

            crossGyroStatusLabel.Text = "Not Ready";
            longGyroStatusLabel.Text = "Not Ready";
            heaterStatusLabel.Text = "Not Ready";

            FogCheck();


        }
    }
}