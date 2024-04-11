using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace Komunikation_test
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        //Global vaiables
        int lastDataSent = 0;
        string selectedFolderPath;
        string filePath;

        string selectedDoorLenght;
        string selectedDoorMotor;
        string currentChannels;

        bool testing = false;
        //bool inATest = false;
        bool firstTest = true;
        bool errorOcurred = false;
        bool oneTimeCall = false;
        bool btnPrintFrocecon = false;
        bool btnPrintCurrentcon = false;
        bool printConDataonce = false;
        bool calibrateLoadcell = false;
        bool totalPulseToLong = false;


        //Limit variables
        int forceLimit = 0;
        int currentLimit = 0;
        int kineticEnergyLimit = 0;
        int timesToRun = 0;
        int timesRuned = 1;
        int waitTime = 0;
        float calculatedKineticEnergy = 0;
        int knownLoad = 0;
        

        float forceReturn = 0;
        int currentReturn1 = 0;
        int currentReturn2 = 0;
        int currentReturn3 = 0;
        int hallTotalReturn = 0;
        int hallDiffReturn = 0;
        int speedReturn = 0;
        int totalPulse = 0;
        int totalPulseLong = 0;


        public Form1()
        {   
            //Loading prots and baudRates
            InitializeComponent();
            InitializeSerialPort();
            LoadAvailablePorts();
            LoadBaudRates();

            //turning off buttens
            btnDisconnect.Enabled = false;
            btnRunTest.Enabled = false;
            btnRunTestOneTime.Enabled = false;
            btnStopTest.Enabled = false;
            btnContinue.Enabled = false;
            btnChangeSavePlace.Enabled = false;
            rbtnLoggerOn.Enabled = false;
            SetButtonEnabled(btnCalibrateLoadcell, false);
            SetButtonEnabled(btnCalibrateLoadcellNoLoad, false);
            SetButtonEnabled(btnCalibrateLoadcellKnownLoad, false);
            SetButtonEnabled(btnPrintFroceContinuously, false);
            SetButtonEnabled(btnPrintCurrentContinuously, false);
            SetTextBoxReadOnly(knownLoadInput, true);
            
        }

        //Turns off sleep mode on computer
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);       
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public static void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        }

        public static void RestoreSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }

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
            if(btnPrintFrocecon || btnPrintCurrentcon) { printConDataonce = true; }
        }

        //DataIn uppdates the console and controles data
        private void DataIn(string data)
        {

            //Controle of data and checksum
            int parsedInt;
            if (int.TryParse(data, out parsedInt))
            {
                //Checksum controle 
                int receivedNumber = int.Parse(data) / 10;
                int receivedChecksum = int.Parse(data) % 10;

                int calculatedChecksum = 0;
                int tempNumber = receivedNumber;
                while (tempNumber > 0)
                {
                    calculatedChecksum += tempNumber % 10;
                    tempNumber /= 10;
                }
                calculatedChecksum %= 10;

                if (calculatedChecksum == receivedChecksum) {

                    //Add impementation for checksums for ESP32 to PC

                }

                if (data.Trim().Equals("1111217")) { SendData(lastDataSent); }

                if (receivedNumber == 411115 && oneTimeCall) {
                    MessageBox.Show("Calibration whid no load is completed! Calibrat whid known load now.");
                    SetButtonEnabled(btnCalibrateLoadcellKnownLoad, true); 
                    SetTextBoxReadOnly(knownLoadInput, false);
                    oneTimeCall = false;
                   
                }
                if (receivedNumber == 411116 && oneTimeCall) { 
                    MessageBox.Show("Calibration is completed!"); 
                    SendData(420000 + (knownLoad * 10)); 
                    oneTimeCall = false;
                    SetButtonEnabled(btnCalibrateLoadcellKnownLoad, false);
                    SetButtonEnabled(btnCalibrateLoadcell, true);
                    SetButtonEnabled(btnRunTestOneTime, true);
                    SetButtonEnabled(btnPrintCurrentContinuously, true);
                    SetButtonEnabled(btnPrintFroceContinuously, true);
                    if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                }

                //Is test is runing
                if (testing && firstTest)
                {
                    //Splits the numbers to right varible
                    firstTest = false;
                    if (receivedNumber == 111113 || receivedNumber == 111115) {

                        MessageBox.Show("Test cant be run something wrong whid the door. Press continue butten when problem is solved");
                        SetButtonEnabled(btnContinue, true);
                        if (!rbtnLoggerOn.Checked) { timesToRun++; }
                    }

                    if (receivedNumber == 111116) {

                        if (totalPulseToLong)
                        {
                            totalPulseToLong = false;
                            hallTotalReturn = (totalPulse * 10) + totalPulseLong;
                        }
                        else { hallTotalReturn = totalPulse; }
                        AddItemToListBox(hallTotalReturn.ToString() + " Total steps");

                        if (timesToRun > 0) { AddItemToListBox("Ready for new run, Waiting for timer"); }

                        timesToRun--;
                        
                        if (rbtnLoggerOn.Checked)
                        {   
                            //Saving data to file
                            if (IntConvet(currentChannels) == 1)
                            {
                                SaveDataToCSV(filePath, timesRuned.ToString(), forceReturn.ToString(), currentReturn1.ToString(), hallTotalReturn.ToString(),
                             hallDiffReturn.ToString(), speedReturn.ToString());
                            }
                            else if (IntConvet(currentChannels) == 2)
                            {
                                SaveDataToCSV(filePath, timesRuned.ToString(), forceReturn.ToString(), currentReturn1.ToString(), currentReturn2.ToString(), hallTotalReturn.ToString(),
                             hallDiffReturn.ToString(), speedReturn.ToString());
                            }
                            else if (IntConvet(currentChannels) == 3) {
                                SaveDataToCSV(filePath, timesRuned.ToString(), forceReturn.ToString(), currentReturn1.ToString(), currentReturn2.ToString(), currentReturn3.ToString(), hallTotalReturn.ToString(),
                             hallDiffReturn.ToString(), speedReturn.ToString());
                            }
                            
                            timesRuned++;

                            //Stopps the program if limit values exceeded
                            if (forceReturn > forceLimit) { MessageBox.Show("Froce has exceeded its limlit value. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn1 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 1. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn2 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 2. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn3 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 3. Press continue to run again");  errorOcurred = true; }
                            if (calculatedKineticEnergy > kineticEnergyLimit) { MessageBox.Show("The kinetic energi has exceeded its limit value. Press continue to run again"); errorOcurred = true; }
                            if (errorOcurred) { SetButtonEnabled(btnContinue, true); errorOcurred = false; }
                            else { RunNextTest(); }

                        }
                        else { RunNextTest(); }


                    }

                    if (receivedNumber == 111112) { /*inATest = true;*/ AddItemToListBox("Next test has begin"); }

                    if (receivedNumber == 111114) {

                        AddItemToListBox("!");
                        AddItemToListBox("Data from this run");
                        AddItemToListBox("Run nr : " + timesRuned);

                    }

                    if (receivedNumber > 119999 && receivedNumber < 130000)
                    {

                        int remainingNumber = receivedNumber % 10000;
                        forceReturn = remainingNumber / 10.0f;
                        AddItemToListBox(forceReturn.ToString() + " N, Maximum force");

                    }

                    if (receivedNumber > 129999 && receivedNumber < 140000)
                    {

                        currentReturn1 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn1.ToString() + " mA, Maximum current sensor 1");

                    }

                    if (receivedNumber > 139999 && receivedNumber < 150000 && (IntConvet(currentChannels) == 2 || IntConvet(currentChannels) == 3))
                    {

                        currentReturn2 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn2.ToString() + " mA, Maximum current sensor 2");

                    }

                    if (receivedNumber > 149999 && receivedNumber < 160000 && IntConvet(currentChannels) == 3)
                    {

                        currentReturn3 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn3.ToString() + " mA, Maximum current sensor 3");

                    }

                    if (receivedNumber > 159999 && receivedNumber < 170000) { totalPulse = receivedNumber % 10000; }
                    
                    if (receivedNumber > 169999 && receivedNumber < 180000)
                    {

                        hallDiffReturn = receivedNumber % 10000;
                        hallDiffReturn = hallDiffReturn - 5000;
                        AddItemToListBox(hallDiffReturn.ToString() + " Total diff steps");

                    }

                    if (receivedNumber > 179999 && receivedNumber < 190000)
                    {
                        
                        speedReturn = receivedNumber % 10000;
                        AddItemToListBox(speedReturn.ToString() + " Maxmimum pulse in 100ms");

                    }

                    if (receivedNumber > 209999 && receivedNumber < 220000) { totalPulseLong = receivedNumber % 10000; if (totalPulseLong > 0) { totalPulseToLong = true; } }
                    

                }
                else if ((btnPrintCurrentcon || btnPrintFrocecon) && printConDataonce) 
                {
                    printConDataonce = false;
                    if (receivedNumber > 119999 && receivedNumber < 130000) {
                        int remainingNumber = receivedNumber % 10000;
                        forceReturn = remainingNumber / 10.0f;
                        AddItemToListBox(forceReturn.ToString() + " N");
                    }
                    if (receivedNumber > 129999 && receivedNumber < 140000)
                    {
                        currentReturn1 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn1.ToString() + " mA, Maximum current sensor 1");
                    }

                    if (receivedNumber > 139999 && receivedNumber < 150000)
                    {
                        currentReturn2 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn2.ToString() + " mA, Maximum current sensor 2");
                    }

                    if (receivedNumber > 149999 && receivedNumber < 160000)
                    {
                        currentReturn3 = receivedNumber % 10000;
                        AddItemToListBox(currentReturn3.ToString() + " mA, Maximum current sensor 3");
                        
                    }
                }

                if (consoleBox.InvokeRequired) { consoleBox.Invoke(new Action<String>(DataIn), data); }

                else
                {

                    consoleBox.Items.Add(data);
                    consoleBox.SelectedIndex = consoleBox.Items.Count - 1;

                }

            }

        }

        //Starting next test if count is still over 1
        private async void RunNextTest() {
            if (timesToRun > 0) { await Task.Delay(waitTime * 1000); SendData(111111); }
            
            else {
                AddItemToListBox("Test is completed");
                MessageBox.Show("Test is completed");
                testing = false;
                timesRuned = 1;
                SetButtonEnabled(btnRunTestOneTime, true);
                SetButtonEnabled(btnDisconnect, true);
                SetButtonEnabled(btnChangeSavePlace, true);
                SetButtonEnabled(btnLoadPorts, true);
                SetButtonEnabled(btnStopTest, false);
                SetTextBoxReadOnly(forceLimitInput, false);
                SetTextBoxReadOnly(currentLimitInput, false);
                SetTextBoxReadOnly(kinetcEnergyInput, false);
                SetTextBoxReadOnly(runTimesInput, false);
                SetTextBoxReadOnly(waitTimeInput, false);
                SetButtonEnabled(btnCalibrateLoadcell, true);
                SetButtonEnabled(btnPrintFroceContinuously, true);
                SetButtonEnabled(btnPrintCurrentContinuously, true);
                RestoreSleep();
            }
        }

        //Print to listbox
        private void AddItemToListBox(string item)
        {
            if (string.IsNullOrEmpty(item)) { return; }
            if (returnDataBox.InvokeRequired) { returnDataBox.Invoke(new Action<string>(AddItemToListBox), item); }

            else
            {
                if (item == "!") { returnDataBox.Items.Clear(); }

                else { returnDataBox.Items.Add(item); returnDataBox.SelectedIndex = returnDataBox.Items.Count - 1; }

            }
        }

        //Send serial data and calculate checksum
        private void SendData(int dataToSend) {

            int data = dataToSend;
            lastDataSent = dataToSend;

            int sum = 0;
            while (dataToSend > 0) {

                sum += dataToSend % 10;
                dataToSend /= 10;

            }

            sum = sum % 10;

            serialPort.WriteLine(data.ToString("D5") + sum.ToString());
            DataIn(data.ToString("D5") + sum.ToString());

        }

        //Saving to file
        private void SaveDataToCSV(string filePath, params object[] values)
        {
            string line = string.Join(";", values);

            try { using (StreamWriter sw = new StreamWriter(filePath, true)) { sw.WriteLine(line); } }

            catch (Exception ex) { MessageBox.Show("Fel vid sparande av data till fil: " + ex.Message); }

        }

        //Closing serialport when program is turned off
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen) { serialPort.Close(); }
        }

        //Controll of string to int
        private int IntConvet(string textIn) {

            int parsedInt;
            if (int.TryParse(textIn, out parsedInt)) { return parsedInt; }

            else { return 0; }

        }

        //Combobox reading
        public string ReadComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            if (comboBox.InvokeRequired) { return (string)comboBox.Invoke(new Func<string>(() => comboBox.SelectedItem?.ToString())); }
            else { return comboBox.SelectedItem?.ToString(); }
        }

        //Btn controler
        public void SetButtonEnabled(System.Windows.Forms.Button button, bool enabled)
        {
            if (button.InvokeRequired) { button.Invoke(new MethodInvoker(delegate { button.Enabled = enabled; })); }
            else { button.Enabled = enabled; }
        }

        //Textbox controler 
        public void SetTextBoxReadOnly(System.Windows.Forms.TextBox textBox, bool readOnly)
        {
            if (textBox.InvokeRequired) { textBox.Invoke(new MethodInvoker(delegate { textBox.ReadOnly = readOnly; })); }

            else { textBox.ReadOnly = readOnly; }
            
        }

        //Connect BTN
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedItem == null || comboBoxBaudRate.SelectedItem == null)
            {
                MessageBox.Show("Select a COM port and baud rate before connecting.");
                return;
            }

            string selectedPort = comboBoxPort.SelectedItem.ToString();
            int selectedBaudRate = Convert.ToInt32(comboBoxBaudRate.SelectedItem);

            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = selectedPort;
                    serialPort.BaudRate = selectedBaudRate;
                    serialPort.Open();
                    MessageBox.Show("Serial port opened.");
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    btnRunTestOneTime.Enabled = true;
                    btnChangeSavePlace.Enabled = true;
                    SetButtonEnabled(btnCalibrateLoadcell, true);
                    SetButtonEnabled(btnPrintFroceContinuously, true);
                    SetButtonEnabled(btnPrintCurrentContinuously, true);
                }
                else { MessageBox.Show("Serial port is already open."); }

            }
            catch (Exception ex) { MessageBox.Show("Error opening serial port: " + ex.Message); }
            
        }

        //Disconnect BTN
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    MessageBox.Show("Serial port closed.");
                    btnConnect.Enabled = true;
                    btnDisconnect.Enabled = false;
                    btnRunTest.Enabled = false;
                    btnRunTestOneTime.Enabled = false;
                    SetButtonEnabled(btnCalibrateLoadcell, false);
                    SetButtonEnabled(btnPrintFroceContinuously, false);
                    SetButtonEnabled(btnPrintCurrentContinuously, false);
                }
                else { MessageBox.Show("Serial port is already closed."); }

            }
            catch (Exception ex) { MessageBox.Show("Error closing serial port: " + ex.Message); }
            
        }

        //Load com ports BTN
        private void btnLoadPorts_Click(object sender, EventArgs e) { LoadAvailablePorts(); }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void consoleBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //BTN for starting a test whid x runtimes
        private void btnRunTest_Click(object sender, EventArgs e)
        {
            
            forceLimit = IntConvet(forceLimitInput.Text.Trim());
            currentLimit = IntConvet(currentLimitInput.Text.Trim());
            kineticEnergyLimit = IntConvet(kinetcEnergyInput.Text.Trim());
            timesToRun = IntConvet(runTimesInput.Text.Trim());
            waitTime = IntConvet(waitTimeInput.Text.Trim());
            AddItemToListBox("!");

            selectedDoorLenght = ReadComboBox(doorLengthCombobox);
            selectedDoorMotor = ReadComboBox(motorTypeCombobox);
            currentChannels = ReadComboBox(currentChannelsCombobox);

            //controls input values and not run the test
            if (forceLimit == 0 || currentLimit == 0 || kineticEnergyLimit == 0 || timesToRun == 0 || waitTime == 0) { MessageBox.Show("Limit values are missing"); }
            else if (string.IsNullOrEmpty(selectedDoorLenght)) { MessageBox.Show("Dorr leght is missing"); }
            else if (string.IsNullOrEmpty(selectedDoorMotor)) { MessageBox.Show("Motor type is missing"); }
            else if (string.IsNullOrEmpty(currentChannels)) { MessageBox.Show("Current channels is missing"); }
            
            //else the test begins
            else
            {
                if(IntConvet(currentChannels) == 1) { SaveDataToCSV(filePath, "Run times", "Max force", "Current 1", "Steps total", "Step diff", "Maximum speed"); }
                else if (IntConvet(currentChannels) == 2) { SaveDataToCSV(filePath, "Run times", "Max force", "Current 1", "Current 2", "Steps total", "Step diff", "Maximum speed"); }
                else if (IntConvet(currentChannels) == 3) { SaveDataToCSV(filePath, "Run times", "Max force", "Current 1", "Current 2", "Current 3", "Steps total", "Step diff", "Maximum speed"); }
                btnRunTest.Enabled = false;
                btnStopTest.Enabled = true;
                btnRunTestOneTime.Enabled = false;
                btnDisconnect.Enabled = false;
                btnLoadPorts.Enabled = false;
                btnChangeSavePlace.Enabled = false;
                SetButtonEnabled(btnCalibrateLoadcell, false);
                SetButtonEnabled(btnPrintFroceContinuously, false);
                SetButtonEnabled(btnPrintCurrentContinuously, false);

                forceLimitInput.ReadOnly = true;
                currentLimitInput.ReadOnly = true;
                kinetcEnergyInput.ReadOnly = true;
                runTimesInput.ReadOnly = true;
                waitTimeInput.ReadOnly = true;

                testing = true;
                PreventSleep();
                SendData(111111);
            }
        }

        //BTN for runing 1 test 
        private void btnRunTestOneTime_Click(object sender, EventArgs e)
        {
            currentChannels = (ReadComboBox(currentChannelsCombobox));

            if (string.IsNullOrEmpty(currentChannels)) { MessageBox.Show("Current channels is missing"); }
              
            else 
            {
                btnRunTestOneTime.Enabled = false;
                btnRunTest.Enabled = false;
                btnChangeSavePlace.Enabled = false;
                btnDisconnect.Enabled = false;
                btnLoadPorts.Enabled = false;
                SetButtonEnabled(btnCalibrateLoadcell, false);
                SetButtonEnabled(btnPrintFroceContinuously, false);
                SetButtonEnabled(btnPrintCurrentContinuously, false);
                testing = true;
                timesToRun = 0;
                AddItemToListBox("!");
                rbtnLoggerOn.Checked = false;
                SendData(111111);
                PreventSleep();
            }
        }

        //Btn for place to save logg file
        private void btnChangeSavePlace_Click(object sender, EventArgs e)
        {

            rbtnLoggerOn.Checked = true;

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedFolderPath = folderBrowser.SelectedPath;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = selectedFolderPath;
                saveFileDialog.Title = "Choose a location and file name to save the file";
                saveFileDialog.Filter = "CSV-filer (*.csv)|*.csv|Alla filer (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFileName = saveFileDialog.FileName;
                    filePath = Path.Combine(selectedFolderPath, selectedFileName);

                    lblSelectedFolder.Text = "Selected filename: " + selectedFileName;
                    if (serialPort.IsOpen && btnPrintCurrentcon && btnPrintFrocecon) { btnRunTest.Enabled = true; }                    
                }
                else { lblSelectedFolder.Text = "No folder or file selected."; }
            }
            else { lblSelectedFolder.Text = "No folder selected."; }
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            btnStopTest.Enabled = false;    
            timesToRun = 0;
            AddItemToListBox("Test will be stoped after this run");
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            btnContinue.Enabled = false;
            RunNextTest();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalibrateLoadcell_Click(object sender, EventArgs e)
        {
            calibrateLoadcell = !calibrateLoadcell;
            if (calibrateLoadcell)
            {
                btnCalibrateLoadcell.Text = "Stop calibrate loadcell";
                SetButtonEnabled(btnRunTestOneTime, false);
                SetButtonEnabled(btnRunTest, false);
                SetButtonEnabled(btnCalibrateLoadcellNoLoad, true);
                SetButtonEnabled(btnPrintCurrentContinuously, false);
                SetButtonEnabled(btnPrintFroceContinuously, false);
            }
            else
            {
                btnCalibrateLoadcell.Text = "Calibrate loadcell";
                SetButtonEnabled(btnRunTestOneTime, true);
                SetButtonEnabled(btnRunTest, true);
                SetButtonEnabled(btnCalibrateLoadcellNoLoad, false);
                SetButtonEnabled(btnPrintCurrentContinuously, true);
                SetButtonEnabled(btnPrintFroceContinuously, true);
            }
            SendData(411111);
        }

        private void btnCalibrateLoadcellNoLoad_Click(object sender, EventArgs e)
        {
            oneTimeCall = true;
            SetButtonEnabled(btnCalibrateLoadcellNoLoad, false);
            SendData(411113);
            MessageBox.Show("Wait for calibration");
            SetButtonEnabled(btnCalibrateLoadcell, false);
        }

        private void btnCalibrateLoadcellKnownLoad_Click(object sender, EventArgs e)
        {
            knownLoad = IntConvet(knownLoadInput.Text.Trim());
            if (knownLoad == 0) { MessageBox.Show("Known load i missing"); }
            else {
                oneTimeCall = true;
                knownLoadInput.Text = "";
                SetTextBoxReadOnly(knownLoadInput, true);
                SendData(411114);
                MessageBox.Show("Wait for calibration");
                btnCalibrateLoadcell.Text = "Calibrate loadcell";
                SetTextBoxReadOnly(knownLoadInput, true);
            }
            
            
        }

        private void btnPrintFroceContinuously_Click(object sender, EventArgs e)
        {
            btnPrintFrocecon = !btnPrintFrocecon;
            if (btnPrintFrocecon) { 
                btnPrintFroceContinuously.Text = "Stop print force continuously";
                SetButtonEnabled(btnRunTestOneTime, false);
                SetButtonEnabled(btnRunTest, false);
                SetButtonEnabled(btnCalibrateLoadcell, false);
            }
            else { 
                btnPrintFroceContinuously.Text = "Print force continuously";
                SetButtonEnabled(btnRunTestOneTime, true);
                if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                SetButtonEnabled(btnCalibrateLoadcell, true);
            }
            
            SendData(411112);
        }

        private void btnPrintCurrentContinuously_Click(object sender, EventArgs e)
        {
            btnPrintCurrentcon = !btnPrintCurrentcon;
            if (btnPrintCurrentcon)
            {
                btnPrintCurrentContinuously.Text = "Stop print current continuously";
                MessageBox.Show("Current sensor is set to 0");
                SetButtonEnabled(btnRunTestOneTime, false);
                SetButtonEnabled(btnRunTest, false);
                SetButtonEnabled(btnCalibrateLoadcell, false);
            }
            else
            {
                btnPrintCurrentContinuously.Text = "Print current continuously";
                SetButtonEnabled(btnRunTestOneTime, true);
                if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                SetButtonEnabled(btnCalibrateLoadcell, true);
            }

            SendData(411121);
        }
    }
}
