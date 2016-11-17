using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace SerialPortTerminal
{
    // iPort[1] relay switches
    // iPort[1] 0 200Hz relay

    // iport[2] 6 airplane mode
    //iport[2]  portC input?

    public static class Errors
    {
        private static Boolean remoteRebooted = false;
        private static Boolean setTimeSuccess = false;
        private static Boolean platformLevel = false;
        private static int timeSetErrorCount = 0;
        private static int serialComErrorCount = 0;

        public static Boolean RemoteRebooted
        {
            get
            {
                return remoteRebooted;
            }
            set
            {
                remoteRebooted = value;
            }
        }

        public static Boolean SetTimeSuccess
        {
            get
            {
                return setTimeSuccess;
            }
            set
            {
                setTimeSuccess = value;
            }
        }

        public static Boolean PlatformLevel
        {
            get
            {
                return platformLevel;
            }
            set
            {
                platformLevel = value;
            }
        }

        public static int TimeSetErrorCount
        {
            get
            {
                return timeSetErrorCount;
            }
            set
            {
                timeSetErrorCount = value;
            }
        }

        public static int SerialComErrorCount
        {
            get
            {
                return serialComErrorCount;
            }
            set
            {
                serialComErrorCount = value;
            }
        }
    }

    public class PortC //  Byte 77
    {
        private static int irqPresent = 0;
        private static int xGyroHeater = 0;
        private static int lGyroHeater = 0;
        private static int meterHeater = 0;
        private static int generalInhibitFlag = 0;

        // reserved
        private static int gearSelector = 0;

        // reserved
        private static int portCVal = 0;

        public int IrqPresent
        {
            get
            {
                return irqPresent;
            }
            set
            {
                irqPresent = value;
            }
        }

        public int XGyroHeater
        {
            get
            {
                return xGyroHeater;
            }
            set
            {
                xGyroHeater = value;
            }
        }

        public int LGyroHeater
        {
            get
            {
                return lGyroHeater;
            }
            set
            {
                lGyroHeater = value;
            }
        }

        public int MeterHeater
        {
            get
            {
                return meterHeater;
            }
            set
            {
                meterHeater = value;
            }
        }

        public int GeneralInhibitFlag
        {
            get
            {
                return generalInhibitFlag;
            }
            set
            {
                generalInhibitFlag = value;
            }
        }

        public int GearSelector
        {
            get
            {
                return gearSelector;
            }
            set
            {
                gearSelector = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public int PortCVal
        {
            get
            {
                return portCVal;
            }
            set
            {
                // portCVal = value;
            }
        }

        public void CalculatePortC()
        {
            portCVal =
               irqPresent * 0x01 +
               xGyroHeater * 0x02 +
               lGyroHeater * 0x04 +
               meterHeater * 0x08 +
               generalInhibitFlag * 0x10 +
               gearSelector * 0x40;

            // 0x20 and 0x80 not used
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public void Port_C_MeterStatus(Int16 portCStatus)
        {
            irqPresent = (portCStatus & 0x01);
            xGyroHeater = (portCStatus & 0x02) >> 1;
            lGyroHeater = (portCStatus & 0x04) >> 2;
            meterHeater = (portCStatus & 0x08) >> 3;
            generalInhibitFlag = (portCStatus & 0x10) >> 4;
            gearSelector = (portCStatus & 0x40) >> 5;
            Boolean showPortStatus = false;
            if (showPortStatus)
            {
                Console.WriteLine("IRQ status " + irqPresent);
                Console.WriteLine("Cross gyro heater " + xGyroHeater);
                Console.WriteLine("Long gyro heater " + lGyroHeater);
                Console.WriteLine("Meter heater " + meterHeater);
                Console.WriteLine("General inhibit flag " + generalInhibitFlag);
                Console.WriteLine("Gear selector " + gearSelector);
            }
        }
    }

    static class MeterStatus// Byte 76
    {
        // CalculateMarineData CalculateMarineData = new CalculateMarineData();
        private static int alarmIndicated = 0;
        private static int xGyro_Fog = 0;   // cross gyro or FOG status
        private static int lGyro_Fog = 0;   // cross gyro or FOG status
        private static int meterHeater = 0;
        private static int dumpIndigator = 0;
        private static int incorrectCommandReceived = 0;
        private static int serialPortTimeout = 0;
        private static int receiveDataCheckSumError = 0;
        private static int meterStatus = 0;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public static int AlarmIndicated
        {
            get
            {
                return alarmIndicated;
            }
            set
            {
            }
        }

        public static int XGyro_Fog
        {
            get
            {
                return xGyro_Fog;
            }
            set
            {
            }
        }

        public static int LGyro_Fog
        {
            get
            {
                return lGyro_Fog;
            }
            set
            {
            }
        }

        public static int MeterHeater
        {
            get
            {
                return meterHeater;
            }
            set
            {
            }
        }

        public static int DumpIndigator
        {
            get
            {
                return dumpIndigator;
            }
            set
            {
            }
        }

        public static int IncorrectCommandReceived
        {
            get
            {
                return incorrectCommandReceived;
            }
            set
            {
            }
        }

        public static int SerialPortTimeout
        {
            get
            {
                return serialPortTimeout;
            }
            set
            {
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CheckSum")]
        public static int ReceiveDataCheckSumError
        {
            get
            {
                return receiveDataCheckSumError;
            }
            set
            {
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "alarmIndicated")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)")]
        public static void GetMeterStatus(Int16 meterUpdate)
        {
            frmTerminal frmTerminal = new frmTerminal();
            alarmIndicated = (meterStatus & 0x01);
            xGyro_Fog = (meterStatus & 0x02) >> 1;
            lGyro_Fog = (meterStatus & 0x04) >> 2;
            meterHeater = (meterStatus & 0x08) >> 3;
            dumpIndigator = (meterStatus & 0x10) >> 4;
            incorrectCommandReceived = (meterStatus & 0x20) >> 5;
            serialPortTimeout = (meterStatus & 0x40) >> 20;
            receiveDataCheckSumError = (meterStatus & 0x80) >> 6;
            meterStatus = meterUpdate;
            frmTerminal.meter_status = meterUpdate;
            frmTerminal.Dispose();
            Boolean meterDebug = false;
            if (meterDebug)
            {
                Console.WriteLine("alarmIndicated " + alarmIndicated);
                Console.WriteLine("Cross FOG " + xGyro_Fog);
                Console.WriteLine("Long FOG " + lGyro_Fog);
                Console.WriteLine("Meter Heater " + meterHeater);
                Console.WriteLine("Dump indicator " + dumpIndigator);
                Console.WriteLine("Bad command " + incorrectCommandReceived);
                Console.WriteLine("Serial port error " + serialPortTimeout);
                Console.WriteLine("Bad checksum " + receiveDataCheckSumError);
            }
            frmTerminal.Dispose();
        }
    }
     class GPS_Status
    {
        private static Boolean gpsTimeSynch;
        private static Boolean gps1HzSynch;
        private static Boolean gpsData;
        private static int gpsNumberOfSat;

        public Boolean GpsTimeSynch
        {
            get
            {
                return gpsTimeSynch;
            }
            set
            {
                gpsTimeSynch = value;
            }
        }
        public Boolean Gps1HzSynch
        {
            get
            {
                return gps1HzSynch;
            }
            set
            {
                gps1HzSynch = value;
            }
           
            
        }
        public Boolean GpsData
        {
            get
            {
                return gpsData;
            }
            set
            {
                gpsData = value;
            }
        }


        public int GpsNumberSat
        {
            get
            {
                return gpsNumberOfSat;
            }
            set
            {
                gpsNumberOfSat = value;
            }

        }
        public void GetSatStatus(int byteData)
        {
            int gpsData1 = byteData & 0x40;
            int timeSync1 = byteData & 0x20;
            int gps1Hz1 = byteData & 0x10;
            gpsNumberOfSat = byteData & 0x0F;
            if (gpsData1 == 0)
            {
                gpsData = true;
            }
            else
            {
                gpsData = false;
            }
            if (timeSync1 == 0)
            {
                gpsTimeSynch = true;
            }
            else
            {
                gpsTimeSynch = false;
            }
            if (gps1Hz1 == 0)
            {
                gps1HzSynch = true;
            }
            else
            {
                gps1HzSynch = false;
            }
         

        }
    }
    public class RelaySwitches
    {
        private int relaySW = 0;

        public int RelaySW
        {
            get
            {
                return relaySW;
            }
            set
            {
                relaySW = value;
            }
        }

        public void Relay200Hz(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x01;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xFE;// Clear bit 1
            }
        }

        public void RelayTorqueMotor(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x02;// Set bit 2
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xFD;// Clear bit 2
            }
        }

        public void Alarm(int state)
        {
            if (state == 1)// Enable alarm
            {
                relaySW = relaySW | 0x04;// Set bit 4
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xFB;// Clear bit 4
            }
        }

        public void SteppingMotorDirection(int state)
        {
            if (state == 1)
            {
                relaySW = relaySW | 0x08;// Set bit 8
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xF7;// Clear bit 8
            }
        }

        public void Slew4(int state)
        {
            if (state == 1)
            {
                relaySW = relaySW | 0x10;// Set bit 10
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xEF;// Clear bit 10
            }
        }

        public void Slew5(int state)
        {
            if (state == 1)
            {
                relaySW = relaySW | 0x20;// Set bit 20
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xDF;// Clear bit 20
            }
        }

        public void TriggerStepperMotor(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x40;// Set bit 40
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xBF;// Clear bit 40
            }
        }

        public void StepperMotorEnable(int state)
        {
            if (state == 1)// Enable stepper Motor
            {
                relaySW = relaySW | 0x80;// Set bit 80
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0x7F;// Clear bit 80
            }
        }
    }

    public class ControlSwitches
    {
        private int controlSw = 0;

        public int ControlSw
        {
            get
            {
                return controlSw;
            }
            set
            {
                controlSw = value;
            }
        }

        public void TorqueMotor(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                controlSw = controlSw | 0x01;// Set bit 1
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xFE;// Clear bit 1
            }
        }

        public void SpringTension(int state)
        {
            if (state == 1)// Enable Spring Tension
            {
                controlSw = controlSw | 0x02;// Set bit 2
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xFD;// Clear bit 2
            }
        }

        public void Alarm(int state)
        {
            if (state == 1)// Enable Alarm
            {
                controlSw = controlSw | 0x08;// Set bit 3
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xF7;// Clear bit 3
            }
        }

        public void DataCollection(int state)
        {
            if (state == 1)// Enable Data Collection
            {
                controlSw = controlSw | 0x04;// Set bit 4
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xFB;// Clear bit 4
            }
        }

        public void AlarmFlag(int state)
        {
            if (state == 1)// Enable Alarm Flag
            {
                controlSw = controlSw | 0x10;// Set bit 5
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xEF;// Clear bit 5
            }
        }
    }

    public class ControlSwitchesOld
    {
        public int RelayControlSW = 0;
        public int torquMotorSwitch = 0;// 0x01
        public int springTensionSwitch = 0;// 0x02
        public int alarmSwitch = 0;// 0x04
        public int dataSwitch = 0;// 0x08
        public int alarmFlag = 0;// 0x10

        public void ControlSwitchCalculate()
        {
            RelayControlSW =
                torquMotorSwitch * 0x01 +
                springTensionSwitch * 0x02 +
                alarmSwitch * 0x04 +
                dataSwitch * 0x08 +
                alarmFlag * 0x10;
        }
    }

    public class Data
    {
        public static Single[] data1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static Single[] data2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static Single[] data3 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static Single[] data4 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static Single[] GT = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static Single beam;
        public static Single oldBeam = 0;
        public static Single beamFirstDifference;
        public static Single totalCorrection;
        public static Single rAwg;
        public static Single gravity;
        public static int nPoint = 0;
        public static Single Beam, VCC, AL, AX, VE, AX2, XACC2, LACC2, XACC, LACC;
        public static DateTime Date;
        public static Single SpringTension;
        public static Single CrossCoupling;
        public static int year;
        public static int day;
        public static int days;
        public static int Hour, Min, Sec;
        private static int thisYear = DateTime.Now.Year;
        public static DateTime myDT = new DateTime(DateTime.Now.Year, 1, 1, new GregorianCalendar());
        // Creates an instance of the JulianCalendar.
        public static JulianCalendar myCal = new JulianCalendar();
        public static double longitude = 0;
        public static double latitude = 0;
        public static double altitude = 0;
        public static int gpsSyncStatus;
        public static int gpsTimeStatus;
        public static int gpsNavigationStatus;
        public static int gpsNumSatelites;




    }

    public class ConfigData// Change all single to double
    {
        public static string version;
        public static Single beamScale;
        public static string meterNumber;
        public static Single crossPeriod;
        public static Single longPeriod;
        public static Single crossDampFactor;
        public static Single longDampFactor;
        public static Single crossGain;
        public static Single longGain;
        public static Single crossLead;
        public static Single longLead;
        public static Single springTensionMax;

        public static Single crossBias;
        public static Single longBias;
        public static Single crossCompFactor_4;
        public static Single crossCompPhase_4;
        public static Single crossCompFactor_16;
        public static Single crossCompPhase_16;
        public static Single longCompFactor_4;
        public static Single longCompPhase_4;
        public static Single longCompFactor_16;
        public static Single longCompPhase_16;

        public static Single CML_Fact;
        public static Single AL_Fact;
        public static Single AX_Fact;
        public static Single VE_Fact;
        public static Single CMX_Fact;
        public static Single XACC2_Fact;
        public static Single LACC2_Fact;
        public static Single XACC_Phase;
        public static Single LACC_AL_Phase;
        public static Single LACC_CML_Phase;
        public static Single LACC_CMX_Phase;
        public static Single kFactor;
        public static string gyroType;

        public static Int16 numAuxChan = 0;
        public static string modeSwitch = "Marine";
        public static Single iAuxGain = (Single)0.0;// IAUXGAIN -> not sure what this is
        public static Int16 serialPortSwitch = 1;
        public static Int16 digitalInputSwitch = 0;
        public static Int16 printerEmulationSwitch = 3;
        public static Int16 serialPortOutputSwitch = -1;
        public static Int16 alarmSwitch = 0;
        public static Int16 linePrinterSwitch = 0;
        public static Int16 fileNameSwitch = 1;
        public static Int16 hardDiskSwitch = 1;
        public static string engPassword = "zls";
        public static Int16 monitorDisplaySwitch = 2;
        public static int screenDisplayFilter = 3;

        //   public static double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };// CCFACT  CROSS COUPLING FACTORS
        //                                               0    1    2    3    4    5       6        7           8        9    10   11   12   13
        //public static double[] crossCouplingFactors = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.9e-2, 2.499999e-3, -1.681e-2, 0.0, 0.0, 0.0, 0.0, -8.9999998e-4, -3.7e-3, 1.0, 1.0 };
        public static Single[] crossCouplingFactors = { (Single)0.0, (Single)0.0, (Single)0.0, (Single)0.0, (Single)0.0, (Single)0.0, (Single)0, (Single).0023, (Single)(-1.681e-2), (Single)0.0, (short)0.0, (Single)0.0, (Single)0.0, (Single)(-8.9999998e-4), (Single)(-3.7e-3), (Single)1.0, (Single)1.0 };

        public static Single[] analogFilter = { 0, 0, 0, 0, 0, 1, 0, 0, 0 };// ANALOG FILTER PARAMETERS

        // public static double[] analogFilter = { 0.0, 0.22400000691413879, 0.24250000715255737, 0.20000000298023224, 0.28999999165534973,0.60000002384185791, 0.60000002384185791, 1.0, 1.0 };
    }

    public class CalculateMarineData
    {
        // need to create constructor and make variables private
        public static Boolean myDebug = false;

        private PortC Port_C = new PortC();
        public static Single cper = 18;

        // Take serial data from meter and separate into various variables and commands
        public byte[] meterBytes;

        public int dataCounter = 0;
        public int dataLength;


        //  public Single gravity;

        public Single AUX1, AUX2, AUX3, AUX4;
        public Single p28V, n28V, p24V, p15V, n15V, p5V;
        public static int MeterSTATUS;
        public int REMOTEREBOOTED;
        public int TimeSetSuccess;
        public int G2000Bias;
        public Int16 portCStatus;

        public Boolean dataValid = false;
        public short gpsStatus;
        public static int nPoint = 0;

        public static Single[] table1 = {
        (Single)0.0, (Single)100.0, (Single)200.0, (Single)300.0 ,(Single)400.0, (Single)500.0, (Single)600.0,(Single)700.0, (Single)800.0, (Single)900.0, (Single)1000.0, (Single)1100.0 ,(Single)1200.0,
        (Single)1300.0, (Single)1400.0, (Single)1500.0, (Single)1600.0, (Single)1700.0, (Single)1800.0, (Single)1900.0, (Single)2000.0, (Single)2100.0, (Single)2200.0, (Single)2300.0 ,(Single)2400.0,
        (Single)2500.0, (Single)2600.0, (Single)2700.0, (Single)2800.0, (Single)2900.0, (Single)3000.0, (Single)3100.0, (Single)3200.0, (Single)3300.0, (Single)3400.0, (Single)3500.0, (Single)3600.0,
        (Single)3700.0, (Single)3800.0, (Single)3900.0, (Single)4000.0, (Single)4100.0, (Single)4200.0, (Single)4300.0, (Single)4400.0, (Single)4500.0, (Single)4600.0, (Single)4700.0, (Single)4800.0,
        (Single)4900.0, (Single)5000.0, (Single)5100.0, (Single)5200.0, (Single)5300.0, (Single)5400.0, (Single)5500.0, (Single)5600.0, (Single)5700.0, (Single)5800.0, (Single)5900.0, (Single)6000.0,
        (Single)6100.0, (Single)6200.0, (Single)6300.0, (Single)6400.0 ,(Single)6500.0, (Single)6600.0, (Single)6700.0, (Single)6800.0, (Single)6900.0, (Single)7000.0, (Single)7100.0, (Single)7200.0,
        (Single)7300.0, (Single)7400.0, (Single)7500.0, (Single)7600.0, (Single)7700.0, (Single)7800.0, (Single)7900.0, (Single)0080.0, (Single)8100.0, (Single)8200.0, (Single)8300.0, (Single)8400.0,
        (Single)8500.0, (Single)8600.0, (Single)8700.0, (Single)8800.0, (Single)8900.0, (Single)9000.0, (Single)9100.0, (Single)9200.0, (Single)9300.0, (Single)9400.0, (Single)9500.0, (Single)9600.0,
        (Single)9700.0, (Single)9800.0, (Single)9900.0, (Single)10000.0, (Single)10100.0, (Single)10200.0, (Single)10300.0, (Single)10400.0, (Single)10500.0, (Single)10600.0, (Single)10700.0,
        (Single)10800.0, (Single)10900.0, (Single)11000.0, (Single)11100.0, (Single)11200.0, (Single)11300.0 ,(Single)11400.0, (Single)11500.0, (Single)11600.0, (Single)11700.0, (Single)11800.0,
       (Single) 11900.0, (Single)12000.0
        };

        public static Single[] table2 = {
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1
        };

        public static Single[] GT = {
         0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
         0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private Single[] DATA = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };



        public static Single i4Par;

        // Sets a DateTime to April 3, 2002 of the Gregorian calendar.


        //     MeterData[] LiveMeterData;

        private Byte[] SetTempByte(Byte[] meterData, int numBytes, int startByte)
        {
            Byte[] tempB = new Byte[numBytes];

            for (int i = 0; i < numBytes; i++)
            {
                tempB[i] = meterData[startByte + i];
            }

            return tempB;
        }

        public void initializeDataCounter(int newval)
        {
            dataCounter = newval;
        }

        //TODO New methods to get marine data
        public Boolean ValidateMeterDataLength(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };
            tempByte[0] = meterBytes[0];
            // int dataLength = BitConverter.ToInt16(tempByte, 0);
            int dataLength = meterBytes.Length;

            if (dataLength == meterBytes[0] + 1)
            {
                // Console.WriteLine("True");
                return true;
            }
            else
            {
                Errors.SerialComErrorCount++;
                Console.WriteLine("Reported length: " + meterBytes[0] + "\t Calculated length: " + (meterBytes.Length - 1));
                //   Console.WriteLine("False");
                return false;
            }
        }

        public int GetMeterYear(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[2];
            tempByte[1] = meterBytes[3];
            int year = BitConverter.ToInt16(tempByte, 0);

            return year;
        }

        public int GetMeterDay(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[4];
            tempByte[1] = meterBytes[5];
            int day = BitConverter.ToInt32(tempByte, 0);

            return day;
        }
        public int GetMeterHour(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[6];
            int hour = BitConverter.ToInt16(tempByte, 0);

            return hour;
        }
        public int GetMeterMin(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[7];
            int min = BitConverter.ToInt16(tempByte, 0);

            return min;
        }
        public int GetMeterSec(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[8];
            int sec = BitConverter.ToInt32(tempByte, 0);

            return sec;
        }
        public DateTime GetDateTime(Data Data)
        {
            Data.myDT = new DateTime();
            Data.myDT = Data.myDT.AddHours(Data.Hour);
            Data.myDT = Data.myDT.AddMinutes(Data.Min);
            Data.myDT = Data.myDT.AddSeconds(Data.Sec);


            Data.day = Data.day - 1;
            Data.myDT = Data.myDT.AddDays(Data.day);
            Data.myDT = Data.myDT.AddYears(Data.year - 1);

            Data.Date = Data.myDT;
            if (frmTerminal.engineerDebug)
            {
                Console.WriteLine("Date = " + Data.Date);
            }

            return Data.myDT;
        }
        public Single GetMeterSpringTension(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[9];
            tempByte[1] = meterBytes[10];
            tempByte[2] = meterBytes[11];
            tempByte[3] = meterBytes[12];

            Single springTension = BitConverter.ToSingle(tempByte, 0);
            Data.data1[3] = springTension;

            Console.WriteLine(springTension);
            return springTension;
        }
        public Single GetMeterRawBeam(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[13];
            tempByte[1] = meterBytes[14];
            tempByte[2] = meterBytes[15];
            tempByte[3] = meterBytes[16];

            Single rawBeam = BitConverter.ToSingle(tempByte, 0);
            Data.data1[5] = rawBeam;

            return rawBeam;
        }
        public Single GetMeterVcc(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[17];
            tempByte[1] = meterBytes[18];
            tempByte[2] = meterBytes[19];
            tempByte[3] = meterBytes[20];

            Single vcc = BitConverter.ToSingle(tempByte, 0);
            Data.data1[6] = vcc;

            return vcc;
        }
        public Single GetMeterAl(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[21];
            tempByte[1] = meterBytes[22];
            tempByte[2] = meterBytes[23];
            tempByte[3] = meterBytes[24];

            Single al = BitConverter.ToSingle(tempByte, 0);

            Data.data1[7] = al;

            return al;
        }
        public Single GetMeterAx(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[25];
            tempByte[1] = meterBytes[26];
            tempByte[2] = meterBytes[27];
            tempByte[3] = meterBytes[28];

            Single ax = BitConverter.ToSingle(tempByte, 0);
            Data.data1[8] = ax;

            return ax;
        }
        public Single GetMeterVe(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[29];
            tempByte[1] = meterBytes[30];
            tempByte[2] = meterBytes[31];
            tempByte[3] = meterBytes[32];

            Single ve = BitConverter.ToSingle(tempByte, 0);
            Data.data1[9] = ve;

            return ve;
        }
        public Single GetMeterAx2(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[33];
            tempByte[1] = meterBytes[34];
            tempByte[2] = meterBytes[35];
            tempByte[3] = meterBytes[36];

            Single ax2 = BitConverter.ToSingle(tempByte, 0);
            Data.data1[10] = ax2;

            return ax2;
        }
        public Single GetMeterXacc2(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[37];
            tempByte[1] = meterBytes[38];
            tempByte[2] = meterBytes[39];
            tempByte[3] = meterBytes[40];

            Single xacc2 = BitConverter.ToSingle(tempByte, 0);
            Data.data1[11] = xacc2;

            return xacc2;
        }
        public Single GetMeterLacc2(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[44];
            tempByte[1] = meterBytes[42];
            tempByte[2] = meterBytes[43];
            tempByte[3] = meterBytes[44];

            Single lacc2 = BitConverter.ToSingle(tempByte, 0);
            Data.data1[12] = lacc2;

            return lacc2;
        }
        public Single GetMeterXacc(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[45];
            tempByte[1] = meterBytes[46];
            tempByte[2] = meterBytes[47];
            tempByte[3] = meterBytes[48];

            Single xacc = BitConverter.ToSingle(tempByte, 0);
            Data.data1[13] = xacc;

            return xacc;
        }
        public Single GetMeterLacc(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[49];
            tempByte[1] = meterBytes[50];
            tempByte[2] = meterBytes[51];
            tempByte[3] = meterBytes[52];

            Single lacc = BitConverter.ToSingle(tempByte, 0);
            Data.data1[14] = lacc;

            return lacc;
        }
        public Single GetMeterGpsAltitude(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };
            Single altitude = 0;
            Int32 gTemp = 0;

            tempByte[0] = meterBytes[59];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = gTemp & 0x0F;
            altitude = Convert.ToSingle(gTemp) * 10000;

            tempByte[0] = meterBytes[60];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + gTemp;
            altitude = Convert.ToSingle(gTemp) * 100 + altitude;

            tempByte[0] = meterBytes[61];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + gTemp;
            altitude = Convert.ToSingle(gTemp) + altitude;

            tempByte[0] = meterBytes[62];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + gTemp;
            altitude = Convert.ToSingle(gTemp) / 100 + altitude;

            if ((meterBytes[59] & 0xF0) != 0)
            {
                altitude *= -1;
            }
            if ((meterBytes[59] & 0xF0) != 0)
            {
                altitude *= -1;
            }


            return altitude;
        }
        public double GetMeterGpsLongitude(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };
            double longitude = 0;
            Int32 gTemp = 0;

            Array.Clear(tempByte, 0, tempByte.Length);
            tempByte[0] = meterBytes[64];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = gTemp & 0x0F;
            longitude = Convert.ToDouble(gTemp) * 100;

            tempByte[0] = meterBytes[65];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            longitude = Convert.ToDouble(gTemp) + longitude;

            tempByte[0] = meterBytes[66];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            longitude = Convert.ToDouble(gTemp) / 60 + longitude;

            tempByte[0] = meterBytes[67];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            longitude = Convert.ToDouble(gTemp) / 6000 + longitude;

            tempByte[0] = meterBytes[68];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            longitude = Math.Round(Convert.ToDouble(gTemp) / 60000 + longitude, 5);

            if (meterBytes[63] != 0)
            {
                longitude *= -1;
            }
            return longitude;
        }
        public double GetMeterGpsLatitude(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };
            double latitude = 0;
            Int32 gTemp = 0;

            Array.Clear(tempByte, 0, tempByte.Length);
            tempByte[0] = meterBytes[70];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            latitude = Convert.ToSingle(gTemp);

            tempByte[0] = meterBytes[71];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            latitude = Convert.ToSingle(gTemp) / 60 + latitude;

            tempByte[0] = meterBytes[72];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            latitude = Convert.ToSingle(gTemp) / 6000 + latitude;

            tempByte[0] = meterBytes[73];
            gTemp = BitConverter.ToInt32(tempByte, 0);
            gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
            latitude = Data.longitude = Math.Round(Convert.ToSingle(gTemp) / 60000 + latitude, 5);

            if (meterBytes[69] != 0)
            {
                latitude *= -1;
            }
            return latitude;
        }
        public int GetMeterGpsStatus(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };
            GPS_Status GPS_Status = new GPS_Status();

            tempByte[0] = meterBytes[74];
            int gpsStatus = BitConverter.ToInt16(tempByte, 0);
        //    Console.WriteLine("gpsStatus: " + gpsStatus);
            GPS_Status.GetSatStatus(tempByte[0]);
            return gpsStatus;
        }
        public int GetMeterPortCStatus(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[77];
            int portCStatus = BitConverter.ToInt16(tempByte, 0);
            Port_C.Port_C_MeterStatus((short)portCStatus);
         //   Console.WriteLine("portCStatus: " + portCStatus);
            return portCStatus;
        }
        public int GetMeterStatus(byte[] meterBytes)
        {
            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[76];
            int meterStatus = BitConverter.ToInt16(tempByte, 0);
            MeterStatus.GetMeterStatus((short)meterStatus);
            //
        //    Console.WriteLine("meterStatus: " + meterStatus);
            return meterStatus;
        }
        public double[] GetMeterG2000_Bias(byte[] meterBytes)
        {
            double[] g2000 = { 0, 0 };

            byte[] tempByte = { 0, 0, 0, 0 };

            tempByte[0] = meterBytes[45];
            tempByte[1] = meterBytes[46];
            tempByte[2] = meterBytes[47];
            tempByte[3] = meterBytes[48];

            double glTemp = Math.Round(BitConverter.ToSingle(tempByte, 0), 4);
            // data1[13] = xacc;
            g2000[0] = glTemp;



            tempByte[0] = meterBytes[45];
            tempByte[1] = meterBytes[46];
            tempByte[2] = meterBytes[47];
            tempByte[3] = meterBytes[48];

            double gxTemp = Math.Round(BitConverter.ToSingle(tempByte, 0), 4);

            g2000[1] = gxTemp;

            return g2000;
        }
        public string GetMeterCommand(byte[] meterBytes)
        {
            string command;
            command = "";

            switch (meterBytes[1])
            {
                case 0:
                    command = "Meter Data";
                    break;

                case 1:
                    command = "Remote Robooted";
                    Errors.RemoteRebooted = true;
                    // Need to print this out somewhere and take action

                    break;
                case 2:
                    command = "Time Set Successful";
                    break;
                case 3:
                    command = "Time Set Failed";
                    break;
                case 4:
                    command = "G2000 Bias";
                    break;

                default:
                    break;
            }

            return command;
        }
        public void CheckErrorCount()
        {
            frmTerminal frmTerminal = new frmTerminal();

            if (Errors.SerialComErrorCount > 30)
            {
                //Thread.Sleep(500);
                Errors.SerialComErrorCount = 0;
                frmTerminal.WriteLogFile("Serial error count exceed max limit.  Total consecutive error count = " + Errors.SerialComErrorCount);
                Console.WriteLine("Serial error count exceed max limit.  Total consecutive error count = " + Errors.SerialComErrorCount);

                // Take some action here.  Print etc.

            }
            frmTerminal.Dispose();
        }



        public byte[] CalculateCheckSum(byte[] intBytes, int numBytes)
        {
            var checkSum = 0;
            Array.Resize(ref intBytes, numBytes);
            byte[] txCmd = new byte[1 + intBytes.Length + 1];
            Buffer.BlockCopy(intBytes, 0, txCmd, 1, intBytes.Length);
            for (int i = 0; i < numBytes; i++)
            {
                checkSum = checkSum ^ txCmd[i];
            }
            txCmd[txCmd.Length - 1] = BitConverter.GetBytes(checkSum)[0]; ;


            return BitConverter.GetBytes(checkSum);
        }
        public bool ValidateMeterDataChecksum(byte[] meterBytes)
        {
            frmTerminal frmTerminal = new frmTerminal();
            byte[] checkSumByte = CalculateCheckSum(meterBytes, meterBytes.Length);
            int checksum = checkSumByte[0];
            if (checksum == meterBytes[meterBytes.Length - 1])
            {
                frmTerminal.Dispose();
                return true;
            }
            else
            {
                Errors.SerialComErrorCount++;
                Console.WriteLine("Reported checksum: " + meterBytes[meterBytes.Length - 1] + "\t Calculated checksum: " + checksum);
                frmTerminal.WriteLogFile("Reported checksum: " + meterBytes[meterBytes.Length - 1] + "\t Calculated checksum: " + checksum);
                frmTerminal.Dispose();
                return false;
            }

        }
        //TODO ParseNewMeterData
        public void ParseNewMeterData(byte[] meterBytes)
        {
            frmTerminal frmTerminal = new frmTerminal();
            Data Data = new Data();
            dataValid = false;
            // 1. length matches reported length
            // 2. checksum match?
            // 3. get command



            if (ValidateMeterDataLength(meterBytes))// Valid data.  Length == reported length
            {
                Boolean validChecksum = ValidateMeterDataChecksum(meterBytes);
                if (validChecksum)
                {
                    dataValid = true;
                }
                else
                {
                    // handle errors here
                    // add method to Errors
                }
            }
            if (dataValid)
            {
                string commandName = "";
                switch (meterBytes[1])
                {
                    case 0:
                        commandName = "Normal Data";
                        break;
                    case 1:
                        commandName = "Remote Rebooted";
                        Errors.RemoteRebooted = true;
                        frmTerminal.WriteLogFile(commandName);
                        Console.WriteLine(commandName);

                        break;
                    case 2:
                        commandName = "Time Set Successful";
                        frmTerminal.WriteLogFile(commandName);
                        Console.WriteLine(commandName);
                        break;
                    case 3:
                        commandName = "Time Set Failed";
                        frmTerminal.WriteLogFile(commandName);
                        Console.WriteLine(commandName);
                        break;
                    case 4:
                        commandName = "G2000 Bias";
                        frmTerminal.WriteLogFile(commandName);
                        Console.WriteLine(commandName);
                        break;

                    default:
                        break;
                }

                // Console.WriteLine("Comamnd: " + commandName);



                if (commandName == "Normal Data")
                {

                    Errors.SerialComErrorCount = 0;

                    Data.year = GetMeterYear(meterBytes);
                    Data.day = GetMeterDay(meterBytes);
                    Data.Hour = GetMeterHour(meterBytes);
                    Data.Min = GetMeterMin(meterBytes);
                    Data.Sec = GetMeterSec(meterBytes);
                    Data.myDT = GetDateTime(Data);
                    Data.SpringTension = (short)GetMeterSpringTension(meterBytes);
                    Data.beam = GetMeterRawBeam(meterBytes);
                    Data.VCC = (short)GetMeterVcc(meterBytes);
                    Data.AL = (short)GetMeterAl(meterBytes);
                    Data.AX = (short)GetMeterAx(meterBytes);
                    Data.VE = (short)GetMeterVe(meterBytes);
                    Data.AX2 = (short)GetMeterAx2(meterBytes);
                    Data.XACC2 = (short)GetMeterXacc2(meterBytes);
                    Data.LACC2 = (short)GetMeterLacc2(meterBytes);
                    Data.XACC = (short)GetMeterXacc(meterBytes);
                    Data.LACC = (short)GetMeterLacc(meterBytes);
                    Data.altitude = GetMeterGpsAltitude(meterBytes);
                    Data.longitude = GetMeterGpsLongitude(meterBytes);
                    Data.latitude = GetMeterGpsLatitude(meterBytes);
                    int tempData = GetMeterStatus(meterBytes);
                    tempData =  GetMeterGpsStatus( meterBytes);
                    tempData = GetMeterPortCStatus(meterBytes);



                }

                /*

                                // Check for command
                                string command = GetMeterCommand(meterBytes);
                                if (command == "G2000 Bias")
                                {
                                    double[] g2000_bias = GetMeterG2000_Bias(meterBytes);
                                }


                */


                /*
                Console.WriteLine(meterBytes);

                Console.WriteLine("Year: " + Data.year);
                Console.WriteLine("Day: " + Data.day);
                Console.WriteLine("Hour: " + Data.Hour);
                Console.WriteLine("Min: " + Data.Min);
                Console.WriteLine("Sec: " + Data.Sec);
                Console.WriteLine("Spring Tension: " + Data.SpringTension);
                Console.WriteLine("Raw Beam: " + Data.beam);
                Console.WriteLine("VCC: " + Data.VCC);
                Console.WriteLine("AL: " + Data.AL);
                Console.WriteLine("AX: " + Data.AX);
                Console.WriteLine("VE: " + Data.VE);
                Console.WriteLine("AX2: " + Data.AX2);
                Console.WriteLine("XACC2: " + Data.XACC2);
                Console.WriteLine("LACC2: " + Data.LACC2);
                Console.WriteLine("XACC2: " + Data.XACC);
                Console.WriteLine("LACC: " + Data.LACC);
                Console.WriteLine("Altitude: " + altitude);
                Console.WriteLine("Longitude: " + longitude);
                Console.WriteLine("Latitude: " + latitude);
                Console.WriteLine("Command: " + command);

                */


                Data = oneSecStuff(Data);  //pass configData rather than using new command
                // for now I will use the 10 sec version
                if (Data.Sec % 10 == 0)
                {
                    
                    Data = ComputeGravityPrelim(Data);
                    Data = DigitalFilter(Data);                            // Filter with LaCoste 60 point table
                    Data = UpLook(Data);
                    int ISCSW = 0;
                    int ICSFLG = 0;
                    int JSCSW = 0;
                    int IDSKSW = 0;
                    int IDSKFLG = 0;
                    int JDSKSW = 0;
                    int ICOMSW = 0;
                    int ICOMFLG = 0;
                    int ISTAT = 0;
                    int JERR = 0;

                    if (ISCSW == 1 && JSCSW == 1)
                    {
                        ICSFLG = 1;
                    }
                    else if (ISCSW == 2 && JSCSW == 1)
                    {
                        ICSFLG = 2;
                    }
                    else if (ISCSW == 3 && JSCSW == 1)
                    {
                        ICSFLG = 3;
                    }
                    else if (ISCSW == 4 && JSCSW == 1)
                    {
                        ICSFLG = 4;
                    }
                    int MODESW = 0;
                    if (MODESW == 0)// Marine I/O)
                    {
                        if (IDSKSW > 0 && JDSKSW > 0)
                        {
                            IDSKFLG = 1;
                        }
                        if (ICOMSW == -1)
                        {
                            ICOMFLG = 1;
                        }
                    }
                    if (ISTAT == 0)
                    {
                        JERR = 0;// Clear error count if OK
                    }

                 //   Data = computeChartOutput(Data);
                }


            }
            else
            {
                // increment error counter for bad data
                Errors.SerialComErrorCount++;
                CheckErrorCount();
            }

            frmTerminal.Dispose();
        }



        // add routine for error accumulation
        private void ControlSteppingMotor()
        {
            int iscsw = 0;
            int jscsw = 0;
            int iscflg;

            if (iscsw == 1 && jscsw == 1)
            {
                iscflg = 1;
            }
            else if (iscsw == 2 && jscsw == 1)
            {
                iscflg = 2;
            }
            else if (iscsw == 3 && jscsw == 1)
            {
                iscflg = 3;
            }
            else if (iscsw == 4 && jscsw == 1)
            {
                iscflg = 4;
            }
            else if (iscsw == 5 && jscsw == 1)
            {
                iscflg = 5;
            }
            int modesw = 0;
            int idsksw = 0;
            int idskflg = 0;
            int icomsw = 0;
            int icomflg = 0;
            if (modesw == 0)// marine mode
            {
                if (idsksw > 0 && jscsw > 0)
                {
                    idskflg = 1;
                }
                if (icomsw == -1)
                {
                    icomflg = 1;
                }
            }
        }


        // I don't remember where I use this
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }





        public Data oneSecStuff(Data thisData)// change to array    double[] data1, double[] data2, double[] data3, double ccFact
        {
            ConfigData ConfigData = new ConfigData();
            // Console.WriteLine("One second stuff ----------------------\n");
            // need to load config data manually;

            Data.data1[4] = 0;       //crossCoupling
            for (int i = 6; i < 13; i++)            //Calculate Cross coupling
            {
                Data.data1[4] = Data.data1[4] + Data.data1[i] * ConfigData.crossCouplingFactors[i];
            }
            Data.CrossCoupling = Data.data1[4];
            //         Console.WriteLine("cross coupling " + CrossCoupling);
            for (int i = 3; i < 19; i++)
            {
                thisData = Filter320(thisData, i);//     , data2[i], data3[i], data4[i]);
            }
            return thisData;


            /*
             if (frmTerminal.dataAquisitionMode == "HighRes"){   // HI RES mode
                    if((IDSKSW > 0) && (JDSKSW > 0)
                    {
                        IDSFLG = 2;            
                    }
               if(ICOMSW == -1)
               {
                    ICOMFLG = 2;
               } 
            }
            */
            
            //    ComputeGravity();

            // compute chart output

            //   AnalogFilter();

        }

        private string[] dataName = { "null", "null", "null", "ST", "CC", "raw beam", "VCC", "AL", "AX", "VE", "AX2", "XACC2", "LACC2", "XACC", "LACC", "AUX1", "AUX2", "AUX3", "AUX4", "AUX5", "AUX6" };

        public Data computeChartOutput(Data Data)
        {
            //filter chart parameters with one or two minute filter
            Data.data4[1] = Data.data4[1] + (Data.rAwg - Data.data4[1]) / cper;                     // Gravity
            Data.data4[21] = Data.data4[21] + (Data.data4[5] - Data.data4[21]) / cper;              // AVG B
            Data.data4[22] = Data.data4[22] + (Data.data4[4] - Data.data4[22]) / cper;              // CC
            Data.data4[23] = Data.data4[23] + (Data.totalCorrection - Data.data4[23]) / cper;       // TC
            return Data;
        }

        public Data Filter320(Data Data, int ii)
        {
            Single VIN = Data.data1[ii];
            Single F1 = Data.data2[ii];
            Single F2 = Data.data3[ii];
            Single F3 = Data.data4[ii];

            F1 = F1 + (VIN - F1) * (Single).05;
            F2 = F2 + (F1 - F2) * (Single).05;
            F3 = F3 + (F2 - F3) * (Single).05;
            Data.data1[ii] = VIN;
            Data.data2[ii] = F1;
            Data.data3[ii] = F2;
            Data.data4[ii] = F3;
            return Data;
            //     Console.WriteLine(dataName[ii] + " " + 1 + " : " + VIN + "\n" +
            //                          dataName[ii] + " " + 2 + " : " + F1 + "\n" +
            //                          dataName[ii] + " " + 3 + " : " + F2 + "\n" +
            //                          dataName[ii] + " " + 4 + " : " + F3 + "\n");
        }

        public Data ComputeGravityPrelim(Data Data)
        {
            ConfigData ConfigData = new ConfigData();
            CalculateMarineData CalculateMarineData = new CalculateMarineData();


            Data.beam = ConfigData.beamScale * Data.data4[5];                    // Beam scale determined by K-check (-1.9) data4[5] = avg B
            Data.beamFirstDifference = Data.beam - Data.oldBeam;                 // DELB Get beam velocities first difference
            Data.oldBeam = Data.beam;
            Data.totalCorrection = Data.beamFirstDifference * 3 + Data.data4[4];      // Scale velocity to mGal & add cross coupling   This should work if FILT320 worked
            Data.data1[23] = Data.totalCorrection;
            Data.rAwg = Data.data4[3] + Data.totalCorrection;                         // Add spring tension
           
            return Data;


            // Below moved to seperate method calls
            // data4[2] is digital gravity
            //    Data.data4[2] = DigitalFilter(Data.rAwg);                            // Filter with LaCoste 60 point table


            //   gravity = UpLook(Data.data4[2]) + (short).05;



        }

        public Data UpLook(Data Data)
        {
            // CONVERTS GRAVITY VALUES ACCORDING TO THE CALIBRATION TABLE
            // FOR METERS WITH A CONSTANT CALIBRATION FACTOR, A DUMMY
            // CALIBRATION TABLE MUST BE CREATED USING MKTABLE.
            Single gVal = Data.data4[2];
            int ind;
            Single upLook;


            if (gVal != 0)
            {
                ind = Convert.ToInt32(Math.Abs(gVal / 100));

                if (ind > 120) { ind = 120; }
                upLook = table1[ind] + (gVal - (ind * 100)) * table2[ind];

            }
            else { Console.WriteLine("divide by zero error"); }
            Data.gravity = gVal;
            return Data;
        }

        public Data DigitalFilter(Data Data)// PERFORMS 60 POINT DIGITAL FILTER ON GRAVITY
        {
            //Data.rAwg; ;
            int[] arr1 = new int[10];
            int K;
            Single dfilt = 0;

            //  Console.WriteLine("Digital filter\n");
            // FILTER WEIGHTS
            Single[] filterWeights = {0, (Single)(-.00034), (Single)(-.00038), (Single)(-.00041), (Single)(-.00044), (Single)(-.00046),(Single)(-.00046), // 1
                                       (Single)(-.00044),  (Single)(-.00039),  (Single)(-.00030), (Single)(-.00015),  (Single).00007,  (Single).00037, // 2
                                       (Single).00079,  (Single).00133,  (Single).00202,  (Single).00289,  (Single).00396,  (Single).00526, // 3
                                       (Single).00679,  (Single).00859,  (Single).01066,  (Single).01299,  (Single).01558,  (Single).01841, // 4
                                       (Single).02143,  (Single).02460,  (Single).02785,  (Single).03110,  (Single).03426,  (Single).03723, // 5
                                       (Single).03992,  (Single).04223,  (Single).04408,  (Single).04539,  (Single).04613,  (Single).04626, // 6
                                       (Single).04579,  (Single).04474,  (Single).04315,  (Single).04109,  (Single).03864,  (Single).03589, // 7
                                       (Single).03292,  (Single).02984,  (Single).02671,  (Single).02362,  (Single).02063,  (Single).01780, // 8
                                       (Single).01516,  (Single).01274,  (Single).01056,  (Single).00863,  (Single).00694,  (Single).00548, // 9
                                       (Single).00424,  (Single).00321,  (Single).00235,  (Single).00166,  (Single).00111,  (Single).00068 };//10

            nPoint++;
            if (nPoint > 60)
            { nPoint = 1; }

            GT[nPoint] = Data.rAwg;
            //        Console.WriteLine("GT[" + nPoint + "] " + GT[nPoint]);
            K = nPoint;
            dfilt = 0;

            for (int i = 1; i <= 60; i++)
            {
                K++;
                if (K > 60) { K = 1; }

                dfilt = dfilt + filterWeights[i] * (GT[K] - GT[1]);
                //  Console.WriteLine("dfilt: " + dfilt);


            }
            dfilt = dfilt + GT[1];
            Data.data4[2] = dfilt;
            return Data;
        }


    }
}