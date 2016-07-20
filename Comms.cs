using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Globalization;

namespace SerialPortTerminal
{
   public class Comms
    {
       
        public SerialPort comport = new SerialPort();
        private RelaySwitches RelaySwitches = new RelaySwitches();
        private ConfigData ConfigData = new ConfigData();
        private ControlSwitches ControlSwitches = new ControlSwitches();
        public CalculateMarineData mdt = new CalculateMarineData();
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
/*
        public void sendCmd(string cmd)
        {
            byte[] data;
            switch (cmd)
            //OpenSerialPort();
                    
            {
                case "Send Relay Switches": // 0

                    data = CreateTxArray(0, RelaySwitches.relaySW);
                //    Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    comport.Write(data, 0, 3);
                  //  textBox24.Text = ByteArrayToHexString(data);
                    break;

                case "Send Control Switches"://1
                    data = CreateTxArray(1, ControlSwitches.controlSw);
               //     Log(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n");
                    comport.Write(data, 0, 3);
              //      textBox24.Text = ByteArrayToHexString(data);
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

                //    textBox24.Text = ByteArrayToHexString(data);
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

              //      textBox24.Text = ByteArrayToHexString(data);
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

               //     textBox24.Text = ByteArrayToHexString(data);
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

*/

    }
}
