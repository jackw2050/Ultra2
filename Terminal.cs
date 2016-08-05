/*
 * Project:    SerialPort Terminal
           Search for "comport" to see how I'm using the SerialPort control.
 */

#region Namespace Inclusions

using FileHelpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SerialPortTerminal.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


//  Delimited file operations using FileHelpers  http://www.filehelpers.net
// iTextSharp  http://www.mikesdotnetting.com/article/89/itextsharp-page-layout-with-columns
//http://www.codeproject.com/Articles/686994/Create-Read-Advance-PDF-Report-using-iTextSharp-in#1

#endregion Namespace Inclusions

namespace SerialPortTerminal
{
    #region Public Enumerations

    public enum DataMode { Text, Hex }

    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };

    #endregion Public Enumerations

    public partial class frmTerminal : Form
    {
        public CalculateMarineData mdt = new CalculateMarineData();
        public RelaySwitches RelaySwitches = new RelaySwitches();
        private ConfigData ConfigData = new ConfigData();
        private ControlSwitches ControlSwitches = new ControlSwitches();
        private MeterStatus MeterStatus = new MeterStatus();
        private DataStatusForm DataStatusForm = new DataStatusForm();
        private SerialPortForm SerialPortForm = new SerialPortForm();
        private static ArrayList listDataSource = new ArrayList();
        public Parameters Parameters = new Parameters();
        public UserDataForm UserDataForm = new UserDataForm();

        public DateTime  myDatetime = DateTime.Now;
        private delegate void SetTextCallback(string text);

        private int countDown = 5;
        public static double springTensionScale = .1041666;
        public static int springTensionMaxStep = 2900;
        public static int springTensionFPM = 525;
        public static double springTensionMax = 20000;
        public static int iStop;
        public static int fmpm = 2340;
        public static int[] iStep = { 0, 0, 0, 0, 0 };
        public Boolean completed = false;
        private Boolean crossFogNotReady = true;
        private Boolean longFogNotReady = true;
        private Boolean heaterNotReady = true;
        private int heaterWaitOptions;
        public Single newSpringTension;
        public static Boolean engineerDebug = true;
        public int set = 1;
        public int enable = 1;
        public int clear = 0;
        public int disable = 0;
        public static int fileDateFormat = 1;
        public static string meterNumber;
        public static string gravityFileName;
        public static Boolean surveyNameSet = false;
        public static string surveyName = null;
        public static string customFileName;
        public static string filePath = "c:\\Ultrasys\\data\\";
        public static string programPath = "c:\\ZLS\\";
        public static string configFilePath = "c:\\ZLS\\";
        public static string calFilePath;
        public static string configFileName;
        public static Boolean userSelect = false;
        public static Boolean yesShutDown = false;
        public static Boolean NoShutDown = false;
        public static string dataAquisitionMode;
        public static Boolean gyrosEnabled = false;
        public static Boolean torqueMotorsEnabled = false;
        public static Boolean springTensionEnabled = false;
        public static Boolean alarmsEnabled = false;
  //      public  Boolean engPasswordValid = false;
  //      public  Boolean mgrPasswordValid = false;
  //      public  Boolean userPasswordValid = false;




        //public double[] analogFilter = { 0.0, 0.2, 0.2, 0.2, 0, 2, 1.0, 1.0, 1.0, 10.0 }; // [0] is not used
        public int NAUX = 0;

        // public double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // [0] not used

        public Single crossCouplingFactor13 = 0;
        public Single crossCouplingFactor14 = 0;
        public Single analogFilter5 = 0;
        public double analogFilter6 = 0;
        public double aCrossPeriod = 0;
        public double aLongPeriod = 0;
        public double aCrossDampFactor = 0;
        public double aLongDampFactor = 0;
        public double aCrossGain = 0;
        public double aLongGain = 0;
        public double aCrossLead = 0;
        public double aLongLead = 0;
        public static Int16 meter_status = 0;
        private Boolean autostart = false;

        public static DateTime fileStartTime;
        public static string fileName = programPath + "test.csv";// c:/zls
        public static string calFileName;

        public static bool fileRecording = false;
        public static bool firstTime = true;
        public static string fileType;
        public static DateTime oldTime = DateTime.Now;
        public string timePeriod;
        public int timeValue;
        // public EngineeringForm EngineeringForm = new EngineeringForm();

        public string connectionString = "Data Source=LAPTOPSERVER\\ULTRASYSDEV;Initial Catalog=DynamicData;Integrated Security=True;Max Pool Size=50;Min Pool Size=5;Pooling=True";
        private BindingSource bindingSource1 = new BindingSource();

        // Database connection strings etc.
        private SqlConnection myConnection = new SqlConnection("Data Source=LAPTOPSERVER\\ULTRASYSDEV;Initial Catalog=DynamicData;Integrated Security=True;Max Pool Size=50;Min Pool Size=5;Pooling=True");

        public Boolean filterData = false;

        public class startupData
        {
            public string Status;
        }

        public class shutdownData
        {
            public string shutDownText;
        }

        private class Record
        {
            private DateTime dateTime;
            private double digitalGravity, springTension, crossCoupling, rawBeam, vcc, al, ax, ve, ax2;
            private double xacc2, lacc2, xacc, lacc, totalCorrection, longitude, latitude, altitude;
            private int year, day, hour, minute, second;
            private Single gpsStatus;

            public Record(DateTime dateTime, double digitalGravity, double springTension, double crossCoupling, double rawBeam, double vcc, double al, double ax, double ve, double ax2, double xacc2, double lacc2, double xacc, double lacc, double totalCorrection)
            {
                this.dateTime = dateTime;
                this.digitalGravity = digitalGravity;
                this.springTension = springTension;
                this.crossCoupling = crossCoupling;
                this.rawBeam = rawBeam;
                this.vcc = vcc;
                this.al = al;
                this.ax = ax;
                this.ve = ve;
                this.ax2 = ax2;
                this.xacc2 = xacc2;
                this.lacc2 = lacc2;
                this.xacc = xacc;
                this.lacc = lacc;
                this.totalCorrection = totalCorrection;
            }

            public int Year
            {
                get { return year; }
                set { year = value; }
            }

            public int Day
            {
                get { return day; }
                set { day = value; }
            }

            public int Hour
            {
                get { return hour; }
                set { hour = value; }
            }

            public int Minute
            {
                get { return Minute; }
                set { Minute = value; }
            }

            public int Second
            {
                get { return Second; }
                set { Second = value; }
            }

            public DateTime Date
            {
                get { return dateTime; }
                set { dateTime = value; }
            }

            public double DigitalGravity
            {
                get { return digitalGravity; }
                set { digitalGravity = value; }
            }

            public double SpringTension
            {
                get { return springTension; }
                set { springTension = value; }
            }

            public double CrossCoupling
            {
                get { return crossCoupling; }
                set { crossCoupling = value; }
            }

            public double RawBeam
            {
                get { return rawBeam; }
                set { rawBeam = value; }
            }

            public double VCC
            {
                get { return vcc; }
                set { vcc = value; }
            }

            public double AL
            {
                get { return al; }
                set { al = value; }
            }

            public double AX
            {
                get { return ax; }
                set { ax = value; }
            }

            public double VE
            {
                get { return ve; }
                set { ve = value; }
            }

            public double AX2
            {
                get { return ax2; }
                set { ax2 = value; }
            }

            public double XACC2
            {
                get { return xacc2; }
                set { xacc2 = value; }
            }

            public double LACC2
            {
                get { return lacc2; }
                set { lacc2 = value; }
            }

            public double XACC
            {
                get { return xacc; }
                set { xacc = value; }
            }

            public double LACC
            {
                get { return lacc; }
                set { lacc = value; }
            }

            public double TotalCorrection
            {
                get { return totalCorrection; }
                set { totalCorrection = value; }
            }

            public double Latitude
            {
                get { return latitude; }
                set { latitude = value; }
            }

            public double Longitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            public double Altitude
            {
                get { return altitude; }
                set { altitude = value; }
            }

            public Single GPS_Status
            {
                get { return gpsStatus; }
                set { gpsStatus = value; }
            }
        }

        public void CleanUp(string timePeriod, int timeValue)
        {
            int maxArraySize = 60;// initialize for 60 seconds
            if (timePeriod == "seconds")
            {
                maxArraySize = timeValue;
            }
            else if (timePeriod == "minutes")
            {
                maxArraySize = 60 * timeValue;
            }
            else if (timePeriod == "hours")
            {
                maxArraySize = 3600 * timeValue;
            }

            if (listDataSource.Count > maxArraySize)
            {
                listDataSource.RemoveAt(0);
            }
        }

        #region Local Variables

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

            _timer1 = new System.Windows.Forms.Timer();
            _timer1.Interval = (3000 - DateTime.Now.Millisecond);
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

            if (comport.IsOpen) btnOpenPort.Text = "&Disconnect Meter";
            else btnOpenPort.Text = "&Connecto to Meter";
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
            /*
            Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
                //tfTerminal.SelectionFont = new Font(SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
            */
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

        #region Callbacks

        public delegate void UpdateRecordBoxCallback(Boolean i);

        public delegate void UpdateFileNameLabelCallback();

        public delegate void UpdatedebugLabelCallback(string debugData);

        public delegate void UpdateTimeTextCallback();

        public delegate void UpdateFileTimeCallback();

        public delegate void ShutDownTextCallback();

        #endregion Callbacks

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
                initMeter();
                meterStatusGroupBox.Visible = true;
                //  startupGroupBox.Visible = true;
                //  gyroCheckBox.Enabled = true;
                //  alarmCheckBox.Enabled = true;
                TimerWithDataCollection("start");

                /*
                // START 1 SEC TIMER
                _timer1.Interval = 1000; // (1000 - DateTime.Now.Millisecond);
                _timer1.Enabled = true;
                _timer1.Start();
                ControlSwitches.DataCollection(enable);
                sendCmd("Send Control Switches");           // 1 ----
                */
            }

            // Change the state of the form's controls
            EnableControls();
        }

        public void TimerWithDataCollection(string state)
        {
            switch (state)
            {
                case "start":// enables data collection and starts 1 second timer
                    _timer1.Interval = 1000; // (1000 - DateTime.Now.Millisecond);
                    _timer1.Enabled = true;
                    _timer1.Start();
                    ControlSwitches.DataCollection(enable);
                    sendCmd("Send Control Switches");           // 1 ----
                    break;

                case "stop":// disables data collection and stops 1 second timer
                    _timer1.Interval = 1000; // (1000 - DateTime.Now.Millisecond);
                    _timer1.Enabled = true;
                    _timer1.Stop();
                    ControlSwitches.DataCollection(disable);
                    sendCmd("Send Control Switches");           // 1 ----
                    break;

                default:
                    break;
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

                    GravityChart.DataBind();
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

                    ThreadProcSafe();
                }

                //          UpdateTextBox(DateTime newDT, double ST, double Beam);

                //           f2.GravityRichTextBox1.Text = Convert.ToString(mdt.myDT ) +  "         " + Convert.ToString(mdt.SpringTension) + "             " + Convert.ToString(mdt.Beam) + "/n/n";
            }
        }

        private void ThreadProcSafe()
        {
            string text = Convert.ToString(mdt.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(mdt.year) + "\t" + Convert.ToString(mdt.day));

            // Check if this method is running on a different thread
            // than the thread that created the control.
            if (this.DataStatusForm.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
                Thread.Sleep(2000);
            }
            else
            {
                if (DataStatusForm.Visible == true)
                {
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
                    DataStatusForm.textBox21.Text = Convert.ToString(meter_status, 2);
                    DataStatusForm.byte76TextBox.Text = Convert.ToString(meter_status, 2);
                    DataStatusForm.byte77TextBox.Text = Convert.ToString(mdt.portCStatus, 2);
                    DataStatusForm.relaySwitchesTextBox.Text = Convert.ToString(RelaySwitches.relaySW, 2);
                    DataStatusForm.controlSwitchesTextBox.Text = Convert.ToString(ControlSwitches.controlSw, 2);
                }

                if (UserDataForm.Visible)
                {
                    UserDataForm.textBox1.Text = (mdt.gravity.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox2.Text = (mdt.SpringTension.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox3.Text = (mdt.CrossCoupling.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox4.Text = (mdt.Beam.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox5.Text = (mdt.myDT.ToString());
                    UserDataForm.textBox6.Text = (mdt.totalCorrection.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox7.Text = (mdt.VCC.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox8.Text = (mdt.AL.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox9.Text = (mdt.AX.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox10.Text = (mdt.VE.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox11.Text = (mdt.AX2.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox12.Text = (mdt.XACC2.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox13.Text = (mdt.LACC2.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox14.Text = (mdt.XACC.ToString("N", CultureInfo.InvariantCulture));
                    UserDataForm.textBox15.Text = (mdt.LACC.ToString("N", CultureInfo.InvariantCulture));

                    if (MeterStatus.lGyro_Fog == 0)
                    {
                        UserDataForm.longGyroStatusLabel.ForeColor = Color.Green;
                        UserDataForm.longGyroStatusLabel.Text = "Ready";
                    }
                    else
                    {
                        UserDataForm.longGyroStatusLabel.ForeColor = Color.Red;
                        UserDataForm.longGyroStatusLabel.Text = "Not Ready";
                    }
                    if (MeterStatus.xGyro_Fog == 0)
                    {
                        UserDataForm.crossGyroStatusLabel.ForeColor = Color.Green;
                        UserDataForm.crossGyroStatusLabel.Text = "Ready";
                    }
                    else
                    {
                        UserDataForm.crossGyroStatusLabel.ForeColor = Color.Red;
                        UserDataForm.crossGyroStatusLabel.Text = "Not Ready";
                    }
                    if (MeterStatus.meterHeater == 0)
                    {
                        UserDataForm.heaterStatusLabel.ForeColor = Color.Green;
                        UserDataForm.heaterStatusLabel.Text = "Ready";
                    }
                    else
                    {
                        UserDataForm.heaterStatusLabel.ForeColor = Color.Red;
                        UserDataForm.heaterStatusLabel.Text = "Not Ready";
                    }
                }

                string specifier;
                specifier = "000.000000";
                textBox16.Text = (mdt.altitude.ToString("N", CultureInfo.InvariantCulture));
                textBox17.Text = (mdt.latitude.ToString(specifier, CultureInfo.InvariantCulture));
                textBox18.Text = (mdt.longitude.ToString(specifier, CultureInfo.InvariantCulture));
                if ((mdt.gpsNavigationStatus == 0) & (mdt.gpsTimeStatus == 0) & (mdt.gpsSyncStatus == 0))
                {
                    gpsStartusTextBox.ForeColor = Color.Green;
                    gpsStartusTextBox.Text = "Good";
                }
                else
                {
                    gpsStartusTextBox.ForeColor = Color.Red;
                    gpsStartusTextBox.Text = "Error";
                }

                if (UserDataForm.Visible)
                {
                    UserDataForm.gpsSatelitesTextBox.Text = mdt.gpsNumSatelites.ToString();

                    if (mdt.gpsNavigationStatus == 0)
                    {
                        UserDataForm.gpsNavigationTextBox.ForeColor = Color.Green;
                        UserDataForm.gpsNavigationTextBox.Text = "Good";
                    }
                    else
                    {
                        UserDataForm.gpsNavigationTextBox.ForeColor = Color.Red;
                        UserDataForm.gpsNavigationTextBox.Text = "Navigation data not available";
                    }
                    if (mdt.gpsSyncStatus == 0)
                    {
                        UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Green;
                        UserDataForm.gps1HzSynchTextBox.Text = "Good";
                    }
                    else
                    {
                        UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Red;
                        UserDataForm.gps1HzSynchTextBox.Text = "1 Hz synch pulse not present";
                    }
                    if (mdt.gpsTimeStatus == 0)
                    {
                        UserDataForm.gpsTimeSetTextBox.ForeColor = Color.Green;
                        UserDataForm.gpsTimeSetTextBox.Text = "Good";
                    }
                    else
                    {
                        UserDataForm.gpsTimeSetTextBox.ForeColor = Color.Red;
                        UserDataForm.gpsTimeSetTextBox.Text = "GPS time set unsuccesful";
                    }
                }

                // Fill up list aray
                if (GravityChart.Visible)
                {
                    listDataSource.Add(new Record(mdt.Date, mdt.gravity, mdt.SpringTension, mdt.CrossCoupling, mdt.Beam, mdt.VCC, mdt.AL, mdt.AX, mdt.VE, mdt.AX2, mdt.XACC2, mdt.LACC2, mdt.XACC, mdt.LACC, mdt.totalCorrection));
                    GravityChart.DataSource = listDataSource;
                }


            

                // write config values to parameter form when required
                if (Parameters.updateConfigData)
                {
                    Parameters.updateConfigData = false;
                    Parameters.crossPeriodTextBox.Text = (ConfigData.crossPeriod.ToString("N", CultureInfo.InvariantCulture));  // Convert.ToString( ConfigData.crossPeriod, 6 );
                    Parameters.crossDampingTextBox.Text = Convert.ToString(ConfigData.crossDampFactor);
                    Parameters.crossGainTextBox.Text = Convert.ToString(ConfigData.crossGain);
                    Parameters.crossLeadTextBox.Text = Convert.ToString(ConfigData.crossLead);
                    Parameters.crossCompFactor4TextBox.Text = Convert.ToString(ConfigData.crossCompFactor_4);
                    Parameters.crossCompPhase4TextBox.Text = Convert.ToString(ConfigData.crossCompPhase_4 );
                    Parameters.crossCompFactor16TextBox.Text = Convert.ToString(ConfigData.crossCompFactor_16);
                    Parameters.crossCompPhase16TextBox.Text = Convert.ToString(ConfigData.crossCompPhase_16);



                    Parameters.longPeriodTextBox.Text = Convert.ToString(ConfigData.longPeriod);
                    Parameters.longDampingTextBox.Text = Convert.ToString(ConfigData.longDampFactor);
                    Parameters.longGainTextBox.Text = Convert.ToString(ConfigData.longGain);
                    Parameters.longLeadTextBox.Text = Convert.ToString(ConfigData.longLead);
                    Parameters.longCompFactor4TextBox.Text = Convert.ToString(ConfigData.longCompFactor_4);
                    Parameters.longCompPhase4TextBox.Text = Convert.ToString(ConfigData.longCompPhase_4);
                    Parameters.longCompFactor16TextBox.Text = Convert.ToString(ConfigData.longCompFactor_16);
                    Parameters.longCompPhase16TextBox.Text = Convert.ToString(ConfigData.longCompPhase_16);


                    Parameters.CMLFactorTextBox.Text = Convert.ToString(ConfigData.CML_Fact);
                    Parameters.ALFactorTextBox.Text = Convert.ToString(ConfigData.AL_Fact);
                    Parameters.AXFactorTextBox.Text = Convert.ToString(ConfigData.AX_Fact);
                    Parameters.VEFactorTextBox.Text = Convert.ToString(ConfigData.VE_Fact);
                    Parameters.CMXFactorTextBox.Text = Convert.ToString(ConfigData.CMX_Fact);
                    Parameters.XACC2FactorTextBox.Text = Convert.ToString(ConfigData.XACC2_Fact);
                    Parameters.LACC2FactorTextBox.Text = Convert.ToString(ConfigData.LACC2_Fact);
                    Parameters.XACCPhasetextBox.Text = Convert.ToString(ConfigData.XACC_Phase);
                    Parameters.LACC_AL_PhaseTextBox.Text = Convert.ToString(ConfigData.LACC_AL_Phase);
                    Parameters.LACC_CML_PhaseTextBox.Text = Convert.ToString(ConfigData.LACC_CML_Phase);
                    Parameters.LACC_CMX_PhaseTextBox.Text = Convert.ToString(ConfigData.LACC_CMX_Phase);

                    Parameters.maxSpringTensionTextBox.Text = Convert.ToString(ConfigData.springTensionMax);
                    Parameters.gyroTypeComboBox.SelectedText = ConfigData.gyroType;
                    Parameters.meterNumberTextBox.Text = ConfigData.meterNumber;
                    Parameters.kFactorTextBox.Text = Convert.ToString(ConfigData.kFactor);
                    Parameters.screenDisplayFilterTextBox.Text = Convert.ToString(ConfigData.screenDisplayFilter);
                    





                    Boolean clearCheckedBoxes = true;
                    if (clearCheckedBoxes)
                    {

                        // reset checkboxs
                        Parameters.crossPeriodTextBox.Enabled = false;
                        Parameters.crossDampingTextBox.Enabled = false;
                        Parameters.crossGainTextBox.Enabled = false;
                        Parameters.crossLeadTextBox.Enabled = false;
                        Parameters.crossCompFactor4TextBox.Enabled = false;
                        Parameters.crossCompPhase4TextBox.Enabled = false;
                        Parameters.crossCompFactor16TextBox.Enabled = false;                  
                        Parameters.crossCompPhase16TextBox.Enabled = false;
                        Parameters.longPeriodTextBox.Enabled = false;
                        Parameters.longDampingTextBox.Enabled = false;
                        Parameters.longGainTextBox.Enabled = false;
                        Parameters.longLeadTextBox.Enabled = false;
                        Parameters.longCompFactor4TextBox.Enabled = false;
                        Parameters.longCompPhase4TextBox.Enabled = false;
                        Parameters.longCompFactor16TextBox.Enabled = false;
                        Parameters.longCompPhase16TextBox.Enabled = false;



                        Parameters.crossPeriodCheckBox.Checked = false;
                        Parameters.crossDampingCheckBox.Checked = false;
                        Parameters.crossGainCheckBox.Checked = false;
                        Parameters.crossLeadCheckBox.Checked = false;
                        Parameters.crossCompFactor4CheckBox.Checked = false;
                        Parameters.crossCompPhase4CheckBox.Checked = false;
                        Parameters.crossCompFactor16CheckBox.Checked = false;
                        Parameters.crossCompPhase16CheckBox.Checked = false;
                        Parameters.longPeriodCheckBox.Checked = false;
                        Parameters.longDampingCheckBox.Checked = false;
                        Parameters.longGainCheckBox.Checked = false;
                        Parameters.longLeadCheckBox.Checked = false;
                        Parameters.longCompFactor4CheckBox.Checked = false;
                        Parameters.longCompPhase4CheckBox.Checked = false;
                        Parameters.longCompFactor16CheckBox.Checked = false;
                        Parameters.longCompPhase16CheckBox.Checked = false;



                        Parameters.CMLFactorTextBox.Enabled = false;
                        Parameters.ALFactorTextBox.Enabled = false;
                        Parameters.AXFactorTextBox.Enabled = false;
                        Parameters.VEFactorTextBox.Enabled = false;
                        Parameters.CMXFactorTextBox.Enabled = false;
                        Parameters.XACC2FactorTextBox.Enabled = false;
                        Parameters.LACC2FactorTextBox.Enabled = false;
                        Parameters.XACCPhasetextBox.Enabled = false;
                        Parameters.LACC_AL_PhaseTextBox.Enabled = false;
                        Parameters.LACC_CML_PhaseTextBox.Enabled = false;
                        Parameters.LACC_CMX_PhaseTextBox.Enabled = false;

                        Parameters.CMLFactorCheckBox.Checked = false;
                        Parameters.ALFactorCheckBox.Checked = false;
                        Parameters.AXFactorCheckBox.Checked = false;
                        Parameters.VEFactorCheckBox.Checked = false;
                        Parameters.CMXFactorCheckBox.Checked = false;
                        Parameters.XACC2FactorCheckBox.Checked = false;
                        Parameters.LACC2FactorCheckBox.Checked = false;
                        Parameters.XACCPhaseCheckBox.Checked = false;
                        Parameters.LACC_AL_PhaseCheckBox.Checked = false;
                        Parameters.LACC_CML_PhaseCheckBox.Checked = false;
                        Parameters.LACC_CMX_PhaseCheckBox.Checked = false;



                        Parameters.maxSpringTensionTextBox.Enabled = false;
                        Parameters.gyroTypeComboBox.Enabled = false;
                        Parameters.meterNumberTextBox.Enabled = false;
                        Parameters.kFactorTextBox.Enabled = false;
                        Parameters.screenDisplayFilterTextBox.Enabled = false;



                        Parameters.maxSpringTensionCheckBox.Checked = false;
                        Parameters.gyroTypeCheckBox.Checked = false;
                        Parameters.meterNumberCheckBox.Checked = false;
                        Parameters.kFactorCheckBox.Checked = false;
                        Parameters.screenDisplayFilterCheckBox.Checked = false;


                    }






                }

                myData myData = new myData();
                myData.Date = mdt.Date;
                myData.Gravity = mdt.gravity;
                myData.SpringTension = mdt.SpringTension;
                myData.CrossCoupling = mdt.CrossCoupling;
                myData.RawBeam = mdt.Beam;
                myData.VCC = mdt.VCC;
                myData.AL = mdt.AL;
                myData.AX = mdt.AX;
                myData.VE = mdt.VE;
                myData.AX2 = mdt.AX2;
                myData.XACC2 = mdt.XACC2;
                myData.LACC2 = mdt.LACC2;
                myData.XACC = mdt.XACC;
                myData.LACC = mdt.LACC;
                myData.TotalCorrection = mdt.totalCorrection;
                myData.longitude = mdt.longitude;
                myData.latitude = mdt.latitude;
                myData.altitude = mdt.altitude;
                myData.gpsStatus = mdt.gpsStatus;
                if (fileRecording == true)
                {
                    RecordDataToFile("Append", myData);
                }

                CleanUp("minutes", 1);// limit chart to 10 min

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (meterStatusGroupBox.Visible)
                {
                    FogCheck();
                }

                if (MeterStatus.lGyro_Fog == 0)
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        crossGyroStatusLabel.ForeColor = Color.Green;
                        crossGyroStatusLabel.Text = "Ready";
                    }
                    DataStatusForm.textBox19.Text = "OK";
                }
                else
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        crossGyroStatusLabel.ForeColor = Color.Red;
                        crossGyroStatusLabel.Text = "Not Ready";
                    }

                    DataStatusForm.textBox19.Text = "Bad";
                }
                if (MeterStatus.xGyro_Fog == 0)
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        longGyroStatusLabel.ForeColor = Color.Green;
                        longGyroStatusLabel.Text = "Ready";
                    }
                    DataStatusForm.textBox20.Text = "OK";
                }
                else
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        longGyroStatusLabel.ForeColor = Color.Red;
                        longGyroStatusLabel.Text = "Not Ready";
                    }

                    DataStatusForm.textBox20.Text = "Bad";
                }
                if (meter_status == 0)
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        heaterStatusLabel.ForeColor = Color.Green;
                        heaterStatusLabel.Text = "Ready";
                    }
                    if (DataStatusForm.Visible)
                    {
                        DataStatusForm.textBox21.Text = "OK";
                    }
                }
                else
                {
                    if (meterStatusGroupBox.Visible)
                    {
                        heaterStatusLabel.ForeColor = Color.Red;
                        heaterStatusLabel.Text = "Not ready";
                    }
                    if (DataStatusForm.Visible)
                    {
                        DataStatusForm.textBox21.Text = "Not ready";
                    }
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

                if (meterStatusGroupBox.Visible)
                {
                    // FogCheck();
                    if (completed)
                    {
                        countDown--;
                    }

                    if (completed = true & countDown == 0)
                    {
                        //Console.WriteLine("ready for gyros");
                        //    Task taskC = Task.Factory.StartNew(() => CloseMeterStatus());
                        //   taskC.Wait();
                        meterStatusGroupBox.Visible = false;
                        startupGroupBox.Visible = true;
                    }
                }

                //   textBox19.Text = (MeterStatus.lGyro_Fog.ToString("N", CultureInfo.InvariantCulture));// long
                //   textBox20.Text = (MeterStatus.xGyro_Fog.ToString("N", CultureInfo.InvariantCulture));// cross
                //   textBox21.Text = (MeterStatus.meterHeater.ToString("N", CultureInfo.InvariantCulture));// meter
            }

            //        this.Invoke(d, new object[] { text });
        }

        private void CloseMeterStatus()
        {
            Thread.Sleep(5);
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

        private void SetupChart()
        {
            BindingSource SBind = new BindingSource();
            //   SBind.DataSource = dataTable;

            //   string chartAreaType = "Single chart area";
            this.GravityChart.Series.Add("Digital Gravity");
            this.GravityChart.Series.Add("Spring Tension");
            this.GravityChart.Series.Add("Cross Coupling");
            this.GravityChart.Series["Cross Coupling"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GravityChart.Series.Add("Raw Beam");
            this.GravityChart.Series.Add("Total Correction");

            //      SETUP MAIN PAIGE GRAVITY CHART
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;
            this.GravityChart.Series["Digital Gravity"].XValueMember = "date";
            this.GravityChart.Series["Digital Gravity"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Digital Gravity"].YValueMembers = "DigitalGravity";
            this.GravityChart.Series["Digital Gravity"].BorderWidth = 4;

            this.GravityChart.Series["Spring Tension"].XValueMember = "date";
            this.GravityChart.Series["Spring Tension"].YValueMembers = "springTension";
            this.GravityChart.Series["Spring Tension"].BorderWidth = 4;

            this.GravityChart.Series["Cross Coupling"].XValueMember = "date";
            this.GravityChart.Series["Cross Coupling"].YValueMembers = "crossCoupling";
            this.GravityChart.Series["Cross Coupling"].BorderWidth = 4;

            this.GravityChart.Series["Raw Beam"].XValueMember = "date";
            this.GravityChart.Series["Raw Beam"].YValueMembers = "RawBeam";
            this.GravityChart.Series["Raw Beam"].BorderWidth = 4;

            /*
                        this.GravityChart.Series["Total Correction"].XValueMember = "date";
                        this.GravityChart.Series["Total Correction"].YValueMembers = "TotalCorrection";
                        this.GravityChart.Series["Total Correction"].BorderWidth = 4;
                        */
            this.GravityChart.Titles.Add("Gravity");

            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90

            //Enable range selection and zooming end user interface

            this.GravityChart.ChartAreas["Gravity"].CursorX.IsUserEnabled = true;
            this.GravityChart.ChartAreas["Gravity"].CursorY.IsUserEnabled = true;
            this.GravityChart.ChartAreas["Gravity"].CursorX.IsUserSelectionEnabled = true;
            this.GravityChart.ChartAreas["Gravity"].CursorY.IsUserSelectionEnabled = true;
            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoomable = true;
            this.GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;
            this.GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.IsPositionedInside = true;
            this.GravityChart.ChartAreas["Gravity"].AxisY.ScrollBar.IsPositionedInside = true;

            //      SETUP FLOATING CROSS COUPLING CHART
            //         this.GravityChart.Series.Add("Raw Gravity");
            this.GravityChart.Series.Add("AL");
            this.GravityChart.Series.Add("AX");
            this.GravityChart.Series.Add("VE");
            this.GravityChart.Series["VE"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GravityChart.Series.Add("AX2");
            this.GravityChart.Series["AX2"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GravityChart.Series.Add("XACC");
            this.GravityChart.Series.Add("LACC");

            //       this.GravityChart.Series["CrossCoupling"].ChartArea = "Raw Gravity";
            this.GravityChart.Series["AL"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["AX"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["VE"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["AX2"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["XACC"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["LACC"].ChartArea = "CrossCoupling";

            this.GravityChart.Series["AL"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["AX"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["VE"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["AX2"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["XACC"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["LACC"].Legend = "Cross Coupling Legend";

            /*
                        this.GravityChart.Series["Raw Gravity"].XValueMember = "dateTime";
                        this.GravityChart.Series["Raw Gravity"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
                        this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
                        this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;
                        this.GravityChart.Series["Raw Gravity"].YValueMembers = "gravity";
                        this.GravityChart.Series["Raw Gravity"].BorderWidth = 4;
                        //     this.GravityChart.DataSource = dataTable;
                        //       this.GravityChart.DataBind();
            */

            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Format = "yyyy-MM-dd HH:mm:ss";
            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;

            this.GravityChart.Series["AL"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["AL"].XValueMember = "date";
            this.GravityChart.Series["AL"].YValueMembers = "springTension";
            this.GravityChart.Series["AL"].BorderWidth = 2;

            this.GravityChart.Series["AX"].XValueMember = "date";
            this.GravityChart.Series["AX"].YValueMembers = "crossCoupling";
            this.GravityChart.Series["AX"].BorderWidth = 2;

            this.GravityChart.Series["VE"].XValueMember = "date";
            this.GravityChart.Series["VE"].YValueMembers = "RawBeam";
            this.GravityChart.Series["VE"].BorderWidth = 2;

            this.GravityChart.Series["AX2"].XValueMember = "date";
            this.GravityChart.Series["AX2"].YValueMembers = "AX2";
            this.GravityChart.Series["AX2"].BorderWidth = 2;

            this.GravityChart.Series["XACC"].XValueMember = "date";
            this.GravityChart.Series["XACC"].YValueMembers = "XACC";
            this.GravityChart.Series["XACC"].BorderWidth = 2;

            this.GravityChart.Series["LACC"].XValueMember = "date";
            this.GravityChart.Series["LACC"].YValueMembers = "LACC";
            this.GravityChart.Series["LACC"].BorderWidth = 2;

            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoom(2, 3);
            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90
            //Enable range selection and zooming end user interface

            this.GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserEnabled = true;
            this.GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserEnabled = true;
            this.GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserSelectionEnabled = true;
            this.GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserSelectionEnabled = true;
            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoomable = true;
            this.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.Zoomable = true;
            this.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.IsPositionedInside = true;
            this.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScrollBar.IsPositionedInside = true;

            //    SetTraceColor(Properties.Settings.Default

            SetChartType();
            SetTraceColor("bright");// Properties.Settings.Default.tracePalette);//              Set trace color palette
            SetChartAreaColors(2);// Properties.Settings.Default.chartColor);//           Set chart background color
            SetChartBorderWidth(2); // Properties.Settings.Default.traceWidth);//          Set trace width
            ChartMarkers(false);// Properties.Settings.Default.traceMarkers);//               Enable/ disable trace markers
            SetLegendLocation();// Properties.Settings.Default.chartLegendLocation);//   Set legend location
            ExtraChartStuff();
            SetChartToolTips();
            SetChartCursors();
            SetChartZoom();
            SetChartScroll();
            //          SetLegend();
        }

        private void SetChartCursors()
        {
            // Set cursor interval properties
            GravityChart.ChartAreas["Gravity"].CursorX.Interval = .001D;
            GravityChart.ChartAreas["Gravity"].CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            GravityChart.ChartAreas["Gravity"].CursorX.IsUserEnabled = true;
            GravityChart.ChartAreas["Gravity"].CursorX.IsUserSelectionEnabled = true;
            GravityChart.ChartAreas["Gravity"].CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;

            GravityChart.ChartAreas["CrossCoupling"].CursorX.Interval = .001D;
            GravityChart.ChartAreas["CrossCoupling"].CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserEnabled = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserSelectionEnabled = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;

            //  Y AXIS
            GravityChart.ChartAreas["Gravity"].CursorY.IsUserEnabled = true;
            GravityChart.ChartAreas["Gravity"].CursorY.IsUserSelectionEnabled = true;
            GravityChart.ChartAreas["Gravity"].CursorY.Interval = 1;

            GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserEnabled = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserSelectionEnabled = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorY.Interval = 1;
        }

        private void SetChartZoom()
        {
            // Set automatic zooming
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.Zoomable = true;

            //            GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(2, 3);
            //            GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset(1);
        }

        private void SetChartScaleView()
        {
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.SmallScrollSize = .1D;
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;

            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.ZoomReset(1);

            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.SmallScrollMinSize = .1D;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.Zoomable = true;

            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoom(2, 3);
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.ZoomReset(1);
            GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.ZoomReset(1);
        }

        private void SetChartScroll()
        {
            // Set automatic scrolling
            GravityChart.ChartAreas["Gravity"].CursorX.AutoScroll = true;
            GravityChart.ChartAreas["Gravity"].CursorY.AutoScroll = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorX.AutoScroll = true;
            GravityChart.ChartAreas["CrossCoupling"].CursorY.AutoScroll = true;
        }

        private void SetChartScrollBars()
        {
            // Change scrollbar colors
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.BackColor = System.Drawing.Color.LightGray;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.IsPositionedInside = false;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.Size = 12;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.ButtonStyle =
            System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.All ^ System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.ResetZoom;

            // Scrollbars position
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.IsPositionedInside = true;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.Enabled = true;
            GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.IsPositionedInside = true;
            GravityChart.ChartAreas["Gravity"].AxisY.ScrollBar.IsPositionedInside = true;

            // Cross Coupling
            // Change scrollbar colors
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.BackColor = System.Drawing.Color.LightGray;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Gray;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.IsPositionedInside = false;

            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.Size = 12;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.ButtonStyle =
            System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.All ^ System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.ResetZoom;

            // Scrollbars position
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.IsPositionedInside = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.Enabled = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.IsPositionedInside = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisY.ScrollBar.IsPositionedInside = true;
        }

        private void SetChartToolTips()
        {
            string mode = "Value";

            if (mode == "Time/Value")
            {
                GravityChart.Series["Digital Gravity"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["Spring Tension"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["Cross Coupling"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["Raw Beam"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["Total Correction"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["AL"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["AX"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["VE"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["AX2"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["XACC"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["LACC"].ToolTip = "Time = #VALX\n#VALY";
                GravityChart.Series["LACC"].ToolTip = "Time = #VALX\n#VALY";
            }
            else if (mode == "Value")
            {
                //     myData d = new myData();
                GravityChart.Series["Digital Gravity"].ToolTip = "Gravity =  " + "#VALY";
                GravityChart.Series["Spring Tension"].ToolTip = "Spring Tension = " + "#VALY";
                GravityChart.Series["Cross Coupling"].ToolTip = "Cross Coupling = " + "#VALY";
                GravityChart.Series["Raw Beam"].ToolTip = "Raw Beam = " + "#VALY";
                GravityChart.Series["Total Correction"].ToolTip = "Total Correction = " + "#VALY";
                GravityChart.Series["AL"].ToolTip = "AL = " + "#VALY";
                GravityChart.Series["AX"].ToolTip = "XL = " + "#VALY";
                GravityChart.Series["VE"].ToolTip = "VE = " + "#VALY";
                GravityChart.Series["AX2"].ToolTip = "AX2 = " + "#VALY";
                GravityChart.Series["XACC"].ToolTip = "XACC = " + "#VALY";
                GravityChart.Series["LACC"].ToolTip = "LACC = " + "#VALY";
            }
        }

        public void SetChartAreaColors(int scheme)
        {
            if (scheme == 0)// Light background
            {
                //  GRAVITY
                GravityChart.ChartAreas["Gravity"].BackColor = System.Drawing.Color.White;
                GravityChart.ChartAreas["Gravity"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;

                // CROSS COUPLING
                GravityChart.ChartAreas["CrossCoupling"].BackColor = System.Drawing.Color.White;
                GravityChart.ChartAreas["CrossCoupling"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;
                //     Properties.Settings.Default.chartColor = 0;
            }
            if (scheme == 1)// gray background
            {
                //  GRAVITY
                GravityChart.ChartAreas["Gravity"].BackColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["Gravity"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;

                // CROSS COUPLING
                GravityChart.ChartAreas["CrossCoupling"].BackColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["CrossCoupling"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Black;
                //  Properties.Settings.Default.chartColor = 1;
            }
            if (scheme == 2)// black background
            {
                //  GRAVITY
                GravityChart.ChartAreas["Gravity"].BackColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["Gravity"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["Gravity"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["Gravity"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["Gravity"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gray;

                // CROSS COUPLING
                GravityChart.ChartAreas["CrossCoupling"].BackColor = System.Drawing.Color.Black;
                GravityChart.ChartAreas["CrossCoupling"].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["CrossCoupling"].AxisX2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gray;
                // Properties.Settings.Default.chartColor = 2;
            }
        }

        private void SetTraceColor(string colorPalette)
        {
            // colorPalette = "Bright";
            switch (colorPalette)
            {
                case "None":
                    this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
                    //   Properties.Settings.Default.tracePalette = "None";
                    break;

                case "Bright":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
                    //       Properties.Settings.Default.GravityChart = "Bright";
                    break;

                case "Grayscale":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
                    //      Properties.Settings.Default.tracePalette = "Grayscale";
                    break;

                case "Excel":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
                    //    Properties.Settings.Default.tracePalette = "Excel";
                    break;

                case "Light":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Light;
                    break;

                case "Pastel":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
                    //    Properties.Settings.Default.tracePalette = "Pastel";
                    break;

                case "EarthTones":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
                    //    Properties.Settings.Default.tracePalette = "EarthTones";
                    break;

                case "SemiTransparant":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
                    //     Properties.Settings.Default.tracePalette = "SemiTransparant";
                    break;

                case "Berry":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
                    //     Properties.Settings.Default.tracePalette = "Berry";
                    break;

                case "Chocolate":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
                    //    Properties.Settings.Default.tracePalette = "Chocolate";
                    break;

                case "Fire":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
                    //   Properties.Settings.Default.tracePalette = "Fire";
                    break;

                case "SeaGreen":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
                    //      Properties.Settings.Default.tracePalette = "SeaGreen";
                    break;

                case "BrightPastel":
                    this.GravityChart.Palette = this.GravityChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
                    //     Properties.Settings.Default.tracePalette = "BrightPastel";
                    break;

                default:
                    break;
            }
            this.GravityChart.ApplyPaletteColors();
        }

        private void SetLegendLocation()
        {
            /*
            string location = "top";
            switch (location)
            {
                case "Bottom":
                    GravityChart.Legends["Legend1"].Docking = Docking.Bottom;
                    GravityChart.Legends["Legend2"].Docking =     Docking.Bottom;
                    break;

                case "Top":
                    GravityChart.Legends["Legend1"].Docking = Docking.Top;
                    GravityChart.Legends["Legend2"].Docking = Docking.Top;

                    break;

                case "Right":
                    GravityChart.Legends["Legend1"].Docking = Docking.Right;
                    GravityChart.Legends["Legend2"].Docking = Docking.Right;
                    break;

                case "Left":
                    GravityChart.Legends["Legend1"].Docking = Docking.Left;
                    GravityChart.Legends["Legend2"].Docking = Docking.Left;
                    break;

                default:
                    break;
            }
            Properties.Settings.Default.chartLegendLocation = location;
            */
        }

        private void SetLegend()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            //  GravityChart.Legends.Add(new Legend("legend1"));
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Enabled = false;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Default";
            this.GravityChart.Legends.Add(legend1);
            //          GravityChart.Legends["Legend1"].Docking = Docking.Bottom;

            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();

            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Enabled = false;
            legend2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend2.IsTextAutoFit = false;
            legend2.Name = "Default";
            this.GravityChart.Legends.Add(legend2);
            //          GravityChart.Legends["Legend2"].Docking = Docking.Bottom;
        }

        //*****************************************************************************
        //                       Chart Methods
        //*****************************************************************************
        public static class ChartColors
        {
            public static System.Drawing.Color digitalGravity = System.Drawing.Color.SteelBlue;
            public static System.Drawing.Color springTension = System.Drawing.Color.BlueViolet;
            public static System.Drawing.Color crossCoupling = System.Drawing.Color.DarkOrange;
            public static System.Drawing.Color rawBeam = System.Drawing.Color.DeepSkyBlue;
            public static System.Drawing.Color totalCorrection = System.Drawing.Color.ForestGreen;

            //   public static System.Drawing.Color rawGravity = System.Drawing.Color.RoyalBlue;
            public static System.Drawing.Color AL = System.Drawing.Color.LightSeaGreen;

            public static System.Drawing.Color AX = System.Drawing.Color.OrangeRed;
            public static System.Drawing.Color VE = System.Drawing.Color.PaleVioletRed;
            public static System.Drawing.Color AX2 = System.Drawing.Color.Red;
            public static System.Drawing.Color LACC = System.Drawing.Color.Wheat;
            public static System.Drawing.Color XACC = System.Drawing.Color.SteelBlue;
        }

        public static class ChartVisibility
        {
            public static Boolean digitalGravity = true;
            public static Boolean springTension = true;
            public static Boolean crossCoupling = true;
            public static Boolean rawBeam = true;
            public static Boolean totalCorrection = true;

            //   public static Boolean rawGravity = true;
            public static Boolean AL = true;

            public static Boolean AX = true;
            public static Boolean VE = true;
            public static Boolean AX2 = true;
            public static Boolean LACC = true;
            public static Boolean XACC = true;
        }

        public void SetChartBorderWidth(int width)
        {
            GravityChart.Series["Digital Gravity"].BorderWidth = width;
            GravityChart.Series["Spring Tension"].BorderWidth = width;
            GravityChart.Series["Raw Beam"].BorderWidth = width;
            GravityChart.Series["Cross Coupling"].BorderWidth = width;
            GravityChart.Series["Total Correction"].BorderWidth = width;
            //  crossCouplingChart.Series["Raw Gravity"].BorderWidth = width;
            GravityChart.Series["LACC"].BorderWidth = width;
            GravityChart.Series["XACC"].BorderWidth = width;
            GravityChart.Series["AX2"].BorderWidth = width;
            GravityChart.Series["VE"].BorderWidth = width;
            GravityChart.Series["AX"].BorderWidth = width;
            GravityChart.Series["AL"].BorderWidth = width;
        }

        public void SetChartColors()
        {
            GravityChart.Series["Digital Gravity"].Color = ChartColors.digitalGravity;
            GravityChart.Series["Spring Tension"].Color = ChartColors.springTension;
            GravityChart.Series["Cross Coupling"].Color = ChartColors.crossCoupling;
            GravityChart.Series["Raw Beam"].Color = ChartColors.rawBeam;
            GravityChart.Series["Total Correction"].Color = ChartColors.totalCorrection;
            //  crossCouplingChart.Series["Raw Gravity"].Color = ChartColors.rawGravity;
            GravityChart.Series["AL"].Color = ChartColors.AL;
            GravityChart.Series["AX"].Color = ChartColors.AX;
            GravityChart.Series["VE"].Color = ChartColors.VE;
            GravityChart.Series["AX2"].Color = ChartColors.AX2;
            GravityChart.Series["XACC"].Color = ChartColors.XACC;
            GravityChart.Series["LACC"].Color = ChartColors.LACC;
            GravityChart.Update();
        }

        private void SetChartMarkerSize(int size)
        {
            GravityChart.Series["Digital Gravity"].MarkerSize = size;
            GravityChart.Series["Spring Tension"].MarkerSize = size;
            GravityChart.Series["Cross Coupling"].MarkerSize = size;
            GravityChart.Series["Raw Beam"].MarkerSize = size;
            GravityChart.Series["Total Correction"].MarkerSize = size;

            //  crossCouplingChart.Series["Raw Gravity"].MarkerStyle = MarkerStyle.Circle;
            GravityChart.Series["AL"].MarkerSize = size;
            GravityChart.Series["AX"].MarkerSize = size;
            GravityChart.Series["VE"].MarkerSize = size;
            GravityChart.Series["AX2"].MarkerSize = size;
            GravityChart.Series["LACC"].MarkerSize = size;
            GravityChart.Series["XACC"].MarkerSize = size;
        }

        public void ChartMarkers(bool enable)
        {
            /*

            if (enable == true)
            {
                GravityChart.Series["Digital Gravity"].MarkerStyle = MarkerStyle.Circle;
                GravityChart.Series["Spring Tension"].MarkerStyle = MarkerStyle.Cross;
                GravityChart.Series["Cross Coupling"].MarkerStyle = MarkerStyle.Diamond;
                GravityChart.Series["Raw Beam"].MarkerStyle = MarkerStyle.Square;
                GravityChart.Series["Total Correction"].MarkerStyle = MarkerStyle.Triangle;

                //  crossCouplingChart.Series["Raw Gravity"].MarkerStyle = MarkerStyle.Circle;
                GravityChart.Series["AL"].MarkerStyle = MarkerStyle.Triangle;
                GravityChart.Series["AX"].MarkerStyle = MarkerStyle.Star5;
                GravityChart.Series["VE"].MarkerStyle = MarkerStyle.Square;
                GravityChart.Series["AX2"].MarkerStyle = MarkerStyle.Diamond;
                GravityChart.Series["LACC"].MarkerStyle = MarkerStyle.Cross;
                GravityChart.Series["XACC"].MarkerStyle = MarkerStyle.Star10;
            }
            else
            {
                GravityChart.Series["Digital Gravity"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Spring Tension"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Cross Coupling"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Raw Beam"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["Total Correction"].MarkerStyle = MarkerStyle.None;

                // crossCouplingChart.Series["Raw Gravity"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["AL"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["AX"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["VE"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["AX2"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["LACC"].MarkerStyle = MarkerStyle.None;
                GravityChart.Series["XACC"].MarkerStyle = MarkerStyle.None;
                GravityChart.Update();
                */
        }

        private void ChartCallback()
        {
        }

        private void SetChartType()
        {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType myChartType = new System.Windows.Forms.DataVisualization.Charting.SeriesChartType();
            myChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            GravityChart.Series["Digital Gravity"].ChartType = myChartType;
            GravityChart.Series["Spring Tension"].ChartType = myChartType;
            GravityChart.Series["Cross Coupling"].ChartType = myChartType;
            GravityChart.Series["Raw Beam"].ChartType = myChartType;
            GravityChart.Series["Total Correction"].ChartType = myChartType;
            GravityChart.Series["AL"].ChartType = myChartType;
            GravityChart.Series["AX"].ChartType = myChartType;
            GravityChart.Series["VE"].ChartType = myChartType;
            GravityChart.Series["AX2"].ChartType = myChartType;
            GravityChart.Series["XACC"].ChartType = myChartType;
            GravityChart.Series["LACC"].ChartType = myChartType;
        }

        private void ExtraChartStuff()
        {
            this.GravityChart.BackColor = System.Drawing.Color.WhiteSmoke;   //.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
            this.GravityChart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.GravityChart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.GravityChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.GravityChart.BorderlineWidth = 2;
            this.GravityChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.Inclination = 15;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.IsClustered = true;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.IsRightAngleAxes = false;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.Perspective = 10;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.Rotation = 10;
            GravityChart.ChartAreas["Gravity"].Area3DStyle.WallWidth = 0;
            GravityChart.ChartAreas["Gravity"].AxisX.IsLabelAutoFit = false;
            GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            GravityChart.ChartAreas["Gravity"].AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["Gravity"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["Gravity"].AxisY.IsLabelAutoFit = false;
            GravityChart.ChartAreas["Gravity"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            GravityChart.ChartAreas["Gravity"].AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["Gravity"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // GravityChart.ChartAreas["ChartArea1"].AxisY.Maximum = 5000D;
            // GravityChart.ChartAreas["ChartArea1"].AxisY.Minimum = 1000D;
            GravityChart.ChartAreas["Gravity"].BackColor = System.Drawing.Color.LightSlateGray;
            GravityChart.ChartAreas["Gravity"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            GravityChart.ChartAreas["Gravity"].BackSecondaryColor = System.Drawing.Color.White;
            GravityChart.ChartAreas["Gravity"].BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["Gravity"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //  GravityChart.ChartAreas["ChartArea1"].Name = "Default";
            GravityChart.ChartAreas["Gravity"].ShadowColor = System.Drawing.Color.Transparent;
            //   this.GravityChart.ChartAreas.Add(chartArea1);

            // Cross Coupling

            this.GravityChart.BackColor = System.Drawing.Color.WhiteSmoke;   //.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
            //this.GravityChart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.GravityChart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            this.GravityChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.GravityChart.BorderlineWidth = 2;
            this.GravityChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.Inclination = 15;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.IsClustered = true;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.IsRightAngleAxes = false;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.Perspective = 10;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.Rotation = 10;
            GravityChart.ChartAreas["CrossCoupling"].Area3DStyle.WallWidth = 0;
            GravityChart.ChartAreas["Gravity"].AxisX.IsLabelAutoFit = false;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            GravityChart.ChartAreas["CrossCoupling"].AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["CrossCoupling"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["CrossCoupling"].AxisY.IsLabelAutoFit = false;
            GravityChart.ChartAreas["CrossCoupling"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            GravityChart.ChartAreas["CrossCoupling"].AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["CrossCoupling"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //  crossCouplingChart.ChartAreas["ChartArea1"].AxisY.Maximum = 5000D;
            //  crossCouplingChart.ChartAreas["ChartArea1"].AxisY.Minimum = 1000D;
            GravityChart.ChartAreas["CrossCoupling"].BackColor = System.Drawing.Color.LightSlateGray;
            GravityChart.ChartAreas["CrossCoupling"].BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            GravityChart.ChartAreas["CrossCoupling"].BackSecondaryColor = System.Drawing.Color.White;
            GravityChart.ChartAreas["CrossCoupling"].BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            GravityChart.ChartAreas["CrossCoupling"].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //  GravityChart.ChartAreas["ChartArea1"].Name = "Default";
            GravityChart.ChartAreas["CrossCoupling"].ShadowColor = System.Drawing.Color.Transparent;
            //   this.GravityChart.ChartAreas.Add(chartArea1);
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

         //   Console.WriteLine(txCmd);
        //    Console.WriteLine(checkSum);
            return BitConverter.GetBytes(checkSum);

            //  comport.Write(txCmd, 0, txCmd.Length);
        }

        #endregion Serial Port

        #region Config File

        private void ReadConfigFile()
        {
            //  NEED TO ADD ERROR CHECKING FOR END OF FILE
            //  NEED TO ADD OPEN FILE DIALOG ONLY IF FILE IS (MISSING OR MANUAL BOX IS CHECKED - ENGINEERING ONLY)
            ConfigData ConfigData = new ConfigData();
            FileStream myStream;
            double[] CCFACT = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] AFILT = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] tempByte = { 0, 0, 0, 0 };
            byte[] byte2 = new byte[2];
            byte[] byte4 = new byte[4];
            byte[] byte10 = new byte[10];

            try
            {
                myStream = new FileStream("C:\\LCT stuff\\CONFIG20.ref", FileMode.Open);
                BinaryReader readBinary = new BinaryReader(myStream);

                readBinary.Read(byte2, 0, 2);
                readBinary.Read(byte4, 0, 4);

                ConfigData.beamScale = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) if (engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}.", ConfigData.beamScale);

                readBinary.Read(byte2, 0, 2);
                ConfigData.numAuxChan = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("NUMBER OF AUXILIARY ANALOG CHANNELS-- \t" + Convert.ToString(ConfigData.numAuxChan));

                readBinary.Read(byte10, 0, 10);
                ConfigData.meterNumber = System.Text.Encoding.Default.GetString(byte10);
                if (engineerDebug) Console.WriteLine("Meter number is---------------------- \t" + ConfigData.meterNumber);

                readBinary.Read(byte2, 0, 2);
                ConfigData.linePrinterSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("LINE PRINTER SWITCH------------------ \t" + Convert.ToString(ConfigData.linePrinterSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.fileNameSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("FILE NAME SWITCH--------------------- \t" + Convert.ToString(ConfigData.fileNameSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.hardDiskSwitch = BitConverter.ToInt16(byte2, 0); ;
                if (engineerDebug) Console.WriteLine("HARD DISK SWITCH--------------------- \t" + Convert.ToString(ConfigData.hardDiskSwitch));

                readBinary.Read(byte10, 0, 10);
                ConfigData.engPassword = System.Text.Encoding.Default.GetString(byte10);
                if (engineerDebug) Console.WriteLine("Magic value is ---------------------- \t" + ConfigData.engPassword);

                readBinary.Read(byte2, 0, 2);
                ConfigData.monitorDisplaySwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("MONITOR DISPLAY SWITCH--------------- \t" + Convert.ToString(ConfigData.monitorDisplaySwitch));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossPeriod = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);

                readBinary.Read(byte4, 0, 4);
                ConfigData.longPeriod = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossDampFactor = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);

                readBinary.Read(byte4, 0, 4);
                ConfigData.longDampFactor = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossGain = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longGain = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.longGain));

                readBinary.Read(byte2, 0, 2);
                ConfigData.serialPortSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("SERIAL PORT FORMAT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.digitalInputSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("DIGITAL INPUT SWITCH----------------- \t" + Convert.ToString(ConfigData.digitalInputSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.printerEmulationSwitch = BitConverter.ToInt16(byte2, 0);
                if (ConfigData.printerEmulationSwitch == 2)
                {
                    if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P");
                }
                if (ConfigData.printerEmulationSwitch == 3)
                {
                    if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P2");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "DPL24C");
                }

                readBinary.Read(byte2, 0, 2);
                ConfigData.serialPortOutputSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("SERIAL PORT OUTPUT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortOutputSwitch));

                readBinary.Read(byte2, 0, 2);
                ConfigData.alarmSwitch = BitConverter.ToInt16(byte2, 0);
                if (engineerDebug) Console.WriteLine("ALARM SWITCH------------------------- \t" + Convert.ToString(ConfigData.alarmSwitch));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossLead = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longLead = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));

                readBinary.Read(byte4, 0, 4);
                ConfigData.springTensionMax = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));

                readBinary.Read(byte4, 0, 4);
                ConfigData.modeSwitch = BitConverter.ToInt16(byte4, 0);
                if (ConfigData.modeSwitch == 0)
                {
                    if (engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Marine");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Hires");
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.iAuxGain = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("I aux gain value is --------------- \t" + Convert.ToString(ConfigData.iAuxGain));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossBias = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));

                readBinary.Read(byte4, 0, 4);
                ConfigData.longBias = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias));

                readBinary.Read(byte2, 0, 2);// extra read for alignment.  need to find out why
                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[6] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[7] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[8] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[9] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[10] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[11] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[12] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12]));

                readBinary.Read(byte2, 0, 2);
                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[13] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (4)---------- \t{0:e4}.", ConfigData.crossCouplingFactors[13]);

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[14] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[15] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.crossCouplingFactors[16] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[1] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[2] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[3] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("AFILT[3]---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[3]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[4] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[5] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[6] = BitConverter.ToSingle(byte4, 0);
                if (engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6]));

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[7] = BitConverter.ToSingle(byte4, 0);
                if (CCFACT[15] == 1)
                {
                    if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                }

                readBinary.Read(byte4, 0, 4);
                ConfigData.analogFilter[8] = BitConverter.ToSingle(byte4, 0);
                if (ConfigData.crossCouplingFactors[15] == 1)
                {
                    if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
                }
                else
                {
                    if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                }

                Console.ReadLine();
                readBinary.Close();

                LogConfigData(ConfigData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LogConfigData(ConfigData ConfigData)
        {
            // ConfigData ConfigData = new ConfigData();
            if (engineerDebug) Console.WriteLine("\n\nConfiguration data for meter number   \t" + ConfigData.meterNumber);
            if (engineerDebug) Console.WriteLine("\n\t User defined parameters\n");
            if (engineerDebug) Console.WriteLine("Number of auxillary analog channels-- \t" + Convert.ToString(ConfigData.numAuxChan));
            if (engineerDebug) Console.WriteLine("DIGITAL INPUT SWITCH----------------- \t" + Convert.ToString(ConfigData.digitalInputSwitch));
            if (engineerDebug) Console.WriteLine("MONITOR DISPLAY SWITCH--------------- \t" + Convert.ToString(ConfigData.monitorDisplaySwitch));
            if (engineerDebug) Console.WriteLine("LINE PRINTER SWITCH------------------ \t" + Convert.ToString(ConfigData.linePrinterSwitch));
            if (engineerDebug) Console.WriteLine("FILE NAME SWITCH--------------------- \t" + Convert.ToString(ConfigData.fileNameSwitch));
            if (engineerDebug) Console.WriteLine("HARD DISK SWITCH--------------------- \t" + Convert.ToString(ConfigData.hardDiskSwitch));
            if (engineerDebug) Console.WriteLine("SERIAL PORT FORMAT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortSwitch));
            if (engineerDebug) Console.WriteLine("SERIAL PORT OUTPUT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortOutputSwitch));
            if (engineerDebug) Console.WriteLine("ALARM SWITCH------------------------- \t" + Convert.ToString(ConfigData.alarmSwitch));
            if (ConfigData.printerEmulationSwitch == 2)
            {
                if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P");
            }
            if (ConfigData.printerEmulationSwitch == 3)
            {
                if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "ESC_P2");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("PRINTER EMULATION-------------------- \t" + "DPL24C");
            }
            if (ConfigData.modeSwitch == 0)
            {
                if (engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Marine");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("MODE SWITCH-------------------------- \t" + "Hires");
            }
            if (engineerDebug) Console.WriteLine("\n\tParameters defined by ZLS.\n");
            if (engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}.", ConfigData.beamScale);
            if (engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);
            if (engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);
            if (engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));
            if (engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));
            if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (4)---------- \t{0:e4}.", ConfigData.crossCouplingFactors[13]);
            if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5]));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15]));
            }

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
            }
            if (engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));
            if (engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);
            if (engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);
            if (engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t{0:n4}", ConfigData.longGain);
            if (engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));
            if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14]));
            if (engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6]));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16]));
            }
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)---- \t" + "N/A");
            }
            else
            {
                if (engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7]));
            }
            if (engineerDebug) Console.WriteLine("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias));
            if (engineerDebug) Console.WriteLine("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6]));
            if (engineerDebug) Console.WriteLine("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7]));
            if (engineerDebug) Console.WriteLine("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8]));
            if (engineerDebug) Console.WriteLine("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9]));
            if (engineerDebug) Console.WriteLine("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10]));
            if (engineerDebug) Console.WriteLine("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11]));
            if (engineerDebug) Console.WriteLine("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12]));
            if (engineerDebug) Console.WriteLine("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));
            if (engineerDebug) Console.WriteLine("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));
            if (engineerDebug) Console.WriteLine("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));
            if (engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));
        }

        #endregion Config File

        private void WSlew(double target)
        {
            double shift = 0;
            if (iStop == 2)
            {
                //write 5
                // read target  error 60
            }
            else if (iStop == 3)
            {
                shift = 500 - CalculateMarineData.data1[3];
                // goto 100
            }
            else if (iStop == 4)
            {
                shift = target - CalculateMarineData.data1[3];
            }
        }

        private static void DoSomeWork(int val)
        {
            // Pretend to do something.
            Thread.Sleep(val);
        }

        private void SpringTensionStep(double target, string slewType)
        {
            Boolean okToSlew = false;
            double shift = target;// target = 100
                                  //  shift = springTensionMax - CalculateMarineData.data1[3] - 500;
                                  //shift = 100
                                  // istop = 1
                                  // spring tension = 1414.5
                                  // stscale = .1041666
                                  // M = 960

            if (engineerDebug)
            {
                Console.WriteLine("FMPM = " + fmpm);
                Console.WriteLine("Slew " + slewType);
                Console.WriteLine("Current ST " + mdt.SpringTension);
                Console.WriteLine("Initial shift = " + shift);
            }

            switch (slewType)
            {
                case "absolute":
                    //  shift = target - CalculateMarineData.data1[3];
                    shift = target - mdt.SpringTension;

                    break;

                case "relative":

                    if (iStop > 1)
                    {
                        // goto 109  startup wizard is active
                    }
                    if (Math.Abs(shift) > fmpm)  // FMPM is MGAL/Min
                    {
                        springTensionValueLabel.Text = Convert.ToString(Math.Abs(shift) / fmpm);
                        // this is currently printed to screen
                    }

                    break;

                case "park":
                    // shift = springTensionMax - CalculateMarineData.data1[3] - 500;
                    shift = springTensionMax - mdt.SpringTension - 500;
                    // goto 100

                    break;

                default:
                    break;
            }
            //            if ((shift + CalculateMarineData.data1[3] > springTensionMax) || shift + CalculateMarineData.data1[3] < 100)

            if ((shift + mdt.SpringTension > springTensionMax) || (shift + mdt.SpringTension < 100) || (target > springTensionMax) || (Math.Abs(shift) > springTensionMax - 500))
            {
                okToSlew = false;
                MessageBox.Show("Error", "Spring tension out of range.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                TimerWithDataCollection("stop");
                okToSlew = true;
            }

            //---------------------------------------------------------------------------------------------
            // not sure where to put these yet
            if (Math.Abs(shift) > springTensionFPM)
            {
                if (engineerDebug)
                {
                    Console.WriteLine(Math.Abs(shift) / springTensionFPM);
                }
                // print to some label or text box etc
            }
            //---------------------------------------------------------------------------------------------------
            if (okToSlew)
            {
                int M = Convert.ToInt32(Math.Abs(shift / springTensionScale)); // scale == .1041666

                double sign = 1;

                if (shift > 0)
                {
                    sign = Math.Abs(M * springTensionScale);
                    if (engineerDebug)
                    {
                        Console.WriteLine("Shift > 0");
                        Console.WriteLine("sign = " + sign);
                    }
                }
                else
                {
                    sign = -1 * Math.Abs(M * springTensionScale);
                    if (engineerDebug)
                    {
                        Console.WriteLine("Shift < 0");
                        Console.WriteLine("sign = " + sign);
                    }
                }

                //  CalculateMarineData.data1[3] += sign;// FORTRAN SIGN(M * springTensionScale, shift
                mdt.SpringTension += sign;
                mdt.SpringTension = Math.Round(mdt.SpringTension, 1);
                if (engineerDebug)
                {
                    Console.WriteLine("New ST = " + mdt.SpringTension);
                }

                iStep[4] = -springTensionMaxStep;

                if (shift > 0)
                {
                    iStep[4] = springTensionMaxStep;
                }

                while (M >= springTensionMaxStep)
                {
                    springTensionStatusLabel.Text = ("Please wait 10 sec");
                    springTensionValueLabel.Text = Convert.ToString(Math.Round(.5 + M * springTensionScale));
                    sendCmd("Slew Spring Tension");
                    Task taskA = Task.Factory.StartNew(() => DoSomeWork(10000));
                    taskA.Wait();
                    STtextBox.Text = Convert.ToString(mdt.SpringTension);

                    // send_cmd 3
                    // sleep 10 sec  need to stop timer for this
                    // Thread.Sleep(2000);

                    M -= springTensionMaxStep;
                }

                // 120
                iStep[4] = -1 * Convert.ToInt32(M);
                if (shift > 0)
                {
                    iStep[4] *= -1;
                }
                springTensionStatusLabel.Text = ("Please wait 10 sec");
                springTensionValueLabel.Text = Convert.ToString(Math.Round(.5 + M * springTensionScale));
                sendCmd("Slew Spring Tension");
                // convert text box etc.
                // send_cmd 3
                Task taskB = Task.Factory.StartNew(() => DoSomeWork(10000));
                taskB.Wait();
                STtextBox.Text = Convert.ToString(mdt.SpringTension);
                // sleep 10 sec  need to stop timer for this
                // return

                springTensionStatusLabel.Text = ("All done");
                springTensionTargetNumericTextBox.Text = null;
                taskB = Task.Factory.StartNew(() => DoSomeWork(300));
                taskB.Wait();
                TimerWithDataCollection("stop");
                springTensionGroupBox.Visible = false;
            }
        }

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

        private void StartDataCollection()
        {
            // OpenPort();
            byte[] data = { 0x01, 0x08, 0x09 };

            _timer1.Interval = 1000; //  (5000 - DateTime.Now.Millisecond);
            //   _timer1.Enabled = true;
            //   Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
        }

        // Start data collection.  Send command 1 for meter to start data stream
        private void button2_Click(object sender, EventArgs e)
        {
            //   OpenPort();
            // Convert the user's string of hex digits (ex: B4 CA E2) to a byte array
            byte[] data = { 0x01, 0x08, 0x09 };                                        //HexStringToByteArray(txtSendData.Text);

            // Send the binary data out the port
            //    comport.Write(data, 0, data.Length);

            //         _timer1.Interval = 1000; //  (5000 - DateTime.Now.Millisecond);
            //         _timer1.Enabled = true;
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

        private void TimeWorker()
        {
            while (true)
            {
                this.Invoke(new UpdateTimeTextCallback(this.UpdateTimeText), new object[] { });
                Thread.Sleep(1000);
            }
        }

        private void UpdateRecordBox(Boolean i)
        {
            recordingTextBox.Visible = i;
            DateTime nowDateTime = DateTime.Now;
            fileStartTime = nowDateTime;
            fileStartTimeLabel.Text = "File start time:  " + Convert.ToString(nowDateTime);

            var runningTime = TimeSpan.FromTicks(DateTime.Now.Ticks - fileStartTime.Ticks);
            string timeDuration = "Duration: " + new DateTime(runningTime.Ticks).ToString("HH:mm:ss");
            recordingDurationLabel.Text = "Duration: " + new DateTime(runningTime.Ticks).ToString("HH:mm:ss");
        }

        private void UpdateNameLabel()
        {
            UserDataForm.dataFileTextBox.Text = fileName;
        }

        private void UpdateTimeText()
        {
            DateTime nowDateTime = DateTime.Now;
            timeNowLabel.Text = Convert.ToString(nowDateTime);
            UserDataForm.modeLabel.Text = dataAquisitionMode + " mode";
            if (fileRecording == true)
            {
                //    this.Invoke(new UpdateFileTimeCallback(this.UpdateDurationTime), new object[] {  });
                var myStartTime = DateTime.Now;
                if (firstTime == true)
                {
                    this.Invoke(new UpdateFileNameLabelCallback(this.UpdateNameLabel), new object[] { });
                    this.Invoke(new UpdateRecordBoxCallback(this.UpdateRecordBox), new object[] { true });
                    fileStartTime = myStartTime;// DateTime.Now;
                    //  frmTerminal.gravityFileName = sampleFileNamelabel.Text;
                    RecordDataToFile("Open");
                    firstTime = false;
                }
                else
                {
                    var runningTime = TimeSpan.FromTicks(DateTime.Now.Ticks - fileStartTime.Ticks);
                    string timeDuration = "Duration: " + new DateTime(runningTime.Ticks).ToString("HH:mm:ss");
                    recordingDurationLabel.Text = "Duration: " + new DateTime(runningTime.Ticks).ToString("HH:mm:ss");
                }
            }
            else
            {
                firstTime = true;
            }
        }


        private void SetInitialVisibility()
        {
            GravityChart.Visible = false;
            meterStatusGroupBox.Visible = false;
            surveyRecordGroupBox.Visible = false;
            gpsGroupBox.Visible = false;
            springTensionGroupBox.Visible = false;
            startupGroupBox.Visible = false;
            chartWindowGroupBox.Visible = false;
            //---------------------------------------------------
            torqueMotorCheckBox.Enabled = false;
            springTensionCheckBox.Enabled = false;
            gyroCheckBox.Enabled = false;
            alarmCheckBox.Enabled = false;
            springTensionRelativeRadioButton.Checked = true;


        }


        private void frmTerminal_Load(object sender, EventArgs e)
        {
            SetInitialVisibility();
            if (engineerDebug)
            {
                rtfTerminal.Visible = true;
            }
            else
            {
                rtfTerminal.Visible = false;
            }

            // Create an instance of NumericTextBox.
            NumericTextBox numericTextBox1 = new NumericTextBox();
            numericTextBox1.Parent = this;
            //Draw the bounds of the NumericTextBox.
            numericTextBox1.Bounds = new System.Drawing.Rectangle(5, 5, 150, 100);



            //  Load stored state
            InitStoredVariables();

            UpdateDataFileName();

            Console.WriteLine(fileName);
            STtextBox.Text = Convert.ToString(mdt.SpringTension);

            // load config file
            ReadConfigFile(configFilePath + "\\" + configFileName);
            UserDataForm.configurationFileTextBox.Text = configFilePath + "\\" + configFileName;

            readCalibrationFile(calFilePath + "\\" + calFileName);

            comboBox1.SelectedItem = "minutes";
            windowSizeNumericUpDown.Minimum = 1;
            windowSizeNumericUpDown.Maximum = 60;

            Thread TimeThread = new Thread(new ThreadStart(TimeWorker));
            TimeThread.IsBackground = true;
            TimeThread.Start();

            SetupChart();
            // Setup DataGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataStatusForm.Show();
        }

        public byte[] CreateTxArray(byte command, double data1, double data2, double data3, double data4, double data5, double data6)
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

        public byte[] CreateTxArray(byte command, double data1, double data2, double data3, double data4, double data5)
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

        public byte[] CreateTxArray4(byte command, double data1, double data2, double data3, double data4)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            byte[] byteArray1 = BitConverter.GetBytes(data1);
            byte[] byteArray2 = BitConverter.GetBytes(data2);
            byte[] byteArray3 = BitConverter.GetBytes(data3);
            byte[] byteArray4 = BitConverter.GetBytes(data4);

            int[] myByteArray = { cmdByte.Length, byteArray1.Length, byteArray2.Length, byteArray3.Length, byteArray4.Length, checkSum.Length };
            byte[] outputBytes = new byte[cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + checkSum.Length];

            Buffer.BlockCopy(cmdByte, 0, outputBytes, 0, cmdByte.Length);
            Buffer.BlockCopy(byteArray1, 0, outputBytes, cmdByte.Length, byteArray1.Length);
            Buffer.BlockCopy(byteArray2, 0, outputBytes, cmdByte.Length + byteArray1.Length, byteArray2.Length);
            Buffer.BlockCopy(byteArray3, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length, byteArray3.Length);
            Buffer.BlockCopy(byteArray4, 0, outputBytes, cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length, checkSum.Length);

            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            //   Console.WriteLine("Transmit array: " + outputBytes);
            //   Console.WriteLine("Done");

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

        public byte[] CreateTxSTArray(byte command, Single data1)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            // byte[] byteArrayTemp = BitConverter.GetBytes(data1);
            byte[] byteArray1 = BitConverter.GetBytes(data1);

            // int[] byteArray1 = { byteArrayTemp[0] };
            byte[] outputBytes = new byte[6]; //cmdByte.Length + byteArray1.Length + checkSum.Length];
            outputBytes[0] = 0x07;
            outputBytes[1] = byteArray1[0];
            outputBytes[2] = byteArray1[1];
            outputBytes[3] = byteArray1[2];
            outputBytes[4] = byteArray1[3];

            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            Console.WriteLine("Transmit array: " + outputBytes);
            Console.WriteLine("Done");

            return outputBytes;
        }

        public byte[] CreateTxStSlewArray(byte command, Single data1)
        {
            byte[] cmdByte = { command };
            byte[] checkSum = new byte[1];
            // byte[] byteArrayTemp = BitConverter.GetBytes(data1);
            byte[] byteArray1 = BitConverter.GetBytes(Convert.ToInt16(data1));

            // int[] byteArray1 = { byteArrayTemp[0] };
            byte[] outputBytes = new byte[4]; //cmdByte.Length + byteArray1.Length + checkSum.Length];
            outputBytes[0] = 0x03;
            outputBytes[1] = byteArray1[0];
            outputBytes[2] = byteArray1[1];

            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];
            // outputBytes[0] = nByte;
            //  Console.WriteLine("Transmit array: " + outputBytes);
            // Console.WriteLine("Done");

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
                    try
                    {
                        comport.Write(data, 0, 3);
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(Convert.ToString( e));
                        
                    }
                    
                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Update Date and Time": // 2
                    byte[] timeData = new byte[20]; // { 2, 0, 0, 0, 0, 0, 0, 0, 0};
                //    DateTime myDatetime = DateTime.Now;
                    int year = myDatetime.Year;
                    int day = myDatetime.DayOfYear;
                    int hour = myDatetime.Hour;
                    int min = myDatetime.Minute;
                    int sec = myDatetime.Second;

                    
                    byte[] byteArrayYr = BitConverter.GetBytes(year);
                    byte[] byteArrayDay = BitConverter.GetBytes(day);
                    byte[] byteArrayHr = BitConverter.GetBytes(hour);
                    byte[] byteArrayMin = BitConverter.GetBytes(min);
                    byte[] byteArraySec = BitConverter.GetBytes(sec);

                    // data = CreateTxArray(2, year, day, hour, min, sec);
                    // comport.Write(data, 0, 9);

                    timeData[0] = 2;
                    timeData[1] = byteArrayYr[0];
                    timeData[2] = byteArrayYr[1];
                    timeData[3] = byteArrayDay[0];
                    timeData[4] = byteArrayDay[1];
                    timeData[5] = byteArrayHr[0];
                    timeData[6] = byteArrayMin[0];
                    timeData[7] = byteArraySec[0];
                    timeData[8] = 5;



                    int checkSum = 0;
                    Array.Resize(ref timeData, 8);
                    byte[] txCmd = new byte[8];

                 //   Buffer.BlockCopy(timeData, 0, txCmd, 1, timeData.Length);

                    for (int i = 0; i < 8; i++)
                    {
                        checkSum = checkSum ^ timeData[i];
                    }
                  //  txCmd[txCmd.Length - 1] = BitConverter.GetBytes(checkSum)[0]; ;
                    timeData[8] = Convert.ToByte( checkSum);
                    //   Console.WriteLine(txCmd);
                        Console.WriteLine(checkSum);
                    //   return BitConverter.GetBytes(checkSum);





                    //    byte[] outputBytes = new byte[cmdByte.Length + byteArray1.Length + byteArray2.Length + byteArray3.Length + byteArray4.Length + byteArray5.Length + byteArray6.Length + checkSum.Length];
              //      byte[] newData = CalculateCheckSum(timeData, timeData.Length);
                //    timeData[8] = newData[0];


                    Console.WriteLine(Convert.ToByte(timeData));
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
                    data = CreateTxStSlewArray(3, iStep[4]);
                    comport.Write(data, 0, 4);

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

                    aLongPeriod = ConfigData.longPeriod;        //     Convert.ToSingle(0.000075); //ConfigData.longPeriod;
                    aLongDampFactor = ConfigData.longDampFactor;//  Convert.ToSingle(.82); //ConfigData.longDampFactor;
                    aLongGain = ConfigData.longGain;            // Convert.ToSingle(6.5); //ConfigData.longGain;
                    aLongLead = ConfigData.longLead;            // Convert.ToSingle(.1); // ConfigData.longLead;
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

                case "Update Spring Tension Value":   //  7
                    data = CreateTxSTArray(4, newSpringTension);
                    comport.Write(data, 0, data.Length);
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

            // turn on 200 Hz
            // RelaySwitches.relay200Hz = enable;// set bit 0  high to turn on 200 Hz

            RelaySwitches.slew4(disable);
            RelaySwitches.slew5(disable);
            RelaySwitches.stepperMotorEnable(enable);

            RelaySwitches.relaySW = 0x80;            // cmd 0
            sendCmd("Send Relay Switches");          // 0 ----
            sendCmd("Set Cross Axis Parameters");    // download platform parameters 4 -----
            sendCmd("Set Long Axis Parameters");     // download platform parametersv 5 -----
            sendCmd("Update Cross Coupling Values"); // download CC parameters 8     -----

            ControlSwitches.controlSw = 0x08;       // ControlSwitches.RelayControlSW = 0x08;

            sendCmd("Send Control Switches");       // 1 ----
            sendCmd("Send Control Switches");       // 1 ----

            RelaySwitches.relaySW = 0xB1;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----
            ControlSwitches.controlSw = 0x08;       //ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");       // 1 ----

            RelaySwitches.relaySW = 0x81;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----

            ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");       // 1 ----
            RelaySwitches.relaySW = 0x80;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----

            // wait for platform to level
            //  sendCmd("Update Gyro Bias Offset");        // 10
        }
        public void StoreDefaultVariables()
        {
            // Load configuration data from stored defaults

            Properties.Settings.Default.springTension = mdt.SpringTension;
            Properties.Settings.Default.beamScale = ConfigData.beamScale;
            Properties.Settings.Default.meterNumber = ConfigData.meterNumber;
            Properties.Settings.Default.crossPeriod = ConfigData.crossPeriod;
            Properties.Settings.Default.longPeriod = ConfigData.longPeriod;
            Properties.Settings.Default.crossDampFactor = ConfigData.crossDampFactor;
            Properties.Settings.Default.longDampFactor = ConfigData.longDampFactor;
            Properties.Settings.Default.crossGain = ConfigData.crossGain;
            Properties.Settings.Default.longGain = ConfigData.longGain;
            Properties.Settings.Default.crossLead = ConfigData.crossLead;
            Properties.Settings.Default.longLead = ConfigData.longLead;
            Properties.Settings.Default.springTensionMax = ConfigData.springTensionMax;

            Properties.Settings.Default.crossBias = ConfigData.crossBias;
            Properties.Settings.Default.longBias = ConfigData.longBias;
            Properties.Settings.Default.crossCompFactor_4 = ConfigData.crossCompFactor_4;
            Properties.Settings.Default.crossCompPhase_4 = ConfigData.crossCompPhase_4;
            Properties.Settings.Default.crossCompFactor_16 = ConfigData.crossCompFactor_16;
            Properties.Settings.Default.crossCompPhase_16 = ConfigData.crossCompPhase_16;
            Properties.Settings.Default.longCompFactor_4 = ConfigData.longCompFactor_4;
            Properties.Settings.Default.longCompPhase_4 = ConfigData.longCompPhase_4;
            Properties.Settings.Default.longCompFactor_16 = ConfigData.longCompFactor_16;
            Properties.Settings.Default.longCompPhase_16 = ConfigData.longCompPhase_16;

            Properties.Settings.Default.CML_Fact = ConfigData.CML_Fact;
            Properties.Settings.Default.AL_Fact = ConfigData.AL_Fact;
            Properties.Settings.Default.AX_Fact = ConfigData.AX_Fact;
            Properties.Settings.Default.VE_Fact = ConfigData.VE_Fact;
            Properties.Settings.Default.CMX_Fact = ConfigData.CMX_Fact;
            Properties.Settings.Default.XACC2_Fact = ConfigData.XACC2_Fact;
            Properties.Settings.Default.LACC2_Fact = ConfigData.LACC2_Fact;
            Properties.Settings.Default.XACC2_Fact = ConfigData.XACC_Phase;
            Properties.Settings.Default.LAXX_AL_Phase = ConfigData.LACC_AL_Phase;
            Properties.Settings.Default.LACC_CMX_Phase = ConfigData.LACC_CML_Phase;
            Properties.Settings.Default.LACC_CMX_Phase = ConfigData.LACC_CMX_Phase;
            Properties.Settings.Default.kFactor = ConfigData.kFactor;
            Properties.Settings.Default.gyroType = ConfigData.gyroType;






            //  public static double[] crossCouplingFactors = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.9e-2, 2.499999e-3, -1.681e-2, 0.0, 0.0, 0.0, 0.0, -8.9999998e-4, -3.7e-3, 1.0, 1.0 };

/*

            dataAquisitionMode = Properties.Settings.Default.dataAquisitionMode;
            configFilePath = Properties.Settings.Default.configFilePath;
            configFileName = Properties.Settings.Default.configFileName;
            calFilePath = Properties.Settings.Default.calFilePath;
            calFileName = Properties.Settings.Default.calFileName;
            filePath = Properties.Settings.Default.filePath;
            fileType = Properties.Settings.Default.fileType;
            fileName = Properties.Settings.Default.dataFileName;
            frmTerminal.fileDateFormat = Properties.Settings.Default.fileDateFormat;
*/

            Properties.Settings.Default.Save();


        }
        public void InitStoredVariables()
        {
            // Load configuration data from stored defaults

            mdt.SpringTension = Properties.Settings.Default.springTension;
            ConfigData.beamScale = Properties.Settings.Default.beamScale;
            ConfigData.meterNumber = Properties.Settings.Default.meterNumber;
            ConfigData.crossPeriod = Properties.Settings.Default.crossPeriod;
            ConfigData.longPeriod = Properties.Settings.Default.longPeriod;
            ConfigData.crossDampFactor = Properties.Settings.Default.crossDampFactor;
            ConfigData.longDampFactor = Properties.Settings.Default.longDampFactor;
            ConfigData.crossGain = Properties.Settings.Default.crossGain;
            ConfigData.longGain = Properties.Settings.Default.longGain;
            ConfigData.crossLead = Properties.Settings.Default.crossLead;
            ConfigData.longLead = Properties.Settings.Default.longLead;
            ConfigData.springTensionMax = Properties.Settings.Default.springTensionMax;

            ConfigData.crossBias = Properties.Settings.Default.crossBias;
            ConfigData.longBias = Properties.Settings.Default.longBias;
            ConfigData.crossCompFactor_4 = Properties.Settings.Default.crossCompFactor_4;
            ConfigData.crossCompPhase_4 = Properties.Settings.Default.crossCompPhase_4;
            ConfigData.crossCompFactor_16 = Properties.Settings.Default.crossCompFactor_16;
            ConfigData.crossCompPhase_16 = Properties.Settings.Default.crossCompPhase_16;
            ConfigData.longCompFactor_4 = Properties.Settings.Default.longCompFactor_4;
            ConfigData.longCompPhase_4 = Properties.Settings.Default.longCompPhase_4;
            ConfigData.longCompFactor_16 = Properties.Settings.Default.longCompFactor_16;
            ConfigData.longCompPhase_16 = Properties.Settings.Default.longCompPhase_16;

            ConfigData.CML_Fact = Properties.Settings.Default.CML_Fact;
            ConfigData.AL_Fact = Properties.Settings.Default.AL_Fact;
            ConfigData.AX_Fact = Properties.Settings.Default.AX_Fact;
            ConfigData.VE_Fact = Properties.Settings.Default.VE_Fact;
            ConfigData.CMX_Fact = Properties.Settings.Default.CMX_Fact;
            ConfigData.XACC2_Fact = Properties.Settings.Default.XACC2_Fact;
            ConfigData.LACC2_Fact = Properties.Settings.Default.LACC2_Fact;
            ConfigData.XACC_Phase = Properties.Settings.Default.XACC2_Fact;
            ConfigData.LACC_AL_Phase = Properties.Settings.Default.LAXX_AL_Phase;
            ConfigData.LACC_CML_Phase = Properties.Settings.Default.LACC_CMX_Phase;
            ConfigData.LACC_CMX_Phase = Properties.Settings.Default.LACC_CMX_Phase;
            ConfigData.kFactor = Properties.Settings.Default.kFactor;
            ConfigData.gyroType = Properties.Settings.Default.gyroType;






            //  public static double[] crossCouplingFactors = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.9e-2, 2.499999e-3, -1.681e-2, 0.0, 0.0, 0.0, 0.0, -8.9999998e-4, -3.7e-3, 1.0, 1.0 };

            // Load program defaults

            dataAquisitionMode = Properties.Settings.Default.dataAquisitionMode;
            configFilePath = Properties.Settings.Default.configFilePath;
            configFileName = Properties.Settings.Default.configFileName;
            calFilePath = Properties.Settings.Default.calFilePath;
            calFileName = Properties.Settings.Default.calFileName;
            filePath = Properties.Settings.Default.filePath;
            fileType = Properties.Settings.Default.fileType;
            fileName = Properties.Settings.Default.dataFileName;
            mdt.SpringTension = Properties.Settings.Default.springTension;
            frmTerminal.fileDateFormat = Properties.Settings.Default.fileDateFormat;

            ControlSwitches.DataCollection(enable);// ControlSwitches.dataSwitch = enable;// ICNTLSW = 8; // data on

            double aCrossPeriod = ConfigData.crossPeriod; ;
            double aCrossDampFactor = ConfigData.crossDampFactor;

            double aLongPeriod = ConfigData.longPeriod;
            double aLongDampFactor = ConfigData.longDampFactor;
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

        private void button11_Click(object sender, EventArgs e)// Autostart
        {
            autostart = true;
            // StartDataCollection();
            // AutoStart();

            // AutoStartForm.FogCheck();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ControlSwitches.DataCollection(enable); //ControlSwitches.dataSwitch = enable;// icntlsw = set bit 3  turn on data transmission
            // ControlSwitches.ControlSwitchCalculate();
            sendCmd("Send Control Switches");          // 1   -------
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

 



        private void CheckPassword()
        {
            PasswordForm PasswordForm = new PasswordForm();
            
            while (PasswordForm.passwordValid == null)
            {
                if (PasswordForm.passwordValid == "userPasswordValid")
                {
                    PasswordsClass.userPasswordValid = true;
                }
                else if (PasswordForm.passwordValid == "managerPasswordValid")
                {
                    PasswordsClass.mgrPasswordValid = true;
                }
                else if (PasswordForm.passwordValid == "zlsPasswordValid")
                {
                    PasswordsClass.engPasswordValid = true;
                }
            }
          //  return PasswordForm.passwordValid;
    

        }



        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm PasswordForm = new PasswordForm();
            if (PasswordsClass.engPasswordValid || PasswordsClass.mgrPasswordValid)
            {
                Parameters.Show();
            }
            else
            {
                MessageBox.Show("You must be logged in to access this.");
            }
            
        }

        private void dataStatusFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataStatusForm.Show();
        }

        private void serialPortFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortForm.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Show the user the about dialog
            (new frmAbout()).ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timePeriod = comboBox1.SelectedItem.ToString();
            windowSizeNumericUpDown.Minimum = 1;
            if (timePeriod == "hours")
            {
                windowSizeNumericUpDown.Maximum = 24;
            }
            else
            {
                windowSizeNumericUpDown.Maximum = 60;
            }

            Console.WriteLine(timePeriod);
            //  (string timePeriod, int timeValue)
        }

        private void setDateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTimeForm myDateForm = new DateTimeForm();
            myDateForm.Show();
        }

        private void fileFormatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileFormatForm myFileForm = new FileFormatForm();
            myFileForm.Show();
        }

        private void recordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordingForm RecordingForm = new RecordingForm();
            RecordingForm.Show();
        }

        #region File Class

        [DelimitedRecord(",")]
        public class ConfigFileData
        {
            public string EntryName;

            public string configValue;
        }

        public void ReadConfigFile(string configFile)
        {
            var engine = new FileHelperAsyncEngine<ConfigFileData>();

            UserDataForm.configurationFileTextBox.Text = configFile;

            //  NEED TO ADD ERROR CHECKING FOR END OF FILE
            //  NEED TO ADD OPEN FILE DIALOG ONLY IF FILE IS (MISSING OR MANUAL BOX IS CHECKED - ENGINEERING ONLY)
            ConfigData ConfigData = new ConfigData();

            if (configFile == null)// load defaults -
            {
                ConfigData.alarmSwitch = 0;
                ConfigData.beamScale = -1.9512490034103394;
                ConfigData.crossBias = 0.0;
                ConfigData.crossCouplingFactors[0] = 0.0;
                ConfigData.crossCouplingFactors[1] = 0.0;
                ConfigData.crossCouplingFactors[2] = 0.0;
                ConfigData.crossCouplingFactors[3] = 0.0;
                ConfigData.crossCouplingFactors[4] = 0.0;
                ConfigData.crossCouplingFactors[5] = 0.0;
                ConfigData.crossCouplingFactors[6] = 0.035199999809265137;
                ConfigData.crossCouplingFactors[7] = -0.0032800000626593828;
                ConfigData.crossCouplingFactors[8] = -0.058649998158216476;
                ConfigData.crossCouplingFactors[9] = 0.0094400001689791679;
                ConfigData.crossCouplingFactors[10] = 0.0;
                ConfigData.crossCouplingFactors[11] = 0.0;
                ConfigData.crossCouplingFactors[12] = 0.0;
                ConfigData.crossCouplingFactors[13] = -0.0011992008658125997;
                ConfigData.crossCouplingFactors[14] = -0.0013000000035390258;
                ConfigData.crossCouplingFactors[15] = 1.0;
                ConfigData.crossCouplingFactors[16] = 1.0;
                ConfigData.crossDampFactor = Convert.ToSingle(0.091499999165534973);
                ConfigData.crossGain = Convert.ToSingle(0.11999999731779099);
                ConfigData.crossLead = Convert.ToSingle(0.44999998807907104);
                ConfigData.crossPeriod = Convert.ToSingle(0.0000084000002971151844);
                ConfigData.digitalInputSwitch = 0;
                ConfigData.engPassword = "zls";
                ConfigData.fileNameSwitch = 1;
                ConfigData.hardDiskSwitch = 1;
                ConfigData.iAuxGain = 0.0;
                ConfigData.linePrinterSwitch = 0;
                ConfigData.longBias = 0.0;
                ConfigData.longDampFactor = Convert.ToSingle(0.091499999165534973);
                ConfigData.longGain = Convert.ToSingle(0.11999999731779099);
                ConfigData.longLead = Convert.ToSingle(0.44999998807907104);
                ConfigData.longPeriod = Convert.ToSingle(0.0000084000002971151844);
                ConfigData.meterNumber = "S91";
                ConfigData.modeSwitch = 1;
                ConfigData.monitorDisplaySwitch = 2;
                ConfigData.numAuxChan = 0;
                ConfigData.printerEmulationSwitch = 3;
                ConfigData.serialPortOutputSwitch = -1;
                ConfigData.serialPortSwitch = 1;
                ConfigData.springTensionMax = 20000.0;
                ConfigData.alarmSwitch = 0;
                ConfigData.analogFilter[0] = 0.0;
                ConfigData.analogFilter[1] = 0.22400000691413879;
                ConfigData.analogFilter[2] = 0.24250000715255737;
                ConfigData.analogFilter[3] = 0.20000000298023224;
                ConfigData.analogFilter[4] = 0.28999999165534973;
                ConfigData.analogFilter[5] = 0.60000002384185791;
                ConfigData.analogFilter[6] = 0.60000002384185791;
                ConfigData.analogFilter[7] = 1.0;
                ConfigData.analogFilter[8] = 1.0;
                UserDataForm.meterNumberTextBox.Text = ConfigData.meterNumber;
            }
            else
            {
                try
                {
                    using (engine.BeginReadFile(configFile))
                    {
                        // The engine is IEnumerable
                        foreach (ConfigFileData dataItem in engine)
                        {
                            // Console.WriteLine(dataItem.EntryName.ToString() + "\t" + dataItem.configValue.ToString());

                            switch (dataItem.EntryName)
                            {
                                case "Version":
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Version number: ---------------------- \t" + Convert.ToString(dataItem.configValue));

                                    break;

                                case "Meter Number":
                                    ConfigData.meterNumber = dataItem.configValue.Trim();
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Meter number is---------------------- \t" + ConfigData.meterNumber);
                                    break;

                                case "Beam Scale Factor":
                                    ConfigData.beamScale = Convert.ToDouble(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}.", ConfigData.beamScale);
                                    break;

                                case "Cross Axis Period":
                                    ConfigData.crossPeriod = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);
                                    break;

                                case "Long Axis Period":
                                    ConfigData.longPeriod = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);
                                    break;

                                case "Cross Axis Damping Factor":
                                    ConfigData.crossDampFactor = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);
                                    break;

                                case "Long Axis Damping Factor":
                                    ConfigData.longDampFactor = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);
                                    break;

                                case "Cross Axis Gain":
                                    ConfigData.crossGain = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));
                                    break;

                                case "Long Axis Gain":
                                    ConfigData.longGain = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.longGain));
                                    break;

                                case "Cross Axis Lead":
                                    ConfigData.crossLead = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));
                                    break;

                                case "Long Axis Lead":
                                    ConfigData.longLead = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));
                                    break;

                                case "Max Spring Tension":
                                    ConfigData.springTensionMax = Convert.ToInt32(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));
                                    break;

                                case "Cross Axis Bias":
                                    ConfigData.crossBias = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));
                                    break;

                                case "Long Axis Bias":
                                    ConfigData.longBias = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS BIAS----------------------- \t" + Convert.ToString(ConfigData.longBias));
                                    break;

                                case "crossCouplingFactors[0]": // Not used
                                    ConfigData.crossCouplingFactors[0] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[0]------------- \t{0:e4}", ConfigData.crossCouplingFactors[0]);
                                    break;

                                case "crossCouplingFactors[1]": // Not used
                                    ConfigData.crossCouplingFactors[1] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[1]------------- \t{0:e4}", ConfigData.crossCouplingFactors[1]);
                                    break;

                                case "crossCouplingFactors[2]": // Not used
                                    ConfigData.crossCouplingFactors[2] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[2]------------- \t{0:e4}", ConfigData.crossCouplingFactors[2]);
                                    break;

                                case "crossCouplingFactors[3]": // Not used
                                    ConfigData.crossCouplingFactors[3] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[3]------------- \t{0:e4}", ConfigData.crossCouplingFactors[3]);
                                    break;

                                case "crossCouplingFactors[4]": // Not used
                                    ConfigData.crossCouplingFactors[4] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[4]------------- \t{0:e4}", ConfigData.crossCouplingFactors[4]);
                                    break;

                                case "crossCouplingFactors[5]": // Not used
                                    ConfigData.crossCouplingFactors[5] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[5]------------- \t{0:e4}", ConfigData.crossCouplingFactors[5]);
                                    break;

                                case "VCC":// VCC
                                    ConfigData.crossCouplingFactors[6] = Convert.ToSingle(dataItem.configValue);
                                    mdt.VCC = ConfigData.crossCouplingFactors[6];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[6]------------- \t{0:e4}", ConfigData.crossCouplingFactors[6]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("VCC----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6]));
                                    break;

                                case "AL":// AL
                                    ConfigData.crossCouplingFactors[7] = Convert.ToSingle(dataItem.configValue);
                                    mdt.AL = ConfigData.crossCouplingFactors[7];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[7]-------------- \t{0:e4}", ConfigData.crossCouplingFactors[7]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AL------------------------------------- \t" + Convert.ToString(mdt.AL));

                                    break;

                                case "AX":// AX
                                    ConfigData.crossCouplingFactors[8] = Convert.ToSingle(dataItem.configValue);
                                    mdt.AX = ConfigData.crossCouplingFactors[8];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[8]-------------- \t{0:e4}", ConfigData.crossCouplingFactors[8]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AL------------------------------------- \t" + Convert.ToString(mdt.AX));

                                    break;

                                case "VE":// VE
                                    ConfigData.crossCouplingFactors[9] = Convert.ToSingle(dataItem.configValue);
                                    mdt.VE = ConfigData.crossCouplingFactors[9];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[9]-------------- \t{0:e4}", ConfigData.crossCouplingFactors[9]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("VE------------------------------------- \t" + Convert.ToString(mdt.VE));

                                    break;

                                case "AX2":// AX2
                                    ConfigData.crossCouplingFactors[10] = Convert.ToSingle(dataItem.configValue);
                                    mdt.AX2 = ConfigData.crossCouplingFactors[10];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[10]------------- \t{0:e4}", ConfigData.crossCouplingFactors[10]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AX2------------------------------------ \t" + Convert.ToString(mdt.AX2));

                                    break;

                                case "XACC2"://XACC2
                                    ConfigData.crossCouplingFactors[11] = Convert.ToSingle(dataItem.configValue);
                                    mdt.XACC2 = ConfigData.crossCouplingFactors[11];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[11]------------- \t{0:e4}", ConfigData.crossCouplingFactors[11]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("XACC2---------------------------------- \t" + Convert.ToString(mdt.XACC2));

                                    break;

                                case "LACC2":// LACC2
                                    ConfigData.crossCouplingFactors[12] = Convert.ToSingle(dataItem.configValue);
                                    mdt.LACC2 = ConfigData.crossCouplingFactors[12];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[12]------------- \t{0:e4}", ConfigData.crossCouplingFactors[12]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LACC2---------------------------------- \t" + Convert.ToString(mdt.LACC2));

                                    break;

                                case "Cross Axis Compensation (4)":// XCOMP (4)
                                    ConfigData.crossCouplingFactors[13] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompFactor_4 = ConfigData.crossCouplingFactors[13];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[13]------------- \t{0:e4}", ConfigData.crossCouplingFactors[13]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (4)------------ \t{0:e4}.", ConfigData.crossCompFactor_4);

                                    break;

                                case "Long Axis Compensation (4)":// LCOMP (4)
                                    ConfigData.crossCouplingFactors[14] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompFactor_4 = ConfigData.crossCouplingFactors[14];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[14]------------- \t{0:e4}", ConfigData.crossCouplingFactors[14]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (4)------------- \t" + Convert.ToString(ConfigData.longCompFactor_4));

                                    break;

                                case "Cross Axis Compensation (16)":// XCOMP (16)
                                    ConfigData.crossCouplingFactors[15] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompFactor_16 = ConfigData.crossCouplingFactors[15];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[15]------------- \t{0:e4}", ConfigData.crossCouplingFactors[15]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION (16)----------- \t" + Convert.ToString(ConfigData.crossCompFactor_16));

                                    break;

                                case "Long Axis Compensation (16)":// LCOMP (16)
                                    ConfigData.crossCouplingFactors[16] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompFactor_16 = ConfigData.crossCouplingFactors[16];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross Coupling Factors[16]------------- \t{0:e4}", ConfigData.crossCouplingFactors[16]);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG-AXIS COMPENSATION (16)------------ \t" + Convert.ToString(ConfigData.longCompFactor_16));
                                    break;

                                case "AX Phase":
                                    ConfigData.analogFilter[1] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AX PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));
                                    break;

                                case "AL Phase":
                                    ConfigData.analogFilter[2] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AL PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));
                                    break;

                                case "analogFilter[3]":
                                    ConfigData.analogFilter[3] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("AFILT[3]------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[3]));
                                    break;

                                case "VCC Phase":
                                    ConfigData.analogFilter[4] = Convert.ToSingle(dataItem.configValue);
                                    if (frmTerminal.engineerDebug) Console.WriteLine("VCC PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));
                                    break;

                                case "Cross Axis Compensation Phase (4)":
                                    ConfigData.analogFilter[5] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompPhase_4 = ConfigData.analogFilter[5];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Analog FIlter [5]----------------------- \t" + Convert.ToString(ConfigData.analogFilter[5]));
                                    if (frmTerminal.engineerDebug) Console.WriteLine("CROSS-AXIS COMPENSATION PHASE (4)------- \t" + Convert.ToString(ConfigData.crossCompPhase_4));

                                    break;

                                case "Long Axis Compensation Phase (4)":
                                    ConfigData.analogFilter[6] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompPhase_4 = ConfigData.analogFilter[6];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Analog FIlter [6]----------------------- \t" + Convert.ToString(ConfigData.analogFilter[6]));
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (4)-------- \t" + Convert.ToString(ConfigData.longCompPhase_4));

                                    break;

                                case "Cross Axis Compensation Phase (16)":
                                    ConfigData.analogFilter[7] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompPhase_16 = ConfigData.analogFilter[7];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Analog FIlter [7]----------------------- \t" + Convert.ToString(ConfigData.analogFilter[7]));
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Cross AXIS COMPENSATION PHASE (16)------ \t" + Convert.ToString(ConfigData.crossCompPhase_16));

                                    break;

                                case "Long Axis Compensation Phase (16)":
                                    ConfigData.analogFilter[8] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompPhase_16 = ConfigData.analogFilter[8];
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Analog FIlter [8]---------------------- \t" + Convert.ToString(ConfigData.analogFilter[8]));
                                    if (frmTerminal.engineerDebug) Console.WriteLine("LONG AXIS COMPENSATION PHASE (16)------ \t" + Convert.ToString(ConfigData.longCompPhase_16));

                                    break;

                                case "Mode":
                                    dataAquisitionMode = dataItem.configValue.Trim();
                                    if (frmTerminal.engineerDebug) Console.WriteLine("Data mode ------------------------------ \t" + Convert.ToString(dataAquisitionMode));

                                    break;

                                default:

                                    // set alert "xxx" entry is not found. check file and try again
                                    break;
                            }
                        }
                    }

                    /*

                                                           ConfigData.numAuxChan = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("NUMBER OF AUXILIARY ANALOG CHANNELS-- \t" + Convert.ToString(ConfigData.numAuxChan));

                                                           ConfigData.linePrinterSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("LINE PRINTER SWITCH------------------ \t" + Convert.ToString(ConfigData.linePrinterSwitch));

                                                           ConfigData.fileNameSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("FILE NAME SWITCH--------------------- \t" + Convert.ToString(ConfigData.fileNameSwitch));

                                                           readBinary.Read(byte2, 0, 2);
                                                           ConfigData.hardDiskSwitch = BitConverter.ToInt16(byte2, 0); ;
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("HARD DISK SWITCH--------------------- \t" + Convert.ToString(ConfigData.hardDiskSwitch));

                                                           ConfigData.engPassword = System.Text.Encoding.Default.GetString(byte10);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("Magic value is ---------------------- \t" + ConfigData.engPassword);

                                                           ConfigData.monitorDisplaySwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("MONITOR DISPLAY SWITCH--------------- \t" + Convert.ToString(ConfigData.monitorDisplaySwitch));

                                                           ConfigData.serialPortSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("SERIAL PORT FORMAT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortSwitch));

                                                           ConfigData.digitalInputSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("DIGITAL INPUT SWITCH----------------- \t" + Convert.ToString(ConfigData.digitalInputSwitch));

                                                           ConfigData.serialPortOutputSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("SERIAL PORT OUTPUT SWITCH------------ \t" + Convert.ToString(ConfigData.serialPortOutputSwitch));

                                                           ConfigData.alarmSwitch = BitConverter.ToInt16(byte2, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("ALARM SWITCH------------------------- \t" + Convert.ToString(ConfigData.alarmSwitch));

                                                           ConfigData.iAuxGain = BitConverter.ToSingle(byte4, 0);
                                                           if (frmTerminal.engineerDebug) Console.WriteLine("I aux gain value is --------------- \t" + Convert.ToString(ConfigData.iAuxGain));

   */

                    UserDataForm.meterNumberTextBox.Text = ConfigData.meterNumber;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /*

                //http://www.codeproject.com/Articles/686994/Create-Read-Advance-PDF-Report-using-iTextSharp-in#1
        */

        public void LogConfigDataToFile()
        {
            PdfPTable table = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase("Header spanning 2 columns"));

            cell.Colspan = 3;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            //  Setup margins
            //  Left Margin: 36pt => 0.5 inch
            //  Right Margin: 72pt => 1 inch
            //  Top Margin: 108pt => 1.5 inch
            //  Bottom Margini: 180pt => 2.5 inch

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 10);

            //Create a System.IO.FileStream object:
            FileStream fs = new FileStream("C:\\Ultrasys\\Meter Configuration.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            //Step 2: Create a iTextSharp.text.Document object: with page size and margins
            Document doc = new Document(PageSize.LETTER, 36, 72, 36, 36);

            // Setting Document properties e.g.
            // 1. Title
            // 2. Subject
            // 3. Keywords
            // 4. Creator
            // 5. Author
            // 6. Header
            doc.AddTitle("Configuration for meter #: " + frmTerminal.meterNumber);
            doc.AddSubject("Configuration data");
            //  doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            doc.AddCreator("ZLS");
            doc.AddAuthor("Jack Walker");
            doc.AddHeader("Nothing", "No Header");// Add creation date

            //Step 3: Create a iTextSharp.text.pdf.PdfWriter object. It helps to write the Document to the Specified FileStream:
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            try
            {
                //Step 4: Openning the Document:
                doc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //Step 5: Adding a Paragraph by creating a iTextSharp.text.Paragraph object:

            doc.Add(new Paragraph("\n\t User defined parameters\n", times));
            doc.Add(new Paragraph("Number of auxillary analog channels \t\t" + Convert.ToString(ConfigData.numAuxChan)));
            doc.Add(new Paragraph("DIGITAL INPUT SWITCH \t\t" + Convert.ToString(ConfigData.digitalInputSwitch)));
            doc.Add(new Paragraph("MONITOR DISPLAY SWITCH \t\t" + Convert.ToString(ConfigData.monitorDisplaySwitch)));
            doc.Add(new Paragraph("LINE PRINTER SWITCH \t\t" + Convert.ToString(ConfigData.linePrinterSwitch)));
            doc.Add(new Paragraph("FILE NAME SWITCH \t\t" + Convert.ToString(ConfigData.fileNameSwitch)));
            doc.Add(new Paragraph("HARD DISK SWITCH \t\t" + Convert.ToString(ConfigData.hardDiskSwitch)));
            doc.Add(new Paragraph("SERIAL PORT FORMAT SWITCH \t\t" + Convert.ToString(ConfigData.serialPortSwitch)));
            doc.Add(new Paragraph("SERIAL PORT OUTPUT SWITCH \t\t" + Convert.ToString(ConfigData.serialPortOutputSwitch)));
            doc.Add(new Paragraph("ALARM SWITCH \t\t" + Convert.ToString(ConfigData.alarmSwitch)));
            if (ConfigData.printerEmulationSwitch == 2)
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "ESC_P"));
            }
            if (ConfigData.printerEmulationSwitch == 3)
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "ESC_P2"));
            }
            else
            {
                doc.Add(new Paragraph("PRINTER EMULATION-------------------- \t" + "DPL24C"));
            }
            if (ConfigData.modeSwitch == 0)
            {
                doc.Add(new Paragraph("MODE SWITCH-------------------------- \t" + "Marine"));
            }
            else
            {
                doc.Add(new Paragraph("MODE SWITCH-------------------------- \t" + "Hires"));
            }
            doc.Add(new Paragraph("\n\tParameters defined by ZLS.\n"));
            doc.Add(new Paragraph("BEAM SCALE FACTOR-------------------- \t" + Math.Round(ConfigData.beamScale, 6)));
            doc.Add(new Paragraph("CROSS-AXIS PERIOD-------------------- \t" + Math.Round(ConfigData.crossPeriod, 4)));

            doc.Add(new Paragraph("CROSS-AXIS DAMPING------------------- \t" + Math.Round(ConfigData.crossDampFactor, 4)));
            doc.Add(new Paragraph("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain)));
            doc.Add(new Paragraph("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead)));
            doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (4)---------- \t" + Math.Round(ConfigData.crossCouplingFactors[13], 4)));
            doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (4)---- \t" + Convert.ToString(ConfigData.analogFilter[5])));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (16)--------- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION (16)--------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[15])));
            }

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("CROSS-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7])));
            }
            doc.Add(new Paragraph("CROSS-AXIS BIAS---------------------- \t" + Math.Round(ConfigData.crossBias, 4)));
            doc.Add(new Paragraph("LONG-AXIS PERIOD-------------------- \t" + Math.Round(ConfigData.longPeriod)));
            doc.Add(new Paragraph("LONG-AXIS DAMPING------------------- \t" + Math.Round(ConfigData.longDampFactor)));
            doc.Add(new Paragraph("LONG-AXIS GAIN---------------------- \t" + Math.Round(ConfigData.longGain)));
            doc.Add(new Paragraph("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead)));
            doc.Add(new Paragraph("LONG-AXIS COMPENSATION (4)----------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[14])));
            doc.Add(new Paragraph("LONG AXIS COMPENSATION PHASE (4)----- \t" + Convert.ToString(ConfigData.analogFilter[6])));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION (16)---------- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION (16)---------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[16])));
            }
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION PHASE (16)---- \t" + "N/A"));
            }
            else
            {
                doc.Add(new Paragraph("LONG-AXIS COMPENSATION PHASE (16)--- \t" + Convert.ToString(ConfigData.analogFilter[7])));
            }
            doc.Add(new Paragraph("LONG-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.longBias)));
            doc.Add(new Paragraph("VCC---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[6])));
            doc.Add(new Paragraph("AL----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[7])));
            doc.Add(new Paragraph("AX----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[8])));
            doc.Add(new Paragraph("VE----------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[9])));
            doc.Add(new Paragraph("AX2---------------------------------- \t" + Convert.ToString(ConfigData.crossCouplingFactors[10])));
            doc.Add(new Paragraph("XACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[11])));
            doc.Add(new Paragraph("LACC**2------------------------------ \t" + Convert.ToString(ConfigData.crossCouplingFactors[12])));
            doc.Add(new Paragraph("AX PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1])));
            doc.Add(new Paragraph("AL PHASE----------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2])));
            doc.Add(new Paragraph("VCC PHASE---------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4])));
            doc.Add(new Paragraph("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax)));

            //Step 6: Closing the Document:
            doc.Close();
        }

        public void LogConfigDataToFileTable()
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 10);

            iTextSharp.text.Font fontTinyItalic = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font font16Normal = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 500f;

            //fix the absolute width of the table
            table.LockedWidth = true;

            //relative col widths in proportions - 1/3 and 2/3
            float[] widths = new float[] { 4f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;

            PdfPCell cell = new PdfPCell(new Phrase("User defined parameters"));
            PdfPCell cell2 = new PdfPCell(new Phrase("ZLS defined parameters"));

            //  PdfPCell theCell = new PdfPCell(new Paragraph("Configuration Data for " + meterNumber, font16Normal));
            //   theCell.Colspan = 2;
            //  theCell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            //  table.AddCell(theCell);

            //leave a gap before and after the table

            table.SpacingBefore = 50f;
            table.SpacingAfter = 50f;

            //Create a System.IO.FileStream object:
            FileStream fs = new FileStream("C:\\ZLS\\Meter Configuration.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            //Step 2: Create a iTextSharp.text.Document object: with page size and margins
            Document doc = new Document(PageSize.LETTER, 36, 36, 36, 36);

            doc.AddTitle("Configuration for meter #: " + frmTerminal.meterNumber);
            doc.AddSubject("Configuration data");
            //  doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            doc.AddCreator("ZLS");
            doc.AddAuthor("Jack Walker");
            doc.AddHeader("Nothing", "No Header");// Add creation date

            //Step 3: Create a iTextSharp.text.pdf.PdfWriter object. It helps to write the Document to the Specified FileStream:
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            try
            {
                //Step 4: Openning the Document:
                doc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            doc.Add(new Paragraph("", fontTinyItalic));
            doc.Add(new Paragraph("Configuration Data for " + frmTerminal.meterNumber, font16Normal));
            doc.Add(new Paragraph("", fontTinyItalic));

            cell.Colspan = 2;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            //Step 5: Adding a Paragraph by creating a iTextSharp.text.Paragraph object:

            //  table.AddCell(new PdfPCell(new Paragraph(new PdfPCell(new Paragraph(Label1.Text, fontTinyItalic)));

            //  table.AddCell(new PdfPCell(new Paragraph("User defined parameters", fontTinyItalic));
            table.AddCell(new PdfPCell(new Paragraph("Number of auxillary analog channels", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.numAuxChan), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("DIGITAL INPUT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.digitalInputSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("MONITOR DISPLAY SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.monitorDisplaySwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("LINE PRINTER SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.linePrinterSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("FILE NAME SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.fileNameSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("HARD DISK SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.hardDiskSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("SERIAL PORT FORMAT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.serialPortSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("SERIAL PORT OUTPUT SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.serialPortOutputSwitch), fontTinyItalic)));

            table.AddCell(new PdfPCell(new Paragraph("ALARM SWITCH", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(ConfigData.alarmSwitch), fontTinyItalic)));

            if (ConfigData.printerEmulationSwitch == 2)
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("ESC_P", fontTinyItalic)));
            }
            if (ConfigData.printerEmulationSwitch == 3)
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("ESC_P2", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("PRINTER EMULATION", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("DPL24C", fontTinyItalic)));
            }
            if (ConfigData.modeSwitch == 0)
            {
                table.AddCell(new PdfPCell(new Paragraph("MODE SWITCH", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("Marine", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("MODE SWITCH", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("Hires", fontTinyItalic)));
            }

            // Items specified by ZLS
            cell2.Colspan = 2;
            cell2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell2);

            table.AddCell(new PdfPCell(new Paragraph("BEAM SCALE FACTOR", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.beamScale, 6)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS PERIOD", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossPeriod, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS DAMPING", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossDampFactor, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS GAIN", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossGain, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS LEAD", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossLead, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[13], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[5], 4)), fontTinyItalic)));
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[15], 4)), fontTinyItalic)));
            }

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE 16 inch", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS COMPENSATION PHASE 16 inch", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[7], 4)), fontTinyItalic)));
            }
            table.AddCell(new PdfPCell(new Paragraph("CROSS-AXIS BIAS ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossBias, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS PERIOD ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longPeriod, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS DAMPING ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longDampFactor, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS GAIN ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longGain, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS LEAD ", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longLead, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (4 inch )", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[14], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LONG AXIS COMPENSATION PHASE (4 inch)", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[6])), fontTinyItalic)));

            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION (16 inch) ", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[16], 4)), fontTinyItalic)));
            }
            if (ConfigData.crossCouplingFactors[15] == 1)
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION PHASE (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph("N/A", fontTinyItalic)));
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS COMPENSATION PHASE (16 inch)", fontTinyItalic)));
                table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[7], 4)), fontTinyItalic)));
            }
            table.AddCell(new PdfPCell(new Paragraph("LONG-AXIS BIAS", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.longBias, 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VCC", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[6], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AL", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[7], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[8], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[9], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[10], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("XACC**2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[11], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("LACC**2", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.crossCouplingFactors[12], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AX PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[1], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("AL PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[2], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("VCC PHASE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.analogFilter[4], 4)), fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph("MAXIMUM SPRING TENSION VALUE", fontTinyItalic)));
            table.AddCell(new PdfPCell(new Paragraph(Convert.ToString(Math.Round(ConfigData.springTensionMax, 4)), fontTinyItalic)));
            doc.Add(table);

            //Step 6: Closing the Document:
            doc.Close();
            //         Process.Start("C:\\ZLS\\Meter Configuration.pdf");
        }

        public void RecordDataToFile(string fileOperation)
        {
            string delimitor = ",";

            switch (frmTerminal.fileType)
            {
                case "csv":
                    delimitor = ",";
                    break;

                case "tsv":
                    delimitor = "\t";
                    break;

                case "txt":
                    delimitor = " ";
                    break;

                default:
                    break;
            }

            if (fileOperation == "Open")
            {
                try
                {
                    using (StreamWriter writer = File.CreateText(frmTerminal.fileName))
                    {
                        string header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                             + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                             + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                             + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Total Correction" + delimitor
                             + "Latitude" + delimitor + "Longitude" + delimitor + "Altitude" + delimitor + "GPS Status";
                        writer.WriteLine(header);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void RecordDataToFile(string fileOperation, myData d)
        {
            // fileOperation  0 - open,  1 - append, 2 - close
            string delimitor = ",";

            switch (frmTerminal.fileType)
            {
                case "csv":
                    delimitor = ",";
                    break;

                case "tsv":
                    delimitor = "\t";
                    break;

                case "txt":
                    delimitor = " ";
                    break;

                default:
                    break;
            }

            if (frmTerminal.gravityFileName == null)
            {
                DateTime now = DateTime.Now;
                // String.Format("{0:dds}", now);
                //  fileName = "C:\\Ultrasys\\Data\\" + "GravityData" + now.ToString("yyyyMMddHHmmsstt") + ".csv";
                //  fileName = frmTerminal.filePath;
            }
            else
            {
                //  fileName = "C:\\Ultrasys\\Data\\" + frmTerminal.gravityFileName;
            }

            if (fileOperation == "Open")
            {
                try
                {
                    using (StreamWriter writer = File.CreateText(frmTerminal.fileName))
                    {
                        string header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                            + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                            + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                            + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Total Correction"
                            + "Latitude" + delimitor + "Longitude" + delimitor + "Altitude" + delimitor + "GPS Status";

                        writer.WriteLine(header);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (fileOperation == "Append")
            {
                string myString = Convert.ToString(d.Date) + delimitor + Convert.ToString(d.Gravity) + delimitor + Convert.ToString(d.SpringTension)
                     + delimitor + Convert.ToString(d.CrossCoupling) + delimitor + Convert.ToString(d.RawBeam) + delimitor + Convert.ToString(d.VCC)
                     + delimitor + Convert.ToString(d.AL) + delimitor + Convert.ToString(d.AX) + delimitor + Convert.ToString(d.VE)
                     + delimitor + Convert.ToString(d.AX2) + delimitor + Convert.ToString(d.XACC2) + delimitor + Convert.ToString(d.LACC2)
                     + delimitor + Convert.ToString(d.XACC) + delimitor + Convert.ToString(d.LACC) + delimitor + Convert.ToString(d.TotalCorrection)
                     + delimitor + Convert.ToString(d.latitude) + delimitor + Convert.ToString(d.longitude) + delimitor + Convert.ToString(d.altitude)
                     + delimitor + Convert.ToString(d.gpsStatus);

                try
                {
                    using (StreamWriter writer = File.AppendText(frmTerminal.fileName))
                    {
                        // writer.WriteLine("{0},{1},{2},{3},{4}, {5},{6}", MeterData.year, MeterData.day, MeterData.Hour, MeterData.Min, MeterData.Sec, MeterData.data4[2]);
                        //  writer.WriteLine(Convert.ToString(MeterData.dataTime), ",",Convert.ToString(MeterData.data4[2]),",");
                        writer.WriteLine(myString);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion File Class

        public class myData//  set this up so all chart data in in one class.  Maybe not need use MeterData
        {
            public DateTime Date;
            public string LineID;
            public int Year;
            public int Days;
            public int Hour;
            public int Minute;
            public int Second;
            public double Gravity;
            public double SpringTension;
            public double CrossCoupling;
            public double RawBeam;
            public double TotalCorrection;
            public double RawGravity;
            public double VCC;
            public double AL;
            public double AX;
            public double VE;
            public double AX2;
            public double XACC2;
            public double LACC2;
            public double XACC;
            public double LACC;
            public double Period;
            public double ParallelData;
            public double AUX1;
            public double AUX2;
            public double AUX3;
            public double AUX4;
            public double longitude;
            public double latitude;
            public double altitude;
            public double gpsStatus;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Boolean okToRun = false;

            if (surveyNameSet == false)
            {
                MessageBox.Show("Warning! \nNo survey information was entered.");
                surveyNameSet = true;
            }
            else
            {
                okToRun = true;
            }

            if (okToRun)
            {
                fileRecording = true;
                recordingTextBox.Text = "Recording file";
                recordingTextBox.BackColor = System.Drawing.Color.LightGreen;
                surveyTextBox.Enabled = false;

                //                Thread WorkerThread = new Thread(new ParameterizedThreadStart(ArrayDataWorker));
                //                WorkerThread.IsBackground = true;
                //                WorkerThread.Start(new Action<myData>(this.AddDataPoint));
            }
            okToRun = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            // Close file
            fileRecording = false;
            surveyTextBox.Enabled = true;
            recordingTextBox.Text = "Recording stopped";
            recordingTextBox.BackColor = System.Drawing.Color.Red;
        }

        private void ShutdownDataWorker(object obj)
        {
            var _delegate = (Action<shutdownData>)obj;
            // this.Invoke(new UpdateRecordBoxCallback(this.UpdateRecordBox), new object[] { true });
            //   this.Invoke(new Action<shutdownData>(this.UpdateShutdownText), new object[] { d });

            _delegate(new shutdownData
            {
                shutDownText = "Preparing to shutdown..."
            });
            Thread.Sleep(1000);
            fileRecording = false;
            _delegate(new shutdownData
            {
                shutDownText = "Closing all open files."
            });
            Thread.Sleep(2000);

            springTensionEnabled = false;
            _delegate(new shutdownData
            {
                shutDownText = "Disabling spring tension"
            });
            Thread.Sleep(3000);
            torqueMotorsEnabled = false;
            _delegate(new shutdownData
            {
                shutDownText = "Turning off torque motors"
            });
            Thread.Sleep(3000);

            gyrosEnabled = false;

            _delegate(new shutdownData
            {
                shutDownText = "Turning off gyros"
            });
            Thread.Sleep(3000);

            _delegate(new shutdownData
            {
                shutDownText = "Shutdown complete.  Program will now terminate."
            });
            Thread.Sleep(3000);

            ExitProgram();
        }

        private void ExitProgram()
        {
            SaveSettings();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void surveyTextBox_TextChanged(object sender, EventArgs e)
        {
            surveyName = surveyTextBox.Text;
            surveyNameSet = true;
            UpdateDataFileName();
            UpdateNameLabel();
        }

        private void UpdateDataFileName()
        {
            DateTime now = DateTime.Now;
            if (fileDateFormat == 1)
            {
                fileName = filePath + "\\" + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-MMM-dd-HH-mm-ss") + "." + fileType;
            }
            else if (fileDateFormat == 2)
            {
                fileName = filePath + "\\" + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-mm-dd-HH-mm-ss") + "." + fileType;
            }
            else if (fileDateFormat == 3)
            {
                fileName = filePath + "\\" + ConfigData.meterNumber + "-" + surveyName + "-" + now.ToString("yyyy-dd-HH-mm-ss") + "." + fileType;
            }
            else if (fileDateFormat == 4)
            {
                fileName = filePath + "\\" + customFileName + "." + fileType;
            }
            //           Properties.Settings.Default.fileDateFormat = fileDateFormat;
        }

        private void exitProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                StoreDefaultVariables();
                

                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                StoreDefaultVariables();
                System.Environment.Exit(1);
            }
        }

        private void setDataFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = " (*.csv, *.txt, *.tsv)|*.csv, *.txt, *.tsv";
            UpdateDataFileName();
            dialog.DefaultExt = fileType;
            dialog.AddExtension = true;
            dialog.FileName = ConfigData.meterNumber + surveyName + "-" + DateTime.Now.ToString("yyyy-MMM-dd-HH-mm-ss");

            // add file name based on selected data             call dialog when start button is pressed
            // maybe add a checkbox for do not show this again
            dialog.InitialDirectory = Properties.Settings.Default.filePath;//  need to get this from stored data
            dialog.ShowDialog();
            FileInfo fileInfo = new FileInfo(dialog.FileName);
            Properties.Settings.Default.fileType = fileType;// set default file type
            Properties.Settings.Default.filePath = fileInfo.DirectoryName.ToString(); // set path for next time
            Properties.Settings.Default.Save();
        }

        private void loadConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();

            OpenFileDialog.ShowDialog();

            ReadConfigFile(OpenFileDialog.FileName);
        }

        private void switchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Switches SwitchesForm = new Switches();
            SwitchesForm.Show();
        }

        private void initMeter()
        {
            // 200 Hz etc
            byte[] data = { 0x01, 0x08, 0x09 };  //HexStringToByteArray(txtSendData.Text);

            RelaySwitches.stepperMotorEnable(enable);
            sendCmd("Send Relay Switches");

            sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
            Task taskA = Task.Factory.StartNew(() => DoSomeWork(500));
            taskA.Wait();

            sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
            taskA = Task.Factory.StartNew(() => DoSomeWork(500));
            taskA.Wait();
            sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----
            taskA = Task.Factory.StartNew(() => DoSomeWork(500));
            taskA.Wait();
            var iGyroSw = 1;
            if (iGyroSw == 2)
            {
                sendCmd("Update Gyro Bias Offset");
            }

            ControlSwitches.Alarm(enable);
            //   ControlSwitches.controlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
            ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");

            RelaySwitches.relay200Hz(enable);
            RelaySwitches.slew4(enable);
            RelaySwitches.slew5(enable);
            RelaySwitches.stepperMotorEnable(enable);

            ////////////////////////////////////////////////////////////////////////////////////
            // relay and control switches
            //  RelaySwitches.relaySW = 0xB1;// cmd 0
            sendCmd("Send Relay Switches");

            ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");
            ////////////////////////////////////////////////////////////////////////////////////

            RelaySwitches.slew4(disable);
            RelaySwitches.slew5(disable);

            //  RelaySwitches.relaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // At tis point Gyros are up and running - ready for torque motor
            torqueMotorCheckBox.Enabled = true;
        }

        private void gyroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gyroCheckBox.Checked)
            {
                initMeter();
            }
            else
            {
                // stop gyros
                //    RelaySwitches.relay200Hz(enable);    //  This option should be grayed out until spring tenstion and torwue motors are off
                //   sendCmd("Send Relay Switches");
            }
        }

        private void torqueMotorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (torqueMotorCheckBox.Checked)
            {
                RelaySwitches.relaySW = 0x83;
                sendCmd("Send Relay Switches");
                ControlSwitches.TorqueMotor(enable);
                sendCmd("Send Control Switches");
                gyroCheckBox.Enabled = false;
                springTensionCheckBox.Enabled = true;
            }
            else
            {
                RelaySwitches.relaySW = 0x81;// cmd 0
                sendCmd("Send Relay Switches");           // 0 ----
                gyroCheckBox.Enabled = true;
                springTensionCheckBox.Enabled = false;
            }
        }

        private void alarmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (alarmCheckBox.Checked)
            {
                ControlSwitches.Alarm(enable);
                sendCmd("Send Control Switches");
            }
            else
            {
                ControlSwitches.Alarm(disable);
                sendCmd("Send Control Switches");
            }
        }

        private void startupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startupGroupBox.Visible = true;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            startupGroupBox.Visible = false;
            TimerWithDataCollection("start");
            GravityChart.Visible = true;
            surveyRecordGroupBox.Visible = true;
            gpsGroupBox.Visible = true;
        }

        private void dataPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDataForm.Show();
        }

        private void surveyTextBox_TextChanged_1(object sender, EventArgs e)
        {
            UserDataForm.surveyTextBox.Text = surveyTextBox.Text;
        }

        private void manualOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startupGroupBox.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                StoreDefaultVariables();
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                StoreDefaultVariables();
                System.Environment.Exit(1);
            }
        }

        private void dataPageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserDataForm.Show();
        }

        private void brightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChartAreaColors(3);
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChartAreaColors(1);
        }

        private void greyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChartAreaColors(2);
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void pastelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void brightPastelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void earthTonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void semiTransparantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void berryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void chocolateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void fireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(brightToolStripMenuItem.AccessibilityObject.Name);
        }

        private void seaGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTraceColor(seaGreenToolStripMenuItem.AccessibilityObject.Name);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem2.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem3.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem4.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem5.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem6.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem7.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem8.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            SetChartBorderWidth((Convert.ToInt16(toolStripMenuItem9.AccessibilityObject.Name)));
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
        }

        [DelimitedRecord(",")]
        public class calFileData
        {
            [FieldConverter(ConverterKind.Double)]
            public double value1;

            [FieldConverter(ConverterKind.Double)]
            public double value2;
        }

        public void readCalibrationFile(string calFile)
        {
            var engine = new FileHelperAsyncEngine<calFileData>();
            UserDataForm.calFileTextBox.Text = calFile;

            if (calFile == null || calFile == "")
            {
                // do nothing.  use generic
            }
            else
            {
                try
                {
                    using (engine.BeginReadFile(calFile))
                    {
                        var i = 0;
                        foreach (calFileData dataItem in engine)
                        {
                            CalculateMarineData.table1[i] = dataItem.value1;
                            CalculateMarineData.table2[i] = dataItem.value2;
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //  Console.WriteLine( "success");
        }

        private void loadCalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.ShowDialog();
            readCalibrationFile(OpenFileDialog.FileName);
        }

        private void springTensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionCheckBox.Checked)
            {
                ControlSwitches.SpringTension(enable);
                ControlSwitches.TorqueMotor(enable);
                ControlSwitches.DataCollection(enable);

                RelaySwitches.relay200Hz(enable);
                RelaySwitches.stepperMotorEnable(enable);

                sendCmd("Send Relay Switches");
                sendCmd("Send Control Switches");
                torqueMotorCheckBox.Enabled = false;
            }
            else
            {
                ControlSwitches.SpringTension(disable);
                RelaySwitches.stepperMotorEnable(disable);
                sendCmd("Send Relay Switches");
                sendCmd("Send Control Switches");
                torqueMotorCheckBox.Enabled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RelaySwitches.stepperMotorEnable(disable);
            sendCmd("Send Relay Switches");
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            RelaySwitches.stepperMotorEnable(enable);
            sendCmd("Send Relay Switches");
            // byte[] data2 = { 0x01, 0x09, 0x08 };
            // comport.Write(data2, 0, 3);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            TimerWithDataCollection("stop");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            TimerWithDataCollection("start");
        }

        private void slewSpringTensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            springTensionGroupBox.Visible = true;
        }

        private void setSpringSensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            springTensionGroupBox.Visible = true;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Single springTensionTarget;
            if (springTensionTargetNumericTextBox.Text != "" | springTensionParkRadioButton.Checked)
            {
                if (springTensionAbsoluteRadioButton.Checked)
                {
                    springTensionTarget = Convert.ToSingle(springTensionTargetNumericTextBox.Text);
                    SpringTensionStep(springTensionTarget, "absolute");
                }
                else if (springTensionRelativeRadioButton.Checked)
                {
                    springTensionTarget = Convert.ToSingle(springTensionTargetNumericTextBox.Text);
                    SpringTensionStep(springTensionTarget, "relative");
                }
                else if (springTensionParkRadioButton.Checked)
                {
                    SpringTensionStep(0, "park");
                }
                else if (springTensionSetRadioButton.Checked)
                {
                    RelaySwitches.relay200Hz(enable);
                    RelaySwitches.stepperMotorEnable(enable);
                    sendCmd("Send Relay Switches");

                    ControlSwitches.DataCollection(enable);
                    //   ControlSwitches.controlSw = 0x08;
                    sendCmd("Send Control Switches");
                    springTensionTarget = Convert.ToSingle(springTensionTargetNumericTextBox.Text);
                    newSpringTension = springTensionTarget;
                    sendCmd("Update Spring Tension Value");

                    Properties.Settings.Default.springTension = newSpringTension;
                    Properties.Settings.Default.Save();
                    STtextBox.Text = Convert.ToString(newSpringTension);
                }
            }
        }

        private void springTensionParkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionParkRadioButton.Checked)
            {
                springTensionTargetNumericTextBox.Enabled = false;
            }
            else
            {
                springTensionTargetNumericTextBox.Enabled = true;
            }
        }

        private void springTensionTargetNumericTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void springTensionRelativeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionRelativeRadioButton.Checked)
            {
                springTensionTargetNumericTextBox.Text = null;
            }
        }

        private void springTensionAbsoluteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (springTensionAbsoluteRadioButton.Checked)
            {
                springTensionTargetNumericTextBox.Text = null;
            }
        }

        public void FogCheck()
        {
            if (engineerDebug)
            {
                Console.WriteLine("CHecking FOG and Heater status");
            }
            // Check Cross FOG status
            if (MeterStatus.xGyro_Fog == 0)
            { crossFogNotReady = false; }
            else
            { crossFogNotReady = true; }

            // Check Long FOG status
            if (MeterStatus.xGyro_Fog == 0)
            { longFogNotReady = false; }
            else
            { longFogNotReady = true; }

            // Check Heater status
            if (MeterStatus.meterHeater == 0)
            { heaterNotReady = false; }
            else
            { heaterNotReady = true; }
            if (heaterWaitOptions == 2)
            {
                heaterNotReady = false;
            }
            if ((!crossFogNotReady) & (!longFogNotReady) & !(heaterNotReady))
            {
                completed = true;
            }
            else if ((heaterWaitOptions == 2))
            {
                completed = true;
            }
        }

        private void radioButtonContinue_CheckedChanged(object sender, EventArgs e)
        {
            heaterWaitOptions = 2;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm PasswordForm = new PasswordForm();
            PasswordForm.Show();
        }

        ///////////////////////////////////////////////
    }
}