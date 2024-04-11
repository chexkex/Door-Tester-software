using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komunikation_test
{
    public partial class Form1
    {

        //Initialize Com port
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort();
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        //Loading available com ports
        private void LoadAvailablePorts()
        {
            comboBoxPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(ports);
        }

        //Load avalible Baud Rates
        private void LoadBaudRates()
        {
            int[] baudRates = { 9600, 19200, 38400, 57600, 115200 }; // Add more Baud rates here
            foreach (int rate in baudRates) { comboBoxBaudRate.Items.Add(rate.ToString()); }
        }

        //resiving serial data
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadLine(); // Reading from serial port
            firstTest = true;

            DataIn(data);
            if (btnPrintFrocecon || btnPrintCurrentcon) { printConDataonce = true; }
        }

        //Send serial data and calculate checksum
        private void SendData(int dataToSend)
        {

            int data = dataToSend;
            lastDataSent = dataToSend;

            int sum = 0;
            while (dataToSend > 0)
            {

                sum += dataToSend % 10;
                dataToSend /= 10;

            }

            sum = sum % 10;

            serialPort.WriteLine(data.ToString("D5") + sum.ToString());
            DataIn(data.ToString("D5") + sum.ToString());

        }

        //Closing serialport when program is turned off
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen) { serialPort.Close(); }
        }

        //Controll of string to int
        private int IntConvet(string textIn)
        {

            int parsedInt;
            if (int.TryParse(textIn, out parsedInt)) { return parsedInt; }

            else { return 0; }

        }
    }
}
