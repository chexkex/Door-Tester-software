using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Kommunikation_test
{
    internal class Serial_com
    {
        private SerialPort serialPort;

        public Serial_com()
        {
            // Skapa SerialPort-objekt
            serialPort = new SerialPort("COM4", 9600); // Ange COM-port och baudrate

            // Konfigurera SerialPort-inställningar
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;

            // Lägg till en händelsehanterare för när data tas emot
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        // Metod som öppnar den seriella porten
        public void OpenPort()
        {
            try
            {
                // Öppna den seriella porten
                serialPort.Open();
                Console.WriteLine("Seriell port öppnad.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid öppning av seriell port: " + ex.Message);
            }
        }

        // Metod som stänger den seriella porten
        public void ClosePort()
        {
            try
            {
                // Stäng den seriella porten
                serialPort.Close();
                Console.WriteLine("Seriell port stängd.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid stängning av seriell port: " + ex.Message);
            }
        }

        // Händelsehanterare för när data tas emot
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadLine(); // Läs en rad av data från den seriella porten
            Console.WriteLine("Mottagen data: " + data);
            // Gör något med den inkommande data
        }
    }
}
