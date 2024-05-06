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
        bool doorIsCalibrated = false;
        bool doorIsInCalibration = false;


        //Limit variables
        int forceLimit = 0;
        int currentLimit = 0;
        int kineticEnergyLimit = 0;
        int timesToRun = 0;
        int timesRuned = 1;
        int waitTime = 0;
        float calculatedKineticEnergy = 0;
        int knownLoad = 0;
        int doorLenght = 0;
        int doorweight = 0;
        int TotalPulseOneOpening = 0;
        string doorType;
        string measurment;
        int kinetickEnergytimes1000 = 0;
        

        float forceReturn = 0;
        int currentReturn1 = 0;
        int currentReturn2 = 0;
        int currentReturn3 = 0;
        int hallTotalReturn = 0;
        int hallDiffReturn = 0;
        int speedReturn = 0;
        int totalPulse = 0;
        int totalPulseLong = 0;
        int Kineticenergy = 0;

        private DateTime lastCallTime;
        


        public Form1()
        {   
            //Loading prots and baudRates
            InitializeComponent();
            InitializeSerialPort();
            LoadAvailablePorts();
            LoadBaudRates();
            ResetTime();

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
            SetButtonEnabled(btnCalibrateDoor, false);
            SetButtonEnabled(btnStopCalibrateDoor, false);
            SetButtonEnabled(btnRecalibrateDoor, false);
            comboBoxPort.Enabled = true;
            comboBoxBaudRate.Enabled = true;
            
        }

        //Turns off sleep mode on computer
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);       
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

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
                    SetButtonEnabled(btnCalibrateDoor, true);
                    comboBoxPort.Enabled = false;
                    comboBoxBaudRate.Enabled = false;
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
                    comboBoxBaudRate.Enabled = true;
                    comboBoxPort.Enabled = true;
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

            
            
            currentChannels = ReadComboBox(currentChannelsCombobox);

            //controls input values and not run the test
            if (forceLimit == 0 || currentLimit == 0 || kineticEnergyLimit == 0 || timesToRun == 0 || waitTime == 0 || forceLimit > 1000 || currentLimit > 10000 || timesToRun > 50000) { MessageBox.Show("Limit values are missing or out of range"); }
            else if (string.IsNullOrEmpty(currentChannels)) { MessageBox.Show("Current channels is missing");  }
            else if (!doorIsCalibrated) { MessageBox.Show("Door is not calibrated"); }
            
            //else the test begins
            else
            {
                if(IntConvet(currentChannels) == 1) { SaveDataToCSV(filePath, "Run times", "Max force (N)", "Current 1 (mA)", "Steps total", "Step diff", "Maximum speed (mm/s)", "Kinetic energy (mJ)"); }
                else if (IntConvet(currentChannels) == 2) { SaveDataToCSV(filePath, "Run times", "Max force (N)", "Current 1 (mA)", "Current 2 (mA)", "Steps total", "Step diff", "Maximum speed (mm/s)", "Kinetic energy (mJ)"); }
                else if (IntConvet(currentChannels) == 3) { SaveDataToCSV(filePath, "Run times", "Max force (N)", "Current 1 (mA)", "Current 2 (mA)", "Current 3 (mA)", "Steps total", "Step diff", "Maximum speed (mm/s)", "Kinetic energy (mJ)"); }
                btnRunTest.Enabled = false;
                btnStopTest.Enabled = true;
                btnRunTestOneTime.Enabled = false;
                btnDisconnect.Enabled = false;
                btnLoadPorts.Enabled = false;
                btnChangeSavePlace.Enabled = false;
                SetButtonEnabled(btnCalibrateLoadcell, false);
                SetButtonEnabled(btnPrintFroceContinuously, false);
                SetButtonEnabled(btnPrintCurrentContinuously, false);
                SetButtonEnabled(btnCalibrateDoor, false);
                SetButtonEnabled(btnRecalibrateDoor, false);

                forceLimitInput.ReadOnly = true;
                currentLimitInput.ReadOnly = true;
                kinetcEnergyInput.ReadOnly = true;
                runTimesInput.ReadOnly = true;
                waitTimeInput.ReadOnly = true;

                testing = true;
                ResetTime();
                PreventSleep();
                SendData(111111);
            }
        }

        //BTN for runing 1 test 
        private void btnRunTestOneTime_Click(object sender, EventArgs e)
        {
            currentChannels = (ReadComboBox(currentChannelsCombobox));

            if (string.IsNullOrEmpty(currentChannels)) { MessageBox.Show("Current channels is missing"); }
            else if (!doorIsCalibrated) { MessageBox.Show("Door is not calibrated"); }

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
                SetButtonEnabled(btnCalibrateDoor, false);
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
                    if (serialPort.IsOpen && !btnPrintCurrentcon && !btnPrintFrocecon) { btnRunTest.Enabled = true; }                    
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
                SetButtonEnabled(btnCalibrateDoor, false);
            }
            else
            {
                btnCalibrateLoadcell.Text = "Calibrate loadcell";
                SetButtonEnabled(btnRunTestOneTime, true);
                SetButtonEnabled(btnRunTest, true);
                SetButtonEnabled(btnCalibrateLoadcellNoLoad, false);
                SetButtonEnabled(btnPrintCurrentContinuously, true);
                SetButtonEnabled(btnPrintFroceContinuously, true);
                SetButtonEnabled(btnCalibrateDoor, true);
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
                SetButtonEnabled(btnCalibrateDoor, false);
            }
            else { 
                btnPrintFroceContinuously.Text = "Print force continuously";
                SetButtonEnabled(btnRunTestOneTime, true);
                if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                SetButtonEnabled(btnCalibrateLoadcell, true);
                SetButtonEnabled(btnCalibrateDoor, true);
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
                SetButtonEnabled(btnCalibrateDoor, false);
            }
            else
            {
                btnPrintCurrentContinuously.Text = "Print current continuously";
                SetButtonEnabled(btnRunTestOneTime, true);
                if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                SetButtonEnabled(btnCalibrateLoadcell, true);
                SetButtonEnabled (btnCalibrateDoor, true);
            }

            SendData(411121);
        }

        private void btnCalibrateDoor_Click(object sender, EventArgs e)
        {
            doorLenght = IntConvet(doorLengthInput.Text.Trim());
            doorweight = IntConvet(doorWeightInput.Text.Trim());
            doorType = ReadComboBox(doorTypeComboBox);
            measurment = ReadComboBox(PulseMesCombobox);

            if (measurment == "Pulse measurement") {
                if (doorweight == 0 || doorLenght == 0)
                {
                    MessageBox.Show("Door information is missing!");
                }
                else if (string.IsNullOrEmpty(doorType)) { MessageBox.Show("Door type is missing!"); }
                else
                {
                    SetButtonEnabled(btnCalibrateDoor, false);
                    SetButtonEnabled(btnStopCalibrateDoor, true);
                    SetButtonEnabled(btnRunTestOneTime, false);
                    SetButtonEnabled(btnRunTest, false);
                    SetButtonEnabled(btnCalibrateLoadcell, false);
                    SetButtonEnabled(btnPrintCurrentContinuously, false);
                    SetButtonEnabled(btnPrintFroceContinuously, false);
                    doorIsInCalibration = true;
                    TotalPulseOneOpening = 0;
                    SetTextBoxReadOnly(doorLengthInput, true);
                    SetTextBoxReadOnly(doorWeightInput, true);
                    SendData(411131);
                    doorTypeComboBox.Enabled = false;
                    PulseMesCombobox.Enabled = false;
                    MessageBox.Show("Open the door manually and let it close.");
                    SendData(111118);

                }
            }
            else
            {
                SendData(111119);
            }

            
            
        }

        private void btnStopCalibrateDoor_Click(object sender, EventArgs e)
        {
            SetButtonEnabled(btnStopCalibrateDoor, false);
            
            MessageBox.Show("Door i calibrated wait for data");
            SendData(411132);
        }

        private void btnRecalibrateDoor_Click(object sender, EventArgs e)
        {
            SetButtonEnabled(btnCalibrateDoor, true);
            SetTextBoxReadOnly(doorWeightInput, false);
            SetTextBoxReadOnly(doorLengthInput, false);
            SetButtonEnabled(btnRecalibrateDoor, false);
            doorTypeComboBox.Enabled = true;
            PulseMesCombobox.Enabled = true;
        }
    }
}
