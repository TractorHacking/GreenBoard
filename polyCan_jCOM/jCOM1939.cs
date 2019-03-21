using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyCan_jCOM
{
    // ----------------------------------------------------------------------------
    // jCOM1939 Communication Module
    // ----------------------------------------------------------------------------
    class jCOM1939
    {
        // ------------------------------------------------------------------------
        // Misc. constants
        // ------------------------------------------------------------------------
        public const long FILTER_PASS_ALL = 0x100000;

        // ------------------------------------------------------------------------
        // Message Format
        // ------------------------------------------------------------------------
        public const byte MSG_MINLENGTH = 5;        // Minimum total length of a message
        // including overhead and stuff bytes

        public const byte MSG_TOKEN_START = 192;
        public const byte MSG_TOKEN_ESC = 219;
        public const byte MSG_START_STUFF = 220;
        public const byte MSG_ESC_STUFF = 221;

        // Message IDs
        // ------------------------------------------------------------------------
        // Gateway firmware version 1.00 and newer:
        public const byte MSG_ID_ACK = 0;
        public const byte MSG_ID_FA = 1;
        public const byte MSG_ID_FD = 2;
        public const byte MSG_ID_TX = 3;
        public const byte MSG_ID_RX = 4;
        public const byte MSG_ID_RESET = 5;
        public const byte MSG_ID_HEART = 6;
        public const byte MSG_ID_SETPARAM = 7;
        public const byte MSG_ID_RQ = 8;
        public const byte MSG_ID_RS = 9;
        public const byte MSG_ID_FLASH = 10;
        public const byte MSG_ID_SETACK = 11;
        public const byte MSG_ID_SETHEART = 12;
        public const byte MSG_ID_VERSION = 13;
        public const byte MSG_ID_SETPARAM1 = 14;
        public const byte MSG_ID_SETMSGMODE = 15;
        public const byte MSG_ID_TXL = 16;
        public const byte MSG_ID_TXP = 17;
        public const byte MSG_ID_TXPL = 18;

        // Message Lenghts
        public const byte MSG_LEN_MSB = 0;
        public const byte MSG_LEN_STATS = 16;
        public const byte MSG_LEN_ACK = 3;
        public const byte MSG_LEN_RX = 8;      // Based on 1 data byte
        public const byte MSG_LEN_TX = 8;      // Based on 1 data byte
        public const byte MSG_LEN_FA = 5;
        public const byte MSG_LEN_RQ = 3;
        public const byte MSG_LEN_FLASH = 2;
        public const byte MSG_LEN_SET = 17;
        public const byte MSG_LEN_SETMSGMODE = 3;
        public const byte MSG_LEN_SETACK = 3;
        public const byte MSG_LEN_SETHEART = 4;
        public const byte MSG_LEN_TXP = 10;     // Based on 1 data byte

        public const int MSG_IDX_MSB = 1;
        public const int MSG_IDX_LSB = 2;
        public const int MSG_IDX_ID = 3;
        public const int MSG_IDX_ACK_MSG = 4;

        public const int RXTX_IDX_MSGSTART = 0;
        public const int RXTX_IDX_MSGLENMSB = 1;
        public const int RXTX_IDX_MSGLENLSB = 2;
        public const int RXTX_IDX_MSGID = 3;
        public const int RXTX_IDX_PGNMSB = 4;
        public const int RXTX_IDX_PGN2ND = 5;
        public const int RXTX_IDX_PGNLSB = 6;
        public const int RXTX_IDX_DESTADDR = 7;
        public const int RXTX_IDX_SRCADDR = 8;
        public const int RXTX_IDX_PRIORITY = 9;
        public const int RXTX_IDX_DATASTART = 10;

        public const int TXP_IDX_MSGSTART = 0;
        public const int TXP_IDX_MSGLENMSB = 1;
        public const int TXP_IDX_MSGLENLSB = 2;
        public const int TXP_IDX_MSGID = 3;
        public const int TXP_IDX_PGNMSB = 4;
        public const int TXP_IDX_PGN2ND = 5;
        public const int TXP_IDX_PGNLSB = 6;
        public const int TXP_IDX_DESTADDR = 7;
        public const int TXP_IDX_SRCADDR = 8;
        public const int TXP_IDX_PRIORITY = 9;
        public const int TXP_IDX_FREQMSB = 10;
        public const int TXP_IDX_FREQLSB = 11;
        public const int TXP_IDX_DATASTART = 12;

        public const int FA_IDX_MSGID = 3;
        public const int FA_IDX_PGN_MSB = 4;
        public const int FA_IDX_PGN_2ND = 5;
        public const int FA_IDX_PGN_LSB = 6;
        public const int FA_IDX_CHKSUM = 7;

        public const int HEART_IDX_HW_MAJOR = 4;
        public const int HEART_IDX_HW_MINOR = 5;
        public const int HEART_IDX_HW_BUGFIX = 6;
        public const int HEART_IDX_SW_MAJOR = 7;
        public const int HEART_IDX_SW_MINOR = 8;
        public const int HEART_IDX_SW_BUGFIX = 9;
        public const int HEART_IDX_CHECKSUMERRORS = 10;
        public const int HEART_IDX_STUFFBYTEERRORS = 11;

        public const int RS_IDX_STATUS = 4;
        public const int RS_IDX_SA = 5;

        public const int SET_IDX_NAME = 4;
        public const int SET_IDX_SA = 12;
        public const int SET_IDX_BOTTOM = 13;
        public const int SET_IDX_TOP = 14;
        public const int SET_IDX_OPMODE = 15;
        public const int SET_IDX_CHKSUM = 16;

        public const int SETMSGMODE_IDX_MODE = 4;
        public const int SETMSGMODE_IDX_CHKSUM = 5;

        public const int SETACK_IDX_ACTIVE = 4;
        public const int SETACK_IDX_CHKSUM = 5;

        public const int SETHEART_IDX_FREQMSB = 4;
        public const int SETHEART_IDX_FREQLSB = 5;
        public const int SETHEART_IDX_CHKSUM = 6;

        // Operation Mode
        public const int OPMODE_ListenOnly = 0;
        public const int OPMODE_Event = 1;
        public const int OPMODE_Polling = 2;

        // Message Mode
        public const byte MSGMODE_ECU = 0;
        public const byte MSGMODE_GATEWAY1 = 1;
        public const byte MSGMODE_GATEWAY2 = 2;

        // Messages Templates --------------------------------------------------------
        public const byte DATA = 0;
        public const byte CHKSUM = 0xFF;

        public const byte SA_DEMO = 128;
        public const byte SA_BOTTOM = 128;
        public const byte SA_TOP = 253;

        public static byte[] msg_FA = { MSG_TOKEN_START, MSG_LEN_MSB, MSG_LEN_FA, MSG_ID_FA, DATA, DATA, DATA, CHKSUM };
        public static byte[] msg_RESET = { MSG_TOKEN_START, MSG_LEN_MSB, 0x05, 0x05, 0xA5, 0x69, 0x5A, CHKSUM };
        public static byte[] msg_SET = { MSG_TOKEN_START, MSG_LEN_MSB, 14, MSG_ID_SETPARAM, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, SA_DEMO, SA_BOTTOM, SA_TOP, 0x00, CHKSUM };
        public static byte[] msg_SET1 = { MSG_TOKEN_START, MSG_LEN_MSB, 14, MSG_ID_SETPARAM1, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, SA_DEMO, SA_BOTTOM, SA_TOP, 0x00, CHKSUM };
        public static byte[] msg_RQ = { MSG_TOKEN_START, MSG_LEN_MSB, MSG_LEN_RQ, MSG_ID_RQ, 0x00, CHKSUM };
        public static byte[] msg_SETMSGMODE = { MSG_TOKEN_START, MSG_LEN_MSB, MSG_LEN_SETMSGMODE, MSG_ID_SETMSGMODE, MSGMODE_ECU, CHKSUM };
        public static byte[] msg_SETACK = { MSG_TOKEN_START, MSG_LEN_MSB, MSG_LEN_SETACK, MSG_ID_SETACK, 0x01, CHKSUM };
        public static byte[] msg_SETHEART = { MSG_TOKEN_START, MSG_LEN_MSB, MSG_LEN_SETHEART, MSG_ID_SETHEART, 0x00, 0x00, CHKSUM };

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : SH_J1939
        // Description : Set heartbeat frequency in milliseconds
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void SH_J1939(int nFrequency)
        {
            msg_SETHEART[SETHEART_IDX_FREQMSB] = (byte)(nFrequency >> 8);
            msg_SETHEART[SETHEART_IDX_FREQLSB] = (byte)(nFrequency & 0xFF);
            msg_SETHEART[SETHEART_IDX_CHKSUM] = ComputeCheckSum(msg_SETHEART);

            // Transmit the message to the COM port
            COMPort.Transmit(msg_SETHEART, 7);

        }// end SH_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : SA_J1939
        // Description : Set ACK message active (=1) / inactive (=0)
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void SA_J1939(byte nActive)
        {
            msg_SETACK[SETACK_IDX_ACTIVE] = nActive;
            msg_SETACK[SETACK_IDX_CHKSUM] = ComputeCheckSum(msg_SETACK);

            // Transmit the message to the COM port
            COMPort.Transmit(msg_SETACK, 6);

        }// end SA_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : RQ_J1939
        // Description : Request info from the gateway
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void RQ_J1939(byte nID)
        {
            msg_RQ[4] = nID;
            msg_RQ[5] = ComputeCheckSum(msg_RQ);

            // Transmit the message to the COM port
            COMPort.Transmit(msg_RQ, 6);

        }// end RQ_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : RESET_J1939
        // Description : Reset the gateway
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void RESET_J1939()
        {
            msg_RESET[7] = ComputeCheckSum(msg_RESET);

            // Transmit the message to the COM port
            COMPort.Transmit(msg_RESET, 8);

            System.Threading.Thread.Sleep(100); // Time in msec

        }// end RESET_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : MM_J1939
        // Description : Set the gateway's message mode
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void MM_J1939(byte nMsgMode)
        {
            msg_SETMSGMODE[SETMSGMODE_IDX_MODE] = nMsgMode;
            msg_SETMSGMODE[SETMSGMODE_IDX_CHKSUM] = ComputeCheckSum(msg_SETMSGMODE);

            // Transmit the message to the COM port
            COMPort.Transmit(msg_SETMSGMODE, MSG_LEN_SETMSGMODE + 3);

        }// end MM_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : SET_J1939
        // Description : Set J1939 Parameters
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void SET_J1939(byte nSrcAddr, byte nAddrBottom, byte nAddrTop, byte nOpMode, byte[] pNAME, bool bRequestRESTATUSMessage)
        {
            if (bRequestRESTATUSMessage == true)
            {
                msg_SET1[SET_IDX_SA] = nSrcAddr;
                msg_SET1[SET_IDX_BOTTOM] = nAddrBottom;
                msg_SET1[SET_IDX_TOP] = nAddrTop;
                msg_SET1[SET_IDX_OPMODE] = nOpMode;

                for (int nIndex = 0; nIndex < 8; nIndex++)
                    msg_SET1[nIndex + SET_IDX_NAME] = pNAME[nIndex];

                msg_SET1[SET_IDX_CHKSUM] = ComputeCheckSum(msg_SET1);

                // Transmit the message to the COM port
                COMPort.Transmit(msg_SET1, MSG_LEN_SET);

            }// end if
            else
            {
                msg_SET[SET_IDX_SA] = nSrcAddr;
                msg_SET[SET_IDX_BOTTOM] = nAddrBottom;
                msg_SET[SET_IDX_TOP] = nAddrTop;
                msg_SET[SET_IDX_OPMODE] = nOpMode;

                for (int nIndex = 0; nIndex < 8; nIndex++)
                    msg_SET[nIndex + SET_IDX_NAME] = pNAME[nIndex];

                msg_SET[SET_IDX_CHKSUM] = ComputeCheckSum(msg_SET);

                // Transmit the message to the COM port
                COMPort.Transmit(msg_SET, MSG_LEN_SET);

            }// end else

        }// end SET_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : FA_J1939
        // Description : Creates filter message, adds PGN & checksum, and sends it
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void FA_J1939(long lPGN)
        {
            // Fill the Message Identifier
            msg_FA[FA_IDX_MSGID] = MSG_ID_FA;

            // Call the FA_FD_J1939 function
            FA_FD_J1939(lPGN);

        }// end FA_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : FD_J1939
        // Description : Creates filter message, adds PGN & checksum, and sends it
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void FD_J1939(long lPGN)
        {
            // Fill the Message Identifier
            msg_FA[FA_IDX_MSGID] = MSG_ID_FD;

            // Call the FA_FD_J1939 function
            FA_FD_J1939(lPGN);

        }// end FD_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : FA_FD_1939
        // Description : Creates filter message, adds PGN & checksum, and sends it
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void FA_FD_J1939(long lPGN)
        {
            // Declarations
            int nMsgLen = MSG_LEN_FA + 3;

            // Fill the port number and PGN
            msg_FA[FA_IDX_PGN_MSB] = (byte)((lPGN & 0xFF0000) >> 16);
            msg_FA[FA_IDX_PGN_2ND] = (byte)((lPGN & 0x00FF00) >> 8);
            msg_FA[FA_IDX_PGN_LSB] = (byte)(lPGN & 0x0000FF);

            // Process and fill the checksum (does not include stuff bytes)
            msg_FA[FA_IDX_CHKSUM] = ComputeCheckSum(msg_FA);

            // Determine the total message length by scanning for START and ESC tokens
            for (int nIndex = 1; nIndex < MSG_LEN_FA + 3; nIndex++)
                if (msg_FA[nIndex] == MSG_TOKEN_START
                || msg_FA[nIndex] == MSG_TOKEN_ESC)
                    nMsgLen++;
            if (msg_FA[FA_IDX_CHKSUM] == MSG_TOKEN_START
            || msg_FA[FA_IDX_CHKSUM] == MSG_TOKEN_ESC)
                nMsgLen++;

            // Resize the message to be transmitted
            byte[] pMsg = new byte[nMsgLen];

            // Copy the message; insert stuff bytes where necessary
            pMsg[0] = MSG_TOKEN_START;  // Insert the START token
            int nPointer = 1;
            for (int nIndex = 1; nIndex < MSG_LEN_FA + 3; nIndex++)
            {
                if (msg_FA[nIndex] == MSG_TOKEN_START)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_START_STUFF;
                }// end if
                else if (msg_FA[nIndex] == MSG_TOKEN_ESC)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_ESC_STUFF;
                }// end else if
                else
                    pMsg[nPointer++] = msg_FA[nIndex];

            }// end for

            // Transmit the message to the COM port
            COMPort.Transmit(pMsg, nMsgLen);

        }// end FA_FD_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : TX_J1939
        // Description : Creates transmit message, adds parameters & checksum,
        //               and sends it
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void TX_J1939(long lPGN, int nDest, int nSrc, int nPriority, byte[] nData, int nDataLen, bool bLoopback)
        {
            // Declarations
            int nMsgLenUnstuffed = MSG_LEN_TX + nDataLen;

            // Fill all parameters
            byte[] pUnstMsg = new byte[nMsgLenUnstuffed + 3]; // Add header length

            pUnstMsg[RXTX_IDX_MSGSTART] = MSG_TOKEN_START;
            pUnstMsg[RXTX_IDX_MSGLENMSB] = (byte)((nMsgLenUnstuffed & 0xFF00) >> 8);
            pUnstMsg[RXTX_IDX_MSGLENLSB] = (byte)(nMsgLenUnstuffed & 0xFF);

            if (bLoopback == true)
                pUnstMsg[RXTX_IDX_MSGID] = MSG_ID_TXL;
            else
                pUnstMsg[RXTX_IDX_MSGID] = MSG_ID_TX;

            pUnstMsg[RXTX_IDX_PGNMSB] = (byte)((lPGN & 0xFF0000) >> 16);
            pUnstMsg[RXTX_IDX_PGN2ND] = (byte)((lPGN & 0x00FF00) >> 8);
            pUnstMsg[RXTX_IDX_PGNLSB] = (byte)(lPGN & 0x0000FF);
            pUnstMsg[RXTX_IDX_DESTADDR] = (byte)nDest;
            pUnstMsg[RXTX_IDX_SRCADDR] = (byte)nSrc;
            pUnstMsg[RXTX_IDX_PRIORITY] = (byte)nPriority;

            // Add actual data without stuff bytes
            nMsgLenUnstuffed += 3; // Add the header length
            int nPointer = RXTX_IDX_DATASTART;
            for (int nIndex = 0; nIndex < nDataLen; nIndex++)
                pUnstMsg[nPointer++] = nData[nIndex];

            // Process and fill the checksum (does not include stuff bytes)
            pUnstMsg[nPointer++] = ComputeCheckSum(pUnstMsg);

            // Determine the total message length by scanning for START and ESC tokens
            int nMsgLenStuffed = nPointer;
            for (int nIndex = 1; nIndex < nPointer; nIndex++)
                if (pUnstMsg[nIndex] == MSG_TOKEN_START
                || pUnstMsg[nIndex] == MSG_TOKEN_ESC)
                    nMsgLenStuffed++;

            // Resize the message to be transmitted
            byte[] pMsg = new byte[nMsgLenStuffed];

            // Copy the message; insert stuff bytes where necessary
            pMsg[0] = MSG_TOKEN_START;  // Insert the START token
            nPointer = 1;
            for (int nIndex = 1; nIndex < nMsgLenUnstuffed; nIndex++)
            {
                if (pUnstMsg[nIndex] == MSG_TOKEN_START)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_START_STUFF;
                }// end if
                else if (pUnstMsg[nIndex] == MSG_TOKEN_ESC)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_ESC_STUFF;
                }// end else if
                else
                    pMsg[nPointer++] = pUnstMsg[nIndex];

            }// end for

            // Transmit the message to the COM port
            COMPort.Transmit(pMsg, nMsgLenStuffed);

        }// end TX_J1939

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : TXP_J1939
        // Description : Creates periodic transmit message, adds parameters & checksum,
        //               and sends it
        // Returncode  : None
        // -------------------------------------------------------------------------
        public static void TXP_J1939(long lPGN, int nDest, int nSrc, int nPriority, byte[] nData, int nDataLen, int nInterval, bool bLoopback)
        {
            // Declarations
            int nMsgLenUnstuffed = MSG_LEN_TXP + nDataLen;

            // Fill all parameters
            byte[] pUnstMsg = new byte[nMsgLenUnstuffed + 3]; // Add header length

            pUnstMsg[TXP_IDX_MSGSTART] = MSG_TOKEN_START;
            pUnstMsg[TXP_IDX_MSGLENMSB] = (byte)((nMsgLenUnstuffed & 0xFF00) >> 8);
            pUnstMsg[TXP_IDX_MSGLENLSB] = (byte)(nMsgLenUnstuffed & 0xFF);

            if (bLoopback == true)
                pUnstMsg[TXP_IDX_MSGID] = MSG_ID_TXPL;
            else
                pUnstMsg[TXP_IDX_MSGID] = MSG_ID_TXP;

            pUnstMsg[TXP_IDX_PGNMSB] = (byte)((lPGN & 0xFF0000) >> 16);
            pUnstMsg[TXP_IDX_PGN2ND] = (byte)((lPGN & 0x00FF00) >> 8);
            pUnstMsg[TXP_IDX_PGNLSB] = (byte)(lPGN & 0x0000FF);
            pUnstMsg[TXP_IDX_DESTADDR] = (byte)nDest;
            pUnstMsg[TXP_IDX_SRCADDR] = (byte)nSrc;
            pUnstMsg[TXP_IDX_PRIORITY] = (byte)nPriority;
            pUnstMsg[TXP_IDX_FREQMSB] = (byte)((nInterval & 0xFF00) >> 8);
            pUnstMsg[TXP_IDX_FREQLSB] = (byte)(nInterval & 0x00FF);

            // Add actual data without stuff bytes
            nMsgLenUnstuffed += 3; // Add the header length
            int nPointer = TXP_IDX_DATASTART;
            for (int nIndex = 0; nIndex < nDataLen; nIndex++)
                pUnstMsg[nPointer++] = nData[nIndex];

            // Process and fill the checksum (does not include stuff bytes)
            pUnstMsg[nPointer++] = ComputeCheckSum(pUnstMsg);

            // Determine the total message length by scanning for START and ESC tokens
            int nMsgLenStuffed = nPointer;
            for (int nIndex = 1; nIndex < nPointer; nIndex++)
                if (pUnstMsg[nIndex] == MSG_TOKEN_START
                || pUnstMsg[nIndex] == MSG_TOKEN_ESC)
                    nMsgLenStuffed++;

            // Resize the message to be transmitted
            byte[] pMsg = new byte[nMsgLenStuffed];

            // Copy the message; insert stuff bytes where necessary
            pMsg[0] = MSG_TOKEN_START;  // Insert the START token
            nPointer = 1;
            for (int nIndex = 1; nIndex < nMsgLenUnstuffed; nIndex++)
            {
                if (pUnstMsg[nIndex] == MSG_TOKEN_START)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_START_STUFF;
                }// end if
                else if (pUnstMsg[nIndex] == MSG_TOKEN_ESC)
                {
                    pMsg[nPointer++] = MSG_TOKEN_ESC;
                    pMsg[nPointer++] = MSG_ESC_STUFF;
                }// end else if
                else
                    pMsg[nPointer++] = pUnstMsg[nIndex];

            }// end for

            // Transmit the message to the COM port
            COMPort.Transmit(pMsg, nMsgLenStuffed);

        }// end TXP_J1939

        public const int RX_Message = 0;
        public const int RX_NoMessage = 1;
        public const int RX_FaultyMessage = 2;
        public const int RX_HEART = 3;
        public const int RX_ACK_FA = 4;
        public const int RX_ACK_FD = 5;
        public const int RX_ACK_TX = 6;
        public const int RX_ACK_RESET = 7;
        public const int RX_ACK_SETPARAM = 8;
        public const int RX_ACK_SETPARAM1 = 9;
        public const int RX_RS = 10;
        public const int RX_ACK_MSGMODE = 11;
        public const int RX_ACK_TXL = 12;
        public const int RX_VERSION = 13;
        public const int RX_ACK_SETACK = 14;
        public const int RX_ACK_SETHEART = 15;
        public const int RX_ACK_FLASH = 16;
        public const int RX_ACK_TXP = 17;
        public const int RX_ACK_TXPL = 18;

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : RX_J1939
        // Description : Checks for received messages from the gateway
        // Returncode  : See codes above
        // -------------------------------------------------------------------------
        public static int RX_J1939(ref long lPGN, ref int nDest, ref int nSrc, ref int nPriority,
                                   ref byte[] nData, ref int nDataLen)
        {
            // Declarations
            bool bNewMsg = false;
            int nReceiveMode = RX_NoMessage;
            int nIndex = 0;
            int nMsgLen = 0;
            int nPointer = 0;
            int nMSB = 0;
            int nLSB = 0;
            int nOffset = 0;
            int nDataCounter = 0;

            try
            {
                // Regardless of the current status, read the com port
                // New data will be attached to the receive buffer
                // COMPort.Receive(); // This is the call when not using the interrupt service routine
                if (frmMain.bSerialDataReceive == false && COMPort.nCOM_ReceiveBufferSize >= MSG_MINLENGTH)
                {
                    frmMain.bSerialDataProcess = true;

                    // Extract the next message
                    // ----------------------------------------------------
                    // Find MSG_TOKEN_START
                    for (nIndex = 0; nIndex < COMPort.nCOM_ReceiveBufferSize; nIndex++)
                        if (COMPort.pCOM_ReceiveBuffer[nIndex] == MSG_TOKEN_START)
                            break;

                    // In case the first byte is NOT MSG_TOKEN_START, clean the buffer
                    if (nIndex > 0)
                        COMPort.CleanReceiveBuffer(0, nIndex);

                    // Extract some message specifics from the buffer
                    nPointer = 1;   // Points to data length MSB
                    nOffset = 1;
                    nMSB = 0;
                    nLSB = 0;

                    // Determine the actual data length MSB
                    if (COMPort.pCOM_ReceiveBuffer[nPointer] == MSG_TOKEN_ESC
                    && COMPort.pCOM_ReceiveBuffer[nPointer + 1] == MSG_START_STUFF)
                    {
                        nMSB = MSG_TOKEN_START;
                        nOffset = 2;
                    }// end if
                    else if (COMPort.pCOM_ReceiveBuffer[nPointer] == MSG_TOKEN_ESC
                            && COMPort.pCOM_ReceiveBuffer[nPointer + 1] == MSG_ESC_STUFF)
                    {
                        nMSB = MSG_TOKEN_ESC;
                        nOffset = 2;
                    }// end else if
                    else
                    {
                        nMSB = COMPort.pCOM_ReceiveBuffer[MSG_IDX_MSB];
                        nOffset = 1;
                    }// end else

                    // Determine the actual data length LSB
                    nPointer = MSG_IDX_MSB + nOffset;
                    if (COMPort.pCOM_ReceiveBuffer[nPointer] == MSG_TOKEN_ESC
                    && COMPort.pCOM_ReceiveBuffer[nPointer + 1] == MSG_START_STUFF)
                    {
                        nLSB = MSG_TOKEN_START;
                        nOffset = 2;
                    }// end if
                    else if (COMPort.pCOM_ReceiveBuffer[nPointer] == MSG_TOKEN_ESC
                            && COMPort.pCOM_ReceiveBuffer[nPointer + 1] == MSG_ESC_STUFF)
                    {
                        nLSB = MSG_TOKEN_ESC;
                        nOffset = 2;
                    }// end else if
                    else
                    {
                        nLSB = COMPort.pCOM_ReceiveBuffer[nPointer];
                        nOffset = 1;
                    }// end else
                    nPointer += nOffset; // nPointer now points to first data byte

                    // Determine the actual data length
                    nMsgLen = (nMSB << 8) + nLSB;

                    // Scan through the buffer to find the end of the message
                    nIndex = nPointer;
                    nDataCounter = 0;
                    while (nIndex < COMPort.nCOM_ReceiveBufferSize)
                    {
                        // Check for another MSG_TOKEN_START
                        if (COMPort.pCOM_ReceiveBuffer[nIndex] == MSG_TOKEN_START)
                        {
                            // Indicate that the start of another message is in the buffer
                            bNewMsg = true;
                            break;
                        }

                        // In the following we do not check if the second stuff byte exists.
                        // This method helps to detect byte stuffing errors.
                        if (COMPort.pCOM_ReceiveBuffer[nIndex] == MSG_TOKEN_ESC)
                            nOffset = 2;
                        else if (COMPort.pCOM_ReceiveBuffer[nIndex] == MSG_TOKEN_ESC)
                            nOffset = 2;
                        else
                            nOffset = 1;

                        nIndex += nOffset;
                        nDataCounter++;

                        if (nDataCounter == nMsgLen)
                            break;

                    }// end while

                    // We have a valid message when the counted data matches the reported data
                    if (nDataCounter == 0 || nDataCounter != nMsgLen)
                    {
                        // Determine whether this is an incomplete or faulty message
                        if (bNewMsg == true)
                        {
                            // The reported message length does not match the message data,
                            // but there is already a new message in the buffer, which
                            // indicates a byte stuffing error
                            nReceiveMode = RX_FaultyMessage;

                            // Clean the receive buffer
                            COMPort.CleanReceiveBuffer(0, nDataCounter + 3);

                        }// end if
                        else
                            nReceiveMode = RX_NoMessage;
                    }
                    else
                    {
                        // nIndex now points to the first byte after the message
                        // Copy the entire message
                        COMPort.nCOM_CopyBufferSize = nIndex;
                        for (nIndex = 0; nIndex < COMPort.nCOM_CopyBufferSize; nIndex++)
                            COMPort.pCOM_CopyBuffer[nIndex] = COMPort.pCOM_ReceiveBuffer[nIndex];

                        // Clean the receive buffer
                        COMPort.CleanReceiveBuffer(0, COMPort.nCOM_CopyBufferSize);

                        // Remove stuffing bytes from copy buffer
                        RemoveStuffBytes(ref COMPort.pCOM_CopyBuffer, ref COMPort.nCOM_CopyBufferSize);

                        // Verify the checksum
                        byte nCheckSum = ComputeCheckSum(COMPort.pCOM_CopyBuffer);
                        if (nCheckSum != COMPort.pCOM_CopyBuffer[nMsgLen + 2])
                            nReceiveMode = RX_FaultyMessage;
                        else
                        {
                            // Check the message identifier
                            nMsgLen = nMsgLen + 3; // Add overhead length
                            nReceiveMode = FilterSystemMessage(COMPort.pCOM_CopyBuffer);

                        }// end else

                    }// end else

                }// end if

                // If this is an actual data message, copy parameters accordingly
                switch (nReceiveMode)
                {
                    case RX_Message:

                        lPGN = ((long)COMPort.pCOM_CopyBuffer[RXTX_IDX_PGNMSB]) << 16;
                        lPGN |= ((long)COMPort.pCOM_CopyBuffer[RXTX_IDX_PGN2ND]) << 8;
                        lPGN |= ((long)COMPort.pCOM_CopyBuffer[RXTX_IDX_PGNLSB]);
                        nPriority = (int)COMPort.pCOM_CopyBuffer[RXTX_IDX_PRIORITY];
                        nSrc = (int)COMPort.pCOM_CopyBuffer[RXTX_IDX_SRCADDR];
                        nDest = (int)COMPort.pCOM_CopyBuffer[RXTX_IDX_DESTADDR];
                        nDataLen = nMsgLen - (RXTX_IDX_DATASTART + 1); // Remove the overhead length
                        nData = new byte[nDataLen];

                        int nIdx = 0;
                        for (nIndex = RXTX_IDX_DATASTART; nIndex < RXTX_IDX_DATASTART + nDataLen; nIndex++)
                            nData[nIdx++] = COMPort.pCOM_CopyBuffer[nIndex];

                        break;

                    case RX_RS:

                        nData = new byte[2];
                        nData[0] = COMPort.pCOM_CopyBuffer[RS_IDX_STATUS];
                        nData[1] = COMPort.pCOM_CopyBuffer[RS_IDX_SA];

                        break;

                    case RX_VERSION:
                        nData = new byte[6];
                        nData[0] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_MAJOR];
                        nData[1] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_MINOR];
                        nData[2] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_BUGFIX];
                        nData[3] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_MAJOR];
                        nData[4] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_MINOR];
                        nData[5] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_BUGFIX];

                        break;

                    case RX_HEART:
                        nData = new byte[8];
                        nData[0] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_MAJOR];
                        nData[1] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_MINOR];
                        nData[2] = COMPort.pCOM_CopyBuffer[HEART_IDX_HW_BUGFIX];
                        nData[3] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_MAJOR];
                        nData[4] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_MINOR];
                        nData[5] = COMPort.pCOM_CopyBuffer[HEART_IDX_SW_BUGFIX];
                        nData[6] = COMPort.pCOM_CopyBuffer[HEART_IDX_CHECKSUMERRORS];
                        nData[7] = COMPort.pCOM_CopyBuffer[HEART_IDX_STUFFBYTEERRORS];

                        break;

                }// end switch

            }// end try
            catch
            {
                nReceiveMode = RX_FaultyMessage;
            };

            frmMain.bSerialDataProcess = false;

            // Return the receive mode
            return nReceiveMode;

        }// end RX_J1939

        //-FUNCTION ----------------------------------------------------------------
        // Routine     : FilterSystemMessage
        // Description : Checks for ystem messages such as STATS and ACK
        // Returncode  : See RX_J1939 codes
        // -------------------------------------------------------------------------
        //
        public static int FilterSystemMessage(byte[] nData)
        {
            // Declarations
            int nRetCode = RX_Message;

            switch (nData[MSG_IDX_ID])
            {
                case MSG_ID_HEART:
                    nRetCode = RX_HEART;
                    break;

                case MSG_ID_RS:
                    nRetCode = RX_RS;
                    break;

                case MSG_ID_VERSION:
                    nRetCode = RX_VERSION;
                    break;

                case MSG_ID_ACK:
                    switch (nData[MSG_IDX_ACK_MSG])
                    {
                        case MSG_ID_FA: // Filter Add acknowledgment
                            nRetCode = RX_ACK_FA;
                            break;

                        case MSG_ID_FD: // Filter Delete acknowlegment
                            nRetCode = RX_ACK_FD;
                            break;

                        case MSG_ID_TX: // Transmit acknowledgment
                            nRetCode = RX_ACK_TX;
                            break;

                        case MSG_ID_RESET:
                            nRetCode = RX_ACK_RESET;
                            break;

                        case MSG_ID_SETPARAM:
                            nRetCode = RX_ACK_SETPARAM;
                            break;

                        case MSG_ID_SETPARAM1:
                            nRetCode = RX_ACK_SETPARAM1;
                            break;

                        case MSG_ID_SETMSGMODE:
                            nRetCode = RX_ACK_MSGMODE;
                            break;

                        case MSG_ID_TXL:
                            nRetCode = RX_ACK_TXL;
                            break;

                        case MSG_ID_TXP:
                            nRetCode = RX_ACK_TXP;
                            break;

                        case MSG_ID_TXPL:
                            nRetCode = RX_ACK_TXPL;
                            break;

                        case MSG_ID_SETACK:
                            nRetCode = RX_ACK_SETACK;
                            break;

                        case MSG_ID_SETHEART:
                            nRetCode = RX_ACK_SETHEART;
                            break;

                        case MSG_ID_FLASH:
                            nRetCode = RX_ACK_FLASH;
                            break;

                    }// end switch

                    break;

            }// end switch

            // Return the result
            return nRetCode;

        }// end FilterSystemMessage

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : ComputeCheckSum
        // Description : Computes the checksum of a message and inserts it
        // Returncode  : byte Checksum
        // -------------------------------------------------------------------------
        public static byte ComputeCheckSum(byte[] pMsg)
        {
            // Declarations
            byte nCheckSum = 0;

            // Checksum range (Data Field + MSB + LSB - Checksum Field)
            int nLen = ((int)pMsg[RXTX_IDX_MSGLENMSB] << 8);
            nLen += (int)pMsg[RXTX_IDX_MSGLENLSB] + 1;

            // Create the checksum
            for (int nIndex = 1; nIndex <= nLen; nIndex++)
                nCheckSum += pMsg[nIndex];

            return (byte)((~(int)nCheckSum) + 1); ;

        }// end ComputeCheckSum

        //-SUB----------------------------------------------------------------------
        // Routine     : RemoveStuffBytes
        // Description : Remove stuff bytes from message
        // -------------------------------------------------------------------------
        public static void RemoveStuffBytes(ref byte[] pBuffer, ref int nBufferSize)
        {
            // Scan through the global receive buffer
            for (int nIndex = 0; nIndex < nBufferSize; nIndex++)
            {
                if (nBufferSize - nIndex >= 2)
                {
                    if (pBuffer[nIndex] == MSG_TOKEN_ESC
                    && pBuffer[nIndex + 1] == MSG_START_STUFF)
                    {
                        pBuffer[nIndex] = MSG_TOKEN_START;
                        COMPort.ShiftBuffer(nIndex + 1, ref pBuffer, ref nBufferSize);
                    }// end if
                    else if (pBuffer[nIndex] == MSG_TOKEN_ESC
                         && pBuffer[nIndex + 1] == MSG_ESC_STUFF)
                    {
                        pBuffer[nIndex] = MSG_TOKEN_ESC;
                        COMPort.ShiftBuffer(nIndex + 1, ref pBuffer, ref nBufferSize);
                    }// end else

                } // end if

            }// end for

        }// end RemoveStuffBytes

        //-FUNCTION-----------------------------------------------------------------
        // Routine     : FillStuffingBytes
        // Description : Add stuffing bytes while copying message
        // Returncode  : New message and its length
        // -------------------------------------------------------------------------
        int FillStuffingBytes(byte[] pOrg, int nOrgLen, ref byte[] pNew)
        {
            // Add stuffing bytes while copying message
            pNew[RXTX_IDX_MSGSTART] = pOrg[RXTX_IDX_MSGSTART];
            int nPointer = RXTX_IDX_MSGLENMSB;

            for (int nIndex = 1; nIndex <= nOrgLen - 1; nIndex++)
            {
                switch (pOrg[nIndex])
                {
                    case MSG_TOKEN_START:
                        pNew[nPointer++] = MSG_TOKEN_ESC;
                        pNew[nPointer++] = MSG_START_STUFF;
                        break;

                    case MSG_TOKEN_ESC:
                        pNew[nPointer++] = MSG_TOKEN_ESC;
                        pNew[nPointer++] = MSG_ESC_STUFF;
                        break;

                    default:
                        pNew[nPointer++] = pOrg[nIndex];
                        break;

                }// end switch

            }// end for

            // Return the total length
            return (nPointer);

        }// end FillStuffingBytes

    }// end class

}// end namespace
