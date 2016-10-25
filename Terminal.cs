#region Namespace Inclusions

using FileHelpers;
using SerialPortTerminal.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        #region Local Variables

        public CalculateMarineData mdt = new CalculateMarineData();
        public RelaySwitches RelaySwitches = new RelaySwitches();
        private ConfigData ConfigData = new ConfigData();
        private ControlSwitches ControlSwitches = new ControlSwitches();
        public Data Data = new Data();

        //        private MeterStatus MeterStatus = new MeterStatus();
        private DataStatusForm DataStatusForm = new DataStatusForm();

        private SerialPortForm SerialPortForm = new SerialPortForm();
        private static ArrayList listDataSource = new ArrayList();
        public Parameters Parameters = new Parameters();
        public UserDataForm UserDataForm = new UserDataForm();
        private static string chartWindowTimePeriod = "minutes";
        private static int chartWindowTimeSpan = 30;
        public DateTime myDatetime = DateTime.Now;

        private delegate void SetTextCallback(string text);

        public static int timeCheck = 0;
        private int countDown = 1;
        public static short springTensionScale = (short).1041666;
        public static int springTensionMaxStep = 2900;
        public static int springTensionFPM = 525;
        public static double springTensionMax = 20000;
        public static int iStop;
        public static int fmpm = 2340;
        public static int[] iStep = { 0, 0, 0, 0, 0 };
        public static Boolean completed = false;
        private Boolean crossFogNotReady = true;
        private Boolean longFogNotReady = true;
        private Boolean heaterNotReady = true;
        private static int heaterWaitOptions;
        public Single newSpringTension;
        public static Boolean engineerDebug = false;
        public static Boolean timerDebug = true;
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
        public static string dataFilePath;
        public static string programPath;
        public static string configFilePath;
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
        public Boolean newDataFile = false;

        //public double[] analogFilter = { 0.0, 0.2, 0.2, 0.2, 0, 2, 1.0, 1.0, 1.0, 10.0 }; // [0] is not used
        public int NAUX = 0;

        // public double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // [0] not used

        public Single crossCouplingFactor13 = 0;
        public Single crossCouplingFactor14 = 0;
        public Single analogFilter5 = 0;
        public double analogFilter6 = 0;
        /*public double aCrossPeriod = 0;
        public double aLongPeriod = 0;
        public double aCrossDampFactor = 0;
        public double aLongDampFactor = 0;
        public double aCrossGain = 0;
        public double aLongGain = 0;
        public double aCrossLead = 0;
        public double aLongLead = 0;
        */
        public static Int16 meter_status = 0;
        private Boolean autostart = false;
        public int timerOffset = 100;
        public static DateTime fileStartTime;
        public static string fileName;// = programPath + "test.csv";// c:/zls
        public static string calFileName;

        public static bool fileRecording = false;
        public static bool firstTime = true;
        public static string fileType;
        public static string dataFileName;
        public static DateTime oldTime = DateTime.Now;
        public string timePeriod;
        public int timeValue;
        public Boolean filterData = false;

        public class ThreadWithState
        {
            private frmTerminal frmTerminal = new frmTerminal();

            // State information used in the task.
            private string boilerplate;

            private myData value;

            // The constructor obtains the state information.
            public ThreadWithState(string text, myData number)
            {
                boilerplate = text;
                value = number;
            }

            // The thread procedure performs the task, such as formatting
            // and printing a document.
            public void ThreadProc()
            {
                Data Data = new Data();
                frmTerminal.RecordDataToFile(Data);
                frmTerminal.Dispose();
            }
        }

        public class startupData
        {
            private string Status;//TODO: where do I use startupData?

            public string status
            {
                get
                {
                    return status;
                }
                set
                {
                    Status = status;
                }
            }
        }

        public class shutdownData
        {
            //TODO: what is shutDownText for?
            private string shutDownText;

            public string ShutDownText
            {
                get
                {
                    return shutDownText;
                }
                set
                {
                    shutDownText = ShutDownText;
                }
            }
        }

        private class Record
        {
            private DateTime dateTime;
            private Single digitalGravity, springTension, crossCoupling, rawBeam, vcc, al, ax, ve, ax2, rAwg;
            private Single xacc2, lacc2, xacc, lacc, totalCorrection, longitude, latitude, altitude;
            private int year, day, hour, minute, second;
            private Single gpsStatus;

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

            public Single DigitalGravity
            {
                get { return digitalGravity; }
                set { digitalGravity = value; }
            }

            public Single SpringTension
            {
                get { return springTension; }
                set { springTension = value; }
            }

            public Single CrossCoupling
            {
                get { return crossCoupling; }
                set { crossCoupling = value; }
            }

            public Single RawBeam
            {
                get { return rawBeam; }
                set { rawBeam = value; }
            }

            public Single RawGravity
            {
                get { return rAwg; }
                set { rAwg = value; }
            }

            public Single VCC
            {
                get { return vcc; }
                set { vcc = value; }
            }

            public Single AL
            {
                get { return al; }
                set { al = value; }
            }

            public Single AX
            {
                get { return ax; }
                set { ax = value; }
            }

            public Single VE
            {
                get { return ve; }
                set { ve = value; }
            }

            public Single AX2
            {
                get { return ax2; }
                set { ax2 = value; }
            }

            public Single XACC2
            {
                get { return xacc2; }
                set { xacc2 = value; }
            }

            public Single LACC2
            {
                get { return lacc2; }
                set { lacc2 = value; }
            }

            public Single XACC
            {
                get { return xacc; }
                set { xacc = value; }
            }

            public Single LACC
            {
                get { return lacc; }
                set { lacc = value; }
            }

            public Single TotalCorrection
            {
                get { return totalCorrection; }
                set { totalCorrection = value; }
            }

            public Single Latitude
            {
                get { return latitude; }
                set { latitude = value; }
            }

            public Single Longitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            public Single Altitude
            {
                get { return altitude; }
                set { altitude = value; }
            }

            public Single GPS_Status
            {
                get { return gpsStatus; }
                set { gpsStatus = value; }
            }

            public Record(DateTime dateTime, Single digitalGravity, Single springTension, Single crossCoupling, Single rawBeam, Single rAwg, Single vcc, Single al, Single ax, Single ve, Single ax2, Single xacc2, Single lacc2, Single xacc, Single lacc, Single totalCorrection)
            {
                this.dateTime = dateTime;
                this.digitalGravity = digitalGravity;
                this.springTension = springTension;
                this.crossCoupling = crossCoupling;
                this.rawBeam = rawBeam;
                this.rAwg = rAwg;
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
        }

        private static int oldMaxArraySize;

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

            if (maxArraySize < oldMaxArraySize)
            {
                if (maxArraySize < listDataSource.Count)
                {
                    int removeCount = listDataSource.Count - maxArraySize;
                    listDataSource.RemoveRange(maxArraySize, removeCount);
                }
            }

            if (listDataSource.Count > maxArraySize)
            {
                listDataSource.RemoveAt(0);
            }
            oldMaxArraySize = maxArraySize;
        }

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

            comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            //   comport.PinChanged += new SerialPinChangedEventHandler(comport_PinChanged);
            /*
                        _timer1 = new System.Windows.Forms.Timer();
                        _timer1.Interval = (1000 - DateTime.Now.Millisecond);
                        _timer1.Enabled = false;  // ENBALE WHEN FIRST DATA IS SENT
                        _timer1.Tick += new EventHandler(port_CheckDataReceived);
              */
        }

        private void comport_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            // Show the state of the pins
            //  UpdatePinState();
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
                Console.WriteLine("This is an error.  Data mode should be HEX");
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
                return DataMode.Hex;
            }
            set
            {
            }
        }

        #endregion Local Properties

        #region Event Handlers

        // This will move to menu item
        private void lnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the user the about dialog
            (new FormAbout()).ShowDialog(this);
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

        /*
                private void rbText_CheckedChanged(object sender, EventArgs e)
                { if (SerialPortForm.rbText.Checked) CurrentDataMode = DataMode.Text; }

                private void rbHex_CheckedChanged(object sender, EventArgs e)
                { if (SerialPortForm.rbHex.Checked) CurrentDataMode = DataMode.Hex; }
                */

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
                    //   UpdatePinState();
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
                    //   UpdatePinState();
                }
                initMeter();
                meterStatusGroupBox.Visible = true;
                //  startupGroupBox.Visible = true;
                gyroCheckBox.Enabled = true;
                torqueMotorCheckBox.Enabled = false;
            }

            EnableControls();
        }

        public void WriteLogFile(string logItem)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string fileName = "\\logs\\log.txt";
                //  System.IO.File.AppendAllText("@" + path + fileName, DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\t" + logItem + Environment.NewLine);

                System.IO.File.AppendAllText(@"C:\ZLS\logs\log.txt", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\t" + logItem + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        public void TimerWithDataCollection(string state)
        {
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            if (timerDebug)
            {
                Console.WriteLine("Timer operation. New state " + state + ". Caller: " + stackTrace);
            }

            // Get calling method name
            Console.WriteLine(stackTrace.GetFrame(1).GetMethod().Name);
            switch (state)
            {
                case "start":// enables data collection and starts 1 second timer

                    if ((ControlSwitches.ControlSw & 0x01) == 0)
                    {
                        ControlSwitches.DataCollection(enable);
                        sendCmd("Send Control Switches");           // 1 ----
                    }
                    timerOffset = 200;
                    _timer1.Interval = 1000 + timerOffset; // (1000 - DateTime.Now.Millisecond);
                    _timer1.Enabled = true;
                    _timer1.Start();

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
            CalculateMarineData mdt = new CalculateMarineData();
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

                //        mdt.ParseNewMeterData(buffer);

                //             mdt.GetMeterData(buffer);// send buffer for parsing
                if (mdt.dataValid)
                {
                    // call calculaton functions

                    // Need to check how to sync threads or have all threads access same data source
                    //                   ThreadProcSafe();//  Initially write data1 -data4 in text boxes
                    if (Data.Sec % 10 == 0)
                    {
                    }
                    GravityChart.DataBind();
                }
            }
        }

        //**********************************************************************************************************

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!comport.IsOpen) return;
            Thread.Sleep(100);
            if (CurrentDataMode == DataMode.Text)
            {
                string data = comport.ReadExisting();

                Log(LogMsgType.Incoming, data);
            }
            else
            {
                /*// Obtain the number of bytes waiting in the port's buffer
                int bytes = comport.BytesToRead;
                Console.WriteLine("Bytes to read: " + bytes);
                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];
                comport.Read(buffer, 0, bytes);
                Log(LogMsgType.Incoming, ByteArrayToHexString(buffer));

            */
                // Obtain the number of bytes waiting in the port's buffer
                int bytes = comport.BytesToRead;
                if (bytes > 1)
                {
                    byte[] buffer = new byte[bytes];
                    comport.Read(buffer, 0, bytes);

                    Thread worker = new Thread(CallGetMeterData);
                    worker.Name = "calcMeterData";
                    worker.IsBackground = true;
                    worker.Start(buffer);// Start new worker thread to process the data
                }
            }
        }

        public void UpdateGravityChart()
        {
            // check for changes in time span.
            // listDataSource.Add(new Record(Data.Date, Data.gravity, Data.SpringTension, Data.CrossCoupling, Data.Beam, Data.VCC, Data.AL, Data.AX, Data.VE, Data.AX2, Data.XACC2, Data.LACC2, Data.XACC, Data.LACC, Data.totalCorrection));
            // listDataSource.Add(new Record(Data.Date, Data.data4[2], Data.data1[3], Data.data4[4], Data.data1[5], Data.rAwg, Data.VCC, Data.AL, Data.AX, Data.VE, Data.AX2, Data.XACC2, Data.LACC2, Data.XACC, Data.LACC, Data.totalCorrection));
            listDataSource.Add(new Record(Data.Date, Data.data4[2], Data.data1[3], Data.data4[4], Data.data1[5], Data.rAwg, Data.data1[6], Data.data1[7], Data.data1[8], Data.data1[9], Data.data1[10], Data.data1[11], Data.data1[12], Data.data1[13], Data.data1[14], Data.totalCorrection));

            //IFIL = 4 DEFAULT

            //  listDataSource.Add(new Record(Data.Date, 10, 50, 100, 150, 200, 10, 20, 30, 40, -10, -20, -30, -40, 50, -50));

            GravityChart.DataSource = listDataSource;
            GravityChart.DataBind();// do I need this?
            // Remove oldest element if new size == max size
            CleanUp(chartWindowTimePeriod, chartWindowTimeSpan);// limit chart to 10 min
        }

        public void DataFileOperations(myData myData)
        {
            /*
             *                  * FILE WRITE
                 * IDSKFLG = 1    LINEID, DAY, HR, MIN, SEC, GRAV, DATA4[3], DATA4[4], BSCALE * DATA4[5] ,  data4[6 -> 14], LAT, LON, ALT,GPSTATS, APERX * 1.0E6
                 * IDSKFLG = 2    LINEID, DAY, HR, MIN, SEC, GRAV, DATA1[3], DATA1[4], BSCALE * DATA1[5] ,  data1[6 -> 14], LAT, LON, ALT,GPSTATS, APERX * 1.0E6
                 *
                 */
            //TODO: file recording
            string fileStatus;
            if (fileRecording == true)
            {
                if (newDataFile)// change this.  New file will be from button or on timer at midnight
                {
                    fileStatus = "Open";
                    newDataFile = false;
                }
                else
                {
                    fileStatus = "Append";
                }

                // Supply the state information required by the task.
                ThreadWithState tws = new ThreadWithState(fileStatus, myData);
                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                t.Start();
            }
        }

        public void ShowStartupStatus()
        {
            frmTerminal frmTerminal = new frmTerminal();
            this.Invoke((MethodInvoker)delegate
            {
                frmTerminal.FogCheck();

                if (MeterStatus.LGyro_Fog == 0)
                {
                    crossGyroStatusLabel.ForeColor = Color.Green;
                    crossGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    crossGyroStatusLabel.ForeColor = Color.Red;
                    crossGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.XGyro_Fog == 0)
                {
                    longGyroStatusLabel.ForeColor = Color.Green;
                    longGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    longGyroStatusLabel.ForeColor = Color.Red;
                    longGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.MeterHeater == 0)
                {
                    heaterStatusLabel.ForeColor = Color.Green;
                    heaterStatusLabel.Text = "Ready";
                }
                else
                {
                    heaterStatusLabel.ForeColor = Color.Red;
                    heaterStatusLabel.Text = "Not ready";
                }
                if (completed)
                {
                    countDown--;
                }

                if ((completed = true) & (countDown == 0))
                {
                    meterStatusGroupBox.Visible = false;
                    startupGroupBox.Visible = true;
                }
            });
            frmTerminal.Dispose();
        }

        public void UpdateGravityDataForm(myData myData)
        {
            frmTerminal frmTerminal = new frmTerminal();
            this.Invoke((MethodInvoker)delegate
            {
                UserDataForm.textBox_gravity.Text = (Data.gravity.ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_springTension.Text = (Data.SpringTension.ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_crossCoupling.Text = (Data.CrossCoupling.ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_beam.Text = (Data.data1[4].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox5.Text = (myData.Date.ToString());

                UserDataForm.textBox_totalCorrection.Text = (Data.totalCorrection.ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_vcc.Text = (Data.data1[6].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_al.Text = (Data.data1[7].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ax.Text = (Data.data1[8].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ve.Text = (Data.data1[9].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ax2.Text = (Data.data1[10].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_xacc2.Text = (Data.data1[11].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_lacc.Text = (Data.data1[14].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_lacc2.Text = (Data.data1[12].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_xacc.Text = (Data.data1[13].ToString("N4", CultureInfo.InvariantCulture));
                UserDataForm.textBox_rawGravity.Text = (Data.rAwg.ToString("N4", CultureInfo.InvariantCulture));

                if (MeterStatus.LGyro_Fog == 0)
                {
                    UserDataForm.longGyroStatusLabel.ForeColor = Color.Green;
                    UserDataForm.longGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.longGyroStatusLabel.ForeColor = Color.Red;
                    UserDataForm.longGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.XGyro_Fog == 0)
                {
                    UserDataForm.crossGyroStatusLabel.ForeColor = Color.Green;
                    UserDataForm.crossGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.crossGyroStatusLabel.ForeColor = Color.Red;
                    UserDataForm.crossGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.MeterHeater == 0)
                {
                    UserDataForm.heaterStatusLabel.ForeColor = Color.Green;
                    UserDataForm.heaterStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.heaterStatusLabel.ForeColor = Color.Red;
                    UserDataForm.heaterStatusLabel.Text = "Not Ready";
                }

                UserDataForm.gpsSatelitesTextBox.Text = Data.gpsNumSatelites.ToString();

                if (myData.gpsNumSatelites == 0)
                {
                    UserDataForm.gpsNavigationTextBox.ForeColor = Color.Green;
                    UserDataForm.gpsNavigationTextBox.Text = "Good";
                }
                else
                {
                    UserDataForm.gpsNavigationTextBox.ForeColor = Color.Red;
                    UserDataForm.gpsNavigationTextBox.Text = "Navigation data not available";
                }
                if (myData.gpsSynchStatus == 0)
                {
                    UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Green;
                    UserDataForm.gps1HzSynchTextBox.Text = "Good";
                }
                else
                {
                    UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Red;
                    UserDataForm.gps1HzSynchTextBox.Text = "1 Hz synch pulse not present";
                }
                if (myData.gpsTimeStatus == 0)
                {
                    UserDataForm.gpsTimeSetTextBox.ForeColor = Color.Green;
                    UserDataForm.gpsTimeSetTextBox.Text = "Good";
                }
                else
                {
                    UserDataForm.gpsTimeSetTextBox.ForeColor = Color.Red;
                    UserDataForm.gpsTimeSetTextBox.Text = "GPS time set unsuccesful";
                }
            });
            frmTerminal.Dispose();
        }

        public void UpdateCalibrationParametersForm()
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (Parameters.updateConfigData)
                {
                    sendCmd("Set Cross Axis Parameters");
                    sendCmd("Set Long Axis Parameters");
                    sendCmd("Update Cross Coupling Values");

                    //  String.Format("{0:0.##}", 123.4567);

                    Parameters.updateConfigData = false;

                    Parameters.crossPeriodTextBox.Text = String.Format("{0:E6}", ConfigData.crossPeriod);
                    //   Parameters.crossPeriodTextBox.Text = (ConfigData.crossPeriod.ToString("N", CultureInfo.InvariantCulture));  // Convert.ToString( ConfigData.crossPeriod, 6 );
                    Parameters.crossDampingTextBox.Text = String.Format("{0:E6}", ConfigData.crossDampFactor);
                    Parameters.crossGainTextBox.Text = String.Format("{0:E6}", ConfigData.crossGain);
                    Parameters.crossLeadTextBox.Text = String.Format("{0:E6}", ConfigData.crossLead);
                    Parameters.crossCompFactor4TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompFactor_4);
                    Parameters.crossCompPhase4TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompPhase_4);
                    Parameters.crossCompFactor16TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompFactor_16);
                    Parameters.crossCompPhase16TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompPhase_16);

                    Parameters.longPeriodTextBox.Text = String.Format("{0:E6}", ConfigData.longPeriod);
                    Parameters.longDampingTextBox.Text = String.Format("{0:E6}", ConfigData.longDampFactor);
                    Parameters.longGainTextBox.Text = String.Format("{0:E6}", ConfigData.longGain);
                    Parameters.longLeadTextBox.Text = String.Format("{0:E6}", ConfigData.longLead);
                    Parameters.longCompFactor4TextBox.Text = String.Format("{0:E6}", ConfigData.longCompFactor_4);
                    Parameters.longCompPhase4TextBox.Text = String.Format("{0:E6}", ConfigData.longCompPhase_4);
                    Parameters.longCompFactor16TextBox.Text = String.Format("{0:E6}", ConfigData.longCompFactor_16);
                    Parameters.longCompPhase16TextBox.Text = String.Format("{0:E6}", ConfigData.longCompPhase_16);

                    Parameters.CMLFactorTextBox.Text = String.Format("{0:E6}", ConfigData.CML_Fact);
                    Parameters.ALFactorTextBox.Text = String.Format("{0:E6}", ConfigData.AL_Fact);
                    Parameters.AXFactorTextBox.Text = String.Format("{0:E6}", ConfigData.AX_Fact);
                    Parameters.VEFactorTextBox.Text = String.Format("{0:E6}", ConfigData.VE_Fact);
                    Parameters.CMXFactorTextBox.Text = String.Format("{0:E6}", ConfigData.CMX_Fact);
                    Parameters.XACC2FactorTextBox.Text = String.Format("{0:E6}", ConfigData.XACC2_Fact);
                    Parameters.LACC2FactorTextBox.Text = String.Format("{0:E6}", ConfigData.LACC2_Fact);
                    Parameters.XACCPhasetextBox.Text = String.Format("{0:E6}", ConfigData.XACC_Phase);
                    Parameters.LACC_AL_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_AL_Phase);
                    Parameters.LACC_CML_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_CML_Phase);
                    Parameters.LACC_CMX_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_CMX_Phase);

                    Parameters.maxSpringTensionTextBox.Text = String.Format("{0:0.##}", ConfigData.springTensionMax);
                    Parameters.gyroTypeComboBox.SelectedText = ConfigData.gyroType;
                    Parameters.meterNumberTextBox.Text = ConfigData.meterNumber;
                    Parameters.kFactorTextBox.Text = String.Format("{0:E6}", ConfigData.kFactor);
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
            });
        }

        public void CallGetMeterData(object buffer)
        {
            frmTerminal frmTerminal = new frmTerminal();
            CalculateMarineData mdt = new CalculateMarineData();
            byte[] bufferByte = (byte[])buffer;
            Boolean showData = false;
            mdt.ParseNewMeterData(bufferByte);

            // mdt.GetMeterData((byte[])buffer);// send buffer for parsing
            Data Data = new Data();

            // check for mode  1 sec or 10 sec
            // for now only 10 sec data

            showData = false;
            if (Data.Date.Second % 10 == 0)
            {
                Console.WriteLine(Data.Date.Second);
                showData = true;
            }

            if (Parameters.Visible)// Update at 1 sec
            {
                UpdateCalibrationParametersForm();
            }

            if (mdt.dataValid && showData)// May want to move this to the called methods
            {
                //  Console.WriteLine(bufferByte.Length + "     " + bufferByte[0] + "    " + mdt.latitude + "    " + DateTime.Now.ToString());
                //TODO: get callbacks working

                myData myData = new myData();

                // *************************************************************************************************8
                //
                //      Update myData based on 1 or 10 second sampling
                //
                // *************************************************************************************************8

                myData.Date = Data.Date;
                myData.Gravity = Data.gravity;
                myData.SpringTension = Data.SpringTension;
                myData.CrossCoupling = Data.CrossCoupling;
                myData.rawBeam = Data.beam;
                myData.VCC = Data.VCC;
                myData.AL = Data.AL;
                myData.AX = Data.AX;
                myData.VE = Data.VE;
                myData.AX2 = Data.AX2;
                myData.XACC2 = Data.XACC2;
                myData.LACC2 = Data.LACC2;
                myData.XACC = Data.XACC;
                myData.LACC = Data.LACC;
                myData.RawGravity = Data.rAwg;
                myData.TotalCorrection = Data.totalCorrection;
                myData.longitude = Data.longitude;
                myData.latitude = Data.latitude;
                myData.altitude = Data.altitude;
                myData.gpsNavigationStatus = Data.gpsNavigationStatus;
                myData.gpsSynchStatus = Data.gpsSyncStatus;
                myData.gpsTimeStatus = Data.gpsTimeStatus;
                myData.gpsNumSatelites = Data.gpsNumSatelites;

                /*  IFIL PROBABLY IS FROM MODE MARINE OR HI
                 * screen write
                 * ISCFLG = 1    RAWG, data(IFIL)[6 -> 10, 13, 14]
                 * ISCFLG = 2    LINEID, DAY, HR, MIN, SEC, DATA4[3], DATA1[3],DATA(IFIL)[4], DATA1[5], TC
                 * ISCFLG = 3    PRSTATS
                 * ISCFLG = 4    LAT, LON, ALT, ISHFT(GPSTATS, -6)
                 *

                 *                 /*  IFIL PROBABLY IS FROM MODE MARINE OR HI
                 * RS232 WRITE
                 * MODESW = 1    LINEID, DAY, HR, MIN, SEC, GRAV, DATA1[3], DATA1[4], BSCALE * DATA1[5] ,  data1[6 -> 14], LAT, LON, ALT,GPSTATS, APERX * 1.0E6
                 * MODESW = 2    LINEID, DAY, HR, MIN, SEC, GRAV, DATA4[3], DATA4[4], BSCALE * DATA4[5] ,  data4[6 -> 14], LAT, LON, ALT,GPSTATS, APERX * 1.0E6

                 * */

                // *******************************************************************************************************
                //
                //                Need to seperate this into multiple methods
                //
                // *******************************************************************************************************

                // Write grav data to file
                DataFileOperations(myData);

                if (this.GravityChart.InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        if (GravityChart.Visible)
                        {
                            UpdateGravityChart();
                        }
                    });
                }
                else
                {
                    if (GravityChart.Visible)
                    {
                        UpdateGravityChart();
                    }
                }

                // Update GPS data on main sheet
                Invoke((MethodInvoker)delegate
                {
                    string specifier;
                    specifier = "000.000000";
                    gpsAltitudeTextBox.Text = (myData.altitude.ToString("N", CultureInfo.InvariantCulture));
                    gpsLatitudeTextBox.Text = (Data.latitude.ToString(specifier, CultureInfo.InvariantCulture));
                    gpsLongitudeTextBox.Text = (Data.longitude.ToString(specifier, CultureInfo.InvariantCulture));
                    if ((Data.gpsNavigationStatus == 0) & (Data.gpsTimeStatus == 0) & (Data.gpsSyncStatus == 0))
                    {
                        gpsStartusTextBox.ForeColor = Color.Green;
                        gpsStartusTextBox.Text = "Good";
                    }
                    else
                    {
                        gpsStartusTextBox.ForeColor = Color.Red;
                        gpsStartusTextBox.Text = "Error";
                    }
                });

                if (meterStatusGroupBox.Visible)
                {
                    ShowStartupStatus();
                }

                if (UserDataForm.Visible)
                {
                    UpdateGravityDataForm(myData);
                }

                /*
                if (frmTerminal.DataStatusForm.Visible)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        DataStatusForm.textBox1.Text = (Data.gravity.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox2.Text = (Data.SpringTension.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox3.Text = (Data.CrossCoupling.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox4.Text = (Data.beam.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox5.Text = (Data.myDT.ToString());
                        DataStatusForm.textBox6.Text = (Data.totalCorrection.ToString("N", CultureInfo.InvariantCulture));

                        DataStatusForm.textBox7.Text = (Data.VCC.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox8.Text = (Data.AL.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox9.Text = (Data.AX.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox10.Text = (Data.VE.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox11.Text = (Data.AX2.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox12.Text = (Data.XACC2.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox13.Text = (Data.LACC2.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox14.Text = (Data.XACC.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox15.Text = (Data.LACC.ToString("N", CultureInfo.InvariantCulture));
                        DataStatusForm.textBox1.Text = (Data.gravity.ToString("N", CultureInfo.InvariantCulture));
                    });
                }   */
            }
            frmTerminal.Dispose();
        }

        private void ThreadParametersFormProcSafe()
        {
            string text = Convert.ToString(Data.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(Data.year) + "\t" + Convert.ToString(Data.day));
            if (this.DataStatusForm.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback parametersFormCallback = new SetTextCallback(SetText);
                this.Invoke(parametersFormCallback, new object[] { text });
            }
            else
            {
                // write config values to parameter form when required
                if (Parameters.updateConfigData)
                {
                    sendCmd("Set Cross Axis Parameters");
                    sendCmd("Set Long Axis Parameters");
                    sendCmd("Update Cross Coupling Values");

                    //  String.Format("{0:0.##}", 123.4567);

                    Parameters.updateConfigData = false;

                    Parameters.crossPeriodTextBox.Text = String.Format("{0:E6}", ConfigData.crossPeriod);
                    //   Parameters.crossPeriodTextBox.Text = (ConfigData.crossPeriod.ToString("N", CultureInfo.InvariantCulture));  // Convert.ToString( ConfigData.crossPeriod, 6 );
                    Parameters.crossDampingTextBox.Text = String.Format("{0:E6}", ConfigData.crossDampFactor);
                    Parameters.crossGainTextBox.Text = String.Format("{0:E6}", ConfigData.crossGain);
                    Parameters.crossLeadTextBox.Text = String.Format("{0:E6}", ConfigData.crossLead);
                    Parameters.crossCompFactor4TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompFactor_4);
                    Parameters.crossCompPhase4TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompPhase_4);
                    Parameters.crossCompFactor16TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompFactor_16);
                    Parameters.crossCompPhase16TextBox.Text = String.Format("{0:E6}", ConfigData.crossCompPhase_16);

                    Parameters.longPeriodTextBox.Text = String.Format("{0:E6}", ConfigData.longPeriod);
                    Parameters.longDampingTextBox.Text = String.Format("{0:E6}", ConfigData.longDampFactor);
                    Parameters.longGainTextBox.Text = String.Format("{0:E6}", ConfigData.longGain);
                    Parameters.longLeadTextBox.Text = String.Format("{0:E6}", ConfigData.longLead);
                    Parameters.longCompFactor4TextBox.Text = String.Format("{0:E6}", ConfigData.longCompFactor_4);
                    Parameters.longCompPhase4TextBox.Text = String.Format("{0:E6}", ConfigData.longCompPhase_4);
                    Parameters.longCompFactor16TextBox.Text = String.Format("{0:E6}", ConfigData.longCompFactor_16);
                    Parameters.longCompPhase16TextBox.Text = String.Format("{0:E6}", ConfigData.longCompPhase_16);

                    Parameters.CMLFactorTextBox.Text = String.Format("{0:E6}", ConfigData.CML_Fact);
                    Parameters.ALFactorTextBox.Text = String.Format("{0:E6}", ConfigData.AL_Fact);
                    Parameters.AXFactorTextBox.Text = String.Format("{0:E6}", ConfigData.AX_Fact);
                    Parameters.VEFactorTextBox.Text = String.Format("{0:E6}", ConfigData.VE_Fact);
                    Parameters.CMXFactorTextBox.Text = String.Format("{0:E6}", ConfigData.CMX_Fact);
                    Parameters.XACC2FactorTextBox.Text = String.Format("{0:E6}", ConfigData.XACC2_Fact);
                    Parameters.LACC2FactorTextBox.Text = String.Format("{0:E6}", ConfigData.LACC2_Fact);
                    Parameters.XACCPhasetextBox.Text = String.Format("{0:E6}", ConfigData.XACC_Phase);
                    Parameters.LACC_AL_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_AL_Phase);
                    Parameters.LACC_CML_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_CML_Phase);
                    Parameters.LACC_CMX_PhaseTextBox.Text = String.Format("{0:E6}", ConfigData.LACC_CMX_Phase);

                    Parameters.maxSpringTensionTextBox.Text = String.Format("{0:0.##}", ConfigData.springTensionMax);
                    Parameters.gyroTypeComboBox.SelectedText = ConfigData.gyroType;
                    Parameters.meterNumberTextBox.Text = ConfigData.meterNumber;
                    Parameters.kFactorTextBox.Text = String.Format("{0:E6}", ConfigData.kFactor);
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
            }
        }

        private void ThreadUserDataFormProcSafe()
        {
            Data Data = new Data();
            string text = Convert.ToString(Data.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(Data.year) + "\t" + Convert.ToString(Data.day));

            if (this.DataStatusForm.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback userDataCallback = new SetTextCallback(SetText);
                this.Invoke(userDataCallback, new object[] { text });
                Thread.Sleep(2000);
            }
            else
            {
                UserDataForm.textBox_gravity.Text = (Data.gravity.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_springTension.Text = (Data.SpringTension.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_crossCoupling.Text = (Data.CrossCoupling.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_beam.Text = (Data.beam.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox5.Text = (Data.myDT.ToString());
                UserDataForm.textBox_totalCorrection.Text = (Data.totalCorrection.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_xacc2.Text = (Data.VCC.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_vcc.Text = (Data.AL.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_al.Text = (Data.AX.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ve.Text = (Data.VE.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ax.Text = (Data.AX2.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_ax2.Text = (Data.XACC2.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_lacc.Text = (Data.LACC2.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_lacc2.Text = (Data.XACC.ToString("N", CultureInfo.InvariantCulture));
                UserDataForm.textBox_xacc.Text = (Data.LACC.ToString("N", CultureInfo.InvariantCulture));

                if (MeterStatus.LGyro_Fog == 0)
                {
                    UserDataForm.longGyroStatusLabel.ForeColor = Color.Green;
                    UserDataForm.longGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.longGyroStatusLabel.ForeColor = Color.Red;
                    UserDataForm.longGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.XGyro_Fog == 0)
                {
                    UserDataForm.crossGyroStatusLabel.ForeColor = Color.Green;
                    UserDataForm.crossGyroStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.crossGyroStatusLabel.ForeColor = Color.Red;
                    UserDataForm.crossGyroStatusLabel.Text = "Not Ready";
                }
                if (MeterStatus.MeterHeater == 0)
                {
                    UserDataForm.heaterStatusLabel.ForeColor = Color.Green;
                    UserDataForm.heaterStatusLabel.Text = "Ready";
                }
                else
                {
                    UserDataForm.heaterStatusLabel.ForeColor = Color.Red;
                    UserDataForm.heaterStatusLabel.Text = "Not Ready";
                }

                /////////
                UserDataForm.gpsSatelitesTextBox.Text = Data.gpsNumSatelites.ToString();

                if (Data.gpsNavigationStatus == 0)
                {
                    UserDataForm.gpsNavigationTextBox.ForeColor = Color.Green;
                    UserDataForm.gpsNavigationTextBox.Text = "Good";
                }
                else
                {
                    UserDataForm.gpsNavigationTextBox.ForeColor = Color.Red;
                    UserDataForm.gpsNavigationTextBox.Text = "Navigation data not available";
                }
                if (Data.gpsSyncStatus == 0)
                {
                    UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Green;
                    UserDataForm.gps1HzSynchTextBox.Text = "Good";
                }
                else
                {
                    UserDataForm.gps1HzSynchTextBox.ForeColor = Color.Red;
                    UserDataForm.gps1HzSynchTextBox.Text = "1 Hz synch pulse not present";
                }
                if (Data.gpsTimeStatus == 0)
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
        }

        private void ThreadDataStatusFormProcSafe()
        {
            Data Data = new Data();
            string text = Convert.ToString(Data.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(Data.year) + "\t" + Convert.ToString(Data.day));

            if (this.DataStatusForm.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback dataStatusCallback = new SetTextCallback(SetText);
                this.Invoke(dataStatusCallback, new object[] { text });
                Thread.Sleep(2000);
            }
            else
            {
                DataStatusForm.textBox1.Text = (Data.gravity.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox2.Text = (Data.SpringTension.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox3.Text = (Data.CrossCoupling.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox4.Text = (Data.beam.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox5.Text = (Data.myDT.ToString());
                DataStatusForm.textBox6.Text = (Data.totalCorrection.ToString("N", CultureInfo.InvariantCulture));

                DataStatusForm.textBox7.Text = (Data.VCC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox8.Text = (Data.AL.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox9.Text = (Data.AX.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox10.Text = (Data.VE.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox11.Text = (Data.AX2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox12.Text = (Data.XACC2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox13.Text = (Data.LACC2.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox14.Text = (Data.XACC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox15.Text = (Data.LACC.ToString("N", CultureInfo.InvariantCulture));
                DataStatusForm.textBox1.Text = (Data.gravity.ToString("N", CultureInfo.InvariantCulture));
            }
        }

        private void ThreadProcSafe(myData myData)
        {
            // TODO: cleaup ThreadProcSafe
            string text = Convert.ToString(Data.myDT) + "\t\t" + "\t Expected bytes: " + Convert.ToString(mdt.dataLength + "\t" + Convert.ToString(Data.year) + "\t" + Convert.ToString(Data.day));

            // Check if this method is running on a different thread
            // than the thread that created the control.
            if (this.gpsStartusTextBox.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                SetTextCallback mainDataCallBack = new SetTextCallback(SetText);
                this.Invoke(mainDataCallBack, new object[] { text });
            }
            else
            {
            }
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

        // TODO Gravity chart setup
        private void SetupChart()
        {
            this.GravityChart.ChartAreas["CrossCoupling"].Visible = false;

            // Add series to chart

            this.GravityChart.Series.Add("Digital Gravity");
            this.GravityChart.Series.Add("Spring Tension");
            this.GravityChart.Series.Add("Cross Coupling");
            this.GravityChart.Series.Add("Raw Beam");
            this.GravityChart.Series.Add("Total Correction");
            this.GravityChart.Series.Add("AL");
            this.GravityChart.Series.Add("AX");
            this.GravityChart.Series.Add("VE");
            this.GravityChart.Series.Add("AX2");
            this.GravityChart.Series.Add("XACC");
            this.GravityChart.Series.Add("LACC");
            this.GravityChart.Series.Add("Raw Gravity");

            // Assign series to secondary axis
            this.GravityChart.Series["XACC"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GravityChart.Series["LACC"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //     this.GravityChart.Series["Cross Coupling"].YAxisType    = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GravityChart.Series["Total Correction"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;

            this.GravityChart.Titles.Add("Gravity");// Should probably change this

            // Select chart area for series

            this.GravityChart.Series["Digital Gravity"].ChartArea = "Gravity";
            this.GravityChart.Series["Spring Tension"].ChartArea = "Gravity";
            this.GravityChart.Series["Cross Coupling"].ChartArea = "Gravity";
            this.GravityChart.Series["Total Correction"].ChartArea = "Gravity";
            this.GravityChart.Series["XACC"].ChartArea = "Gravity";
            this.GravityChart.Series["LACC"].ChartArea = "Gravity";

            this.GravityChart.Series["Digital Gravity"].Legend = "Gravity Legend";
            this.GravityChart.Series["Spring Tension"].Legend = "Gravity Legend";
            this.GravityChart.Series["Cross Coupling"].Legend = "Gravity Legend";
            this.GravityChart.Series["Total Correction"].Legend = "Gravity Legend";
            this.GravityChart.Series["XACC"].Legend = "Gravity Legend";
            this.GravityChart.Series["LACC"].Legend = "Gravity Legend";

            this.GravityChart.Series["AL"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["AX"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["VE"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["AX2"].ChartArea = "CrossCoupling";
            // this.GravityChart.Series["XACC"].ChartArea = "CrossCoupling";
            // this.GravityChart.Series["LACC"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["Raw Gravity"].ChartArea = "CrossCoupling";
            this.GravityChart.Series["Raw Beam"].ChartArea = "CrossCoupling";

            this.GravityChart.Series["AL"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["AX"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["VE"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["AX2"].Legend = "Cross Coupling Legend";
            // this.GravityChart.Series["XACC"].Legend = "Cross Coupling Legend";
            // this.GravityChart.Series["LACC"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["Raw Gravity"].Legend = "Cross Coupling Legend";
            this.GravityChart.Series["Raw Beam"].Legend = "Cross Coupling Legend";

            //      SETUP MAIN PAIGE GRAVITY CHART
            this.GravityChart.ChartAreas["Gravity"].AxisX.IsMarginVisible = false;
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Format = "HH:mm:ss";  //  "yyyy-MM-dd HH:mm:ss";
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;
            this.GravityChart.Series["Digital Gravity"].XValueMember = "date";
            this.GravityChart.Series["Digital Gravity"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Digital Gravity"].YValueMembers = "digitalGravity";
            this.GravityChart.Series["Digital Gravity"].BorderWidth = 4;

            this.GravityChart.Series["Spring Tension"].XValueMember = "date";
            this.GravityChart.Series["Spring Tension"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Spring Tension"].YValueMembers = "springTension";
            this.GravityChart.Series["Spring Tension"].BorderWidth = 4;

            this.GravityChart.Series["Cross Coupling"].XValueMember = "date";
            this.GravityChart.Series["Cross Coupling"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Cross Coupling"].YValueMembers = "crossCoupling";
            this.GravityChart.Series["Cross Coupling"].BorderWidth = 4;

            this.GravityChart.Series["Raw Beam"].XValueMember = "date";
            this.GravityChart.Series["Raw Beam"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Raw Beam"].YValueMembers = "RawBeam";
            this.GravityChart.Series["Raw Beam"].BorderWidth = 4;

            this.GravityChart.Series["Total Correction"].XValueMember = "date";
            this.GravityChart.Series["Total Correction"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["Total Correction"].YValueMembers = "totalCorrection";
            this.GravityChart.Series["Total Correction"].BorderWidth = 4;

            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90

            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            this.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            this.GravityChart.ChartAreas["Gravity"].AxisY2.ScaleView.ZoomReset(1);
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

            //this.GravityChart.ChartAreas["CrossCoupling"].AxisX.IsMarginVisible = false;
            //this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Format = "HH:mm:ss"; ; //  "yyyy-MM-dd HH:mm:ss";
            //this.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;

            this.GravityChart.Series["AL"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["AL"].XValueMember = "date";
            this.GravityChart.Series["AL"].YValueMembers = "AL";
            this.GravityChart.Series["AL"].BorderWidth = 2;

            this.GravityChart.Series["AX"].XValueMember = "date";
            this.GravityChart.Series["AX"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["AX"].YValueMembers = "AX";
            this.GravityChart.Series["AX"].BorderWidth = 2;

            this.GravityChart.Series["VE"].XValueMember = "date";
            this.GravityChart.Series["VE"].YValueMembers = "VE";
            this.GravityChart.Series["VE"].BorderWidth = 2;

            this.GravityChart.Series["AX2"].XValueMember = "date";
            this.GravityChart.Series["AX2"].YValueMembers = "AX2";
            this.GravityChart.Series["AX2"].BorderWidth = 2;

            this.GravityChart.Series["XACC"].XValueMember = "date";
            this.GravityChart.Series["XACC"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["XACC"].YValueMembers = "XACC";
            this.GravityChart.Series["XACC"].BorderWidth = 2;

            this.GravityChart.Series["LACC"].XValueMember = "date";
            this.GravityChart.Series["LACC"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.GravityChart.Series["LACC"].YValueMembers = "LACC";
            this.GravityChart.Series["LACC"].BorderWidth = 2;

            this.GravityChart.Series["Raw Gravity"].XValueMember = "date";
            this.GravityChart.Series["Raw Gravity"].YValueMembers = "rawGravity";// rAwg
            this.GravityChart.Series["Raw Gravity"].BorderWidth = 2;

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
            SetChartAxis(false);
            SetChartColors();
            //          SetLegend();
        }

        private void SetChartAxis(Boolean auto)
        {
            if (auto)
            {
                // chart.ChartAreas[0].RecalculateAxesScale();

                GravityChart.ChartAreas["Gravity"].AxisY.Maximum = Double.NaN;
                GravityChart.ChartAreas["Gravity"].AxisY.Minimum = Double.NaN;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.Maximum = Double.NaN;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.Minimum = Double.NaN;

                GravityChart.ChartAreas["Gravity"].AxisY2.Maximum = Double.NaN;
                GravityChart.ChartAreas["Gravity"].AxisY2.Minimum = Double.NaN;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.Maximum = Double.NaN;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.Minimum = Double.NaN;
            }
            else
            {
                GravityChart.ChartAreas["Gravity"].AxisY.Maximum = 20000;
                GravityChart.ChartAreas["Gravity"].AxisY.Minimum = -20000;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.Maximum = 100;
                GravityChart.ChartAreas["CrossCoupling"].AxisY.Minimum = -100;

                GravityChart.ChartAreas["Gravity"].AxisY2.Maximum = 100;
                GravityChart.ChartAreas["Gravity"].AxisY2.Minimum = -100;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.Maximum = 10;
                GravityChart.ChartAreas["CrossCoupling"].AxisY2.Minimum = -10;
            }
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
            GravityChart.ChartAreas["Gravity"].AxisY2.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["CrossCoupling"].AxisY2.ScaleView.Zoomable = true;

            //            GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(2, 3);
            //            GravityChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset(1);
        }

        private void SetChartScaleView()
        {
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Minutes;
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.SmallScrollSize = .1D;
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;
            GravityChart.ChartAreas["Gravity"].AxisY2.ScaleView.Zoomable = true;

            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.ZoomReset(1);
            GravityChart.ChartAreas["Gravity"].AxisY2.ScaleView.ZoomReset(1);

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
            GravityChart.ChartAreas["Gravity"].AxisY2.ScrollBar.IsPositionedInside = true;

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

        // TODO SetChartToolTips
        private void SetChartToolTips()
        {
            string mode = "Time/Value";

            if (mode == "Time/Value")
            {
                GravityChart.Series["Spring Tension"].ToolTip = "Spring Tension\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["Cross Coupling"].ToolTip = "Cross Coupling\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["Raw Beam"].ToolTip = "Raw Beam\nTime: #VALX{HH:mm:ss}\nValue: VALY{N4}";
                GravityChart.Series["Total Correction"].ToolTip = "Total Correction\nTime: VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["AL"].ToolTip = "AL\nTime: #VALX{HH:mm:ss}\nValue:# VALY{N4}";
                GravityChart.Series["AX"].ToolTip = "AX\nTime: #VALX{HH:mm:ss}\nValue:# VALY{N4}";
                GravityChart.Series["VE"].ToolTip = "VE\nTime: n#VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["AX2"].ToolTip = "AX2\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["XACC"].ToolTip = "XACC\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["LACC"].ToolTip = "LACC\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
                GravityChart.Series["Raw Gravity"].ToolTip = "Raw Gravity\nTime: VALX{HH:mm:ss}\n#Value: #VALY{N4}";
                GravityChart.Series["Digital Gravity"].ToolTip = "Digital Gravity\nTime: #VALX{HH:mm:ss}\nValue: #VALY{N4}";
            }
            else if (mode == "Value")
            {
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
                GravityChart.Series["Raw Gravity"].ToolTip = "Raw Gravity = " + "#VALY";
            }
        }

        public void SetChartAreaColors(int scheme)
        {
            if (scheme == 1)// Light background
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
            if (scheme == 2)// gray background
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
            if (scheme == 3)// black background
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
            if (scheme == 4)// Light grey background
            {
                //  GRAVITY
                GravityChart.ChartAreas["Gravity"].BackColor = System.Drawing.Color.LightGray;
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
            private static System.Drawing.Color digitalGravity = System.Drawing.Color.Green;

            public static System.Drawing.Color DigitalGravity
            {
                get { return digitalGravity; }
                set { digitalGravity = value; }
            }

            private static System.Drawing.Color springTension = System.Drawing.Color.Orange;

            public static System.Drawing.Color SpringTension
            {
                get { return springTension; }
                set { springTension = value; }
            }

            private static System.Drawing.Color crossCoupling = System.Drawing.Color.Red;

            public static System.Drawing.Color CrossCoupling
            {
                get { return crossCoupling; }
                set { crossCoupling = value; }
            }

            private static System.Drawing.Color rawBeam = System.Drawing.Color.Pink;

            public static System.Drawing.Color RawBeam
            {
                get { return rawBeam; }
                set { rawBeam = value; }
            }

            private static System.Drawing.Color totalCorrection = System.Drawing.Color.Black;

            public static System.Drawing.Color TotalCorrection
            {
                get { return totalCorrection; }
                set { totalCorrection = value; }
            }

            private static System.Drawing.Color al = System.Drawing.Color.LightSeaGreen;

            public static System.Drawing.Color AL
            {
                get { return al; }
                set { al = value; }
            }

            private static System.Drawing.Color ax = System.Drawing.Color.OrangeRed;

            public static System.Drawing.Color AX
            {
                get { return ax; }
                set { ax = value; }
            }

            private static System.Drawing.Color ve = System.Drawing.Color.PaleVioletRed;

            public static System.Drawing.Color VE
            {
                get { return ve; }
                set { ve = value; }
            }

            private static System.Drawing.Color ax2 = System.Drawing.Color.Red;

            public static System.Drawing.Color AX2
            {
                get { return ax2; }
                set { ax2 = value; }
            }

            private static System.Drawing.Color lacc = System.Drawing.Color.LightBlue;

            public static System.Drawing.Color LACC
            {
                get { return lacc; }
                set { lacc = value; }
            }

            private static System.Drawing.Color xacc = System.Drawing.Color.SteelBlue;

            public static System.Drawing.Color XACC
            {
                get { return xacc; }
                set { xacc = value; }
            }

            private static System.Drawing.Color rAwg = System.Drawing.Color.SteelBlue;

            public static System.Drawing.Color RAWG
            {
                get { return rAwg; }
                set { rAwg = value; }
            }
        }

        private static class ChartVisibility
        {
            private static Boolean digitalGravity = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean DigitalGravity
            {
                get { return digitalGravity; }
                set { digitalGravity = value; }
            }

            private static Boolean springTension = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean SpringTension
            {
                get { return springTension; }
                set { springTension = value; }
            }

            private static Boolean crossCoupling = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean CrossCoupling
            {
                get { return crossCoupling; }
                set { crossCoupling = value; }
            }

            private static Boolean rawBeam = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean RawBeam
            {
                get { return rawBeam; }
                set { rawBeam = value; }
            }

            private static Boolean totalCorrection = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean TotalCorrection
            {
                get { return totalCorrection; }
                set { totalCorrection = value; }
            }

            //   public static Boolean rawGravity = true;
            private static Boolean al = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean AL
            {
                get { return al; }
                set { al = value; }
            }

            private static Boolean ax = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean AX
            {
                get { return ax; }
                set { ax = value; }
            }

            private static Boolean ve = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean VE
            {
                get { return ve; }
                set { ve = value; }
            }

            private static Boolean ax2 = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean AX2
            {
                get { return ax2; }
                set { ax2 = value; }
            }

            private static Boolean lacc = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean LACC
            {
                get { return lacc; }
                set { lacc = value; }
            }

            private static Boolean xacc = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean XACC
            {
                get { return xacc; }
                set { xacc = value; }
            }

            private static Boolean rAwg = true;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public static Boolean RAWG
            {
                get { return rAwg; }
                set { rAwg = value; }
            }
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
            GravityChart.Series["Raw Gravity"].BorderWidth = width;
        }

        public void SetChartColors()
        {
            GravityChart.Series["Digital Gravity"].Color = ChartColors.DigitalGravity;
            GravityChart.Series["Spring Tension"].Color = ChartColors.SpringTension;
            GravityChart.Series["Cross Coupling"].Color = ChartColors.CrossCoupling;
            GravityChart.Series["Raw Beam"].Color = ChartColors.RawBeam;
            GravityChart.Series["Total Correction"].Color = ChartColors.TotalCorrection;
            GravityChart.Series["Raw Gravity"].Color = ChartColors.RAWG;
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

            GravityChart.Series["Raw Gravity"].MarkerSize = size;
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
            GravityChart.Series["Raw Gravity"].ChartType = myChartType;
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

        // TODO: remove or disable with new computer - user will not have options
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

        private void LogConfigData(ConfigData ConfigData)
        {
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
                shift = 500 - Data.data1[3];
                // goto 100
            }
            else if (iStop == 4)
            {
                shift = target - Data.data1[3];
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
                Console.WriteLine("Current ST " + Data.SpringTension);
                Console.WriteLine("Initial shift = " + shift);
            }

            switch (slewType)
            {
                case "absolute":
                    //  shift = target - CalculateMarineData.data1[3];
                    shift = target - Data.SpringTension;

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
                    shift = springTensionMax - Data.SpringTension - 500;
                    break;

                default:
                    break;
            }
            //            if ((shift + CalculateMarineData.data1[3] > springTensionMax) || shift + CalculateMarineData.data1[3] < 100)

            if ((shift + Data.SpringTension > springTensionMax) || (shift + Data.SpringTension < 100) || (target > springTensionMax) || (Math.Abs(shift) > springTensionMax - 500))
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

                int sign = 1;

                if (shift > 0)
                {
                    sign = Math.Abs(Convert.ToSByte(M) * springTensionScale);
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
                Data.SpringTension += sign;
                var tempSt = Math.Round(Convert.ToDouble(Data.SpringTension), 1);
                Data.SpringTension = Convert.ToSingle(tempSt);

                if (engineerDebug)
                {
                    Console.WriteLine("New ST = " + Data.SpringTension);
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
                    STtextBox.Text = Convert.ToString(Data.SpringTension);
                    M -= springTensionMaxStep;
                }

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
                STtextBox.Text = Convert.ToString(Data.SpringTension);
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
                var myStartTime = DateTime.Now;
                if (firstTime == true)
                {
                    this.Invoke(new UpdateFileNameLabelCallback(this.UpdateNameLabel), new object[] { });
                    this.Invoke(new UpdateRecordBoxCallback(this.UpdateRecordBox), new object[] { true });
                    fileStartTime = myStartTime;// DateTime.Now;
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
            springTensionValueLabel.Text = "";
            springTensionStatusLabel.Text = "";
        }

        private void frmTerminal_Load(object sender, EventArgs e)
        {
            SetInitialVisibility();

            // Console.WriteLine(a);
            //  Console.WriteLine(DateTime.UtcNow);

            if (engineerDebug)
            {
                rtfTerminal.Visible = true;
            }
            else
            {
                rtfTerminal.Visible = false;
            }

            CurrentDataMode = DataMode.Hex;

            // Create an instance of NumericTextBox.
            NumericTextBox numericTextBox1 = new NumericTextBox();
            numericTextBox1.Parent = this;
            //Draw the bounds of the NumericTextBox.
            numericTextBox1.Bounds = new System.Drawing.Rectangle(5, 5, 150, 100);

            //  Load stored state
            InitStoredVariables();

            if (engineerDebug)
            {
                Console.WriteLine("Data file name " + fileName);
            }

            STtextBox.Text = Convert.ToString(Data.SpringTension);

            // load config file
            CheckConfigFile(configFilePath + "\\" + configFileName);
            //  ReadConfigFile(configFilePath + "\\" + configFileName);
            //  UserDataForm.configurationFileTextBox.Text = configFilePath + "\\" + configFileName;

            CheckCalFile(calFilePath + "\\" + calFileName);

            comboBox1.SelectedItem = "minutes";
            windowSizeNumericUpDown.Minimum = 1;
            windowSizeNumericUpDown.Maximum = 60;
            Thread.CurrentThread.Name = "time";

            Thread TimeThread = new Thread(new ThreadStart(TimeWorker));
            TimeThread.IsBackground = true;
            TimeThread.Start();

            SetupChart();
            // Setup DataGrid();
            WriteLogFile("System loaded");
        }

        //==================================================================================================

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

            return outputBytes;
        }

        public byte[] CreateCrossAxisParametersArray(byte command, double data1, double data2, double data3, double data4, double data5, double data6)
        {
            byte cmdByte = command;

            byte[] checkSum = new byte[1];
            byte[] byteArray1 = BitConverter.GetBytes(data1);
            Single sdata1 = Convert.ToSingle(0.0000191 * 1000) / 1000;

            byte[] outputBytes = new byte[26];
            data1 = Convert.ToSingle(data1);
            byte[] byte1 = BitConverter.GetBytes(sdata1);
            byte[] byte2 = BitConverter.GetBytes(Convert.ToSingle(data2));
            byte[] byte3 = BitConverter.GetBytes(Convert.ToSingle(data3));
            byte[] byte4 = BitConverter.GetBytes(Convert.ToSingle(data4));
            byte[] byte5 = BitConverter.GetBytes(Convert.ToSingle(data5));
            byte[] byte6 = BitConverter.GetBytes(Convert.ToSingle(data6));

            outputBytes[0] = cmdByte;
            outputBytes[1] = byte1[0];
            outputBytes[2] = byte1[1];
            outputBytes[3] = byte1[2];
            outputBytes[4] = byte1[3];

            outputBytes[5] = byte2[0];
            outputBytes[6] = byte2[1];
            outputBytes[7] = byte2[2];
            outputBytes[8] = byte2[3];

            outputBytes[9] = byte3[0];
            outputBytes[10] = byte3[1];
            outputBytes[11] = byte3[2];
            outputBytes[12] = byte3[3];

            outputBytes[13] = byte4[0];
            outputBytes[14] = byte4[1];
            outputBytes[15] = byte4[2];
            outputBytes[16] = byte4[3];

            outputBytes[17] = byte5[0];
            outputBytes[18] = byte5[1];
            outputBytes[19] = byte5[2];
            outputBytes[20] = byte5[3];

            outputBytes[21] = byte6[0];
            outputBytes[22] = byte6[1];
            outputBytes[23] = byte6[2];
            outputBytes[24] = byte6[3];

            //------------------------------------------------------------------------------------------------------------
            /*
                        outputBytes[0] = 0x07;
                        outputBytes[1] = byteArray1[0];
                        outputBytes[2] = byteArray1[1];
                        outputBytes[3] = byteArray1[2];
                        outputBytes[4] = byteArray1[3];
            */
            checkSum = CalculateCheckSum(outputBytes, outputBytes.Length);
            byte nByte = BitConverter.GetBytes(outputBytes.Length)[0];
            outputBytes[outputBytes.Length - 1] = checkSum[0];

            return outputBytes;
        }

        public void sendCmd(string cmd)
        {
            byte[] data;
            if (!comport.IsOpen)
            {
                OpenPort();
            }
            switch (cmd)
            {
                case "Send Relay Switches": // 0

                    data = CreateTxArray(0, RelaySwitches.RelaySW);
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    comport.Write(data, 0, 3);
                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Send Control Switches"://1
                    data = CreateTxArray(1, ControlSwitches.ControlSw);
                    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    try
                    {
                        comport.Write(data, 0, 3);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(Convert.ToString(e));
                    }

                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Update Date and Time": // 2
                    //TODO: complete date/ time and clean up code
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
                    timeData[8] = Convert.ToByte(checkSum);
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
                                                  // TODO: clean up code for Set Cross Axis Parameters
                                                  /*      aCrossPeriod = Convert.ToSingle(0.000075); //ConfigData.crossPeriod;
                                                        aCrossDampFactor = Convert.ToSingle(.82); // ConfigData.crossDampFactor;
                                                        aCrossGain = Convert.ToSingle(6.5);//ConfigData.crossGain;
                                                        aCrossLead = Convert.ToSingle(.1);//ConfigData.crossLead;*/
                    crossCouplingFactor13 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[13]);
                    analogFilter5 = System.Convert.ToSingle(ConfigData.analogFilter[5]);

                    // data = CreateTxArray(4, ConfigData.crossPeriod, ConfigData.crossDampFactor, ConfigData.crossGain, ConfigData.crossLead, ConfigData.crossCouplingFactors[13], ConfigData.analogFilter[5]);
                    data = CreateCrossAxisParametersArray(4, ConfigData.crossPeriod, ConfigData.crossDampFactor, ConfigData.crossGain, ConfigData.crossLead, ConfigData.crossCouplingFactors[13], ConfigData.analogFilter[5]);

                    //     data = CreateCrossAxisParametersArray(0x04, 1.91e-5, .212, .15, .5, -8.99998e-4, 1.0);
                    /*
                                        data[0] = 0x04;// Command
                                        data[1] = 0xBA;// crossPeriod
                                        data[2] = 0xED;// crossPeriod
                                        data[3] = 0x0C;// crossPeriod
                                        data[4] = 0x37;// crossPeriod
                                        data[5] = 0x5A;
                                        data[6] = 0x64;
                                        data[7] = 0xBB;
                                        data[8] = 0x3D;
                                        data[9] = 0xCD;
                                        data[10] = 0xCC;
                                        data[11] = 0x4C;
                                        data[12] = 0x3E;
                                        data[13] = 0x66;
                                        data[14] = 0x66;
                                        data[15] = 0xE6;
                                        data[16] = 0x3E;
                                        data[17] = 0x00;
                                        data[18] = 0x00;
                                        data[19] = 0x00;
                                        data[20] = 0x00;
                                        data[21] = 0x00;
                                        data[22] = 0x00;
                                        data[23] = 0x80;
                                        data[24] = 0x3F;
                                        data[25] = 0xC4;// checksum
                    */
                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);

                    //      data = CreateCrossAxisParametersArray(0x04, ConfigData.crossPeriod, ConfigData.crossDampFactor, ConfigData.crossGain, ConfigData.crossLead, ConfigData.crossCouplingFactors[13], ConfigData.analogFilter[5]);
                    comport.Write(data, 0, 26);
                    // transmit command

                    break;

                case "Set Long Axis Parameters": //  5
                                                 // TODO: clean up code for  Set Long Axis Parameters
                                                 //  data = CreateCrossAxisParametersArray(0x05, 1.91e-5, .213, .15, .5, -2e-3, 1.0);
                    data = CreateCrossAxisParametersArray(5, ConfigData.longPeriod, ConfigData.longDampFactor, ConfigData.longGain, ConfigData.longLead, ConfigData.crossCouplingFactors[14], ConfigData.analogFilter[6]);

                    data[0] = 0x05;
                    data[1] = 0xBA;
                    data[2] = 0xED;
                    data[3] = 0x0C;
                    data[4] = 0x37;
                    data[5] = 0x5A;
                    data[6] = 0x64;
                    data[7] = 0xBB;
                    data[8] = 0x3D;
                    data[9] = 0xCD;
                    data[10] = 0xCC;
                    data[11] = 0x4C;
                    data[12] = 0x3E;
                    data[13] = 0x66;
                    data[14] = 0x66;
                    data[15] = 0xE6;
                    data[16] = 0x3E;
                    data[17] = 0x00;
                    data[18] = 0x00;
                    data[19] = 0x00;
                    data[20] = 0x00;
                    data[21] = 0x00;
                    data[22] = 0x00;
                    data[23] = 0x80;
                    data[24] = 0x3F;
                    data[25] = 0xC5;

                    SerialPortForm.textBox24.Text = ByteArrayToHexString(data);

                    //       data = CreateCrossAxisParametersArray(0x04, ConfigData.longPeriod, ConfigData.longDampFactor, ConfigData.longGain, ConfigData.longLead, ConfigData.crossCouplingFactors[14], ConfigData.analogFilter[6]);

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
                                                      // TODO: clean up code for     Update Cross Coupling Values

                    //          data = CreateTxArray(8, System.Convert.ToSingle(ConfigData.analogFilter[1]), System.Convert.ToSingle(ConfigData.analogFilter[2]), System.Convert.ToSingle(ConfigData.analogFilter[4]), System.Convert.ToSingle(ConfigData.analogFilter[3]), crossCouplingFactor14, ConfigData.springTensionMax);

                    data = CreateCrossAxisParametersArray(0x08, ConfigData.analogFilter[1], ConfigData.analogFilter[2], ConfigData.analogFilter[4], ConfigData.analogFilter[3], ConfigData.crossCouplingFactors[14], ConfigData.springTensionMax);
                    //    data = CreateCrossAxisParametersArray(0x08, .219, .2185, .247, .19, -1.046, 7000);

                    //      data = CreateTxArray(8, System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), System.Convert.ToSingle(.2), crossCouplingFactor14, ConfigData.springTensionMax);

                    //   Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");

                    data[0] = 0x08;
                    data[1] = 0x3F;
                    data[2] = 0x35;
                    data[3] = 0x5E;
                    data[4] = 0x3E;
                    data[5] = 0x68;
                    data[6] = 0x91;
                    data[7] = 0x2D;
                    data[8] = 0x3E;
                    data[9] = 0x14;
                    data[10] = 0xAE;
                    data[11] = 0x47;
                    data[12] = 0x3E;
                    data[13] = 0xA4;
                    data[14] = 0x70;
                    data[15] = 0x3D;
                    data[16] = 0x3E;
                    data[17] = 0x00;
                    data[18] = 0x00;
                    data[19] = 0x00;
                    data[20] = 0xC0;
                    data[21] = 0x00;
                    data[22] = 0x40;
                    data[23] = 0x96;
                    data[24] = 0x46;
                    data[25] = 0xC6;

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
            /*   aCrossPeriod = ConfigData.crossPeriod;// aCrossPeriod = perX
               aLongPeriod = ConfigData.longPeriod;// aLongPeriod = perL
               aCrossDampFactor = ConfigData.crossDampFactor;// aCrossDampFactor = dampX
               aLongDampFactor = ConfigData.longDampFactor;// aLongDampFactor = dampL*/
            crossCouplingFactor13 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[13]);
            crossCouplingFactor14 = System.Convert.ToSingle(ConfigData.crossCouplingFactors[14]);
            analogFilter5 = System.Convert.ToSingle(ConfigData.analogFilter[5]);
            analogFilter6 = System.Convert.ToSingle(ConfigData.analogFilter[6]);

            // turn on 200 Hz
            // RelaySwitches.relay200Hz = enable;// set bit 0  high to turn on 200 Hz

            RelaySwitches.Slew4(disable);
            RelaySwitches.Slew5(disable);
            RelaySwitches.StepperMotorEnable(enable);

            RelaySwitches.RelaySW = 0x80;            // cmd 0
            sendCmd("Send Relay Switches");          // 0 ----
            sendCmd("Set Cross Axis Parameters");    // download platform parameters 4 -----
            sendCmd("Set Long Axis Parameters");     // download platform parametersv 5 -----
            sendCmd("Update Cross Coupling Values"); // download CC parameters 8     -----

            ControlSwitches.ControlSw = 0x08;       // ControlSwitches.RelayControlSW = 0x08;

            sendCmd("Send Control Switches");       // 1 ----
            sendCmd("Send Control Switches");       // 1 ----

            RelaySwitches.RelaySW = 0xB1;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----
            ControlSwitches.ControlSw = 0x08;       //ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");       // 1 ----

            RelaySwitches.RelaySW = 0x81;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----

            ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");       // 1 ----
            RelaySwitches.RelaySW = 0x80;           // cmd 0
            sendCmd("Send Relay Switches");         // 0 ----

            // wait for platform to level
            //  sendCmd("Update Gyro Bias Offset");        // 10
        }

        // TODO StoreDefaultVariables
        public void StoreDefaultVariables()
        {
            // Load configuration data from stored defaults
            Properties.Settings.Default.chartWindowTimeSpan = chartWindowTimeSpan;
            Properties.Settings.Default.chartWindowTimePeriod = chartWindowTimePeriod;

            Properties.Settings.Default.springTension = Data.SpringTension;
            Properties.Settings.Default.beamScale = (short)ConfigData.beamScale;
            Properties.Settings.Default.meterNumber = ConfigData.meterNumber;
            Properties.Settings.Default.crossPeriod = (short)ConfigData.crossPeriod;
            Properties.Settings.Default.longPeriod = (short)ConfigData.longPeriod;
            Properties.Settings.Default.crossDampFactor = (short)ConfigData.crossDampFactor;
            Properties.Settings.Default.longDampFactor = (short)ConfigData.longDampFactor;
            Properties.Settings.Default.crossGain = (short)ConfigData.crossGain;
            Properties.Settings.Default.longGain = (short)ConfigData.longGain;
            Properties.Settings.Default.crossLead = (short)ConfigData.crossLead;
            Properties.Settings.Default.longLead = (short)ConfigData.longLead;
            Properties.Settings.Default.springTensionMax = (short)ConfigData.springTensionMax;

            Properties.Settings.Default.crossBias = (short)ConfigData.crossBias;
            Properties.Settings.Default.longBias = (short)ConfigData.longBias;
            Properties.Settings.Default.crossCompFactor_4 = (short)ConfigData.crossCompFactor_4;
            Properties.Settings.Default.crossCompPhase_4 = (short)ConfigData.crossCompPhase_4;
            Properties.Settings.Default.crossCompFactor_16 = (short)ConfigData.crossCompFactor_16;
            Properties.Settings.Default.crossCompPhase_16 = (short)ConfigData.crossCompPhase_16;
            Properties.Settings.Default.longCompFactor_4 = (short)ConfigData.longCompFactor_4;
            Properties.Settings.Default.longCompPhase_4 = (short)ConfigData.longCompPhase_4;
            Properties.Settings.Default.longCompFactor_16 = (short)ConfigData.longCompFactor_16;
            Properties.Settings.Default.longCompPhase_16 = (short)ConfigData.longCompPhase_16;

            Properties.Settings.Default.CML_Fact = (short)ConfigData.CML_Fact;
            Properties.Settings.Default.AL_Fact = ConfigData.AL_Fact;
            Properties.Settings.Default.AX_Fact = ConfigData.AX_Fact;
            Properties.Settings.Default.VE_Fact = (short)ConfigData.VE_Fact;
            Properties.Settings.Default.CMX_Fact = (short)ConfigData.CMX_Fact;
            Properties.Settings.Default.XACC2_Fact = (short)ConfigData.XACC2_Fact;
            Properties.Settings.Default.LACC2_Fact = (short)ConfigData.LACC2_Fact;
            Properties.Settings.Default.XACC2_Fact = (short)ConfigData.XACC_Phase;
            Properties.Settings.Default.LAXX_AL_Phase = (short)ConfigData.LACC_AL_Phase;
            Properties.Settings.Default.LACC_CMX_Phase = (short)ConfigData.LACC_CML_Phase;
            Properties.Settings.Default.LACC_CMX_Phase = (short)ConfigData.LACC_CMX_Phase;
            Properties.Settings.Default.kFactor = (short)ConfigData.kFactor;
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
            frmTerminal frmTerminal = new frmTerminal();
            comport.PortName = Properties.Settings.Default.PortName;
            comport.BaudRate = Properties.Settings.Default.BaudRate;
            comport.StopBits = Properties.Settings.Default.StopBits;
            chartWindowTimeSpan = Properties.Settings.Default.chartWindowTimeSpan;
            chartWindowTimePeriod = Properties.Settings.Default.chartWindowTimePeriod;
            // Load configuration data from stored defaults
            ConfigData.version = Properties.Settings.Default.version;
            Data.SpringTension = Properties.Settings.Default.springTension;
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
            //   ConfigData.crossCompPhase_4 = Properties.Settings.Default.crossCompPhase_4;
            ConfigData.crossCompFactor_16 = Properties.Settings.Default.crossCompFactor_16;
            //   ConfigData.crossCompPhase_16 = Properties.Settings.Default.crossCompPhase_16;
            ConfigData.longCompFactor_4 = Properties.Settings.Default.longCompFactor_4;
            //   ConfigData.longCompPhase_4 = Properties.Settings.Default.longCompPhase_4;
            ConfigData.longCompFactor_16 = Properties.Settings.Default.longCompFactor_16;
            //   ConfigData.longCompPhase_16 = Properties.Settings.Default.longCompPhase_16;

            ConfigData.CML_Fact = Properties.Settings.Default.CML_Fact;
            ConfigData.AL_Fact = (Single)Properties.Settings.Default.AL_Fact;
            ConfigData.AX_Fact = (Single)Properties.Settings.Default.AX_Fact;
            ConfigData.VE_Fact = Properties.Settings.Default.VE_Fact;
            ConfigData.CMX_Fact = Properties.Settings.Default.CMX_Fact;
            ConfigData.XACC2_Fact = Properties.Settings.Default.XACC2_Fact;
            ConfigData.LACC2_Fact = Properties.Settings.Default.LACC2_Fact;
            ConfigData.XACC_Phase = Properties.Settings.Default.XACC2_Fact;
            ConfigData.LACC_AL_Phase = Properties.Settings.Default.LAXX_AL_Phase;
            ConfigData.LACC_CML_Phase = Properties.Settings.Default.LACC_CMX_Phase;
            ConfigData.LACC_CMX_Phase = Properties.Settings.Default.LACC_CMX_Phase;
            ConfigData.kFactor = (Single)Properties.Settings.Default.kFactor;
            ConfigData.gyroType = Properties.Settings.Default.gyroType;

            ConfigData.beamScale = Properties.Settings.Default.beamScale;
            ConfigData.crossBias = Properties.Settings.Default.crossBias;
            ConfigData.crossCouplingFactors[0] = Properties.Settings.Default.crossCouplingFactors0;
            ConfigData.crossCouplingFactors[1] = Properties.Settings.Default.crossCouplingFactors1;
            ConfigData.crossCouplingFactors[2] = Properties.Settings.Default.crossCouplingFactors2;
            ConfigData.crossCouplingFactors[3] = Properties.Settings.Default.crossCouplingFactors3;
            ConfigData.crossCouplingFactors[4] = Properties.Settings.Default.crossCouplingFactors4;
            ConfigData.crossCouplingFactors[5] = Properties.Settings.Default.crossCouplingFactors5;
            ConfigData.crossCouplingFactors[6] = Properties.Settings.Default.crossCouplingFactors6_VCC;
            ConfigData.crossCouplingFactors[7] = Properties.Settings.Default.crossCouplingFactors7_AL;
            ConfigData.crossCouplingFactors[8] = Properties.Settings.Default.crossCouplingFactors8_AX;
            ConfigData.crossCouplingFactors[9] = Properties.Settings.Default.crossCouplingFactors9_VE;
            ConfigData.crossCouplingFactors[10] = Properties.Settings.Default.crossCouplingFactors10_AX2;
            ConfigData.crossCouplingFactors[11] = Properties.Settings.Default.crossCouplingFactors11_XACC2;
            ConfigData.crossCouplingFactors[12] = Properties.Settings.Default.crossCouplingFactors12_LACC2;
            ConfigData.crossCouplingFactors[13] = Properties.Settings.Default.crossCouplingFactors13_CrossAxisCompensation4;
            ConfigData.crossCouplingFactors[14] = Properties.Settings.Default.crossCouplingFactors14_LongAxisCompensation4;
            ConfigData.crossCouplingFactors[15] = Properties.Settings.Default.crossCouplingFactors15_CrossAxisCompensation16;
            ConfigData.crossCouplingFactors[16] = Properties.Settings.Default.crossCouplingFactors16_LongAxisCompensation16;
            ConfigData.crossDampFactor = Properties.Settings.Default.crossDampFactor;
            ConfigData.crossGain = Properties.Settings.Default.crossGain;
            ConfigData.crossLead = Properties.Settings.Default.crossLead;
            ConfigData.crossPeriod = Properties.Settings.Default.crossPeriod;
            ConfigData.digitalInputSwitch = 0;// not needed
            ConfigData.engPassword = "zls";
            ConfigData.fileNameSwitch = 1;// not needed
            ConfigData.hardDiskSwitch = 1;// not needed
            ConfigData.iAuxGain = (short)0.0;// not needed
            ConfigData.linePrinterSwitch = 0;// not needed
            ConfigData.longBias = Properties.Settings.Default.longBias;
            ConfigData.longDampFactor = Properties.Settings.Default.longDampFactor;
            ConfigData.longGain = Properties.Settings.Default.longGain;
            ConfigData.longLead = Properties.Settings.Default.longLead;
            ConfigData.longPeriod = Properties.Settings.Default.longPeriod;
            ConfigData.meterNumber = Properties.Settings.Default.meterNumber;
            ConfigData.modeSwitch = 1;// hires or marine??
            ConfigData.monitorDisplaySwitch = 2;// not needed
            ConfigData.numAuxChan = 0;// not needed
            ConfigData.printerEmulationSwitch = 3;// not needed
            ConfigData.serialPortOutputSwitch = -1;// check on function
            ConfigData.serialPortSwitch = 1;// check on function - should save default port for meter and data output
            ConfigData.springTensionMax = Properties.Settings.Default.springTensionMax;
            ConfigData.alarmSwitch = 0;// check on function
            ConfigData.analogFilter[0] = (Single)Properties.Settings.Default.analogFilter0;
            ConfigData.analogFilter[1] = (Single)Properties.Settings.Default.analogFilter1_AXPhase;
            ConfigData.analogFilter[2] = (Single)Properties.Settings.Default.analogFilter2_ALPhase;
            ConfigData.analogFilter[3] = (Single)Properties.Settings.Default.analogFilter3;// N/A
            ConfigData.analogFilter[4] = (Single)Properties.Settings.Default.analogFilter4_VCCPhase;
            ConfigData.analogFilter[5] = (Single)Properties.Settings.Default.analogFilter5_CrossAxisCompensationPhase4;
            ConfigData.analogFilter[6] = (Single)Properties.Settings.Default.analogFilter6_LongAxisCompensationPhase4;
            ConfigData.analogFilter[7] = (Single)Properties.Settings.Default.analogFilter8_LongAxisCompensationPhase16;
            ConfigData.analogFilter[8] = (Single)Properties.Settings.Default.analogFilter8_LongAxisCompensationPhase16;

            if (engineerDebug)
            {
                Console.WriteLine("Beam scale: " + ConfigData.beamScale);
                Console.WriteLine("Meter number: " + ConfigData.meterNumber);
                Console.WriteLine("Cross period: " + Properties.Settings.Default.crossPeriod);
                Console.WriteLine("Cross damping: " + Properties.Settings.Default.crossDampFactor);
                Console.WriteLine("Cross gain: " + Properties.Settings.Default.crossGain);
                Console.WriteLine("Cross lead: " + Properties.Settings.Default.crossLead);
                Console.WriteLine("Cross bias: " + Properties.Settings.Default.crossBias);
                Console.WriteLine("Long period: " + Properties.Settings.Default.longPeriod);
                Console.WriteLine("Long damping: " + Properties.Settings.Default.longDampFactor);
                Console.WriteLine("Long gain: " + Properties.Settings.Default.longGain);
                Console.WriteLine("Long lead: " + Properties.Settings.Default.longLead);
                Console.WriteLine("Long bias: " + Properties.Settings.Default.longBias);
                Console.WriteLine("Max spring tension: " + Properties.Settings.Default.springTensionMax);
                Console.WriteLine("Mode: " + Properties.Settings.Default.DataMode);
                Console.WriteLine("crossCouplingFactors0: " + ConfigData.crossCouplingFactors[0]);
                Console.WriteLine("crossCouplingFactors1: " + ConfigData.crossCouplingFactors[1]);
                Console.WriteLine("crossCouplingFactors2: " + ConfigData.crossCouplingFactors[2]);
                Console.WriteLine("crossCouplingFactors3: " + ConfigData.crossCouplingFactors[3]);
                Console.WriteLine("crossCouplingFactors4: " + ConfigData.crossCouplingFactors[4]);
                Console.WriteLine("crossCouplingFactors5: " + ConfigData.crossCouplingFactors[5]);
                Console.WriteLine("crossCouplingFactors6_VCC: " + ConfigData.crossCouplingFactors[6]);
                Console.WriteLine("crossCouplingFactors7_AL: " + ConfigData.crossCouplingFactors[7]);
                Console.WriteLine("crossCouplingFactors8_AX: " + ConfigData.crossCouplingFactors[8]);
                Console.WriteLine("crossCouplingFactors9_VE: " + ConfigData.crossCouplingFactors[9]);
                Console.WriteLine("crossCouplingFactors10_AX2: " + ConfigData.crossCouplingFactors[10]);
                Console.WriteLine("crossCouplingFactors11_XACC2: " + ConfigData.crossCouplingFactors[11]);
                Console.WriteLine("crossCouplingFactors12_LACC2: " + ConfigData.crossCouplingFactors[12]);
                Console.WriteLine("crossCouplingFactors13_CrossAxisCompensation4: " + ConfigData.crossCouplingFactors[13]);
                Console.WriteLine("crossCouplingFactors14_LongAxisCompensation4: " + ConfigData.crossCouplingFactors[14]);
                Console.WriteLine("crossCouplingFactors15_CrossAxisCompensation16: " + ConfigData.crossCouplingFactors[15]);
                Console.WriteLine("crossCouplingFactors16_LongAxisCompensation16: " + ConfigData.crossCouplingFactors[16]);

                Console.WriteLine(ConfigData.analogFilter[0]);
                Console.WriteLine(ConfigData.analogFilter[1]);
                Console.WriteLine(ConfigData.analogFilter[2]);
                Console.WriteLine(ConfigData.analogFilter[3]);
                Console.WriteLine(ConfigData.analogFilter[4]);
                Console.WriteLine(ConfigData.analogFilter[5]);
                Console.WriteLine(ConfigData.analogFilter[6]);
                Console.WriteLine(ConfigData.analogFilter[7]);
                Console.WriteLine(ConfigData.analogFilter[8]);
            }

            //  public static double[] crossCouplingFactors = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.9e-2, 2.499999e-3, -1.681e-2, 0.0, 0.0, 0.0, 0.0, -8.9999998e-4, -3.7e-3, 1.0, 1.0 };

            // Load program defaults

            dataAquisitionMode = Properties.Settings.Default.dataAquisitionMode;
            configFilePath = Properties.Settings.Default.configFilePath;
            configFileName = Properties.Settings.Default.configFileName;
            calFilePath = Properties.Settings.Default.calFilePath;
            calFileName = Properties.Settings.Default.calFileName;
            dataFilePath = Properties.Settings.Default.dataFilePath;// file path for data logs
            fileType = Properties.Settings.Default.fileType;// file type for data logs csv, txt etc
            dataFileName = Properties.Settings.Default.dataFileName;// stored file name i.e. survey name
            surveyName = dataFileName;
            surveyTextBox.Text = surveyName;
            Data.SpringTension = Properties.Settings.Default.springTension;
            frmTerminal.fileDateFormat = Properties.Settings.Default.fileDateFormat;

            ControlSwitches.DataCollection(enable);// ControlSwitches.dataSwitch = enable;// ICNTLSW = 8; // data on

            double aCrossPeriod = ConfigData.crossPeriod; ;
            double aCrossDampFactor = ConfigData.crossDampFactor;

            double aLongPeriod = ConfigData.longPeriod;
            double aLongDampFactor = ConfigData.longDampFactor;

            // windowSizeNumericUpDown.Maximum = Properties.Settings.Default.chartWindowTimeSpan;
            // comboBox1.SelectedValue = Properties.Settings.Default.chartWindowTimePeriod;

            chartWindowTimeSpan = Properties.Settings.Default.chartWindowTimeSpan;
            chartWindowTimePeriod = Properties.Settings.Default.chartWindowTimePeriod;
            comboBox1.SelectedValue = chartWindowTimePeriod;
            if (chartWindowTimePeriod == "minutes")
            {
                comboBox1.SelectedItem = "minutes";
                windowSizeNumericUpDown.Maximum = 10;
            }
            else
            {
                comboBox1.SelectedItem = "seconds";
                windowSizeNumericUpDown.Maximum = 60;
            }
            windowSizeNumericUpDown.Value = chartWindowTimeSpan;

            frmTerminal.Dispose();
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
            RelaySwitches.StepperMotorEnable(enable);

            //    RelaySwitches.RelaySwitchCalculate();// 0x80
            RelaySwitches.RelaySW = 0x80;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
            sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
            sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
            sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----

            ControlSwitches.ControlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;

            sendCmd("Send Control Switches");           // 1 ----
            sendCmd("Send Control Switches");           // 1 ----
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0xB1;// cmd 0

            sendCmd("Send Relay Switches");           // 0 ----
            ControlSwitches.ControlSw = 0x08; //ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");           // 1 ----
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ControlSwitches.ControlSw = 0x08;// ControlSwitches.RelayControlSW = 0x08;
            sendCmd("Send Control Switches");           // 1 ----
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0x80;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0x83;// cmd 0
            sendCmd("Send Relay Switches");
            // 0 ----
            ControlSwitches.TorqueMotor(enable);
            ControlSwitches.Alarm(enable);
            // ControlSwitches.controlSw = 0x09; // ControlSwitches.RelayControlSW = 0x09;
            sendCmd("Send Control Switches");           // 1 ----
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RelaySwitches.RelaySW = 0x81;// cmd 0
            sendCmd("Send Relay Switches");           // 0 ----
            ControlSwitches.ControlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
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
                Environment.Exit(Environment.ExitCode);
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(Environment.ExitCode);
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
            PasswordForm.Dispose();
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
            PasswordForm.Dispose();
        }

        private void dataStatusFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDataForm.Show();
        }

        private void serialPortFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortForm.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Show the user the about dialog
            (new FormAbout()).ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timePeriod = comboBox1.SelectedItem.ToString();
            chartWindowTimePeriod = timePeriod;
            windowSizeNumericUpDown.Minimum = 1;
            if (timePeriod == "seconds")
            {
                windowSizeNumericUpDown.Maximum = 60;
            }
            else
            {
                windowSizeNumericUpDown.Maximum = 10;
            }

            Console.WriteLine(timePeriod);
            //  (string timePeriod, int timeValue)
        }

        private void setDateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTimeForm myDateForm = new DateTimeForm();
            myDateForm.Show();
            myDateForm.Dispose();
        }

        private void fileFormatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FileFormatForm myFileForm = new FileFormatForm();
            myFileForm.Show();
            myFileForm.Dispose();
        }

        private void recordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordingForm RecordingForm = new RecordingForm();
            RecordingForm.Show();
            RecordingForm.Dispose();
        }

        #region File Class

        [DelimitedRecord(",")]
        public class ConfigFileData
        {
            public string EntryName;
            public string configValue;
        }

        private void CheckConfigFile(string configFile)
        {
            var fileExists = File.Exists(configFile);// || File.Exists(Path.Combine(Directory.GetParent(Path.GetDirectoryName(configFile)).FullName, Path.GetFileName(configFile)));

            if (engineerDebug)
            {
                Console.WriteLine("Config file name " + Path.GetFileName(configFile));
                Console.WriteLine("Config file path " + Path.GetDirectoryName(configFile));
            }
            DialogResult result = DialogResult.No;

            if (fileExists == false)
            {
                string message = "Config file not found!  Load default values from last session?";
                string title = "File not found";

                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                result = MessageBox.Show(message, title, buttons);

                if (result == DialogResult.Yes)

                {
                    // Defaults are already loaded.  Do nothing
                    //  this.Close();
                }
                else
                {
                    //Open file dialog then read file
                    OpenFileDialog OpenFileDialog = new OpenFileDialog();
                    OpenFileDialog.InitialDirectory = Properties.Settings.Default.configFilePath;
                    OpenFileDialog.Filter = "Comma delimited Files (.csv)|*.csv|All Files (*.*)|*.*";
                    OpenFileDialog.FilterIndex = 1;
                    OpenFileDialog.RestoreDirectory = true;
                    OpenFileDialog.Multiselect = true;
                    OpenFileDialog.ShowDialog();
                    // save path and file name to defaults
                    Properties.Settings.Default.configFilePath = Path.GetDirectoryName(OpenFileDialog.FileName);
                    Properties.Settings.Default.configFileName = Path.GetFileName(OpenFileDialog.FileName);
                    Properties.Settings.Default.Save();
                    configFile = OpenFileDialog.FileName;
                    ReadConfigFile(configFile);// Load config file selected
                }
            }
            else
            {
                ReadConfigFile(configFile);
            }
        }

        public void ReadConfigFile(string configFile)
        {
            var engine = new FileHelperAsyncEngine<ConfigFileData>();
            UserDataForm.configurationFileTextBox.Text = configFile;
            ConfigData ConfigData = new ConfigData();

            var fileName = @configFile;// Double check the selected file exists
            var fileExists =
                File.Exists(fileName) ||
                File.Exists(
                    Path.Combine(
                        Directory.GetParent(Path.GetDirectoryName(fileName)).FullName,
                        Path.GetFileName(fileName)
                    )
                );

            if (fileExists)// Load the data
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
                                    if (engineerDebug) Console.WriteLine("Version number: ---------------------- \t" + Convert.ToString(dataItem.configValue));
                                    break;

                                case "Meter Number":
                                    ConfigData.meterNumber = dataItem.configValue.Trim();
                                    if (engineerDebug) Console.WriteLine("Meter number is---------------------- \t" + ConfigData.meterNumber);
                                    break;

                                case "Beam Scale Factor":
                                    ConfigData.beamScale = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("BEAM SCALE FACTOR-------------------- \t{0:n6}", ConfigData.beamScale);
                                    break;

                                case "Cross Axis Period":
                                    ConfigData.crossPeriod = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("CROSS-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.crossPeriod);
                                    break;

                                case "Long Axis Period":
                                    ConfigData.longPeriod = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("LONG-AXIS PERIOD-------------------- \t{0:e4}", ConfigData.longPeriod);
                                    break;

                                case "Cross Axis Damping Factor":
                                    ConfigData.crossDampFactor = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("CROSS-AXIS DAMPING------------------- \t{0:e4}", ConfigData.crossDampFactor);
                                    break;

                                case "Long Axis Damping Factor":
                                    ConfigData.longDampFactor = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("LONG-AXIS DAMPING------------------- \t{0:e4}", ConfigData.longDampFactor);
                                    break;

                                case "Cross Axis Gain":
                                    ConfigData.crossGain = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("CROSS-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.crossGain));
                                    break;

                                case "Long Axis Gain":
                                    ConfigData.longGain = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("LONG-AXIS GAIN---------------------- \t" + Convert.ToString(ConfigData.longGain));
                                    break;

                                case "Cross Axis Lead":
                                    ConfigData.crossLead = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("CROSS-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.crossLead));
                                    break;

                                case "Long Axis Lead":
                                    ConfigData.longLead = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("LONG-AXIS LEAD---------------------- \t" + Convert.ToString(ConfigData.longLead));
                                    break;

                                case "Max Spring Tension":
                                    ConfigData.springTensionMax = Convert.ToInt32(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("MAXIMUM SPRING TENSION VALUE--------- \t" + Convert.ToString(ConfigData.springTensionMax));
                                    break;

                                case "Cross Axis Bias":
                                    ConfigData.crossBias = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("CROSS-AXIS BIAS---------------------- \t" + Convert.ToString(ConfigData.crossBias));
                                    break;

                                case "Long Axis Bias":
                                    ConfigData.longBias = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("LONG-AXIS BIAS----------------------- \t" + Convert.ToString(ConfigData.longBias));
                                    break;

                                case "crossCouplingFactors0": // Not used
                                    ConfigData.crossCouplingFactors[0] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[0]------------- \t{0:e4}", ConfigData.crossCouplingFactors[0]);
                                    break;

                                case "crossCouplingFactors1": // Not used
                                    ConfigData.crossCouplingFactors[1] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[1]------------- \t{0:e4}", ConfigData.crossCouplingFactors[1]);
                                    break;

                                case "crossCouplingFactors2": // Not used
                                    ConfigData.crossCouplingFactors[2] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[2]------------- \t{0:e4}", ConfigData.crossCouplingFactors[2]);
                                    break;

                                case "crossCouplingFactors3": // Not used
                                    ConfigData.crossCouplingFactors[3] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[3]------------- \t{0:e4}", ConfigData.crossCouplingFactors[3]);
                                    break;

                                case "crossCouplingFactors4": // Not used
                                    ConfigData.crossCouplingFactors[4] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[4]------------- \t{0:e4}", ConfigData.crossCouplingFactors[4]);
                                    break;

                                case "crossCouplingFactors5": // Not used
                                    ConfigData.crossCouplingFactors[5] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[5]------------- \t{0:e4}", ConfigData.crossCouplingFactors[5]);
                                    break;

                                case "crossCouplingFactors6-VCC": // VCC
                                    ConfigData.crossCouplingFactors[6] = Convert.ToSingle(dataItem.configValue);
                                    Data.VCC = ConfigData.crossCouplingFactors[6];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[6]/VCC------------- \t{0:e4}", ConfigData.crossCouplingFactors[6]);
                                    break;

                                case "crossCouplingFactors7-AL": // AL
                                    ConfigData.crossCouplingFactors[7] = Convert.ToSingle(dataItem.configValue);
                                    Data.AL = ConfigData.crossCouplingFactors[7];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[7]/AL------------- \t{0:e4}", ConfigData.crossCouplingFactors[7]);
                                    break;

                                case "crossCouplingFactors8-AX": // AX
                                    ConfigData.crossCouplingFactors[8] = Convert.ToSingle(dataItem.configValue);
                                    Data.AX = ConfigData.crossCouplingFactors[8];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[8]/AX------------- \t{0:e4}", ConfigData.crossCouplingFactors[8]);
                                    break;

                                case "crossCouplingFactors9-VE": // VE
                                    ConfigData.crossCouplingFactors[9] = Convert.ToSingle(dataItem.configValue);
                                    Data.VE = ConfigData.crossCouplingFactors[9];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[9]/VE------------- \t{0:e4}", ConfigData.crossCouplingFactors[9]);
                                    break;

                                case "crossCouplingFactors10-AX2": // AX2
                                    ConfigData.crossCouplingFactors[10] = Convert.ToSingle(dataItem.configValue);
                                    Data.AX2 = ConfigData.crossCouplingFactors[10];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[10]/AX2------------- \t{0:e4}", ConfigData.crossCouplingFactors[10]);
                                    break;

                                case "crossCouplingFactors11-XACC2": // XACC2
                                    ConfigData.crossCouplingFactors[11] = Convert.ToSingle(dataItem.configValue);
                                    Data.XACC2 = ConfigData.crossCouplingFactors[11];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[11]/XACC2------------- \t{0:e4}", ConfigData.crossCouplingFactors[11]);
                                    break;

                                case "crossCouplingFactors12-LACC2": // LACC2
                                    ConfigData.crossCouplingFactors[12] = Convert.ToSingle(dataItem.configValue);
                                    Data.LACC2 = ConfigData.crossCouplingFactors[12];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[12]/LACC2------------- \t{0:e4}", ConfigData.crossCouplingFactors[12]);
                                    break;

                                case "crossCouplingFactors13-Cross Axis Compensation(4)": // Cross Axis Compensation (4)
                                    ConfigData.crossCouplingFactors[13] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompFactor_4 = ConfigData.crossCouplingFactors[13];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[13]/Cross Axis Comp(4)------------- \t{0:e4}", ConfigData.crossCouplingFactors[13]);
                                    break;

                                case "crossCouplingFactors14-Long Axis Compensation(4)": // Long Axis Compensation (4)
                                    ConfigData.crossCouplingFactors[14] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompFactor_4 = ConfigData.crossCouplingFactors[14];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[14]/Long Axis Comp(4)------------- \t{0:e4}", ConfigData.crossCouplingFactors[14]);
                                    break;

                                case "crossCouplingFactors15-Cross Axis Compensation(16)": // Cross Axis Compensation (16)
                                    ConfigData.crossCouplingFactors[15] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompFactor_16 = ConfigData.crossCouplingFactors[15];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[15]/Cross Axis Comp(16)------------- \t{0:e4}", ConfigData.crossCouplingFactors[15]);
                                    break;

                                case "crossCouplingFactors16-Long Axis Compensation(16)": // Long Axis Compensation (16)
                                    ConfigData.crossCouplingFactors[16] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompFactor_16 = ConfigData.crossCouplingFactors[16];
                                    if (engineerDebug) Console.WriteLine("Cross Coupling Factors[16]/Long Axis Comp(16)------------- \t{0:e4}", ConfigData.crossCouplingFactors[16]);
                                    break;

                                case "analogFilter1-AX Phase":// AX Phase
                                    ConfigData.analogFilter[1] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.XACC_Phase = ConfigData.analogFilter[1];
                                    if (engineerDebug) Console.WriteLine("AFILT[1]/AX PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[1]));
                                    break;

                                case "analogFilter2-AL Phase"://  AL Phase
                                    ConfigData.analogFilter[2] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.LACC_AL_Phase = ConfigData.analogFilter[2];
                                    if (engineerDebug) Console.WriteLine("AFILT[2]/AL PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[2]));
                                    break;

                                case "analogFilter3":
                                    ConfigData.analogFilter[3] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("AFILT[3]------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[3]));
                                    break;

                                case "analogFilter4-VCC Phase":// VCC Phase
                                    ConfigData.analogFilter[4] = Convert.ToSingle(dataItem.configValue);
                                    if (engineerDebug) Console.WriteLine("AFILT[3]/VCC PHASE------------------------------- \t" + Convert.ToString(ConfigData.analogFilter[4]));
                                    break;

                                case "analogFilter5-Cross Axis Compensation Phase(4)":
                                    ConfigData.analogFilter[5] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompPhase_4 = ConfigData.analogFilter[5];
                                    if (engineerDebug) Console.WriteLine("Analog FIlter [5]/CROSS-AXIS COMPENSATION PHASE (4)------- \t" + Convert.ToString(ConfigData.crossCompPhase_4));
                                    break;

                                case "analogFilter6-Long Axis Compensation Phase(4)":
                                    ConfigData.analogFilter[6] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompPhase_4 = ConfigData.analogFilter[6];
                                    if (engineerDebug) Console.WriteLine("Analog FIlter [6]/LONG AXIS COMPENSATION PHASE (4)-------- \t" + Convert.ToString(ConfigData.longCompPhase_4));
                                    break;

                                case "analogFilter7-Cross Axis Compensation Phase(16)":
                                    ConfigData.analogFilter[7] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.crossCompPhase_16 = ConfigData.analogFilter[7];
                                    if (engineerDebug) Console.WriteLine("Analog FIlter [7]/Cross AXIS COMPENSATION PHASE (16)------ \t" + Convert.ToString(ConfigData.crossCompPhase_16));
                                    break;

                                case "analogFilter8-Long Axis Compensation Phase(16)":
                                    ConfigData.analogFilter[8] = Convert.ToSingle(dataItem.configValue);
                                    ConfigData.longCompPhase_16 = ConfigData.analogFilter[8];
                                    if (engineerDebug) Console.WriteLine("Analog FIlter [8]/LONG AXIS COMPENSATION PHASE (16)------ \t" + Convert.ToString(ConfigData.longCompPhase_16));
                                    break;

                                case "Mode":
                                    dataAquisitionMode = dataItem.configValue.Trim();
                                    if (engineerDebug) Console.WriteLine("Data mode ------------------------------ \t" + Convert.ToString(dataAquisitionMode));
                                    break;

                                default:
                                    // set alert "xxx" entry is not found. check file and try again
                                    //Print to  log file
                                    string message = dataItem.EntryName + "is not a valid property for config file";
                                    string title = "Bad Entry";
                                    MessageBox.Show(message, title);
                                    break;
                            }// switch
                        }// for each
                    }// using
                    UserDataForm.meterNumberTextBox.Text = ConfigData.meterNumber;
                }// try
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    GetConfigFileDialog();
                }
            }
            else
            {
                CheckConfigFile(configFile);// File does not exist. Have user pick a new file.
            }
        }// ReadConfigFile

         /*
                 [DelimitedRecord(",")]
                 public class MarineData
                 {
                     public string lineId;
                     public int Year;
                     public int Days;
                     public int Hour;
                     public int Minute;
                     public int Second;
                     public double Gravity;
                     public double SpringTension;
                     public double CrossCoupling;
                     public double RawBeam;// need to change this to avg beam
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
                     public double longitude;
                     public double latitude;
                     public double altitude;
                 }

                 [DelimitedRecord(",")]
                 public class MarineDataHeader
                 {
                     public string lineId;
                     public string Year;
                     public string Days;
                     public string Hour;
                     public string Minute;
                     public string Second;
                     public string Gravity;
                     public string SpringTension;
                     public string CrossCoupling;
                     public string RawBeam;// need to change this to avg beam
                     public string VCC;
                     public string AL;
                     public string AX;
                     public string VE;
                     public string AX2;
                     public string XACC2;
                     public string LACC2;
                     public string XACC;
                     public string LACC;
                     public string Period;
                     public string longitude;
                     public string latitude;
                     public string altitude;
                 }

                 public void RecordMeterDataToFile()
                 {
                     var engine = new FileHelperEngine<MarineData>();
                     var MarineData = new List<MarineData>();

                     if (newDataFile)
                     {
                         var MarineDataHeader = new List<MarineDataHeader>();
                         MarineDataHeader.Add(new MarineDataHeader()
                         {
                             lineId = "Survey Name",
                             Year = "Year",
                             Days = "Day",
                             Hour = "Hour",
                             Minute = "Minute",
                             Second = "Second",
                             Gravity = "Gravity",
                             SpringTension = "Spring Tension",
                             CrossCoupling = "Coupling",
                             RawBeam = "Avg. Beam",
                             VCC = "VCC",
                             AL = "AL",
                             AX = "AX",
                             VE = "VE",
                             AX2 = "AX2",
                             XACC2 = "XACC2",
                             LACC2 = "LACC2",
                             XACC = "XACC",
                             LACC = "LACC",
                             Period = "Period",
                             longitude = "Longitude",
                             latitude = "Latitude",
                             altitude = "Altitude"
                         });
                         engine.WriteFile("FileOut.csv", MarineData);
                     }

                     MarineData.Add(new MarineData()
                     {
                         lineId = surveyName,
                         Year = Data.year,
                         Days = Data.day,
                         Hour = Data.Hour,
                         Minute = Data.Min,
                         Second = Data.Sec,
                         Gravity = Data.gravity,
                         SpringTension = Data.SpringTension,
                         CrossCoupling = Data.CrossCoupling,
                         RawBeam = Data.beam,// need to change this  to avg beam
                         VCC = Data.VCC,
                         AL = Data.AL,
                         AX = Data.AX,
                         VE = Data.VE,
                         AX2 = Data.AX2,
                         XACC2 = Data.XACC2,
                         LACC2 = Data.LACC2,
                         XACC = Data.XACC,
                         LACC = Data.LACC,
                         Period = 0,// need to add this in calculate marine data
                         longitude = Data.longitude,
                         latitude = Data.latitude,
                         altitude = Data.altitude
                     });

                     engine.WriteFile("FileOut.csv", MarineData);
                 }

                 // This method opens and writes the header to the data log file

           */
    public void CloseDataFile()
        {
            
        }
        // TODO Open new data file
        public void OpenDataFile()
        {
            string thisDate = String.Format("{0:yyyyMMdd-HHmm}", fileStartTime);
            string delimitor = ",";
            fileType = "csv";


            switch (fileType)
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

            string fileFormat = "standard";
            /*
                Format options
                Long marine mode
                Long High Res mode
                Short
                Norstar
                ZLS - new format

              */
            string header = "";
            string myString = "";
            if (timeCheck == 0)
            {
                timeCheck = fileStartTime.Minute;
            }
            if (DateTime.Now.Minute == timeCheck + 2)
            {
                timeCheck = DateTime.Now.Minute;
                thisDate = String.Format("{0:yyyy-MM-dd-hh-mm-ss}", DateTime.Now);     //Convert.ToString(DateTime.Now);
            }
            dataFileName = dataFilePath + ConfigData.meterNumber + "-" + thisDate + "-" + surveyName + "." + fileType;

            try
            {
                using (StreamWriter writer = File.CreateText(dataFileName))
                {
                    switch (fileFormat)// There is only two formats std and new
                    {
                        case "standard":// copy this as serial data format
                            header = "Line ID" + delimitor + "Year" + delimitor + "Days" + delimitor + "Hours" + delimitor
                            + "Minuites" + delimitor + "Seconds" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                            + "Cross coupling" + delimitor + "Average Beam" + delimitor + "VCC or CML" + delimitor + "AL" + delimitor
                            + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor + "LACC2" + delimitor
                            + "XACC" + delimitor + "LACC" + delimitor + "Latitude" + delimitor + "Longitude" + delimitor + "Altitude"
                            + delimitor + "GPS Status";

                            break;
/*
                        case "short":// this is serial data format
                            header = "Line ID" + delimitor + "Year" + delimitor + "Days" + delimitor + "Hours" + delimitor
                            + "Minuites" + delimitor + "Seconds" + delimitor + "Gravity";
                            break;

                        case "norstar":// this is serial data format
                            header = "Line ID" + delimitor + "Year" + delimitor + "Days" + delimitor + "Hours" + delimitor
                            + "Minuites" + delimitor + "Seconds" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                            + "Average Beam" + delimitor + "Cross coupling" + delimitor + "Total Correction" + delimitor + "VCC or CML" + delimitor + "AL" + delimitor
                            + "AX" + delimitor + "VE" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "AX2 or CMX";
                            break;
*/
                        case "new":
                            header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                            + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                            + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                            + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Total Correction"
                            + "Latitude" + delimitor + "Longitude" + delimitor + "Altitude" + delimitor + "GPS Status";

                            break;

                        default:
                            header = "Date" + delimitor + "Gravity" + delimitor + "Spring Tension" + delimitor
                            + "Cross coupling" + delimitor + "Raw Beam" + delimitor + "VCC or CML" + delimitor + "AL"
                            + delimitor + "AX" + delimitor + "VE" + delimitor + "AX2 or CMX" + delimitor + "XACC2" + delimitor
                            + "LACC2" + delimitor + "XACC" + delimitor + "LACC" + delimitor + "Total Correction"
                            + "Latitude" + delimitor + "Longitude" + delimitor + "Altitude" + delimitor + "GPS Status";

                            break;
                    }
                    writer.WriteLine(header);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // TODO Record to data file
        public void RecordDataToFile( Data Data)
        {
            string thisDate = String.Format("{0:yyyyMMdd-HHmm}", fileStartTime);
            string delimitor = ",";

            switch (fileType)
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

            string fileFormat = "standard";
            /*
                Format options
                Long marine mode
                Long High Res mode
                Short
                Norstar
                ZLS - new format

              */
            string header = "";
            string myString = "";
            if (timeCheck == 0)
            {
                timeCheck = fileStartTime.Minute;
            }
            if (DateTime.Now.Minute == timeCheck + 2)
            {
                
                timeCheck = DateTime.Now.Minute;
                thisDate = Convert.ToString(DateTime.Now);
            }
 
            switch (fileFormat)// why do I have a second switch statement???
            {
                case "standard":
                    // Need to change from raw beam to avg beam

                    // This is for 10 sec data
                    myString = Convert.ToString(surveyName) + delimitor + Convert.ToString(Data.Date.Year) + delimitor + Convert.ToString(Data.Date.Day) + delimitor + Convert.ToString(Data.Date.Hour) + delimitor
               + Convert.ToString(Data.Date.Minute) + delimitor + Convert.ToString(Data.Date.Second) + delimitor + Convert.ToString(Data.data4[2]) + delimitor + Convert.ToString(Data.data1[3])
               + delimitor + Convert.ToString(Data.data4[4]) + delimitor + Convert.ToString(Data.data1[5]) + delimitor + Convert.ToString(Data.data1[6])
               + delimitor + Convert.ToString(Data.data1[7]) + delimitor + Convert.ToString(Data.data1[8]) + delimitor + Convert.ToString(Data.data1[9])
               + delimitor + Convert.ToString(Data.data1[10]) + delimitor + Convert.ToString(Data.data1[11]) + delimitor + Convert.ToString(Data.data1[12])
               + delimitor + Convert.ToString(Data.data1[13]) + delimitor + Convert.ToString(Data.data1[14]) + delimitor + Convert.ToString(Data.totalCorrection)
               + delimitor + Convert.ToString(Data.latitude) + delimitor + Convert.ToString(Data.longitude) + delimitor + Convert.ToString(Data.altitude) + delimitor + Convert.ToString(Data.gpsNavigationStatus);

                    break;

                // Add 1 sec data here

                /*
                                case "short":
                                    break;
                */
                case "new":
              /*      myString = String.Format("{0:yyyyMMdd-HHmmss}", d.Date) + delimitor + Convert.ToString(d.Gravity) + delimitor + Convert.ToString(d.SpringTension)
                + delimitor + Convert.ToString(d.CrossCoupling) + delimitor + Convert.ToString(d.rawBeam) + delimitor + Convert.ToString(d.VCC)
                + delimitor + Convert.ToString(d.AL) + delimitor + Convert.ToString(d.AX) + delimitor + Convert.ToString(d.VE)
                + delimitor + Convert.ToString(d.AX2) + delimitor + Convert.ToString(d.XACC2) + delimitor + Convert.ToString(d.LACC2)
                + delimitor + Convert.ToString(d.XACC) + delimitor + Convert.ToString(d.LACC) + delimitor + Convert.ToString(d.TotalCorrection)
                + delimitor + Convert.ToString(d.latitude) + delimitor + Convert.ToString(d.longitude) + delimitor + Convert.ToString(d.altitude);
    */        
    break;

                default:
                    /*
                    myString = String.Format("{0:yyyyMMdd-HHmmss}", d.Date) + delimitor + Convert.ToString(d.Gravity) + delimitor + Convert.ToString(d.SpringTension)
                + delimitor + Convert.ToString(d.CrossCoupling) + delimitor + Convert.ToString(d.rawBeam) + delimitor + Convert.ToString(d.VCC)
                + delimitor + Convert.ToString(d.AL) + delimitor + Convert.ToString(d.AX) + delimitor + Convert.ToString(d.VE)
                + delimitor + Convert.ToString(d.AX2) + delimitor + Convert.ToString(d.XACC2) + delimitor + Convert.ToString(d.LACC2)
                + delimitor + Convert.ToString(d.XACC) + delimitor + Convert.ToString(d.LACC) + delimitor + Convert.ToString(d.TotalCorrection)
                + delimitor + Convert.ToString(d.latitude) + delimitor + Convert.ToString(d.longitude) + delimitor + Convert.ToString(d.altitude);
    */             
    break;
            }

            try
            {
                using (StreamWriter writer = File.AppendText(dataFileName))
                {
                    writer.WriteLine(myString);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //      }
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
            public double rawBeam;
            public double avgBeam;
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
            public int gpsNavigationStatus;
            public int gpsSynchStatus;
            public int gpsTimeStatus;
            public int gpsNumSatelites;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Boolean okToRun = false;

            if (surveyName == "")
            {
                MessageBox.Show("Warning! \nNo survey information was entered.\nRecording will start on next attempt");
                surveyNameSet = true;
            }
            else
            {
                okToRun = true;
            }

            if (okToRun)
            {
                // call create new file
                OpenDataFile();
                fileRecording = true;
                newDataFile = true;
                recordingTextBox.Text = "Recording file";
                recordingTextBox.BackColor = System.Drawing.Color.LightGreen;
                surveyTextBox.Enabled = false;
                stopButton.Enabled = true;
                startButton.Enabled = false;
            }
           // okToRun = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            // Close file
           
            fileRecording = false;
            surveyTextBox.Enabled = true;
            recordingTextBox.Text = "Recording stopped";
            recordingTextBox.BackColor = System.Drawing.Color.Red;
            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void ShutdownDataWorker(object obj)
        {
            var _delegate = (Action<shutdownData>)obj;
            // this.Invoke(new UpdateRecordBoxCallback(this.UpdateRecordBox), new object[] { true });
            //   this.Invoke(new Action<shutdownData>(this.UpdateShutdownText), new object[] { d });

            _delegate(new shutdownData
            {
                ShutDownText = "Preparing to shutdown..."
            });
            Thread.Sleep(1000);
            fileRecording = false;
            _delegate(new shutdownData
            {
                ShutDownText = "Closing all open files."
            });
            Thread.Sleep(2000);

            springTensionEnabled = false;
            _delegate(new shutdownData
            {
                ShutDownText = "Disabling spring tension"
            });
            Thread.Sleep(3000);
            torqueMotorsEnabled = false;
            _delegate(new shutdownData
            {
                ShutDownText = "Turning off torque motors"
            });
            Thread.Sleep(3000);

            gyrosEnabled = false;

            _delegate(new shutdownData
            {
                ShutDownText = "Turning off gyros"
            });
            Thread.Sleep(3000);

            _delegate(new shutdownData
            {
                ShutDownText = "Shutdown complete.  Program will now terminate."
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
                Environment.Exit(Environment.ExitCode);
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(Environment.ExitCode);
                System.Environment.Exit(1);
            }
        }

        private void surveyTextBox_TextChanged(object sender, EventArgs e)
        {
            UserDataForm.surveyTextBox.Text = surveyTextBox.Text;
            surveyName = surveyTextBox.Text;
            surveyNameSet = true;
            newDataFile = true;  //   Changing survey name will crate a new data file
            UpdateNameLabel();
        }

        private void exitProgramToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                StoreDefaultVariables();
                Environment.Exit(Environment.ExitCode);
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                StoreDefaultVariables();
                Environment.Exit(Environment.ExitCode);
                System.Environment.Exit(1);
            }
        }

        private void setDataFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = " (*.csv, *.txt, *.tsv)|*.csv, *.txt, *.tsv";
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

        public void GetConfigFileDialog()
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog();
            OpenFileDialog.InitialDirectory = Properties.Settings.Default.configFilePath;

            OpenFileDialog.Filter = "Comma delimited Files (.csv)|*.csv|All Files (*.*)|*.*";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.RestoreDirectory = true;

            OpenFileDialog.Multiselect = true;

            OpenFileDialog.ShowDialog();
            // save path and file name to defaults
            Properties.Settings.Default.configFilePath = Path.GetDirectoryName(OpenFileDialog.FileName);

            ReadConfigFile(OpenFileDialog.FileName);
        }

        private void loadConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetConfigFileDialog();
        }

        private void switchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Switches SwitchesForm = new Switches();
            SwitchesForm.Show();
        }

        private void initMeter()
        {
            // 200 Hz etc

            RelaySwitches.RelaySW = 0x80;
            RelaySwitches.StepperMotorEnable(enable);
            sendCmd("Send Relay Switches");

            sendCmd("Set Cross Axis Parameters");      // download platform parameters 4 -----
                                                       // Task taskA = Task.Factory.StartNew(() => DoSomeWork(200));
                                                       //taskA.Wait();

            sendCmd("Set Long Axis Parameters");       // download platform parametersv 5 -----
                                                       // taskA = Task.Factory.StartNew(() => DoSomeWork(200));
                                                       //taskA.Wait();

            sendCmd("Update Cross Coupling Values");   // download CC parameters 8     -----
                                                       // taskA = Task.Factory.StartNew(() => DoSomeWork(200));
                                                       // taskA.Wait();
            var iGyroSw = 1;
            if (iGyroSw == 2)
            {
                sendCmd("Update Gyro Bias Offset");
            }

            ControlSwitches.DataCollection(enable);
            ControlSwitches.ControlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
            //ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");
            ControlSwitches.ControlSw = 0x08; // ControlSwitches.RelayControlSW = 0x08;
            //ControlSwitches.DataCollection(enable);
            sendCmd("Send Control Switches");

            // RelaySwitches.Relay200Hz(enable);
            // RelaySwitches.Slew4(enable);
            // RelaySwitches.Slew5(enable);
            // RelaySwitches.StepperMotorEnable(enable);

            ////////////////////////////////////////////////////////////////////////////////////
            // relay and control switches
            //  RelaySwitches.RelaySW = 0xB1;// cmd 0
            Task taskA = Task.Factory.StartNew(() => DoSomeWork(1000));
            taskA.Wait();
            // RelaySwitches.RelaySW = 0xB1;
            // sendCmd("Send Relay Switches");

            // ControlSwitches.ControlSw = 0x08;
            //ControlSwitches.DataCollection(enable);
            // sendCmd("Send Control Switches");
        }

        private void gyroCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gyroCheckBox.Checked)
            {
                startupLabel.Text = "Waiting for gyros to spin up";
                // RelaySwitches.Slew4(disable);
                // RelaySwitches.Slew5(disable);
                ControlSwitches.ControlSw = 0x08;
                sendCmd("Send Control Switches");
                Task taskA = Task.Factory.StartNew(() => DoSomeWork(7000));
                taskA.Wait();

                RelaySwitches.RelaySW = 0x81;
                sendCmd("Send Relay Switches");
                //               taskA = Task.Factory.StartNew(() => DoSomeWork(2000));
                //               taskA.Wait();

                torqueMotorCheckBox.Enabled = true;
                WriteLogFile("Gyros enabled");
            }
            else
            {
                RelaySwitches.Slew4(enable);
                RelaySwitches.Slew5(enable);

                sendCmd("Send Relay Switches");
                WriteLogFile("Gyros disabled");
            }
            startupLabel.Text = "";
        }

        private void torqueMotorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (torqueMotorCheckBox.Checked)
            {
                //RelaySwitches.RelayTorqueMotor(enable);
                startupLabel.Text = "Wait for platform to level";
                RelaySwitches.RelaySW = 0x83;
                sendCmd("Send Relay Switches");

                //Task taskA = Task.Factory.StartNew(() => DoSomeWork(2000));// change this to 7 sec
                //taskA.Wait();

                //ControlSwitches.TorqueMotor(enable);
                ControlSwitches.ControlSw = 0x09;
                sendCmd("Send Control Switches");
                Task taskA = Task.Factory.StartNew(() => DoSomeWork(2000));// change this to 1 min
                taskA.Wait();

                gyroCheckBox.Enabled = false;
                springTensionCheckBox.Enabled = true;
                WriteLogFile("Torque motor enabled");
            }
            else
            {
                // RelaySwitches.RelaySW = 0x81;// cmd 0
                RelaySwitches.RelayTorqueMotor(disable);
                sendCmd("Send Relay Switches");
                gyroCheckBox.Enabled = true;
                springTensionCheckBox.Enabled = false;
                WriteLogFile("Torque motor enabled");
            }
            startupLabel.Text = "";
        }

        private void alarmCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (alarmCheckBox.Checked)
            {
                ControlSwitches.Alarm(enable);
                sendCmd("Send Control Switches");
                WriteLogFile("Alarm enabled");
            }
            else
            {
                ControlSwitches.Alarm(disable);
                sendCmd("Send Control Switches");
                WriteLogFile("Alarm disabled");
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
            SetChartAreaColors(1);// 2 black  1 gray
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

        private void CheckCalFile(string calFile)
        {
            var fileExists = File.Exists(calFile) || File.Exists(Path.Combine(Directory.GetParent(Path.GetDirectoryName(calFile)).FullName, Path.GetFileName(calFile)));

            if (engineerDebug)
            {
                Console.WriteLine("Calibration file name " + Path.GetFileName(calFile));
                Console.WriteLine("Calibration file path " + Path.GetDirectoryName(calFile));
            }
            DialogResult result = DialogResult.No;

            if (fileExists == false)
            {
                string message = "Calibration file not found!  Would you like to find it? \nAnswering No will load a scale factor of 1.";
                string title = "File not found";

                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                result = MessageBox.Show(message, title, buttons);

                if (result == DialogResult.No)
                {
                    // Defaults are already loaded.  Do nothing
                    // load defaults
                    if (engineerDebug)
                    {
                        Console.WriteLine("Calibration table data");
                        Console.WriteLine("Spring tension\tCal factor");
                        Console.WriteLine("___________________________________");
                    }
                    for (int i = 0; i <= 120; i += 1)
                    {
                        CalculateMarineData.table1[i] = i * 100;
                        CalculateMarineData.table2[i] = 1;
                        {
                            Console.WriteLine(CalculateMarineData.table1[i].ToString() + "\t" + CalculateMarineData.table2[i].ToString());
                        }
                    }
                }
                else
                {
                    //Open file dialog then read file
                    OpenFileDialog OpenFileDialog = new OpenFileDialog();
                    OpenFileDialog.InitialDirectory = Properties.Settings.Default.configFilePath;
                    OpenFileDialog.Filter = "Comma delimited Files (.csv)|*.csv|All Files (*.*)|*.*";
                    OpenFileDialog.FilterIndex = 1;
                    OpenFileDialog.RestoreDirectory = true;
                    OpenFileDialog.Multiselect = true;
                    OpenFileDialog.ShowDialog();
                    // save path and file name to defaults
                    Properties.Settings.Default.calFilePath = Path.GetDirectoryName(OpenFileDialog.FileName);
                    Properties.Settings.Default.calFileName = Path.GetFileName(OpenFileDialog.FileName);
                    Properties.Settings.Default.Save();
                    calFile = OpenFileDialog.FileName;
                    readCalibrationFile(calFile);// Load config file selected
                }
            }
            else
            {
                readCalibrationFile(calFile);
            }
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

            var fileName = calFile;// Double check the selected file exists
            var fileExists =
                File.Exists(fileName) ||
                File.Exists(
                    Path.Combine(
                        Directory.GetParent(Path.GetDirectoryName(fileName)).FullName,
                        Path.GetFileName(fileName)
                    )
                );

            if (fileExists)// Load the data
            {
                try
                {
                    using (engine.BeginReadFile(calFile))
                    {
                        var i = 0;
                        if (engineerDebug)
                        {
                            Console.WriteLine("Calibration table data");
                            Console.WriteLine("Spring tension\tCal factor");
                            Console.WriteLine("___________________________________");
                        }
                        foreach (calFileData dataItem in engine)
                        {
                            CalculateMarineData.table1[i] = (Single)dataItem.value1;
                            CalculateMarineData.table2[i] = (Single)dataItem.value2;
                            if (engineerDebug)
                            {
                                Console.WriteLine(CalculateMarineData.table1[i].ToString() + "\t" + CalculateMarineData.table2[i].ToString());
                            }

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
            //TODO: spring tension enable not working - debug
            if (springTensionCheckBox.Checked)
            {
                // ControlSwitches.Alarm(disable);
                //ControlSwitches.SpringTension(enable);
                //  ControlSwitches.TorqueMotor(enable);
                //  ControlSwitches.DataCollection(enable);

                //   RelaySwitches.Relay200Hz(enable);
                //RelaySwitches.StepperMotorEnable(enable);
                ControlSwitches.ControlSw = 0x0B;
                sendCmd("Send Control Switches");
                Console.WriteLine("ST: control sw = 0x0B");

                //sendCmd("Send Relay Switches");
                torqueMotorCheckBox.Enabled = false;
                WriteLogFile("Spring tension enabled");
            }
            else
            {
                ControlSwitches.SpringTension(disable);
                RelaySwitches.StepperMotorEnable(disable);
                sendCmd("Send Relay Switches");
                sendCmd("Send Control Switches");
                torqueMotorCheckBox.Enabled = true;
                WriteLogFile("Spring tension disabled");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RelaySwitches.StepperMotorEnable(disable);
            sendCmd("Send Relay Switches");
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            RelaySwitches.StepperMotorEnable(enable);
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
                    RelaySwitches.Relay200Hz(enable);
                    RelaySwitches.StepperMotorEnable(enable);
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
            if (MeterStatus.XGyro_Fog == 0)
            { crossFogNotReady = false; }
            else
            { crossFogNotReady = true; }

            // Check Long FOG status
            if (MeterStatus.XGyro_Fog == 0)
            { longFogNotReady = false; }
            else
            { longFogNotReady = true; }

            // Check Heater status
            if (MeterStatus.MeterHeater == 0)
            { heaterNotReady = false; }
            else
            { heaterNotReady = true; }
            if (heaterBypassCheckBox.Checked)
            {
                heaterNotReady = false;
            }
            if ((!crossFogNotReady) & (!longFogNotReady) & (!heaterNotReady))
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

        private void saveConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveConfigFile();
        }

        [DelimitedRecord(",")]
        public class ConfigFile
        {
            public string configItem;
        }

        private void SaveConfigFile()
        {
            DialogResult result = DialogResult.No;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = " (*.csv)|*.csv";

            saveFileDialog1.DefaultExt = fileType;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "csv";
            //    dialog.FileName = ConfigData.meterNumber + surveyName + "-" + DateTime.Now.ToString("yyyy-MMM-dd-HH-mm-ss");

            saveFileDialog1.InitialDirectory = Properties.Settings.Default.configFilePath;//  need to get this from stored data
            result = saveFileDialog1.ShowDialog();
            FileInfo fileInfo = new FileInfo(saveFileDialog1.FileName);
            Properties.Settings.Default.configFileName = Path.GetFileName(saveFileDialog1.FileName);// saveFileDialog1.FileName;
            Properties.Settings.Default.configFilePath = fileInfo.DirectoryName.ToString(); // set path for next time
            Properties.Settings.Default.Save();

            if (result == DialogResult.OK)
            {
                Console.WriteLine("File name " + saveFileDialog1.FileName);
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    sw.WriteLine("Version" + "," + ConfigData.version);
                    sw.WriteLine("Meter Number" + "," + ConfigData.meterNumber);
                    sw.WriteLine("Beam Scale Factor" + "," + ConfigData.beamScale);
                    sw.WriteLine("Cross Axis Period" + "," + ConfigData.crossPeriod);
                    sw.WriteLine("Cross Axis Damping Factor" + "," + ConfigData.crossDampFactor);
                    sw.WriteLine("Cross Axis Gain" + "," + ConfigData.crossGain);
                    sw.WriteLine("Cross Axis Lead" + "," + ConfigData.crossLead);
                    sw.WriteLine("Cross Axis Bias" + "," + ConfigData.crossBias);
                    sw.WriteLine("Long Axis Period" + "," + ConfigData.crossPeriod);
                    sw.WriteLine("Long Axis Damping Factor" + "," + ConfigData.longDampFactor);
                    sw.WriteLine("Long Axis Gain" + "," + ConfigData.longGain);
                    sw.WriteLine("Long Axis Lead" + "," + ConfigData.longLead);
                    sw.WriteLine("Long Axis Bias" + "," + ConfigData.longBias);
                    sw.WriteLine("Max Spring Tension" + "," + ConfigData.springTensionMax);
                    sw.WriteLine("Mode" + "," + ConfigData.modeSwitch);
                    sw.WriteLine("crossCouplingFactors1" + "," + ConfigData.crossCouplingFactors[1]);
                    sw.WriteLine("crossCouplingFactors2" + "," + ConfigData.crossCouplingFactors[2]);
                    sw.WriteLine("crossCouplingFactors3" + "," + ConfigData.crossCouplingFactors[3]);
                    sw.WriteLine("crossCouplingFactors4" + "," + ConfigData.crossCouplingFactors[4]);
                    sw.WriteLine("crossCouplingFactors5" + "," + ConfigData.crossCouplingFactors[5]);
                    sw.WriteLine("crossCouplingFactors6-VCC" + "," + ConfigData.crossCouplingFactors[6]);
                    sw.WriteLine("crossCouplingFactors7-AL" + "," + ConfigData.crossCouplingFactors[7]);
                    sw.WriteLine("crossCouplingFactors8-AX" + "," + ConfigData.crossCouplingFactors[8]);
                    sw.WriteLine("crossCouplingFactors9-VE" + "," + ConfigData.crossCouplingFactors[9]);
                    sw.WriteLine("crossCouplingFactors10-AX2" + "," + ConfigData.crossCouplingFactors[10]);
                    sw.WriteLine("crossCouplingFactors11-XACC2" + "," + ConfigData.crossCouplingFactors[11]);
                    sw.WriteLine("crossCouplingFactors12-LACC2" + "," + ConfigData.crossCouplingFactors[12]);
                    sw.WriteLine("crossCouplingFactors13-Cross Axis Compensation(4)" + "," + ConfigData.crossCouplingFactors[13]);
                    sw.WriteLine("crossCouplingFactors14-Long Axis Compensation(4)" + "," + ConfigData.crossCouplingFactors[14]);
                    sw.WriteLine("crossCouplingFactors15-Cross Axis Compensation(16)" + "," + ConfigData.crossCouplingFactors[15]);
                    sw.WriteLine("crossCouplingFactors16-Long Axis Compensation(16)" + "," + ConfigData.crossCouplingFactors[16]);
                    sw.WriteLine("analogFilter1-AX Phase" + "," + ConfigData.analogFilter[1]);
                    sw.WriteLine("analogFilter2-AL Phase" + "," + ConfigData.analogFilter[2]);
                    //  sw.WriteLine("analogFilter3" + "," + ConfigData.analogFilter[3]);
                    sw.WriteLine("analogFilter4-VCC Phase" + "," + ConfigData.analogFilter[4]);
                    sw.WriteLine("analogFilter5-Cross Axis Compensation Phase(4)" + "," + ConfigData.analogFilter[5]);
                    sw.WriteLine("analogFilter6-Long Axis Compensation Phase(4)" + "," + ConfigData.analogFilter[6]);
                    sw.WriteLine("analogFilter7-Cross Axis Compensation Phase(16)" + "," + ConfigData.analogFilter[7]);
                    sw.WriteLine("analogFilter8-Long Axis Compensation Phase(16)" + "," + ConfigData.analogFilter[8]);
                }
            }
        }

        private void saveDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void heaterBypassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.heaterBypassCheckBox.Checked)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    heaterBypassCheckBox.Checked = true;
                });
            }
        }

        private void windowSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            chartWindowTimeSpan = Convert.ToInt16(windowSizeNumericUpDown.Value);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void chartWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartWindowGroupBox.Visible = true;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            chartWindowGroupBox.Visible = false;
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
            springTensionGroupBox.Visible = false;
        }

        ///////////////////////////////////////////////
    }
}