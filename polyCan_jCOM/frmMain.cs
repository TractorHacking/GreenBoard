using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace polyCan_jCOM
{
    public partial class frmMain : Form
    {
        public static bool bFormActivated = false;
        public static bool bGatewayResetAtProgramStart = false;
        public static StreamWriter dataFile;
        public static TimeSpan timeDiff;
        public static DateTime start;
        public static int cmdFoundCount = 0;
        public static byte nSrcAddr = 254;

        // Control flags for serial data receive buffer, i.e. the handling of receiving serial data
        // per interrupt and processing them with the jCOM1939.RX_J1939 function call.
        public static bool bSerialDataReceive = false;      // Managed by COMPort.DataReceivedHandler
        public static bool bSerialDataProcess = false;      // Managed by jCOM1939.RX_J1939

        // Node address default settings ---------------------------------------------
        public const byte SRCADDR = 128;
        public const byte ADDRBOTTOM = 129;
        public const byte ADDRTOP = 247;

        // Address Claim Procedure ---------------------------------------------------
        public const long PGNRequest = 59904;                       // 0xEA00
        public const long PGNAddressClaimed = 60928;                // 0xEE00

        // NAME Default Settings -----------------------------------------------------
        public const long NAME_IdentityNumber = 0x1FFFFF;
        public const long NAME_ManufacturerCode = 0x7FF;
        public const long NAME_FunctionInstance = 0;
        public const long NAME_ECUInstance = 0;
        public const long NAME_Function = 0xFF;
        public const long NAME_Reserved = 0;
        public const long NAME_VehicleSystem = 0x7F;
        public const long NAME_VehicleSystemInstance = 0;
        public const long NAME_IndustryGroup = 0;
        public const long NAME_ArbitraryAddressCapable = 1;

        // NAME ----------------------------------------------------------------------
        public static long lIdentityNumber = NAME_IdentityNumber;
        public static long lManufacturerCode = NAME_ManufacturerCode;
        public static long lFunctionInstance = NAME_FunctionInstance;
        public static long lECUInstance = NAME_ECUInstance;
        public static long lFunction = NAME_Function;
        public static long lVehicleSystem = NAME_VehicleSystem;
        public static long lVehicleSystemInstance = NAME_VehicleSystemInstance;
        public static long lIndustryGroup = NAME_IndustryGroup;
        public static long lArbitraryAddressCapable = NAME_ArbitraryAddressCapable;

        public byte[] pNAME = new byte[8];
        //Used to read a log file to be transmitted
        System.IO.StreamReader readFile = null;
        //boolean if we are sending a log
        public bool sendingLog = false;
        //count of how many commands sent
        public int cmdSent = 0;
        //----------------------------------------------------------------------------
        // Initialize Form 
        //----------------------------------------------------------------------------
        public frmMain()
        {
            InitializeComponent();
            
        }

        //-SUB------------------------------------------------------------------------
        // Event : frmMain_Activated
        //----------------------------------------------------------------------------
        private void frmMain_Activated(object sender, EventArgs e)
        {
            // Let's have it called only once...
            if (bFormActivated == false)
            {
                bFormActivated = true;

                if (COMPort.Initialize(-1) != 0)
                    MessageBox.Show("No COM Port available!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    // Fill the COM combobox
                    for (int nIndex = 0; nIndex < COMPort.nPorts; nIndex++)
                        cboCOMPort.Items.Add(COMPort.sPorts[nIndex]);

                    // Select the combobox index
                    cboCOMPort.SelectedIndex = COMPort.nSelPort;

                    // Close the serial port
                    COMPort._serialport.Close();

                    // Enable the Start button
                    btnStart.Enabled = true;

                }// end else

            }// end if

        }// end frmMain_Activated

        //-SUB------------------------------------------------------------------------
        // Event : frmMain_FormClosing
        //----------------------------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
                // Make sure the COM port is open to receive the reset command
                COMPort.Initialize(cboCOMPort.SelectedIndex);

                // Reset the gateway
                jCOM1939.RESET_J1939();

                // Close the COM port
                COMPort.Terminate();

        }// end frmMain_FormClosing

        //-SUB------------------------------------------------------------------------
        // Event : btnStart_Click
        //----------------------------------------------------------------------------
        private void btnStart_Click(object sender, EventArgs e)
        {
            String fileName  = "logs/JcomData"+DateTime.Now.ToString("MM-dd--hh-mm") + ".csv";
            if (COMPort.Initialize(cboCOMPort.SelectedIndex) == 0)
            {
                timerLoop.Start();

                // Enable/disable start/stop buttons
                btnStart.Enabled = false;
                btnStop.Enabled = true;

                // Enable further buttons
                btnRequestStatus.Enabled = true;
                btnClaimAddr.Enabled = true;
                btnAddFilter.Enabled = true;
                btnDelFilter.Enabled = true;
                loadLog.Enabled = true;
                TrLog.Enabled = true;

                // Reset the gateway
                jCOM1939.RESET_J1939();
                nSrcAddr = 0;

                // Set the Request message filter
                jCOM1939.FA_J1939(PGNRequest);

                jCOM1939.FA_J1939(jCOM1939.FILTER_PASS_ALL);
                
                // Set the gateway mode
                jCOM1939.MM_J1939(jCOM1939.MSGMODE_GATEWAY2);
                dataFile = new StreamWriter(fileName);
                string header = "time , pgn , destination , source , priority , data,\n";
                txtGatewayLog.Text = "";
                dataFile.WriteLine(header);
                start = DateTime.Now;
                cmdFoundCount = 0;
                cmdsGotten.Text = "0";
                foundCmd.Text = "0";
                addr.Text = "0";
            }// end if
            else
                MessageBox.Show("Sorry! There is a problem with the COM port.", "Attention!");

        }// end btnStart_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnStop_Click
        //----------------------------------------------------------------------------
        private void btnStop_Click(object sender, EventArgs e)
        {
            // Enable/disable start/stop buttons
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            // Disable further buttons
            btnRequestStatus.Enabled = false;
            btnClaimAddr.Enabled = false;
            btnAddFilter.Enabled = false;
            btnDelFilter.Enabled = false;
            btnTransmit.Enabled = false;
            loadLog.Enabled = false;
            TrLog.Enabled = false;
            timerLoop.Stop();
            COMPort.Terminate();
            dataFile.Close();

        }// end buttonStopCOM_Click

        //-SYSTEM LOOP ---------------------------------------------------------------
        // Event : timerLoop_Tick
        //----------------------------------------------------------------------------
        private void timerLoop_Tick(object sender, EventArgs e)
        {
            // Declarations
            long lPGN = 0;
            int nDest = 0;
            int nSrc = 0;
            int nPriority = 0;
            byte[] nData = new byte[8]; // Will be resized with the RX_1939 function call
            int nDataLen = 0;

            int nReceiveMode = jCOM1939.RX_J1939(ref lPGN, ref nDest, ref nSrc, ref nPriority, ref nData, ref nDataLen);

            // Process the message by type
            switch (nReceiveMode)
            {
                case jCOM1939.RX_NoMessage:
                    break;

                case jCOM1939.RX_FaultyMessage:
                    break;

                case jCOM1939.RX_Message:
                    // In the following we display the data only.
                    // You can also add srource address, destination address, priority, etc. as received
                    // through the RX_1939 call.
                    
                    string sRow = lPGN.ToString();
                    sRow += ", ";
                    sRow += nDest.ToString();
                    sRow += ", ";
                    sRow += nSrc.ToString();
                    sRow += ", ";
                    sRow += nPriority.ToString();
                    sRow += ", ";

                    // Convert the data
                    for (int nIndex = 0; nIndex < nDataLen; nIndex++)
                    {
                        string sData = String.Format("{0:X}", nData[nIndex]);
                        if (sData.Length == 1)
                            sData = "0" + sData;

                        sRow += sData + " ";

                    }// end for

                    //UpdateGatewayLog("RXDATA - " + sRow, true);
                    timeDiff = DateTime.Now - start;
                    //cmds[cmdFoundCount] = timeDiff.TotalSeconds.ToString("000.000000") + ", " + sRow;
                    dataFile.WriteLine(timeDiff.TotalSeconds.ToString("000.000000") + ", " + sRow);
                    cmdFoundCount++;
                    break;

                case jCOM1939.RX_RS:

                    string sStatus = "";

                    // Translate the status code into text
                    switch (nData[0])
                    {
                        case 1:
                            sStatus = "Address Claim in Progress.";
                            break;

                        case 2:
                            sStatus = "Address Claim Successful. SA = " + nData[1].ToString();
                            TrLog.Enabled = true;
                            // Store the negotiated address
                            nSrcAddr = nData[1];

                            break;

                        case 3:
                            sStatus = "Address Claim Failed.";
                            break;

                        case 4:
                            sStatus = "Listen-Only Mode.";
                            break;

                    }// end switch

                    // Update the gateway log
                    UpdateGatewayLog("REPSTATUS - " + sStatus, true);

                    break;

                case jCOM1939.RX_HEART:

                    //UpdateGatewayLog("HEART ", true);
                    break;

                case jCOM1939.RX_ACK_FA:
                    UpdateGatewayLog("ADDFILTER - ACK", true);
                    break;

                case jCOM1939.RX_ACK_FD:
                    UpdateGatewayLog("DELFILTER - ACK", true);
                    break;

                /*
                 * This is the message we recieve from the board whenever it sucessfully sent out of command
                 * Use this to throllte the output of the GreenBoard, wait till it says it sucessfully sent out 
                 * a command then send the next one
                 */
                case jCOM1939.RX_ACK_TX:
                    cmdSent++;
                    cmdsGotten.Text = cmdSent + "";
                   // UpdateGatewayLog("TXDATA - ACK", true);
                    sendNextLogFile();
                    break;

                case jCOM1939.RX_ACK_RESET:
                    UpdateGatewayLog("RESET - ACK", true);
                    break;

                case jCOM1939.RX_ACK_SETPARAM:
                    UpdateGatewayLog("SETPARAM - ACK", true);
                    break;

                case jCOM1939.RX_ACK_SETPARAM1:
                    UpdateGatewayLog("SETPARAM1 - ACK", true);
                    break;

                case jCOM1939.RX_ACK_MSGMODE:
                    UpdateGatewayLog("SETMSGMODE - ACK", true);
                    break;

                case jCOM1939.RX_ACK_TXL:
                    UpdateGatewayLog("TXDATA LOOPBACK - ACK", true);
                    break;

                case jCOM1939.RX_ACK_TXP:
                    UpdateGatewayLog("TXDATAP LOOPBACK - ACK", true);
                    break;

                case jCOM1939.RX_ACK_TXPL:
                    UpdateGatewayLog("TXDATAPL LOOPBACK - ACK", true);
                    break;

                case jCOM1939.RX_ACK_SETACK:
                    UpdateGatewayLog("SETACK - ACK", true);
                    break;

                case jCOM1939.RX_ACK_SETHEART:
                    UpdateGatewayLog("SETHEART - ACK", true);
                    break;

                case jCOM1939.RX_ACK_FLASH:
                    UpdateGatewayLog("FLASH - ACK", true);
                    break;

            }// end switch

        }// end timerLoop_Tick

        //-SUB----------------------------------------------------------------------
        // Routine     : UpdateGatewayLog
        // Description : Print to Real Time Monitor
        // -------------------------------------------------------------------------
        public void UpdateGatewayLog(string sData, bool CrLf)
        {
            txtGatewayLog.Text += sData;
            if (CrLf == true)
                txtGatewayLog.Text += "\n\r\n\r";

            txtGatewayLog.SelectionStart = txtGatewayLog.TextLength;
            txtGatewayLog.ScrollToCaret();
            txtGatewayLog.Refresh();

        }// end UpdateGatewayLog

        //-SUB------------------------------------------------------------------------
        // Event : btnClearGatewayLog_Click
        //----------------------------------------------------------------------------
        private void btnClearGatewayLog_Click(object sender, EventArgs e)
        {
            // CLear the gateway log
            txtGatewayLog.Text = "";

        }// end btnClearGatewayLog_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnRequestStatus_Click
        //----------------------------------------------------------------------------
        private void btnRequestStatus_Click(object sender, EventArgs e)
        {
            // Send out the REQUEST command to request the status
            jCOM1939.RQ_J1939(jCOM1939.MSG_ID_RS);
 }// end btnRequestStatus_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnClaimAddr_Click
        //----------------------------------------------------------------------------
        private void btnClaimAddr_Click(object sender, EventArgs e)
        {
            // Fill the NAME array
            pNAME[0] = (byte)(lIdentityNumber & 0xFF);
            pNAME[1] = (byte)((lIdentityNumber >> 8) & 0xFF);
            pNAME[2] = (byte)(((lManufacturerCode << 5) & 0xFF) | (lIdentityNumber >> 16));
            pNAME[3] = (byte)(lManufacturerCode >> 3);
            pNAME[4] = (byte)((lFunctionInstance << 3) | lECUInstance);
            pNAME[5] = (byte)(lFunction);
            pNAME[6] = (byte)(lVehicleSystem << 1);
            pNAME[7] = (byte)((lArbitraryAddressCapable << 7) | (lIndustryGroup << 4) | (lVehicleSystemInstance));
            long address;
            if (!Int64.TryParse(addr.Text, out address)) { 
                txtGatewayLog.Text += "Address not valid!\n";
                return;
            }
            jCOM1939.SET_J1939((byte)address, ADDRBOTTOM, ADDRTOP, jCOM1939.OPMODE_Event, pNAME, true);

            // Initiate the address claim process; request status feedback

        }// end btnClaimAddr_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnAddFilter_Click
        //----------------------------------------------------------------------------
        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            jCOM1939.FA_J1939(65280);

        }// end btnAddFilter_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnDelFilter_Click
        //----------------------------------------------------------------------------
        private void btnDelFilter_Click(object sender, EventArgs e)
        {
            jCOM1939.FD_J1939(65280);

        }// end btnDelFilter_Click

        //-SUB------------------------------------------------------------------------
        // Event : btnTransmit_Click
        //----------------------------------------------------------------------------
        private void btnTransmit_Click(object sender, EventArgs e)
        {
            byte[] msg_Data = { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 };
       
            if(nSrcAddr == 254) // Check for Null Address
                MessageBox.Show("You need to claim a node address before transmitting data.", "Attention!");
            else
                jCOM1939.TX_J1939(65281, 255, (int)nSrcAddr, 6, msg_Data, 8, true);
                //jCOM1939.TXP_J1939(65281, 255, (int)nSrcAddr, 6, msg_Data, 8, 1000, true);

        }// end btnTransmit_Click

        private void updateLoop_Tick(object sender, EventArgs e)
        {
            foundCmd.Text = cmdFoundCount.ToString();
        }

        /*
         * loads a log file into buffer stream reader to be later sent out
         */
        private void loadLogAction(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)  
                {  
                     readFile = new   
                      System.IO.StreamReader(openFileDialog.FileName);  
                 }
            }
        }
        /*
        * Will send out the next command in the log file, keeps track by reading through a csv file
        * Parses the csv file into the components and sends out to the GreenBoard
         */
        private void sendNextLogFile()
        {
            long pgn;
            int dest;
            int source;
            int dataLen;
            int prior;
            String line;
            byte[] msg_Data = new byte[48];
            while (true)
               
            {
                line = readFile.ReadLine();
                if (line == null) {
                    sendingLog = false;
                    UpdateGatewayLog("all transmitted!",true);
                    return;
                }
                try
                {
                    line = line.Substring(line.IndexOf(',')+1);//cut off time
                    pgn = long.Parse(line.Substring(0, line.IndexOf(',')));//get pgn
                    line = line.Substring(line.IndexOf(',')+1);//cut off pgn
                    dest = int.Parse(line.Substring(0, line.IndexOf(',')));//get dest
                    line = line.Substring(line.IndexOf(',')+1);//cut off dest
                    source = int.Parse(line.Substring(0, line.IndexOf(',')));//get source
                    line = line.Substring(line.IndexOf(',')+1);//cut off source
                    prior = int.Parse(line.Substring(0, line.IndexOf(',')));//get prior
                    line = line.Substring(line.IndexOf(',')+1);//cut off prioir
                    dataLen = 0;
                    while (line.Length > 2)
                    {
                        System.Globalization.NumberStyles style = System.Globalization.NumberStyles.AllowHexSpecifier;
                        String s = line.Substring(0, line.IndexOf(' '));
                        line = line.Substring(line.IndexOf(' ')+1);//cut off data piece
                        if (s.Length < 2)
                        {
                            continue;
                        }
                        msg_Data[dataLen] = byte.Parse(s, style);
                        dataLen++;
                    }

                }
                catch (FormatException e)
                {
                    continue;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    continue;
                }
                break;
            }
            jCOM1939.TX_J1939(pgn, dest, source,prior, msg_Data, dataLen, false);
            if (readFile.EndOfStream)
            {

                sendingLog = false;
                UpdateGatewayLog("all transmitted!",true);
            }

        }

        /*
         * transmit log file, this is called when   user clicks on the transfer log file button
         * it reads the file selected and starts transmitting the log file with the command sendNextLogFile
         */
        private void trLogFile(object sender, EventArgs e)
        {
            if (readFile == null)
            {
                txtGatewayLog.Text += "No file selected!\n";
                return;
            }
            readFile.DiscardBufferedData();
            readFile.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            sendingLog = true;
            readFile.ReadLine();
            sendNextLogFile();
        }
    }// end class

}// end namespace
