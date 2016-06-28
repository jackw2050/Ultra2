using System;
using System.Globalization;
using System.Text;

namespace SerialPortTerminal
{
    // iPort[1] relay switches
    // iPort[1] 0 200Hz relay

    // iport[2] 6 airplane mode
    //iport[2]  portC input?

    public class Errors
    {
        public Boolean remoteRebooted = false;
        public Boolean timeSetSuccessful = false;
    }

    public class PortC //  Byte 77
    {
        public int irqPresent = 0;
        public int xGyroHeater = 0;
        public int lGyroHeater = 0;
        public int meterHeater = 0;
        public int generalInhibitFlag = 0;

        // reserved
        public int gearSelector = 0;

        // reserved
        public int portCVal = 0;

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

        public void Port_C_MeterStatus(Int16 portCStatus)
        {
            irqPresent = (portCStatus & 0x01);
            xGyroHeater = (portCStatus & 0x02) >> 1;
            lGyroHeater = (portCStatus & 0x04) >> 2;
            meterHeater = (portCStatus & 0x08) >> 4;
            generalInhibitFlag = (portCStatus & 0x10) >> 8;
            gearSelector = (portCStatus & 0x40) >> 20;
            if (false)
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

    public class MeterStatus// Byte 76
    {
        public int alarmIndicated = 0;
        public int xGyro_Fog = 0;   // cross gyro or FOG status
        public int lGyro_Fog = 0;   // cross gyro or FOG status
        public int meterHeater = 0;
        public int dumpIndigator = 0;
        public int incorrectCommandReceived = 0;
        public int serialPortTimeout = 0;
        public int receiveDataCheckSumError = 0;

        // reserved
        public int meterStatus = 0;

        public void GetMeterStatus(Int16 meterStatus)
        {
            alarmIndicated = (meterStatus & 0x01);
            xGyro_Fog = (meterStatus & 0x02) >> 1;
            lGyro_Fog = (meterStatus & 0x04) >> 2;
            meterHeater = (meterStatus & 0x08) >> 4;
            dumpIndigator = (meterStatus & 0x10) >> 8;
            incorrectCommandReceived = (meterStatus & 0x20) >> 10;
            serialPortTimeout = (meterStatus & 0x40) >> 20;
            receiveDataCheckSumError = (meterStatus & 0x80) >> 40;
            if (false)
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
        }
    }

    public class RelaySwitchesOld// IPORT[1]\\kij
    {
        //  public int RelaySW = 0;

        public int relay200Hz = 0;
        public int relayTorqueMotor = 0;
        public int alarm = 0;
        public int steppingMotorDirection = 0;// need to check this
        public int slew4 = 0;
        public int slew5 = 0;
        public int triggerStepperMotor = 0;
        public int stepperMotorEnable = 0;
        /*
         * IPORT(1)
         * BIT
         * 0    200Hz relay                 0b1             0x01
         * 1    Torque relay                0b10            0x02
         * 2    Alarm                       0b100           0x04
         * 3    Stepping motor direction    0b1000          0x08
         * 4    TTL or slew                 0b10000         0x10
         * 5    TTL or slew                 0b100000        0x20
         * 6    Trigger Stepper Motor       0b1000000       0x40
         * 7    Stepper Motor enable        0b10000000      0x80
            */

        public void RelaySwitchCalculate()// 0xB1  1011 0001
        {
            /*     RelaySW =
                     relay200Hz              * 0x01 +
                     relayTorqueMotor        * 0x02 +
                     alarm                   * 0x04 +
                     steppingMotorDirection  * 0x08 +
                     slew4                   * 0x10 +
                     slew5                   * 0x20 +
                     triggerStepperMotor     * 0x40 +
                     stepperMotorEnable      * 0x80;*/
        }
    }



    public class RelaySwitches
    {
        public int relaySW = 0;

        public void relay200Hz(int state)
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
        public void relayTorqueMotor(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x02;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xFD;// Clear bit 1
            }
        }
        public void alarm(int state)
        {
            if (state == 1)// Enable alarm
            {
                relaySW = relaySW | 0x04;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xFB;// Clear bit 1
            }
        }
        public void steppingMotorDirection(int state)
        {
            if (state == 1)
            {
                relaySW = relaySW | 0x08;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xF7;// Clear bit 1
            }
        }
        public void slew4(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x10;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xEF;// Clear bit 1
            }
        }
        public void slew5(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x20;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xDF;// Clear bit 1
            }
        }
        public void triggerStepperMotor(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x40;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0xBF;// Clear bit 1
            }
        }
        public void stepperMotorEnable(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                relaySW = relaySW | 0x80;// Set bit 1
            }
            else// Disable Torque Motor
            {
                relaySW = relaySW & 0x7F;// Clear bit 1
            }
        }
    }




    public class ControlSwitches
    {
        public int controlSw = 0;

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
            if (state == 1)// Enable Torque Motor
            {
                controlSw = controlSw | 0x02;// Set bit 1
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xFD;// Clear bit 1
            }
        }
        public void Alarm(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                controlSw = controlSw | 0x08;// Set bit 1
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xF7;// Clear bit 1
            }
        }
        public void DataCollection(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                controlSw = controlSw | 0x04;// Set bit 1
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xFB;// Clear bit 1
            }
        }
        public void AlarmFlag(int state)
        {
            if (state == 1)// Enable Torque Motor
            {
                controlSw = controlSw | 0x10;// Set bit 1
            }
            else// Disable Torque Motor
            {
                controlSw = controlSw & 0xEF;// Clear bit 1
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







    public class ConfigData
    {
        public static double beamScale = 1.298317;
        public static Int16 numAuxChan = 0;
        public static string meterNumber = "S91";
        public static Int16 linePrinterSwitch = 0;
        public static Int16 fileNameSwitch = 1;
        public static Int16 hardDiskSwitch = 1;
        public static string engPassword = "zls";
        public static Int16 monitorDisplaySwitch = 2;
        public static Single crossPeriod = System.Convert.ToSingle(0.0000191);
        public static Single longPeriod = System.Convert.ToSingle(0.0000084000002971151844);
        public static Single crossDampFactor = System.Convert.ToSingle(0.212);
        public static Single longDampFactor = System.Convert.ToSingle(0.091499999165534973);
        public static Single crossGain = System.Convert.ToSingle(0.15);
        public static Single longGain = System.Convert.ToSingle(0.11999999731779099);
        public static Int16 serialPortSwitch = 1;
        public static Int16 digitalInputSwitch = 0;
        public static Int16 printerEmulationSwitch = 3;
        public static Int16 serialPortOutputSwitch = -1;
        public static Int16 alarmSwitch = 0;
        public static Single crossLead = System.Convert.ToSingle(0.5);
        public static Single longLead = System.Convert.ToSingle(0.44999998807907104);
        public static double springTensionMax = 20000.0;
        public static Int16 modeSwitch = 1;
        public static double iAuxGain = 0.0;// IAUXGAIN -> not sure what this is
        public static double crossBias = 0.0;
        public static double longBias = 0.0;

        //   public static double[] crossCouplingFactors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };// CCFACT  CROSS COUPLING FACTORS
        //                                               0    1    2    3    4    5       6        7           8        9    10   11   12   13
        public static double[] crossCouplingFactors = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.9e-2, 2.499999e-3, -1.681e-2, 0.0, 0.0, 0.0, 0.0, -8.9999998e-4, -3.7e-3, 1.0, 1.0 };

        public static double[] analogFilter = { 0, 0, 0, 0, 0, 1, 0, 0, 0 };// ANALOG FILTER PARAMETERS

        // public static double[] analogFilter = { 0.0, 0.22400000691413879, 0.24250000715255737, 0.20000000298023224, 0.28999999165534973,0.60000002384185791, 0.60000002384185791, 1.0, 1.0 };
    }

    public class CalculateMarineData
    {
        // need to create constructor and make variables private
        public static Boolean myDebug = false;

        private PortC Port_C = new PortC();
        private MeterStatus MeterStatus = new MeterStatus();
        public static double cper = 18;

        // Take serial data from meter and separate into various variables and commands
        public byte[] meterBytes;

        public int dataCounter = 0;
        public int dataLength;
        public DateTime Date;
        public float SpringTension;
        public double CrossCoupling;
        public double gravity;
        public float Beam, VCC, AL, AX, VE, AX2, XACC2, LACC2, XACC, LACC, AUX1, AUX2, AUX3, AUX4;
        public double p28V, n28V, p24V, p15V, n15V, p5V;
        public int MeterSTATUS;
        public int REMOTEREBOOTED;
        public int TimeSetSuccess;
        public int G2000Bias;
        public Int16 portCStatus;
        public int year;
        public int day;
        public double Hour, Min, Sec;
        public Boolean dataValid = false;
        public Single gpsStatus;
        private static int nPoint = 0;
        public double longitude = 0;
        public double latitude = 0;
        public double altitude = 0;

        public static double[] table1 = {
    	0.0, 100.0, 200.0, 300.0 ,400.0, 500.0, 600.0,700.0, 800.0, 900.0, 1000.0, 1100.0 ,1200.0,
        1300.0, 1400.0, 1500.0, 1600.0, 1700.0, 1800.0, 1900.0, 2000.0, 2100.0, 2200.0, 2300.0 ,2400.0,
        2500.0, 2600.0, 2700.0, 2800.0, 2900.0, 3000.0, 3100.0, 3200.0, 3300.0, 3400.0, 3500.0, 3600.0,
        3700.0, 3800.0, 3900.0, 4000.0, 4100.0, 4200.0, 4300.0, 4400.0, 4500.0, 4600.0, 4700.0, 4800.0,
        4900.0, 5000.0, 5100.0, 5200.0, 5300.0, 5400.0, 5500.0, 5600.0, 5700.0, 5800.0, 5900.0, 6000.0,
        6100.0, 6200.0 ,6300.0, 6400.0 ,6500.0, 6600.0, 6700.0, 6800.0, 6900.0, 7000.0, 7100.0, 7200.0,
        7300.0, 7400.0, 7500.0, 7600.0, 7700.0, 7800.0, 7900.0, 0080.0, 8100.0, 8200.0, 8300.0, 8400.0,
        8500.0, 8600.0, 8700.0, 8800.0, 8900.0, 9000.0, 9100.0, 9200.0, 9300.0, 9400.0, 9500.0, 9600.0,
        9700.0, 9800.0, 9900.0, 10000.0, 10100.0, 10200.0, 10300.0, 10400.0, 10500.0, 10600.0, 10700.0,
        10800.0, 10900.0, 11000.0, 11100.0, 11200.0, 11300.0 ,11400.0, 11500.0, 11600.0, 11700.0, 11800.0,
        11900.0, 12000.0
        };

        public static double[] table2 = {
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0
        };

        private double[] GT = {
         0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
         0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private double[] DATA = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static double[] data1 = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static double[] data2 = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static double[] data3 = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static double[] data4 = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static double beam;
        public static double oldBeam = 0;
        public static double beamFirstDifference;
        public double totalCorrection;
        public double rAwg;

        public static double i4Par;

        // Sets a DateTime to April 3, 2002 of the Gregorian calendar.
        private int thisYear = DateTime.Now.Year;

        public DateTime myDT = new DateTime(DateTime.Now.Year, 1, 1, new GregorianCalendar());

        // there should be a way to intialize date to this year jan 1

        // Creates an instance of the JulianCalendar.
        public JulianCalendar myCal = new JulianCalendar();

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

        public void GetMeterData(byte[] meterBytes)
        {
            dataValid = false;
            myDT = new DateTime(DateTime.Now.Year, 1, 1, new GregorianCalendar());
            //GET SPRING TENSION
            // byte[] tempByte = new byte[4];
            byte[] tempByte = { 0, 0, 0, 0 };
            byte[] tempByte2 = { 0, 0 };

            // CHECK METERBYTES LENGTH AND SEND TO SWITCH CASE - ONLY PARSE FOR FILLED DATA

            if (meterBytes.Length > 77)// remove this
            {
                tempByte[0] = meterBytes[0];
                tempByte[1] = 0;

                dataLength = BitConverter.ToInt16(tempByte, 0);
            }

            // best way to zero an array?  Array.Clear(tempByte, 0, tempByte.Length);

            //          Add checksum verification.  Only record valid data

            if (meterBytes.Length < 10)
            {
            }
            else if ((meterBytes[0] == meterBytes.Length - 1) && (meterBytes.Length == 79))  // &&(checkSum(meterBytes) ==meterBytes{ meterBytes.length])
            {
                dataValid = true;

                byte[] array = new byte[4];
                array[0] = meterBytes[6]; // Lowest
                Hour = BitConverter.ToInt16(array, 0);

                tempByte[0] = meterBytes[7];
                tempByte[1] = 0;
                tempByte[2] = 0;
                tempByte[3] = 0;

                //      tempByte = SetTempByte(meterBytes, 1, 7);
                Min = BitConverter.ToInt16(tempByte, 0);

                tempByte[0] = meterBytes[8];
                tempByte[1] = 0;
                tempByte[2] = 0;
                tempByte[3] = 0;
                //          tempByte = SetTempByte(meterBytes, 1, 8);
                Sec = BitConverter.ToInt16(tempByte, 0);

                myDT = myDT.AddHours(Hour);
                myDT = myDT.AddMinutes(Min);
                myDT = myDT.AddSeconds(Sec);

                tempByte[0] = meterBytes[4];
                tempByte[1] = meterBytes[5];
                //         tempByte = SetTempByte(meterBytes, 2, 4);
                day = BitConverter.ToInt32(tempByte, 0);
                day = day - 1;
                myDT = myDT.AddDays(day);

                Array.Clear(tempByte, 0, tempByte.Length);
                tempByte[0] = meterBytes[2];
                tempByte[1] = meterBytes[3];
                //       tempByte = SetTempByte(meterBytes, 2, 2);
                year = BitConverter.ToInt16(tempByte, 0);
                Date = myDT;

                tempByte[0] = meterBytes[9];
                tempByte[1] = meterBytes[10];
                tempByte[2] = meterBytes[11];
                tempByte[3] = meterBytes[12];
                //     tempByte = SetTempByte(meterBytes, 4, 9);
                SpringTension = BitConverter.ToSingle(tempByte, 0);
                data1[3] = SpringTension;

                //GET RAW BEAM  ------------------------------------------------------------
                tempByte[0] = meterBytes[13];
                tempByte[1] = meterBytes[14];
                tempByte[2] = meterBytes[15];
                tempByte[3] = meterBytes[16];
                //    tempByte = SetTempByte(meterBytes, 4, 13);
                Beam = BitConverter.ToSingle(tempByte, 0);
                data1[5] = Beam;

                //GET VCC  ------------------------------------------------------------
                tempByte[0] = meterBytes[17];
                tempByte[1] = meterBytes[18];
                tempByte[2] = meterBytes[19];
                tempByte[3] = meterBytes[20];
                //     tempByte = SetTempByte(meterBytes, 4, 17);
                VCC = BitConverter.ToSingle(tempByte, 0);
                data1[6] = VCC;

                //GET AL  ------------------------------------------------------------
                tempByte[0] = meterBytes[21];
                tempByte[1] = meterBytes[22];
                tempByte[2] = meterBytes[23];
                tempByte[3] = meterBytes[24];
                AL = BitConverter.ToSingle(tempByte, 0);
                data1[7] = AL;

                //GET AX  ------------------------------------------------------------
                tempByte[0] = meterBytes[25];
                tempByte[1] = meterBytes[26];
                tempByte[2] = meterBytes[27];
                tempByte[3] = meterBytes[28];
                AX = BitConverter.ToSingle(tempByte, 0);
                data1[8] = AX;

                //GET VE  ------------------------------------------------------------
                tempByte[0] = meterBytes[29];
                tempByte[1] = meterBytes[30];
                tempByte[2] = meterBytes[31];
                tempByte[3] = meterBytes[32];
                VE = BitConverter.ToSingle(tempByte, 0);
                data1[9] = VE;

                //GET AX2  ------------------------------------------------------------
                tempByte[0] = meterBytes[33];
                tempByte[1] = meterBytes[34];
                tempByte[2] = meterBytes[35];
                tempByte[3] = meterBytes[36];
                AX2 = BitConverter.ToSingle(tempByte, 0);
                data1[10] = AX2;

                //GET XACC2  ------------------------------------------------------------
                // foreach (byte c in tempByte) tempByte[c] = 0x00;
                tempByte[0] = meterBytes[37];
                tempByte[1] = meterBytes[38];
                tempByte[2] = meterBytes[39];
                tempByte[3] = meterBytes[40];
                XACC2 = BitConverter.ToSingle(tempByte, 0);
                data1[11] = XACC2;

                //GET LACC2  ------------------------------------------------------------
                tempByte[0] = meterBytes[41];
                tempByte[1] = meterBytes[42];
                tempByte[2] = meterBytes[43];
                tempByte[3] = meterBytes[44];
                LACC2 = BitConverter.ToSingle(tempByte, 0);
                data1[12] = LACC2;

                //GET XACC  ------------------------------------------------------------
                // foreach (byte c in tempByte) tempByte[c] = 0x00;
                tempByte[0] = meterBytes[45];
                tempByte[1] = meterBytes[46];
                tempByte[2] = meterBytes[47];
                tempByte[3] = meterBytes[48];
                XACC = BitConverter.ToSingle(tempByte, 0);
                data1[13] = XACC;

                //GET LACC  ------------------------------------------------------------
                tempByte[0] = meterBytes[49];
                tempByte[1] = meterBytes[50];
                tempByte[2] = meterBytes[51];
                tempByte[3] = meterBytes[52];
                LACC = BitConverter.ToSingle(tempByte, 0);
                data1[14] = LACC;

                // place if here for GPS vs OLD
                Boolean oldMeter = false;
                if (oldMeter)
                {
                    //GET AUX1  ------------------------------------------------------------
                    Array.Clear(tempByte, 0, tempByte.Length);
                    tempByte[0] = meterBytes[53];
                    tempByte[1] = meterBytes[54];
                    AUX1 = BitConverter.ToInt16(tempByte, 0);
                    data1[15] = AUX1;

                    //GET AUX2  ------------------------------------------------------------
                    tempByte[0] = meterBytes[55];
                    tempByte[1] = meterBytes[56];
                    AUX2 = BitConverter.ToInt16(tempByte, 0);

                    //GET AUX3  ------------------------------------------------------------
                    tempByte[0] = meterBytes[57];
                    tempByte[1] = meterBytes[58];
                    AUX3 = BitConverter.ToInt16(tempByte, 0);
                    //     LiveMeterData[dataCounter].AUX3 = AUX3;

                    //GET AUX4  ------------------------------------------------------------
                    // foreach (byte c in tempByte) tempByte[c] = 0x00;
                    tempByte[0] = meterBytes[59];
                    tempByte[1] = meterBytes[60];
                    AUX4 = BitConverter.ToInt16(tempByte, 0);
                    //     LiveMeterData[dataCounter].AUX4 = AUX4;

                    //GET +28V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[64];
                    // tempByte[1] = meterBytes[65];
                    int p28Vi = BitConverter.ToInt32(tempByte, 0);
                    double p28vd = BitConverter.ToDouble(tempByte, 0);
                    p28V = Convert.ToDouble(p28Vi * 2 / 3276.7);

                    Console.WriteLine(p28V + "\t" + p28Vi + "\t" + p28vd + "\n");

                    //    LiveMeterData[dataCounter].p28V = p28V;
                    byte[] myTempByte = { meterBytes[64] };
                    string myString;
                    myString = ByteArrayToHexString(myTempByte);
                    int numVal;
                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[65];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[66];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[67];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);

                    // what the  hell am I doing here???

                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[70];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[71];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);
                    myTempByte[0] = meterBytes[72];
                    myString = ByteArrayToHexString(myTempByte);
                    Int32.TryParse(myString, out numVal);
                    Console.WriteLine(numVal);

                    //GET -28V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[66];
                    tempByte[1] = meterBytes[67];
                    int n28Vi = BitConverter.ToInt32(tempByte, 0);
                    n28V = Convert.ToDouble(n28Vi * -5 / 3276.7);   //  check this conversion
                    Console.WriteLine("-28V: " + n28V);
                    //GET +24V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[68];
                    tempByte[1] = meterBytes[69];
                    int p24Vi = BitConverter.ToInt32(tempByte, 0);
                    p24V = Convert.ToDouble(p24Vi * 2 / 3276.7);

                    //GET+15V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[70];
                    tempByte[1] = meterBytes[71];
                    int p15Vi = BitConverter.ToInt32(tempByte, 0);
                    p15V = Convert.ToDouble(p15Vi / 3276.7);   //  check this conversion

                    //GET -15V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[72];
                    tempByte[1] = meterBytes[73];
                    int n15Vi = BitConverter.ToInt32(tempByte, 0);
                    n15V = Convert.ToDouble(n15Vi * -3 / 3276.7);   //  check this conversion

                    //GET +5V  ------------------------------------------------------------
                    for (int i = 0; i < 4; i++) { tempByte[i] = 0x00; }
                    tempByte[0] = meterBytes[74];
                    tempByte[1] = meterBytes[75];
                    int p5Vi = BitConverter.ToInt32(tempByte, 0);
                    p5V = Convert.ToDouble(p5Vi / 3 / 3276.7);   //  check this conversion
                }
                else
                {


                    altitude = 0;
                    latitude = 0;
                    longitude = 0;




                    // Altitude
                    Int32 gTemp = 0;
                    // calculate latitude
                    // double altitude = 0;
                    Array.Clear(tempByte, 0, tempByte.Length);
                    tempByte[0] = meterBytes[59];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = gTemp & 0x0F;
                    altitude = Convert.ToDouble(gTemp) * 10000;

                    tempByte[0] = meterBytes[60];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + gTemp;
                    altitude = Convert.ToDouble(gTemp) * 100 + altitude;

                    tempByte[0] = meterBytes[61];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + gTemp;
                    altitude = Convert.ToDouble(gTemp) + altitude;

                    tempByte[0] = meterBytes[62];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + gTemp;
                    altitude = Convert.ToDouble(gTemp) / 100 + altitude;

                    if ((meterBytes[59] & 0xF0) != 0)
                    {
                        altitude *= -1;
                    }
                    Console.WriteLine("Altitude: " + altitude);

                    // Longigude

                    //     double longitude = 0;
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
                    longitude = Convert.ToDouble(gTemp) / 60000 + longitude;

                    if (meterBytes[63] != 0)
                    {
                        longitude *= -1;
                    }
                    Console.WriteLine("Longitude: " + longitude);
                    // Latitude
                    gTemp = 0;
                    //  double latitude = 0;
                    Array.Clear(tempByte, 0, tempByte.Length);
                    tempByte[0] = meterBytes[70];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
                    latitude = gTemp;

                    tempByte[0] = meterBytes[71];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
                    latitude = Convert.ToDouble(gTemp) / 60 + latitude;

                    tempByte[0] = meterBytes[72];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
                    latitude = Convert.ToDouble(gTemp) / 6000 + latitude;

                    tempByte[0] = meterBytes[73];
                    gTemp = BitConverter.ToInt32(tempByte, 0);
                    gTemp = (gTemp >> 4) * 10 + (gTemp & 0x0F);
                    latitude = Convert.ToDouble(gTemp) / 60000 + latitude;

                    if (meterBytes[69] != 0)
                    {
                        latitude *= -1;
                    }

                }

                //GET PRINTER STATUS  -- No longer used
                tempByte[0] = meterBytes[75];

                // GET METER STATUS
                Array.Clear(tempByte, 0, tempByte.Length);
                tempByte[0] = meterBytes[76];
                Int16 meterStatus = BitConverter.ToInt16(tempByte, 0);
                MeterStatus.GetMeterStatus(meterStatus);
                Console.WriteLine("Meter status " + meterStatus);


                //GET PORT C INPUT
                Array.Clear(tempByte, 0, tempByte.Length);
                tempByte[0] = meterBytes[77];
                portCStatus = BitConverter.ToInt16(tempByte, 0);//  make global later
                Port_C.Port_C_MeterStatus(portCStatus);
                Console.WriteLine("PortC status " + portCStatus);
                // CHECK FOR REMOTE EMBEDDED COMPUTER REBOOT
                tempByte[0] = meterBytes[1];
                if (tempByte[0] == 1)//remote rebooted
                {
                    //  var iError = -4;

                    // need error routine here.  if pulse is ok just re-sync otherwise set alert to reboot
                }
                // CHECK FOR TIME SET SUCCESSFULL/ FAIL
                else if (tempByte[0] == 2)//
                {
                    //   var iError = -10;
                    // timeBusy = false;
                    // gpFlg = 1;
                }
                else if (tempByte[0] == 2)// Time set failed
                {
                    //   var iError = -11;
                    // timeBusy = false;
                }
                //CHECK G2000 BIAS
                else if (tempByte[0] == 4)
                {
                    Array.Clear(tempByte, 0, tempByte.Length);
                    tempByte[0] = meterBytes[2];
                    tempByte[1] = meterBytes[3];
                    tempByte[2] = meterBytes[4];
                    tempByte[3] = meterBytes[5];
                    var gxTemp = BitConverter.ToDouble(tempByte, 0);

                    Array.Clear(tempByte, 0, tempByte.Length);
                    tempByte[0] = meterBytes[6];
                    tempByte[1] = meterBytes[7];
                    tempByte[2] = meterBytes[8];
                    tempByte[3] = meterBytes[9];
                    var glTemp = BitConverter.ToDouble(tempByte, 0);
                }

                //               foreach (var item in data1) { Console.WriteLine("data1 " + item.ToString()); }
                var pdbType = "GPS";// alt is "old"  :)
                if (pdbType == "GPS")
                {
                    // get gps and period
                }
                else if (pdbType == "old")
                {
                    //get digital and aux
                }
                oneSecStuff();
            }
            else
            {
                dataValid = false;
            }
        }

        // add routine for error accumulation








        // I don't remember where I use this
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        private void oneSecStuff()// change to array    double[] data1, double[] data2, double[] data3, double ccFact
        {
            ConfigData ConfigData = new ConfigData();
            // Console.WriteLine("One second stuff ----------------------\n");
            // need to load config data manually;

            data1[4] = 0.0;       //crossCoupling
            for (int i = 6; i < 13; i++)            //Calculate Cross coupling
            {
                data1[4] = data1[4] + data1[i] * ConfigData.crossCouplingFactors[i];
            }
            CrossCoupling = data1[4];
            //         Console.WriteLine("cross coupling " + CrossCoupling);
            for (int i = 3; i < 19; i++)
            {
                Filter320(data1[i], i);//     , data2[i], data3[i], data4[i]);
            }

            ComputeGravity();

            // compute chart output

            //   AnalogFilter();
        }

        private string[] dataName = { "null", "null", "null", "ST", "CC", "raw beam", "VCC", "AL", "AX", "VE", "AX2", "XACC2", "LACC2", "XACC", "LACC", "AUX1", "AUX2", "AUX3", "AUX4", "AUX5", "AUX6" };

        private void computeChartOutput()
        {
            //filter chart parameters with one or two minute filter
            data4[1] = data4[1] + (rAwg - data4[1]) / cper;                 // Gravity
            data4[21] = data4[21] + (data4[5] - data4[21]) / cper;          // AVG B
            data4[22] = data4[22] + (data4[4] - data4[22]) / cper;          // CC
            data4[23] = data4[23] + (totalCorrection - data4[23]) / cper;   // TC
        }

        private void Filter320(double VIN, int ii)
        {
            // VIN = DATA1[ii]
            var F1 = data2[ii];
            var F2 = data3[ii];
            var F3 = data4[ii];

            F1 = F1 + (VIN - F1) * .05;
            F2 = F2 + (F1 - F2) * .05;
            F3 = F3 + (F2 - F3) * .05;
            data2[ii] = F1;
            data3[ii] = F2;
            data4[ii] = F3;

            //     Console.WriteLine(dataName[ii] + " " + 1 + " : " + VIN + "\n" +
            //                          dataName[ii] + " " + 2 + " : " + F1 + "\n" +
            //                          dataName[ii] + " " + 3 + " : " + F2 + "\n" +
            //                          dataName[ii] + " " + 4 + " : " + F3 + "\n");
        }

        private void ComputeGravity()
        {
            ConfigData ConfigData = new ConfigData();
            //  Class1 = new );
            //     Console.WriteLine("Compute Gravity in mGal\n");

            beam = ConfigData.beamScale * data4[5];                 // Beam scale determined by K-check (-1.9) data4[5] = avg B
            beamFirstDifference = beam - oldBeam;                   // Get beam velocities first difference
            oldBeam = beam;
            totalCorrection = beamFirstDifference * 3 + data4[4];   // Scale velocity to mGal & add cross coupling   This should work if FILT320 worked
            rAwg = data4[3] + totalCorrection;                      // Add spring tension
            // data4[2] is digital gravity
            data4[2] = DigitalFilter(rAwg);                         // Filter with LaCoste 60 point table

            /*
                        Console.WriteLine("beam= " + beam + " | beamscale = " + ConfigData.beamScale + "    |    data4[5] = " + data4[5]);
                        Console.WriteLine("beamFirstDifference: " + beamFirstDifference);
                        Console.WriteLine("totalCorrection =  " + totalCorrection + " Data4[4] = " + data4[4]);
                        Console.WriteLine("rAwg = " + rAwg + " data4[3] = " + data4[3] + "         totalCorrection  " + totalCorrection);
                        Console.WriteLine("data4[2] " + data4[2] + "\n");
            */
            gravity = UpLook(data4[2]) + .05;
            // Console.WriteLine("Gravity = " + gravity);// Apply calibration table
        }

        private double UpLook(double gVal)
        {
            // CONVERTS GRAVITY VALUES ACCORDING TO THE CALIBRATION TABLE
            // FOR METERS WITH A CONSTANT CALIBRATION FACTOR, A DUMMY
            // CALIBRATION TABLE MUST BE CREATED USING MKTABLE.

            int ind;
            double upLook;

            //        Console.WriteLine("Uplook\n");
            //        Console.WriteLine("Gravity to be converted gVal: " + gVal + "\n");

            if (gVal != 0)
            {
                ind = Convert.ToInt32(Math.Abs(gVal / 100));

                if (ind > 120) { ind = 120; }
                upLook = table1[ind] + (gVal - (ind * 100)) * table2[ind];

                //      Console.WriteLine("Table 1 value @ " + table1[ind] + "\tTable 2 value @ " + table2[ind] + "\t");
                //     Console.WriteLine("upLook value : " + upLook + "\n");
                //     Console.WriteLine("ind " + ind + "\n");
            }
            else { Console.WriteLine("divide by zero error"); }

            return gVal;
        }

        private double DigitalFilter(double data)// PERFORMS 60 POINT DIGITAL FILTER ON GRAVITY
        {
            int[] arr1 = new int[10];
            int K;
            double dfilt;

            //  Console.WriteLine("Digital filter\n");
            // FILTER WEIGHTS
            double[] filterWeights = {0, -.00034, -.00038, -.00041, -.00044, -.00046, -.00046, // 1
                                      -.00044, -.00039,  .00030, -.00015,  .00007,  .00037, // 2
                                       .00079,  .00133,  .00202,  .00289,  .00396,  .00526, // 3
                                       .00679,  .00859,  .01066,  .01299,  .01558,  .01841, // 4
                                       .02143,  .02460,  .02785,  .03110,  .03426,  .03723, // 5
                                       .03992,  .04223,  .04408,  .04539,  .04613,  .04626, // 6
                                       .04579,  .04474,  .04315,  .04109,  .03864,  .03589, // 7
                                       .03292,  .02984,  .02671,  .02362,  .02063,  .01780, // 8
                                       .01516,  .01274,  .01056,  .00863,  .00694,  .00548, // 9
                                       .00424,  .00321,  .00235,  .00166,  .00111,  .00068 };//10

            nPoint++;
            if (nPoint > 60)
            { nPoint = 1; }

            GT[nPoint] = data;
            //        Console.WriteLine("GT[" + nPoint + "] " + GT[nPoint]);
            K = nPoint;
            dfilt = 0;

            for (int i = 1; i < 61; i++)
            {
                K++;
                if (K > 60) { K = 1; }

                dfilt = dfilt + filterWeights[i] * (GT[K] - GT[1]);
                if (dfilt < -1000)
                {
                    //Console.WriteLine(dfilt);
                }
                if (K > 550)
                {
                    Console.WriteLine("Filter weight " + filterWeights[i]);
                    Console.WriteLine("GT[" + K + "]  " + GT[K]);
                    Console.WriteLine("GT[1] " + GT[1]);
                    Console.WriteLine("dfilt " + dfilt + "\n");
                }
            }
            dfilt = dfilt + GT[1];
            return dfilt;
        }

        private void AnalogFilter()
        {
            data4[1] = data4[1] + (rAwg - data4[1]) / cper;  // Gravity
            data4[21] = data4[21] + (data4[5] - data4[21]) / cper;  // AV B
            data4[22] = data4[22] + (data4[4] - data4[22]) / cper;  // Cross Coupling
            data4[23] = data4[23] + (totalCorrection - data4[23]) / cper;          // TC
        }
    }
}