using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace polyCan_jCOM
{
    // Connection (Driver) to the Windows COM port
    class COMPort
    {
        // ---------------------------------------------------------------------------
        // General Constants
        // ---------------------------------------------------------------------------
        public const int OK = 0;
        public const int ERROR = 1;
        public const int NOMSG = 1;

        // ---------------------------------------------------------------------------
        // COM Port
        // ---------------------------------------------------------------------------
        public const int MAX_COM_PORTS = 20;            // Maximum number of COM ports

        public const int REC_BUFFER_SIZE = 4096;        // The maximum J1939 message length = 255 x 7 = 1785
        // but we need to add some space for stuffing bytes
        public const int READ_TIMEOUT = 1;
        public const int WRITE_TIMEOUT = 1;
        public const int REC_BUFFER_FILLTIME = 80;
        public static string[] sPorts = new string[MAX_COM_PORTS];
        public static int nPorts = -1;
        public static int nSelPort = -1;
        public static string sLastUsedPort = "";

        public static SerialPort _serialport;
        public static byte[] pCOM_ReceiveBuffer = new byte[REC_BUFFER_SIZE + 1];
        public static byte[] pCOM_CopyBuffer = new byte[REC_BUFFER_SIZE + 1];
        public static int nCOM_ReceiveBufferSize = 0;
        public static int nCOM_CopyBufferSize = 0;

        public static int nErrorCounter = 0; // Receive Errors

        //-SUB------------------------------------------------------------------------
        // Routine     : Initialize
        // Description : Initializes the COM interface
        // Returncode  : OK / ERROR
        // ---------------------------------------------------------------------------
        public static int Initialize(int nComPort)
        {
            // Declarations
            int nRetCode = ERROR;

            // Initialize global parameters
            nCOM_ReceiveBufferSize = 0;

            // Determine the available ports
            sPorts = SerialPort.GetPortNames();
            nPorts = sPorts.Length;

            // Check if last used port still exists
            if (nComPort == -1 && sLastUsedPort.Length > 0)
            {
                for (int nIndex = 0; nIndex < nPorts; nIndex++)
                {
                    if (sPorts[nIndex] == sLastUsedPort)
                    {
                        nComPort = nIndex;
                        break;

                    }// end if

                }// end for

            }// end if

            // Check which COM port to initiate
            if (nComPort >= 0)
            {
                try
                {
                    // Initialize the serial port
                    _serialport = new SerialPort(sPorts[nComPort], 115200, Parity.None, 8, StopBits.One);
                    _serialport.Handshake = Handshake.None;

                    // Set the read/write timeouts
                    _serialport.ReadTimeout = READ_TIMEOUT;
                    _serialport.WriteTimeout = WRITE_TIMEOUT;
                    _serialport.ReadBufferSize = REC_BUFFER_SIZE;

                    // Set the interrupt receive program
                    _serialport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                    // Open the port
                    _serialport.Open();

                    // The following will only apply when COM port was found
                    nSelPort = nComPort;
                    nRetCode = OK;

                }// end try
                catch
                {
                    nRetCode = ERROR;
                }// end catch

            }// end if
            else
            {
                // Scan for the appropriate COM port
                for (int nIndex = 0; nIndex < nPorts; nIndex++)
                {
                    try
                    {
                        // Initialize the serial port
                        _serialport = new SerialPort(sPorts[nIndex], 115200, Parity.None, 8, StopBits.One);
                        _serialport.Handshake = Handshake.None;

                        // Set the read/write timeouts
                        _serialport.ReadTimeout = READ_TIMEOUT;
                        _serialport.WriteTimeout = WRITE_TIMEOUT;
                        _serialport.ReadBufferSize = REC_BUFFER_SIZE;
                        _serialport.Open();

                        // The following will only apply when COM port was found
                        nSelPort = nIndex;
                        nRetCode = OK;

                    }// end try
                    catch
                    {
                        nRetCode = ERROR;
                    }// end catch

                    // Leave the loop when COM port was found
                    if (nRetCode == OK)
                        goto Initialize_End;

                }// end for

            }// end else

Initialize_End:
            return nRetCode;

        }// end Initialize

        //-SUB------------------------------------------------------------------------
        // Routine     : Terminate
        // Description : Terminates the serial communication
        // Returncode  : None
        // ---------------------------------------------------------------------------
        public static void Terminate()
        {
            // Close the serial port
            try
            {
                _serialport.Close();
            }
            catch { }

        }// end Terminate

        //-SUB------------------------------------------------------------------------
        // Routine     : Transmit
        // Description : Transmit data to the selected COM port
        // ---------------------------------------------------------------------------
        public static int Transmit(byte[] pMsg, int nMsgLen)
        {
            // Declarations
            int nRetCode = OK;

            // Send the message
            try
            {
                if (_serialport.IsOpen)
                    _serialport.Write(pMsg, 0, nMsgLen);
                else
                    nRetCode = ERROR;
            }
            catch
            {
                nRetCode = ERROR;
            }

            return nRetCode;

        }// end Transmit

        //-SUB------------------------------------------------------------------------
        // Routine     : DataReceivedHandler
        // Description : Receive data and store it into the global buffer
        // ---------------------------------------------------------------------------
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                // Check the COM port
                if (frmMain.bSerialDataProcess == false && _serialport.BytesToRead > 0)
                {
                    frmMain.bSerialDataReceive = true;

                    //System.Threading.Thread.Sleep(1);
                    int nBytesToRead = _serialport.BytesToRead;

                    // Check if data was received
                    if (nBytesToRead > 0)
                    {
                        // Read the string and attach to global buffer
                        for (int nIndex = 0; nIndex < nBytesToRead; nIndex++)
                        {
                            pCOM_ReceiveBuffer[nCOM_ReceiveBufferSize] = (byte)_serialport.ReadByte();
                            if (nCOM_ReceiveBufferSize == REC_BUFFER_SIZE - 1)
                                break;
                            else
                                nCOM_ReceiveBufferSize++;

                        }// end for

                    }// end if

                }// end if

            }// end try
            catch { }

            frmMain.bSerialDataReceive = false;

        }// end DataReceivedHandler

        //-SUB------------------------------------------------------------------------
        // Routine     : Receive
        // Description : Receive data and store it into the global buffer
        // Returncode  : Number of bytes received
        // ---------------------------------------------------------------------------
        public static int Receive(object sender, SerialDataReceivedEventArgs e)
        {
            // Declarations
            int nBytes = 0;

            // Note: At times we experienced an error exception that was somehow related 
            // to the serial port function calls.
            // The implementation of a 1 msec timeout solved that problem.
            try
            {
                // Check the COM port
                if (_serialport.BytesToRead > 0)
                {
                    System.Threading.Thread.Sleep(1);
                    int nBytesToRead = _serialport.BytesToRead;

                    // Check if data was received
                    if (nBytesToRead > 0)
                    {
                        // Read the string and attach to global buffer
                        for (int nIndex = 0; nIndex < nBytesToRead; nIndex++)
                        {
                            pCOM_ReceiveBuffer[nCOM_ReceiveBufferSize] = (byte)_serialport.ReadByte();
                            if (nCOM_ReceiveBufferSize == REC_BUFFER_SIZE - 1)
                                break;
                            else
                            {
                                nCOM_ReceiveBufferSize++;
                                nBytes++;
                            }// end else

                        }// end for

                    }// end if

                }// end if

            }// end try
            catch { }

            return nBytes;

        }// end Receive

        //-SUB----------------------------------------------------------------------
        // Routine     : ShiftBuffer
        // Description : Removes byte in copy buffer
        // -------------------------------------------------------------------------
        public static void ShiftBuffer(int nPos, ref byte[] pBuffer, ref int nBufferSize)
        {
            // Declarations 
            int nSource;
            int nDest;
            int nIndex;

            // Any leading bytes in the buffer must be trash 
            nSource = nPos + 1;
            nDest = nPos;

            for (nIndex = 0; nIndex < nBufferSize - nPos - 1; nIndex++)
                pBuffer[nDest++] = pBuffer[nSource++];

            nBufferSize--;

        }// end ShiftBuffer

        //-SUB----------------------------------------------------------------------
        // Routine     : CleanReceiveBuffer
        // Description : Remove current messages from global receive buffer
        // -------------------------------------------------------------------------
        public static void CleanReceiveBuffer(int nStart, int nLen)
        {
            // Any leading bytes in the buffer must be trash
            nLen += nStart;
            int nSource = nLen;
            int nDest = 0;

            nCOM_ReceiveBufferSize -= nLen;

            for (int nIndex = 0; nIndex < nCOM_ReceiveBufferSize; nIndex++)
                pCOM_ReceiveBuffer[nDest++] = pCOM_ReceiveBuffer[nSource++];

        }// end CleanReceiveBuffer

    }// end class

}// end namespace
