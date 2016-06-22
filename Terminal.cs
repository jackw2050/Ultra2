/*
 * Project:    SerialPort Terminal
           Search for "comport" to see how I'm using the SerialPort control.
 */

#region Namespace Inclusions

using SerialPortTerminal.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#endregion Namespace Inclusions

namespace SerialPortTerminal
{
    #region Public Enumerations

    public enum DataMode { Text, Hex }

    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };

    #endregion Public Enumerations

    public partial class frmTerminal : Form
    {

        #region Callbacks

        private delegate void SetTextCallback(string text);
        public delegate void SetSerialPortCallback(string text);// Callback for writing to the serial port
             

        #endregion Callbacks


        #region Global Variables

        public int set = 1;
        public int enable = 1;
        public int clear = 0;
        public int disable = 0;

        //public double[] analogFilter = { 0.0, 0.2, 0.2, 0.2, 0, 2, 1.0, 1.0, 1.0, 10.0 }; // [0] is not used
        public int NAUX = 0;

        // public double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // [0] not used

        public Single crossCouplingFactor13 = 0;
        public Single crossCouplingFactor14 = 0;
        public Single analogFilter5 = 0;
        public Single analogFilter6 = 0;
        public Single aCrossPeriod = 0;
        public Single aLongPeriod = 0;
        public Single aCrossDampFactor = 0;
        public Single aLongDampFactor = 0;
        public Single aCrossGain = 0;
        public Single aLongGain = 0;
        public Single aCrossLead = 0;
        public Single aLongLead = 0;

        public string connectionString = "Data Source=LAPTOPSERVER\\ULTRASYSDEV;Initial Catalog=DynamicData;Integrated Security=True;Max Pool Size=50;Min Pool Size=5;Pooling=True";

        // Database connection strings etc.
        private SqlConnection myConnection = new SqlConnection("Data Source=LAPTOPSERVER\\ULTRASYSDEV;Initial Catalog=DynamicData;Integrated Security=True;Max Pool Size=50;Min Pool Size=5;Pooling=True");

        public Boolean filterData = false;

        #endregion Global Variables



        #region Local Variables

        private CalculateMarineData mdt = new CalculateMarineData();
        private DataForm f2 = new DataForm();
        private RelaySwitches RelaySwitches = new RelaySwitches();
        private ConfigData ConfigData = new ConfigData();
        private ControlSwitches ControlSwitches = new ControlSwitches();
        private MeterStatus MeterStatus = new MeterStatus();
        private DataStatusForm DataStatusForm = new DataStatusForm();
        private SerialPortForm SerialPortForm = new SerialPortForm();

        // The main control for communicating through the RS-232 port
        private SerialPort comport = new SerialPort();

        // Various colors for logging info
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };

        // Temp holder for whether a key was pressed
        private bool KeyHandled = false;

        private Settings settings = Settings.Default;

        #endregion Local Variables

        #region Constructor

        public frmTerminal()
        {
            // Load user settings
            settings.Reload();

            // Build the form
            InitializeComponent();

            // Restore the users settings
            InitializeControlValues();

            // Enable/disable controls based on the current state
            EnableControls();

            // When data is recieved through the port, call this method

            // THESE EVENT HANDLERS WILL NEED TO  BE REMOVED AND REPLACED WITH THE 1 SEC TIMER EVENT HANDLER
            // NOT SURE WHAT THE PIN STATE HANDLER DOES.  NEED TO INVESTIGATE.
            //     comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            //			comport.PinChanged += new SerialPinChangedEventHandler(comport_PinChanged);

            _timer1 = new System.Windows.Forms.Timer();
            //     _timer1.Interval = (1000 - DateTime.Now.Millisecond);
            _timer1.Enabled = false;  // ENBALE WHEN FIRST DATAN IS SENT
            _timer1.Tick += new EventHandler(port_CheckDataReceived);
        }

        private void comport_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            // Show the state of the pins
            UpdatePinState();
        }

        private void UpdatePinState()
        {
            this.Invoke(new ThreadStart(() =>
            {
            }));
        }

        #endregion Constructor

        #region Local Methods

        /// <summary> Save the user's settings. </summary>
        private void SaveSettings()
        {
            // Saved setting, connection strings etc.
            // Should be called on exit or when a change is made.

            settings.BaudRate = 9600; // int.Parse(cmbBaudRate.Text);
            settings.DataBits = 8;// int.Parse(cmbDataBits.Text);
            settings.DataMode = CurrentDataMode;
            settings.Parity = (Parity)Enum.Parse(typeof(Parity), "None");//   cmbParity.Text);
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");//    cmbStopBits.Text);
            settings.PortName = SerialPortForm.cmbPortName.Text;
            settings.ClearOnOpen = true;// chkClearOnOpen.Checked;
            settings.ClearWithDTR = false;// chkClearWithDTR.Checked;

            settings.Save();
        }

        /// <summary> Populate the form's controls with default settings. </summary>
        private void InitializeControlValues()
        {
            //  Need to duplicate serial port settings.
            // One port for meter communications
            // Second for output data stream.

            CurrentDataMode = settings.DataMode;
            RefreshComPortList();//  Need two ports

            // This clears the terminal window which is for debug only.
            // Final  app delete rich text box and replace with console.writeLine(data);
            // Use if(debug){};
            SerialPortForm.chkClearOnOpen.Checked = settings.ClearOnOpen;

            // If it is still avalible, select the last com port used
            // Again.  Duplicate for output data port
            if (SerialPortForm.cmbPortName.Items.Contains(settings.PortName)) SerialPortForm.cmbPortName.Text = settings.PortName;
            else if (SerialPortForm.cmbPortName.Items.Count > 0) SerialPortForm.cmbPortName.SelectedIndex = SerialPortForm.cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary> Enable/disable controls based on the app's current state. </summary>
        ///
        // Most of this will be deleted as this is not a terminal program
        private void EnableControls()
        {
            // Enable/disable controls based on whether the port is open or not
            SerialPortForm.gbPortSettings.Enabled = !comport.IsOpen;
            SerialPortForm.txtSendData.Enabled = SerialPortForm.btnSend.Enabled = comport.IsOpen;
            //chkDTR.Enabled = chkRTS.Enabled = comport.IsOpen;

            if (comport.IsOpen) btnOpenPort.Text = "&Close Port";
            else btnOpenPort.Text = "&Open Port";
        }

        /// <summary> Send the user's data currently entered in the 'send' box.</summary>
        /// // Remove text mode and any reference to text box
        private void SendData()
        {
            if (CurrentDataMode == DataMode.Text)
            {
                // Send the user's text straight out the port
                comport.Write(SerialPortForm.txtSendData.Text);

                // Show in the terminal window the user's text
                Log(LogMsgType.Outgoing, SerialPortForm.txtSendData.Text + "\n");
            }
            else
            {
                try
                {
                    // Convert the user's string of hex digits (ex: B4 CA E2) to a byte array
                    byte[] data = HexStringToByteArray(SerialPortForm.txtSendData.Text);

                    // Send the binary data out the port
                    comport.Write(data, 0, data.Length);

                    // Show the hex digits on in the terminal window
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                }
                catch (FormatException)
                {
                    // Inform the user if the hex string was not properly formatted
                    Log(LogMsgType.Error, "Not properly formatted hex string: " + SerialPortForm.txtSendData.Text + "\n");
                }
            }
            SerialPortForm.txtSendData.SelectAll();
        }

        /// <summary> Log data to the terminal window. </summary>
        /// <param name="msgtype"> The type of message to be written. </param>
        /// <param name="msg"> The string containing the message to be shown. </param>
        /// // Change this to console.writeLine() with if(debug)
        public void Log(LogMsgType msgtype, string msg)
        {
            Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
               //tfTerminal.SelectionFont = new Font(SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        #endregion Local Methods

        #region Local Properties

        private DataMode CurrentDataMode
        {
            get
            {
                if (SerialPortForm.rbHex.Checked) return DataMode.Hex;
                else return DataMode.Text;
            }
            set
            {
                if (value == DataMode.Text) SerialPortForm.rbText.Checked = true;
                else SerialPortForm.rbHex.Checked = true;
            }
        }

        #endregion Local Properties

        #region Event Handlers

        // This will move to menu item
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the user the about dialog
            (new frmAbout()).ShowDialog(this);
        }

        private void frmTerminal_Shown(object sender, EventArgs e)
        {
            Log(LogMsgType.Normal, String.Format("Application Started at {0}\n", DateTime.Now));
        }

        private void frmTerminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // The form is closing, save the user's preferences
            SaveSettings();
        }

        private void rbText_CheckedChanged(object sender, EventArgs e)
        { if (SerialPortForm.rbText.Checked) CurrentDataMode = DataMode.Text; }

        private void rbHex_CheckedChanged(object sender, EventArgs e)
        { if (SerialPortForm.rbHex.Checked) CurrentDataMode = DataMode.Hex; }

        private void OpenPort()
        {
            // ADD 1 SEC TIMER.  START AT END
            bool error = false;
            //           if (this.f2.GravityRichTextBox1.InvokeRequired)

            // If the port is open, close it.
            if (comport.IsOpen) comport.Close();
            else
            {
                // Set the port's settings
                comport.PortName = SerialPortForm.cmbPortName.Text;

                try
                {
                    // Open the port
                    comport.Open();
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    // Show the initial pin states
                    UpdatePinState();
                }

                f2.Show();
                // START 1 SEC TIMER
                //       timer1.Enabled = true;
                //       timer1.Start();
            }

            // Change the state of the form's controls
            EnableControls();

            // If the port is open, send focus to the send data box
            if (comport.IsOpen)
            {
                SerialPortForm.txtSendData.Focus();
                if (SerialPortForm.chkClearOnOpen.Checked) ClearTerminal();
            }
        }

        //  Add new method for open port with parameters for control or data port
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            // ADD 1 SEC TIMER.  START AT END
            bool error = false;
            //           if (this.f2.GravityRichTextBox1.InvokeRequired)

            // If the port is open, close it.
            if (comport.IsOpen) comport.Close();
            else
            {
                // Set the port's settings
                comport.PortName = SerialPortForm.cmbPortName.Text;

                try { comport.Open(); }// Open serial port
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    UpdatePinState();
                }

                //f2.Show();
                // START 1 SEC TIMER
                //       timer1.Enabled = true;
                //       timer1.Start();
            }

            // Change the state of the form's controls
            EnableControls();

            // If the port is open, send focus to the send data box
            if (comport.IsOpen)
            {
                SerialPortForm.txtSendData.Focus();
                if (SerialPortForm.chkClearOnOpen.Checked) ClearTerminal();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        { SendData(); }

        //*******************************************************************************************************

        private byte CalculateChecksum(byte[] myArray)
        {
            byte checkSum = 0x00;
            for (int x = 0; x < myArray.Length; x++)
            {
                checkSum ^= myArray[x];
            }
            return checkSum;
        }

        private void port_CheckDataReceived(object sender, EventArgs e)
        {
            // SET VARIABLE FOR DATA LENGTH.  CREATE ENUMERATED BASED ON COMMAND SENT AND SET FOR LENGTH
            // CHECK EXPECTED LENGTH VS comport.BytesToRead.  LOG ERROR IN MESSAGE EXPECTED X BYTES.  RECEIVED Y BYTES
            // IF PASS READ ALL BYTES
            // CHECK FIRST BYTE AND VERIFY IT IS SAME AS LENGTH
            //  SEND COMMANDS
            //  ID  FUNCTION                BYTES       + BYTE LENGTH AND CHECKSUM
            //  0   SEND RELAY SWITCHES             1
            //  1   SEND CONTROL SWITCHES           1
            //  2   UPDATE TIME AND DATE            7
            //  3   SLEW SPRING TENSION             2
            //  4   SET CROSS AXIS PARAMETERS       24
            //  5   SET LONG AXIS PARAMETERS        24
            //  6   SET GAIN FOR  AUX A/D CHANNELS  4
            //  7   UPDATE SPRING TENSION VALUE     24
            //  8   UPDATE CROSS COUPLING VALUES    24
            //  9   REQUEST GYRO BIAS               8
            //
            //  RECEIVE COMMANDS/ DATA
            //  0   METER DATA                      78
            //  1   REMOTE REBOOTED                 1
            //  2   DATE/ TIME SET SUCCESSFUL       1
            //  3   DATE/ TIME FAILED               1
            //  4   G2000 BIAS                      8

            //0x4E, 0x00, 0xDF, 0x07, 0x1F, 0x01, 0x14, 0x2C, 0x0B, 0x24, 0x3E, 0x9D, 0x45, 0x6D, 0x62, 0xE3, 0xC4, 0xA8, 0x39, 0x1D, 0x41, 0xF5, 0x00, 0x1D, 0x3E, 0xB0, 0xEB, 0xA1, 0x3D, 0x03, 0xC7, 0xC9, 0x3D, 0xED, 0x6F, 0x01, 0xBA, 0xC5, 0xF9, 0xD0, 0x3D, 0x1B, 0xF3, 0x20, 0x3F, 0xAD, 0xE6, 0xD8, 0xC0, 0xF7, 0x1B, 0xFE, 0xC0, 0xBC, 0xFE, 0xE1, 0xFE, 0xCA, 0xFE, 0x28, 0xFF, 0xFF, 0xFF, 0xFF, 0x99, 0xB7, 0x04, 0x47, 0x08, 0x96, 0x9D, 0xBF, 0x72, 0x40, 0x7A, 0xBE, 0x00, 0xFF, 0xBA
            //LEN   CMD				                                      CHECKSUM

            // NEED TO SETUP A CHECK HERE FOR DATA LENGTH
            // IF SEND COMMAND BEFORE EXPECT LESS THAN 79
            // CHECK BYTES TO READ
            // READ ONE BYTE COMPARE TO BYTES TO READ IF THE SAME READ REMAINING BYTES
            // NEED FUNCTION TO DUMP BAD DATA AND SYNC NEW DATA

            // If the com port has been closed, do nothing
            if (!comport.IsOpen) return;

            // Determain which mode (string or binary) the user is in
            if (CurrentDataMode == DataMode.Text)
            {
                // Read all the data waiting in the buffer
                string data = comport.ReadExisting();

                // Display the text to the user in the terminal
                Log(LogMsgType.Incoming, data);
            }
            else
            {
                // Obtain the number of bytes waiting in the port's buffer
                int bytes = comport.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];

                // Read the data from the port and store it in our buffer
                comport.Read(buffer, 0, bytes);

                // Show the user the incoming data in hex format
                Log(LogMsgType.Incoming, ByteArrayToHexString(buffer) + "\tBytes to read: " + bytes + "Length: \t" + Convert.ToString(buffer.Length) + "\n\n");

                mdt.GetMeterData(buffer);// send buffer for parsing
                if (mdt.dataValid)
                {
                    // call calculaton functions

                    // Need to check how to sync threads or have all threads access same data source
                    ThreadProcSafe();//  Initially write data1 -data4 in text boxes
                    DataBaseWrite(mdt);
                }
            }
        }

        //**********************************************************************************************************

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com port has been closed, do nothing
            if (!comport.IsOpen) return;

            // This method will be called when there is data waiting in the port's buffer

            // Determain which mode (string or binary) the user is in
            if (CurrentDataMode == DataMode.Text)
            {
                // Read all the data waiting in the buffer
                string data = comport.ReadExisting();

                // Display the text to the user in the terminal
                Log(LogMsgType.Incoming, data);
            }
            else
            {
                // Obtain the number of bytes waiting in the port's buffer
                int bytes = comport.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];

                // Read the data from the port and store it in our buffer
                comport.Read(buffer, 0, bytes);

                // Show the user the incoming data in hex format
                Log(LogMsgType.Incoming, ByteArrayToHexString(buffer));

                mdt.GetMeterData(buffer);// send buffer for parsing

                if (mdt.dataValid)
                {
                    //   textBox1.Text = (mdt.gravity.ToString());
                    DataBaseWrite(mdt);
                    ThreadProcSafe();
                }

                //          UpdateTextBox(DateTime newDT, double ST, double Beam);

                //           f2.GravityRichTextBox1.Text = Convert.ToString(mdt.myDT ) +  "         " + Convert.ToString(mdt.SpringTension) + "             " + Convert.ToString(mdt.Beam) + "/n/n";
            }
        }




        private void ThreadSerialWriteSafe(string command)
        {

            string text = Convert.ToString(mdt.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(mdt.year) + "\t" + Convert.ToString(mdt.day));

            // Check if this method is running on a different thread
            // than the thread that created the control.
            if (this.f2.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
             
                SetSerialPortCallback SerialCallBack = new SetSerialPortCallback(SetText);
                this.Invoke(SerialCallBack, new object[] { text });
                Thread.Sleep(2000);// do I need this?

            }
            else
            {
                // It's on the same thread, no need for Invoke


                // write to serial port
                 sendCmd(command);

            }

        }







        private void ThreadProcSafe()
        {

            string text = Convert.ToString(mdt.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(mdt.year) + "\t" + Convert.ToString(mdt.day));

            // Check if this method is running on a different thread
            // than the thread that created the control.
            if (this.f2.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
                Thread.Sleep(2000);
                DataBaseWrite(mdt);
             /*   textBox1.Text = (mdt.gravity.ToString());
                textBox16.Text = mdt.altitude.ToString();
                textBox17.Text = mdt.latitude.ToString();
                textBox18.Text = mdt.longitude.ToString();*/
            }
            else
            {
                // It's on the same thread, no need for Invoke
                //   this.f2.GravityRichTextBox1.Text = text;
                //   DataBaseWrite();
                
                DataStatusForm.textBox1.Text = (mdt.gravity.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox2.Text = (mdt.SpringTension.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox3.Text = (mdt.CrossCoupling.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox4.Text = (mdt.Beam.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox5.Text = (mdt.myDT.ToString());
                DataStatusForm.textBox6.Text = (mdt.totalCorrection.ToString("N", CultureInfo.InvariantCulture));

                DataStatusForm.textBox7.Text = (mdt.VCC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox8.Text = (mdt.AL.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox9.Text = (mdt.AX.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox10.Text = (mdt.VE.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox11.Text = (mdt.AX2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox12.Text = (mdt.XACC2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox13.Text = (mdt.LACC2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox14.Text = (mdt.XACC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox15.Text = (mdt.LACC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox1.Text = (mdt.gravity.ToString("N", CultureInfo.InvariantCulture));
                
                string specifier;
                specifier = "000.000000";
                textBox16.Text = (mdt.altitude.ToString("N", CultureInfo.InvariantCulture));
                textBox17.Text = (mdt.latitude.ToString(specifier, CultureInfo.InvariantCulture));
                textBox18.Text = (mdt.longitude.ToString(specifier, CultureInfo.InvariantCulture));

                if (MeterStatus.lGyro_Fog == 0)
                {
                    DataStatusForm.textBox19.Text = "OK";
                }
                else
                {
                    DataStatusForm.textBox19.Text = "Bad";
                }
                if (MeterStatus.xGyro_Fog == 0)
                {
                    DataStatusForm.textBox20.Text = "OK";
                }
                else
                {
                    DataStatusForm.textBox20.Text = "Bad";
                }
                if (MeterStatus.meterHeater == 0)
                {
                    DataStatusForm.textBox21.Text = "OK";
                }
                else
                {
                    DataStatusForm.textBox21.Text = "Bad";
                }
                if (MeterStatus.incorrectCommandReceived == 0)
                {
                    DataStatusForm.textBox23.Text = "OK";
                }
                else
                {
                    DataStatusForm.textBox23.Text = "Incorrect Command";
                }
                if (MeterStatus.receiveDataCheckSumError == 0)
                {
                    DataStatusForm.textBox22.Text = "OK";
                }
                else
                {
                    DataStatusForm.textBox22.Text = "Checksum error";
                }

                //   textBox19.Text = (MeterStatus.lGyro_Fog.ToString("N", CultureInfo.InvariantCulture));// long
                //   textBox20.Text = (MeterStatus.xGyro_Fog.ToString("N", CultureInfo.InvariantCulture));// cross
                //   textBox21.Text = (MeterStatus.meterHeater.ToString("N", CultureInfo.InvariantCulture));// meter
            }

            //        this.Invoke(d, new object[] { text });
        }

        // This method is passed in to the SetTextCallBack delegate
        // to set the Text property of textBox1.
        private void SetText(string text)
        {
            //  this.f2.GravityRichTextBox1.AppendText(text + "\n");   //  Convert.ToString(mdt.myDT) + "\t\t" + Convert.ToString(mdt.ST) + "\t\t" + Convert.ToString(mdt.Beam) + "\n"; ;
        }

        private void txtSendData_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter) { e.Handled = true; SendData(); }
        }

        private void txtSendData_KeyPress(object sender, KeyPressEventArgs e)
        { e.Handled = KeyHandled; }

        #endregion Event Handlers

        #region Database stuff

        private void DataBaseConnect()
        {
            try { myConnection.Open(); }
            catch (Exception) { MessageBox.Show("Error.  Unable to connect to database"); }
        }

        private void DataBaseDisconnect()
        {
            try { myConnection.Close(); }
            catch (Exception) { MessageBox.Show("Error.  Unable to connect to database"); }
        }

        private Boolean CheckDbConnection()
        {
            if (myConnection.State == System.Data.ConnectionState.Open) { return true; }
            else { return false; }
        }

        // SQL query to load config data.   Need keyword allowing for multiple versions
        /*   SELECT        meterNumber, beamScale, engPassword, crossPeriod, longPeriod, crossDampFactor, longDampFactor, crossGain, longGain, crossLead, longLead, springTensionMax, crossBias, longBiass, id
           FROM            ConfigData
           WHERE        (meterNumber = 'S67')
     */

        private void DataBaseReadConfigData()
        {
            ConfigData ConfigData = new ConfigData();

            string query = "SELECT beamScale, crossPeriod, longPeriod, crossDampFactor, longDampFactor, crossGain, longGain, crossLead, longLead, springTensionMax, crossBias, longBias FROM ConfigData WHERE meterNumber = 'S67'";

            try
            {
                using (myConnection)
                {
                    SqlCommand cmd = new SqlCommand(query, myConnection);

                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ConfigData.beamScale = (double)read["beamScale"];
                        ConfigData.crossPeriod = (Single)read["crossPeriod"];
                        ConfigData.longPeriod = (Single)read["longPeriod"];
                        ConfigData.crossDampFactor = (Single)read["crossDampFactor"];
                        ConfigData.longDampFactor = (Single)read["longDampFactor"];
                        ConfigData.crossGain = (Single)read["crossGain"];
                        ConfigData.longGain = (Single)read["longGain"];
                        ConfigData.crossLead = (Single)read["crossLead"];
                        ConfigData.longLead = (Single)read["longLead"];
                        ConfigData.springTensionMax = (double)read["springTensionMax"];
                        ConfigData.crossBias = (double)read["crossBias"];
                        ConfigData.longBias = (double)read["longBias"];
                    }
                    if (read != null)
                    {
                        cmd.Cancel();
                        read.Close();
                    }

                    // close the connection
                    if (myConnection != null)
                    {
                        cmd.Cancel();
                        myConnection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + "\n\n" + e.Message);
            }
        }

        private void DataBaseWrite(CalculateMarineData CalculateMarineData)
        {
            if (false)
            {
                // CalculateMarineData CalculateMarineData = new CalculateMarineData();

                string tableSelect = "INSERT INTO Data_Table_Simulated (";
                string date = "Date, YEAR, DAYS, HOUR, MIN, SEC,";
                //    string dataValues = "DigitalGravity, SpringTension, CrossCoupling, RawBeam, AL, AX, VE, AX2, XACC2, LACC2, XACC, LACC";
                string data1 = "@DigitalGravity, @SpringTension, @CrossCoupling, @RawBeam,@VCC, @AL, @AX, @VE, @AX2, @XACC2. @LACC2, @XACC, @LACC ";
                string gpsData = ",@Altitude, @Latitude, @Longitude, @GPS_Status, @Period";
                string dateValues = " VALUES (@Date, @Year, @Days,  @Hour, @Min, @Sec";

                var query1 = tableSelect + date + data1 + dateValues + gpsData;

                var query = "INSERT INTO Data_Table_Simulated (Date, YEAR, DAYS, HOUR, MIN, SEC,DigitalGravity, SpringTension, CrossCoupling, RawBeam, VCC, AL, AX, VE, AX2, XACC2, LACC2, XACC, LACC) VALUES (@Date, @Year, @Days,  @Hour, @Min, @Sec,@DigitalGravity, @SpringTension, @CrossCoupling, @RawBeam,@VCC, @AL, @AX, @VE, @AX2, @XACC2, @LACC2, @XACC, @LACC)";

                using (var command = new SqlCommand(query, myConnection))
                {
                    if (CheckDbConnection() == false)
                    {
                        // DataBaseDisconnect();
                        DataBaseConnect();
                    }
/*
                    // Add your parameters
                    command.Parameters.AddWithValue("@Date", CalculateMarineData.myDT);
                    command.Parameters.AddWithValue("@Year", CalculateMarineData.year);
                    command.Parameters.AddWithValue("@Days", CalculateMarineData.day);
                    command.Parameters.AddWithValue("@Hour", CalculateMarineData.Hour);
                    command.Parameters.AddWithValue("@Min", CalculateMarineData.Min);
                    command.Parameters.AddWithValue("@Sec", CalculateMarineData.Sec);

                    command.Parameters.AddWithValue("@DigitalGravity", CalculateMarineData.gravity);// this is calculated for now set  eq spring tenstion
                    command.Parameters.AddWithValue("@SpringTension", CalculateMarineData.SpringTension);
                    command.Parameters.AddWithValue("@CrossCoupling", 0);// CalculateMarineData.c);// calculated
                    command.Parameters.AddWithValue("@RawBeam", CalculateMarineData.Beam);
                    command.Parameters.AddWithValue("@VCC", CalculateMarineData.VCC);
                    command.Parameters.AddWithValue("@AL", CalculateMarineData.AL);
                    command.Parameters.AddWithValue("@AX", CalculateMarineData.AX);
                    command.Parameters.AddWithValue("@VE", CalculateMarineData.VE);
                    command.Parameters.AddWithValue("@AX2", CalculateMarineData.AX2);
                    command.Parameters.AddWithValue("@XACC2", CalculateMarineData.XACC2);
                    command.Parameters.AddWithValue("@LACC2", CalculateMarineData.LACC2);
                    command.Parameters.AddWithValue("@XACC", CalculateMarineData.XACC);
                    command.Parameters.AddWithValue("@LACC", CalculateMarineData.LACC);
                    */
                    // Execute your query
                    command.ExecuteNonQuery();
                }
                //   DataBaseDisconnect();

                /*
                            var debug = false;
                            var GPS_Meter = false;
                            if (!GPS_Meter || debug)
                            {
                                command.Parameters.AddWithValue("@P28V", CalculateMarineData.p28V);
                                command.Parameters.AddWithValue("@M28V", CalculateMarineData.n28V);
                                command.Parameters.AddWithValue("@P24V", CalculateMarineData.p24V);
                                command.Parameters.AddWithValue("@P15V", CalculateMarineData.p15V);
                                command.Parameters.AddWithValue("@M15V", CalculateMarineData.n15V);
                                command.Parameters.AddWithValue("@P5V", CalculateMarineData.p5V);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@Altitude", CalculateMarineData.Altitude);
                                command.Parameters.AddWithValue("@Latitude", CalculateMarineData.Latitude);
                                command.Parameters.AddWithValue("@Longitude", CalculateMarineData.Longitude);
                                command.Parameters.AddWithValue("@GPS_Status", CalculateMarineData.GPS_Status);
                                command.Parameters.AddWithValue("@Period", CalculateMarineData.Period);
                            }

                            */

                this.data_Table_SimulatedTableAdapter.Fill(this.dynamicDataDataSet.Data_Table_Simulated);
            }
        }

        #endregion Database stuff

        #region Data Grid View

        private void InitDataGridView()
        {
        }

        private void SetDataGridViewColumns()
        {
        }

        private void DataGridViewCallback()
        {
        }

        #endregion Data Grid View

        #region Chart

        private void InitChart()
        {
        }

        private void ChartSeries()
        {
        }

        private void ChartCallback()
        {
        }

        #endregion Chart

        #region Serial Port

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTerminal();
        }

        private void ClearTerminal()
        {
            rtfTerminal.Clear();
        }

        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            // checks to see if COM ports have been added or removed
            // since it is quite common now with USB-to-Serial adapters
            RefreshComPortList();
        }

        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(SerialPortForm.cmbPortName.Items.Cast<string>(), SerialPortForm.cmbPortName.SelectedItem as string, comport.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                SerialPortForm.cmbPortName.Items.Clear();
                SerialPortForm.cmbPortName.Items.AddRange(OrderedPortNames());
                SerialPortForm.cmbPortName.SelectedItem = selected;
            }
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }

        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }

        private void rtfTerminal_TextChanged(object sender, EventArgs e)
        {
        }


        public byte[] CalculateCheckSum(byte[] intBytes, int numBytes)
        {
            var checkSum = 0;
            // byte[] intBytes = BitConverter.GetBytes(data);
            Array.Resize(ref intBytes, numBytes);
            byte[] txCmd = new byte[1 + intBytes.Length + 1];

            Buffer.BlockCopy(intBytes, 0, txCmd, 1, intBytes.Length);

            // Concatenate array1 and array2.

            //  txCmd[0] = cmd;
            for (int i = 0; i < numBytes; i++)
            {
                checkSum = checkSum ^ txCmd[i];
            }
            txCmd[txCmd.Length - 1] = BitConverter.GetBytes(checkSum)[0]; ;

            Console.WriteLine(txCmd);
            Console.WriteLine(checkSum);
            return BitConverter.GetBytes(checkSum);

            //  comport.Write(txCmd, 0, txCmd.Length);
        }


        #endregion Serial Port

        private void button1_Click(object sender, EventArgs e)// Send data button.  For debug only.  Remove in final app.
        {
            // create hex data stream.  Convert to string and place in text box for sending.
            // Command 1 - send relay switches

            byte[] data = { 0x00, 0x80, 0x80, 0x04, 0x07, 0x45, 0xf3, 0x37, 0x9a, 0x99, 0x99, 0x3e, 0xcd, 0xcc, 0x4c, 0x3e, 0x33, 0x33, 0xb3, 0x3e, 0x2e, 0x90, 0x20, 0xbb, 0x66, 0x66, 0x66, 0x3f, 0xa4, 0x05, 0x93, 0x1a, 0xda, 0x37, 0x85, 0xeb, 0x91, 0x3e, 0xcd, 0xcc, 0x4c, 0x3e, 0x33, 0x33, 0xb3, 0x3e, 0x89, 0xd2, 0x5e, 0xbb, 0x00, 0x00, 0x80, 0x3f, 0x5f, 0x08, 0x3f, 0x35, 0x5e, 0x3e, 0x68, 0x91, 0x2d, 0x3e, 0x14, 0xae, 0x47, 0x3e, 0xa4, 0x70, 0x3d, 0x3e, 0x5c, 0x21, 0x88, 0xbf, 0x00, 0xc0, 0xda, 0x45, 0x89, 0x01, 0x08, 0x09, 0x0a, 0x88, 0x45, 0xb3, 0xc3, 0xa3, 0x8e, 0xf3, 0xc2, 0xab };

            // Send the binary data out the port
            comport.Write(data, 0, data.Length);

            // Show the hex digits on in the terminal window
            Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
        }

        // Start data collection.  Send command 1 for meter to start data stream
        private void button2_Click(object sender, EventArgs e)
        {
            //   OpenPort();
            // Convert the user's string of hex digits (ex: B4 CA E2) to a byte array
            byte[] data = { 0x01, 0x08, 0x09 };                                        //HexStringToByteArray(txtSendData.Text);

            // Send the binary data out the port
            //    comport.Write(data, 0, data.Length);

            _timer1.Interval = 1000; //  (5000 - DateTime.Now.Millisecond);
            _timer1.Enabled = true;
            // Show the hex digits on in the terminal window
            Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
        }

        //  This will be sendData later will full command byte array and checksum.
        //  Not needed until I implement sending commands, settings etc.
        private void button3_Click(object sender, EventArgs e)
        {
            byte checkSum = 0;
            var year = 2016;

            byte[] myByte = BitConverter.GetBytes(year);
            //     Array.Reverse(myByte);
            // REPLACE WITH COMMAND AND # BYTES LATER WITH

            byte[] myByteArray = { 3, 0, 0 };
            checkSum = CalculateChecksum(myByteArray);
            myByteArray[myByteArray.Length - 1] = checkSum;
            string StringOutput = "";
            for (int i = 0; i < myByteArray.Length; i++)
            {
                StringOutput = string.Concat(StringOutput, myByteArray[i].ToString("X"));
            }

            Log(LogMsgType.Incoming, Convert.ToString(StringOutput) + "\n");
        }

     

        private void frmTerminal_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dynamicDataDataSet5.Data_Table_Simulated' table. You can move, or remove it, as needed.
            this.data_Table_SimulatedTableAdapter2.Fill(this.dynamicDataDataSet5.Data_Table_Simulated);


            // Connect to database and leave connection open.
            //  Need to add desconnect on exit or error.
            // Database writes should be try with if connection open else connect for safety
            //    DataBaseConnect();
            //    Console.WriteLine(CheckDbConnection());
            //    DataBaseReadConfigData();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*DateTime myDatetime = DateTime.Now;
            int year = myDatetime.Year;
            int day = myDatetime.DayOfYear;
            int hour = myDatetime.Hour;
            int min = myDatetime.Minute;
            int sec = myDatetime.Second;*/

            // IPORT(1) = 0x80 0b1000 0000
            // IPORT(2) = 0x00
            // IPORT(2) = 0xFF

            Console.WriteLine();
        }

        public byte[] CreateTxArray(byte command, Single data1, Single data2, Single data3, Single data4, double data5, double data6)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            byte[] byteArray1 = BitConverter.GetBytes(data1);
            byte[] byteArray2 = BitConverter.GetBytes(data2);
            byte[] byteArray3 = BitConverter.GetBytes(data3);
            byte[] byteArray4 = BitConverter.GetBytes(data4);
            byte[] byteArray5 = BitConverter.GetBytes(System.Convert.ToSingle(data5));
            byte[] byteArray6 = BitConverter.GetBytes(System.Convert.ToSingle(data6));

            int[] myByteArray = { cmdByte.Length, byteArray1.Length, byteArray2.Length, byteArray3.Length, byteArray4.Length, byteArray5.Length, byteArray6.Length + checkSum.Length };
            byte[] outputBytes = new byte[cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length + byteArray6.Length + checkSum.Length];

            Buffer.BlockCopy(cmdByte, 0, outputBytes, 0, cmdByte.Length);
            Buffer.BlockCopy(byteArray1, 0, outputBytes, cmdByte.Length, byteArray1.Length);
            Buffer.BlockCopy(byteArray2, 0, outputBytes, cmdByte.Length + byteArray1.Length, byteArray2.Length);
            Buffer.BlockCopy(byteArray3, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length, byteArray3.Length);
            Buffer.BlockCopy(byteArray4, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length, byteArray4.Length);
            Buffer.BlockCopy(byteArray5, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length, byteArray5.Length);
            Buffer.BlockCopy(byteArray6, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length, byteArray6.Length);
            Buffer.BlockCopy(byteArray6, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length + byteArray6.Length, checkSum.Length);
            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            Console.WriteLine("Transmit array: " + outputBytes);
            Console.WriteLine("Done");

            return outputBytes;
        }
        public byte[] CreateTxArray(byte command, Single data1, Single data2, Single data3, Single data4, double data5)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            byte[] byteArray1 = BitConverter.GetBytes(data1);
            byte[] byteArray2 = BitConverter.GetBytes(data2);
            byte[] byteArray3 = BitConverter.GetBytes(data3);
            byte[] byteArray4 = BitConverter.GetBytes(data4);
            byte[] byteArray5 = BitConverter.GetBytes(data5);

            int[] myByteArray = { cmdByte.Length, byteArray1.Length, byteArray2.Length, byteArray3.Length, byteArray4.Length, byteArray5.Length, checkSum.Length };
            byte[] outputBytes = new byte[cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length + checkSum.Length];

            Buffer.BlockCopy(cmdByte, 0, outputBytes, 0, cmdByte.Length);
            Buffer.BlockCopy(byteArray1, 0, outputBytes, cmdByte.Length, byteArray1.Length);
            Buffer.BlockCopy(byteArray2, 0, outputBytes, cmdByte.Length + byteArray1.Length, byteArray2.Length);
            Buffer.BlockCopy(byteArray3, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length, byteArray3.Length);
            Buffer.BlockCopy(byteArray4, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length, byteArray4.Length);
            Buffer.BlockCopy(byteArray5, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length, byteArray5.Length);
            Buffer.BlockCopy(byteArray5, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length + byteArray5.Length, checkSum.Length);

            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            Console.WriteLine("Transmit array: " + outputBytes);
            Console.WriteLine("Done");

            return outputBytes;
        }
        public byte[] CreateTxArray(byte command, Single data1, Single data2)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            byte[] byteArray1 = BitConverter.GetBytes(data1);
            byte[] byteArray2 = BitConverter.GetBytes(data2);
            int[] myByteArray = { cmdByte.Length, byteArray1.Length, byteArray2.Length, checkSum.Length };
            byte[] outputBytes = new byte[cmdByte.Length + byteArray1.Length + byteArray2.Length + checkSum.Length];
            Buffer.BlockCopy(cmdByte, 0, outputBytes, 0, cmdByte.Length);
            Buffer.BlockCopy(byteArray1, 0, outputBytes, cmdByte.Length, byteArray1.Length);
            Buffer.BlockCopy(byteArray2, 0, outputBytes, cmdByte.Length + byteArray1.Length, byteArray2.Length);
            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            Console.WriteLine("Transmit array: " + outputBytes);
            Console.WriteLine("Done");

            return outputBytes;
        }
        public byte[] CreateTxArray(byte command, int data1)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            byte[] byteArrayTemp = BitConverter.GetBytes(data1);
            int[] byteArray1 = { byteArrayTemp[0] };
            byte[] outputBytes = new byte[3]; //cmdByte.Length + byteArray1.Length + checkSum.Length];

            Buffer.BlockCopy(cmdByte, 0, outputBytes, 0, cmdByte.Length);
            Buffer.BlockCopy(byteArray1, 0, outputBytes, cmdByte.Length, byteArray1.Length);

            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            Console.WriteLine("Transmit array: " + outputBytes);
            Console.WriteLine("Done");

            return outputBytes;
        }
        public void sendCmd(string cmd)
        {
            byte[] data;
            switch (cmd)
            {
                case "Send Relay Switches": // 0

                    data = CreateTxArray(0, RelaySwitches.relaySW);
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    comport.Write(data, 0, 3);
                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Send Control Switches"://1
                    data = CreateTxArray(1, ControlSwitches.controlSw);
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    comport.Write(data, 0, 3);
                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Update Date and Time": // 2

                    DateTime myDatetime = DateTime.Now;
                    int year = myDatetime.Year;
                    int day = myDatetime.DayOfYear;
                    int hour = myDatetime.Hour;
                    int min = myDatetime.Minute;
                    int sec = myDatetime.Second;

                    data = CreateTxArray(2, year, day, hour, min, sec);
                    comport.Write(data, 0, 9);

                    // trcmd(2) = iyr                   iYr = 07E0
                    // trcmd(3) = iyr <<8
                    // trcmd(4) =  iday                 IDAY = 009A
                    // trcmd(5) =  iday << 8
                    // trcmd(6) = ihr                   IHR = 0E
                    // trcmd(7) = imin                  IMIN = 22
                    // trcmd(8) = isec                  ISEC = 0F
                    // nByte = 7;                       TRCMD[2] .. [8] E0 07 98 00 09 22 0F
                    break;

                case "Slew Spring Tension": //  3
                    // trcmd(2) = iStep[4]
                    // trcmde(3) = iStep[4] <<8
                    // nByte = 4;

                    //     data = CreateTxArray(3, iStep[4] );
                    //   comport.Write(data, 0, 4);

                    break;

                case "Set Cross Axis Parameters": //  4

                    aCrossPeriod = Convert.ToSingle(0.000075); //ConfigData.crossPeriod;
                    aCrossDampFactor = Convert.ToSingle(.82); // ConfigData.crossDampFactor;
                    aCrossGain = Convert.ToSingle(6.5);//ConfigData.crossGain;
                    aCrossLead = Convert.ToSingle(.1);//ConfigData.crossLead;
                    crossCouplingFactor13 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[13]);
                    analogFilter5 = System.Convert.ToSingle(ConfigData.analogFilter[5]);

                    data = CreateTxArray(4, aCrossPeriod, aCrossDampFactor, aCrossGain, aCrossLead, crossCouplingFactor13, analogFilter5);
                    // Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");

                    data[0] = 0X04;
                    data[1] = 0xF0;
                    data[2] = 0x38;
                    data[3] = 0xa0;
                    data[4] = 0x37;
                    data[5] = 0x87;
                    data[6] = 0x16;
                    data[7] = 0x59;
                    data[8] = 0x3E;
                    data[9] = 0x9A;
                    data[10] = 0x99;
                    data[11] = 0x19;
                    data[12] = 0x3E;
                    data[13] = 0x00;
                    data[14] = 0x00;
                    data[15] = 0x00;
                    data[16] = 0x3F;
                    data[17] = 0xFA;
                    data[18] = 0xED;
                    data[19] = 0x6B;
                    data[20] = 0xBA;
                    data[21] = 0x00;
                    data[22] = 0x00;
                    data[23] = 0x80;
                    data[24] = 0x3F;
                    data[25] = 0xCF;

                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    comport.Write(data, 0, 26);
                    // transmit command

                    break;

                case "Set Long Axis Parameters": //  5
                    // dice( aLongPeriod( trCms(5), trCms(4), trCms(3), trCms(2))
                    // dice( aLongDampFactor( trCms(9), trCms(8), trCms(7), trCms(6))
                    // dice( againl( trCms(13), trCms(12), trCms11), trCms(10))
                    // dice( aleadl( trCms(17), trCms(16), trCms(15), trCms(14))
                    // dice( crossCouplingFactor14( trCms(21), trCms(20), trCms(19), trCms(18))
                    // dice( analogFilter6( trCms25), trCms(24), trCms(23), trCms(22))
                    // nByte = 24;

                    aLongPeriod = Convert.ToSingle(0.000075); //ConfigData.longPeriod;
                    aLongDampFactor = Convert.ToSingle(.82); //ConfigData.longDampFactor;
                    aLongGain = Convert.ToSingle(6.5); //ConfigData.longGain;
                    aLongLead = Convert.ToSingle(.1); // ConfigData.longLead;
                    crossCouplingFactor14 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[14]);
                    analogFilter6 = System.Convert.ToSingle(ConfigData.analogFilter[6]);

                    data = CreateTxArray(5, aLongPeriod, aLongDampFactor, aLongGain, aLongLead, crossCouplingFactor14, analogFilter6);
                    // Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");

                    data[0] = 0X05;
                    data[1] = 0xF0;
                    data[2] = 0x38;
                    data[3] = 0xa0;
                    data[4] = 0x37;
                    data[5] = 0xAC;
                    data[6] = 0x1C;
                    data[7] = 0x5A;
                    data[8] = 0x3E;
                    data[9] = 0x9A;
                    data[10] = 0x99;
                    data[11] = 0x19;
                    data[12] = 0x3E;
                    data[13] = 0x00;
                    data[14] = 0x00;
                    data[15] = 0x00;
                    data[16] = 0x3F;
                    data[17] = 0x6F;
                    data[18] = 0x12;
                    data[19] = 0x03;
                    data[20] = 0xBB;
                    data[21] = 0x00;
                    data[22] = 0x00;
                    data[23] = 0x80;
                    data[24] = 0x3F;
                    data[25] = 0xEF;

                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    comport.Write(data, 0, 26);
                    // transmit command

                    break;

                case "Set Gain for AUX A/D Channels"://  6
                    // trcmd(2) = IAUSGAIN[1]
                    // trcmd(3) = IAUSGAIN[2]
                    // trcmd(4) = IAUSGAIN[3]
                    // trcmd(5) = IAUSGAIN[4]
                    //nByte = 6
                    break;

                case "Update Spring Tension Value":  //  7
                    // dice( SpringTension( trCms(5), trCms(4), trCms(3), trCms(2))
                    // nByte = 4;
                    //comport.Write(data, 0, 6);
                    break;

                case "Update Cross Coupling Values":  //   8
                    // dice( analogFilter[1]( trCms(5), trCms(4), trCms(3), trCms(2))
                    // dice( analogFilter[2]( trCms(9), trCms(8), trCms(7), trCms(6))
                    // dice( analogFilter[4]( trCms(13), trCms(12), trCms11), trCms(10))
                    // dice( analogFilter[3]( trCms(17), trCms(16), trCms(15), trCms(14))
                    // dice( crossCouplingFactor14( trCms(21), trCms(20), trCms(19), trCms(18))
                    // dice( springTensionMax( trCms25), trCms(24), trCms(23), trCms(22))
                    // nByte = 24

                    //          data = CreateTxArray(8, System.Convert.ToSingle(ConfigData.analogFilter[1]), System.Convert.ToSingle(ConfigData.analogFilter[2]), System.Convert.ToSingle(ConfigData.analogFilter[4]), System.Convert.ToSingle(ConfigData.analogFilter[3]), crossCouplingFactor14, ConfigData.springTensionMax);

                    data = CreateTxArray(8, System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), crossCouplingFactor14, ConfigData.springTensionMax);

                    //   Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");

                    data[0] = 0X08;
                    data[1] = 0x89;
                    data[2] = 0x41;
                    data[3] = 0x60;
                    data[4] = 0x3E;
                    data[5] = 0x77;
                    data[6] = 0xBE;
                    data[7] = 0x5F;
                    data[8] = 0x3E;
                    data[9] = 0x91;
                    data[10] = 0xED;
                    data[11] = 0x7C;
                    data[12] = 0x3E;
                    data[13] = 0x5C;
                    data[14] = 0x8F;
                    data[15] = 0x42;
                    data[16] = 0x3E;
                    data[17] = 0x54;
                    data[18] = 0xE3;
                    data[19] = 0x85;
                    data[20] = 0xBF;
                    data[21] = 0x00;
                    data[22] = 0xC0;
                    data[23] = 0xDA;
                    data[24] = 0x45;
                    data[25] = 0x75;

                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    comport.Write(data, 0, 26);

                    break;

                case "Request Gyro Bias":  //   9
                    //nByte = 0;
                    //comport.Write(data, 0, 1);
                    break;

                case "Update Gyro Bias Offset":  //  10
                    // dice(GXBIAS( trCms(5), trCms(4), trCms(3), trCms(3))
                    // dice( GLBIAS( trCms(9), trCms(8), trCms(7), trCms(6))
                    // nBytes 8;
                    //comport.Write(data, 0, 10);
                    break;

                default:
                    break;
            }

            // Trasmit the  command
            // Calculate checksum
            // Assemble byte array
            // send
        }

        private void AutoStart()
        {
            // init() variables etc.
            // InitRelaySwitches();
            // InitControlSwitches();

            // config()  this loads config data from file or database. for now it is hard coded
            // open serial port to meter

            // create temp variables for startup
            aCrossPeriod = ConfigData.crossPeriod;// aCrossPeriod = perX
            aLongPeriod = ConfigData.longPeriod;// aLongPeriod = perL
            aCrossDampFactor = ConfigData.crossDampFactor;// aCrossDampFactor = dampX
            aLongDampFactor = ConfigData.longDampFactor;// aLongDampFactor = dampL
            crossCouplingFactor13 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[13]);
            crossCouplingFactor14 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[14]);
            analogFilter5 = System.Convert.ToSingle(ConfigData.analogFilter[5]);
            analogFilter6 = System.Convert.ToSingle(ConfigData.analogFilter[6]);

            // CLEAR CONTROL SW 6 AND 7  not sure what this is

            // START HERE

            // while( notReady){   see if PLINQ will work here
            //  need to loop until meter and fog come up.  need seperate thread for this
            // check for timeout.  On timeout alert and stop auto start
            //    }

            // turn on 200 Hz
            /*    RelaySwitches.relay200Hz = enable;// set bit 0  high to turn on 200 Hz
                RelaySwitches.slew4 = enable;
                RelaySwitches.slew5 = enable;
                RelaySwitches.RelaySwitchCalculate();// 0x80
                sendCmd("Send Relay Switches");           // 0 ----

               sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
               sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
               sendCmd("Update Cross Coupling Values");   // CC parameters 8     -----

               ControlSwitches.dataSwitch = enable;// icntlsw = set bit 3  turn on data transmission
               ControlSwitches.ControlSwitchCalculate();
               sendCmd("Send Control Switches");// start data collection
               sendCmd("Send Control Switches");// start data collection

            RelaySwitches.relay200Hz = enable;
            RelaySwitches.slew4 = enable;// probably  don't need this.  for GG49?
            RelaySwitches.slew5 = enable;// probably  don't need this.  for GG49?
            RelaySwitches.stepperMotorEnable = enable;
            RelaySwitches.RelaySwitchCalculate();// 0x
            sendCmd("Send Relay Switches");           // 0 ----

            ControlSwitches.dataSwitch = enable;// icntlsw = set bit 3  turn on data transmission
            ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------

            RelaySwitches.relay200Hz = enable;
            RelaySwitches.slew4 = disable;
            RelaySwitches.slew5 = disable;
            RelaySwitches.stepperMotorEnable = enable;
            RelaySwitches.RelaySwitchCalculate();
            sendCmd("Send Relay Switches");           // 0 ----

            RelaySwitches.alarm = enable;
            RelaySwitches.RelaySwitchCalculate();
            sendCmd("Send Relay Switches");           // 0 ----

            ControlSwitches.dataSwitch = enable;// icntlsw = set bit 3  turn on data transmission
            ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------

            RelaySwitches.relayTorqueMotor = enable;  // turn on torque motor.
            RelaySwitches.RelaySwitchCalculate();
            sendCmd("Send Relay Switches");           // 0 ----

            ControlSwitches.springTensionSwitch = enable;
            ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------
              */

            // wait for platform to level
            //  sendCmd("Update Gyro Bias Offset");        // 10

            /*
             *
             * RelaySwitches.relay200Hz = 1;// set bit 0  high to turn on 200 Hz
             * sendCmd("Send Relay Switches") ;           // 0
             * Display status window 200Hz,  Cross FOG, Long FOG, Heaters, Torque Motors, Spring Tension
             * read meter status for 30 sec before timeout check for 200Hs
             * if (PortC.irqPresent == 1) then check for gyro
             * if((MeterStatus.xGyro_Fog == 1) && (MeterStatus.lGyro_Fog == 1)){
             * RelaySwitches.relayTorqueMotor = 1;//enable torque motor
             * }
             *
             * */
        }

        public void InitStuff()
        {
            /*    String Version = "7.00";
                Boolean timeBusy = false;
                Boolean seaBusy = false;
                int gpflg = 0; // set to 1 after time updaet in haneia
                int iStop = 0;  // start up inactive
                int iGyroSw = 2;

                int ISED = 1;
                int ICOM = 1;
                int IOutCom = 2;
                int Ierr = 0;
                int Ntry = 0;
                int mStat = 0;
                double CPER = 18.0;
                int LPTSEL = 1; // select beam as default
                int MULATE = 1; // DPL24C dot matrix printer - not needed
                int ISTAT = 14; // Set heater flags high 0b1110
                */
            //iPort[3] = 128;  // Spring Tension ON 0x80
            //     Iport3.sp = 255;  // iPort[3] is not used

            ControlSwitches.DataCollection(enable);// ControlSwitches.dataSwitch = enable;// ICNTLSW = 8; // data on

            double aCrossPeriod = ConfigData.crossPeriod; ;
            double aCrossDampFactor = ConfigData.crossDampFactor;

            double aLongPeriod = ConfigData.longPeriod;
            double aLongDampFactor = ConfigData.longDampFactor;

            /*    MODESW = 0
                LPTSW = 0
                NAMESW =1
               //  IDSKSW = 1 Hard disk options 1 -auto open file, 2 - toggle write switch ???, 3 - manually open file
                JDSKSW = 0              !DON'T WRITE BEFORE Hz200 SW IS ON
                //ISCSW=0 return -99 if no meter number if found
              //  JSCSW = 0               !TURN SCREEN OFF UNTIL AFTER STARTUP  not needed
              //  ICOMSW = 0  Indicates if serial port is  open or closed
                IFORM = 2
                I1FLAG = 0
                IFIL = 4
                KYDAT = '          '
             //   LINEID = 'MY_LINE # '
        c    //    LINEID = '    $HEGRO'     ! SPECIAL DEFAULT FOR SSI
            //    METERNO = '?????'
                NAME = 'NONAME.DAT'
                LARMSW = 0
                GRAV = 0
                    */
        }

        private void TorqueMotorButton_Click(object sender, EventArgs e)
        {
            AutoStart();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            // turn on 200 Hz
            // RelaySwitches.relay200Hz = enable;// set bit 0  high to turn on 200 Hz
            //  RelaySwitches.slew4 = enable;
            //   RelaySwitches.slew5 = enable;
            RelaySwitches.stepperMotorEnable(enable);

            //    RelaySwitches.RelaySwitchCalculate();// 0x80
            RelaySwitches.relaySW = 0x80;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
            sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
            sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
            sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----


          // Thread safe version
            ThreadSerialWriteSafe("Send Relay Switches");           // 0 ----
            ThreadSerialWriteSafe("Set Cross Axis Parameters");      // download platform parameters 4 -----
            ThreadSerialWriteSafe("Set Long Axis Parameters");       // download platform parametersv 5 -----
            ThreadSerialWriteSafe("Update Cross Coupling Values");   // download CC parameters 8     -----



            ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;

            sendCmd("Send Control Switches");           // 1 ----
            sendCmd("Send Control Switches");           // 1 ----
        }
        private void button4_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0xB1;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
            ControlSwitches.controlSw = 0x08; //ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");           // 1 ----
        }
        private void button5_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ControlSwitches.controlSw = 0x08;// ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");           // 1 ----
        }
        private void button7_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x80;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button8_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button9_Click(object sender, EventArgs e)
        {

            RelaySwitches.relaySW = 0x83;// cmd 0
            sendCmd("Send Relay Switches");
            // 0 ----
            ControlSwitches.TorqueMotor(enable);
            ControlSwitches.Alarm(enable);
            // ControlSwitches.controlSw = 0x09; // ControlSwitches.RelayControlSW = 0x09;
            sendCmd("Send Control Switches");           // 1 ----
        }
        private void button10_Click(object sender, EventArgs e)
        {
            RelaySwitches.relaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
            ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");           // 1 ----
        }
        private void button11_Click(object sender, EventArgs e)
        {
            RelaySwitches.alarm(enable);
        //    RelaySwitches.RelaySwitchCalculate();
 
            RelaySwitches.relaySW = 0x83;
            sendCmd("Send Relay Switches");           // 0 ----
        }
        private void button12_Click(object sender, EventArgs e)
        {
            ControlSwitches.DataCollection(enable); //ControlSwitches.dataSwitch = enable;// icntlsw = set bit 3  turn on data transmission
            // ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------
        }
        private void button13_Click(object sender, EventArgs e)
        {
            ControlSwitches.TorqueMotor(enable);// ControlSwitches.torquMotorSwitch = enable;
            //  ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------

            /*    RelaySwitches.relayTorqueMotor = enable;  // turn on torque motor.
                RelaySwitches.RelaySwitchCalculate();
                RelaySwitches.RelaySW = 0x09;
                sendCmd("Send Relay Switches");           // 0 ----*/
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.data_Table_SimulatedTableAdapter1.FillBy(this.dynamicDataDataSet6.Data_Table_Simulated);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}