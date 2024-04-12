namespace Komunikation_test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.btnLoadPorts = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.forceLimitInput = new System.Windows.Forms.TextBox();
            this.currentLimitInput = new System.Windows.Forms.TextBox();
            this.limitValuesLable = new System.Windows.Forms.Label();
            this.forceLimitLable = new System.Windows.Forms.Label();
            this.currentLimitLable = new System.Windows.Forms.Label();
            this.kineticEnergyLable = new System.Windows.Forms.Label();
            this.consoleBox = new System.Windows.Forms.ListBox();
            this.runTimesInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRunTest = new System.Windows.Forms.Button();
            this.btnRunTestOneTime = new System.Windows.Forms.Button();
            this.lblSelectedFolder = new System.Windows.Forms.Label();
            this.rbtnLoggerOn = new System.Windows.Forms.RadioButton();
            this.btnChangeSavePlace = new System.Windows.Forms.Button();
            this.returnDataBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.kinetcEnergyInput = new System.Windows.Forms.TextBox();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.waitTimeInput = new System.Windows.Forms.TextBox();
            this.currentChannelsCombobox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCalibrateLoadcell = new System.Windows.Forms.Button();
            this.btnCalibrateLoadcellNoLoad = new System.Windows.Forms.Button();
            this.btnCalibrateLoadcellKnownLoad = new System.Windows.Forms.Button();
            this.knownLoadInput = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPrintFroceContinuously = new System.Windows.Forms.Button();
            this.btnPrintCurrentContinuously = new System.Windows.Forms.Button();
            this.btnCalibrateDoor = new System.Windows.Forms.Button();
            this.doorLengthInput = new System.Windows.Forms.TextBox();
            this.doorWeightInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStopCalibrateDoor = new System.Windows.Forms.Button();
            this.doorTypeComboBox = new System.Windows.Forms.ComboBox();
            this.btnRecalibrateDoor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(85, 221);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(150, 77);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(275, 221);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(150, 77);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(117, 77);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(118, 28);
            this.comboBoxPort.TabIndex = 2;
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(117, 136);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(118, 28);
            this.comboBoxBaudRate.TabIndex = 3;
            // 
            // btnLoadPorts
            // 
            this.btnLoadPorts.Location = new System.Drawing.Point(275, 77);
            this.btnLoadPorts.Name = "btnLoadPorts";
            this.btnLoadPorts.Size = new System.Drawing.Size(150, 77);
            this.btnLoadPorts.TabIndex = 5;
            this.btnLoadPorts.Text = "Load ports";
            this.btnLoadPorts.UseVisualStyleBackColor = true;
            this.btnLoadPorts.Click += new System.EventHandler(this.btnLoadPorts_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Com port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Baud Rate";
            // 
            // forceLimitInput
            // 
            this.forceLimitInput.Location = new System.Drawing.Point(637, 128);
            this.forceLimitInput.Name = "forceLimitInput";
            this.forceLimitInput.Size = new System.Drawing.Size(100, 26);
            this.forceLimitInput.TabIndex = 8;
            this.forceLimitInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // currentLimitInput
            // 
            this.currentLimitInput.Location = new System.Drawing.Point(637, 172);
            this.currentLimitInput.Name = "currentLimitInput";
            this.currentLimitInput.Size = new System.Drawing.Size(100, 26);
            this.currentLimitInput.TabIndex = 9;
            // 
            // limitValuesLable
            // 
            this.limitValuesLable.AutoSize = true;
            this.limitValuesLable.Location = new System.Drawing.Point(633, 95);
            this.limitValuesLable.Name = "limitValuesLable";
            this.limitValuesLable.Size = new System.Drawing.Size(91, 20);
            this.limitValuesLable.TabIndex = 12;
            this.limitValuesLable.Text = "Limit values";
            // 
            // forceLimitLable
            // 
            this.forceLimitLable.AutoSize = true;
            this.forceLimitLable.Location = new System.Drawing.Point(743, 131);
            this.forceLimitLable.Name = "forceLimitLable";
            this.forceLimitLable.Size = new System.Drawing.Size(106, 20);
            this.forceLimitLable.TabIndex = 13;
            this.forceLimitLable.Text = "Force limit (N)";
            // 
            // currentLimitLable
            // 
            this.currentLimitLable.AutoSize = true;
            this.currentLimitLable.Location = new System.Drawing.Point(743, 175);
            this.currentLimitLable.Name = "currentLimitLable";
            this.currentLimitLable.Size = new System.Drawing.Size(131, 20);
            this.currentLimitLable.TabIndex = 14;
            this.currentLimitLable.Text = "Current limit (mA)";
            // 
            // kineticEnergyLable
            // 
            this.kineticEnergyLable.AutoSize = true;
            this.kineticEnergyLable.Location = new System.Drawing.Point(743, 227);
            this.kineticEnergyLable.Name = "kineticEnergyLable";
            this.kineticEnergyLable.Size = new System.Drawing.Size(161, 20);
            this.kineticEnergyLable.TabIndex = 15;
            this.kineticEnergyLable.Text = "Kinetic energy limit (J)";
            // 
            // consoleBox
            // 
            this.consoleBox.FormattingEnabled = true;
            this.consoleBox.ItemHeight = 20;
            this.consoleBox.Location = new System.Drawing.Point(85, 449);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.Size = new System.Drawing.Size(288, 284);
            this.consoleBox.TabIndex = 16;
            this.consoleBox.SelectedIndexChanged += new System.EventHandler(this.consoleBox_SelectedIndexChanged);
            // 
            // runTimesInput
            // 
            this.runTimesInput.Location = new System.Drawing.Point(637, 272);
            this.runTimesInput.Name = "runTimesInput";
            this.runTimesInput.Size = new System.Drawing.Size(100, 26);
            this.runTimesInput.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(743, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Run count";
            // 
            // btnRunTest
            // 
            this.btnRunTest.Location = new System.Drawing.Point(637, 370);
            this.btnRunTest.Name = "btnRunTest";
            this.btnRunTest.Size = new System.Drawing.Size(150, 77);
            this.btnRunTest.TabIndex = 19;
            this.btnRunTest.Text = "Run test";
            this.btnRunTest.UseVisualStyleBackColor = true;
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);
            // 
            // btnRunTestOneTime
            // 
            this.btnRunTestOneTime.Location = new System.Drawing.Point(813, 370);
            this.btnRunTestOneTime.Name = "btnRunTestOneTime";
            this.btnRunTestOneTime.Size = new System.Drawing.Size(150, 77);
            this.btnRunTestOneTime.TabIndex = 20;
            this.btnRunTestOneTime.Text = "Run test one time (No logging of data)";
            this.btnRunTestOneTime.UseVisualStyleBackColor = true;
            this.btnRunTestOneTime.Click += new System.EventHandler(this.btnRunTestOneTime_Click);
            // 
            // lblSelectedFolder
            // 
            this.lblSelectedFolder.AutoSize = true;
            this.lblSelectedFolder.Location = new System.Drawing.Point(738, 552);
            this.lblSelectedFolder.Name = "lblSelectedFolder";
            this.lblSelectedFolder.Size = new System.Drawing.Size(216, 20);
            this.lblSelectedFolder.TabIndex = 21;
            this.lblSelectedFolder.Text = "Selected path for saving data";
            // 
            // rbtnLoggerOn
            // 
            this.rbtnLoggerOn.Location = new System.Drawing.Point(578, 550);
            this.rbtnLoggerOn.Name = "rbtnLoggerOn";
            this.rbtnLoggerOn.Size = new System.Drawing.Size(104, 24);
            this.rbtnLoggerOn.TabIndex = 29;
            this.rbtnLoggerOn.Text = "Logger";
            // 
            // btnChangeSavePlace
            // 
            this.btnChangeSavePlace.Location = new System.Drawing.Point(742, 599);
            this.btnChangeSavePlace.Name = "btnChangeSavePlace";
            this.btnChangeSavePlace.Size = new System.Drawing.Size(150, 77);
            this.btnChangeSavePlace.TabIndex = 24;
            this.btnChangeSavePlace.Text = "Change path";
            this.btnChangeSavePlace.UseVisualStyleBackColor = true;
            this.btnChangeSavePlace.Click += new System.EventHandler(this.btnChangeSavePlace_Click);
            // 
            // returnDataBox
            // 
            this.returnDataBox.FormattingEnabled = true;
            this.returnDataBox.ItemHeight = 20;
            this.returnDataBox.Location = new System.Drawing.Point(1128, 50);
            this.returnDataBox.Name = "returnDataBox";
            this.returnDataBox.Size = new System.Drawing.Size(288, 284);
            this.returnDataBox.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(743, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Wait time between test (s)";
            // 
            // kinetcEnergyInput
            // 
            this.kinetcEnergyInput.Location = new System.Drawing.Point(637, 227);
            this.kinetcEnergyInput.Name = "kinetcEnergyInput";
            this.kinetcEnergyInput.Size = new System.Drawing.Size(100, 26);
            this.kinetcEnergyInput.TabIndex = 30;
            // 
            // btnStopTest
            // 
            this.btnStopTest.Location = new System.Drawing.Point(987, 370);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(150, 77);
            this.btnStopTest.TabIndex = 31;
            this.btnStopTest.Text = "Stop test";
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.btnStopTest_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(1160, 370);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(150, 77);
            this.btnContinue.TabIndex = 32;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // waitTimeInput
            // 
            this.waitTimeInput.Location = new System.Drawing.Point(637, 321);
            this.waitTimeInput.Name = "waitTimeInput";
            this.waitTimeInput.Size = new System.Drawing.Size(100, 26);
            this.waitTimeInput.TabIndex = 33;
            // 
            // currentChannelsCombobox
            // 
            this.currentChannelsCombobox.FormattingEnabled = true;
            this.currentChannelsCombobox.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.currentChannelsCombobox.Location = new System.Drawing.Point(1434, 95);
            this.currentChannelsCombobox.Name = "currentChannelsCombobox";
            this.currentChannelsCombobox.Size = new System.Drawing.Size(170, 28);
            this.currentChannelsCombobox.TabIndex = 36;
            this.currentChannelsCombobox.Text = "Current channels";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1610, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Current channels";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1124, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 20);
            this.label8.TabIndex = 40;
            this.label8.Text = "Return data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(81, 416);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 41;
            this.label9.Text = "Data info";
            // 
            // btnCalibrateLoadcell
            // 
            this.btnCalibrateLoadcell.Location = new System.Drawing.Point(1780, 52);
            this.btnCalibrateLoadcell.Name = "btnCalibrateLoadcell";
            this.btnCalibrateLoadcell.Size = new System.Drawing.Size(150, 77);
            this.btnCalibrateLoadcell.TabIndex = 42;
            this.btnCalibrateLoadcell.Text = "Calibrate loadcell";
            this.btnCalibrateLoadcell.UseVisualStyleBackColor = true;
            this.btnCalibrateLoadcell.Click += new System.EventHandler(this.btnCalibrateLoadcell_Click);
            // 
            // btnCalibrateLoadcellNoLoad
            // 
            this.btnCalibrateLoadcellNoLoad.Location = new System.Drawing.Point(1780, 146);
            this.btnCalibrateLoadcellNoLoad.Name = "btnCalibrateLoadcellNoLoad";
            this.btnCalibrateLoadcellNoLoad.Size = new System.Drawing.Size(150, 77);
            this.btnCalibrateLoadcellNoLoad.TabIndex = 43;
            this.btnCalibrateLoadcellNoLoad.Text = "Calibrate no load";
            this.btnCalibrateLoadcellNoLoad.UseVisualStyleBackColor = true;
            this.btnCalibrateLoadcellNoLoad.Click += new System.EventHandler(this.btnCalibrateLoadcellNoLoad_Click);
            // 
            // btnCalibrateLoadcellKnownLoad
            // 
            this.btnCalibrateLoadcellKnownLoad.Location = new System.Drawing.Point(1780, 242);
            this.btnCalibrateLoadcellKnownLoad.Name = "btnCalibrateLoadcellKnownLoad";
            this.btnCalibrateLoadcellKnownLoad.Size = new System.Drawing.Size(150, 77);
            this.btnCalibrateLoadcellKnownLoad.TabIndex = 44;
            this.btnCalibrateLoadcellKnownLoad.Text = "Calibrate known load";
            this.btnCalibrateLoadcellKnownLoad.UseVisualStyleBackColor = true;
            this.btnCalibrateLoadcellKnownLoad.Click += new System.EventHandler(this.btnCalibrateLoadcellKnownLoad_Click);
            // 
            // knownLoadInput
            // 
            this.knownLoadInput.Location = new System.Drawing.Point(1936, 267);
            this.knownLoadInput.Name = "knownLoadInput";
            this.knownLoadInput.Size = new System.Drawing.Size(129, 26);
            this.knownLoadInput.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1936, 242);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 20);
            this.label10.TabIndex = 46;
            this.label10.Text = "Known load (N)";
            // 
            // btnPrintFroceContinuously
            // 
            this.btnPrintFroceContinuously.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrintFroceContinuously.Location = new System.Drawing.Point(1780, 339);
            this.btnPrintFroceContinuously.Name = "btnPrintFroceContinuously";
            this.btnPrintFroceContinuously.Size = new System.Drawing.Size(150, 77);
            this.btnPrintFroceContinuously.TabIndex = 47;
            this.btnPrintFroceContinuously.Text = "Print force continuously";
            this.btnPrintFroceContinuously.UseVisualStyleBackColor = true;
            this.btnPrintFroceContinuously.Click += new System.EventHandler(this.btnPrintFroceContinuously_Click);
            // 
            // btnPrintCurrentContinuously
            // 
            this.btnPrintCurrentContinuously.Location = new System.Drawing.Point(1606, 339);
            this.btnPrintCurrentContinuously.Name = "btnPrintCurrentContinuously";
            this.btnPrintCurrentContinuously.Size = new System.Drawing.Size(150, 77);
            this.btnPrintCurrentContinuously.TabIndex = 48;
            this.btnPrintCurrentContinuously.Text = "Print current continuously";
            this.btnPrintCurrentContinuously.UseVisualStyleBackColor = true;
            this.btnPrintCurrentContinuously.Click += new System.EventHandler(this.btnPrintCurrentContinuously_Click);
            // 
            // btnCalibrateDoor
            // 
            this.btnCalibrateDoor.Location = new System.Drawing.Point(1407, 778);
            this.btnCalibrateDoor.Name = "btnCalibrateDoor";
            this.btnCalibrateDoor.Size = new System.Drawing.Size(150, 77);
            this.btnCalibrateDoor.TabIndex = 49;
            this.btnCalibrateDoor.Text = "Calibrate door";
            this.btnCalibrateDoor.UseVisualStyleBackColor = true;
            this.btnCalibrateDoor.Click += new System.EventHandler(this.btnCalibrateDoor_Click);
            // 
            // doorLengthInput
            // 
            this.doorLengthInput.Location = new System.Drawing.Point(1606, 775);
            this.doorLengthInput.Name = "doorLengthInput";
            this.doorLengthInput.Size = new System.Drawing.Size(134, 26);
            this.doorLengthInput.TabIndex = 50;
            // 
            // doorWeightInput
            // 
            this.doorWeightInput.Location = new System.Drawing.Point(1606, 826);
            this.doorWeightInput.Name = "doorWeightInput";
            this.doorWeightInput.Size = new System.Drawing.Size(134, 26);
            this.doorWeightInput.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1746, 778);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 20);
            this.label4.TabIndex = 52;
            this.label4.Text = "Door length (mm)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1746, 826);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 53;
            this.label6.Text = "Door weight (kg)";
            // 
            // btnStopCalibrateDoor
            // 
            this.btnStopCalibrateDoor.Location = new System.Drawing.Point(1236, 779);
            this.btnStopCalibrateDoor.Name = "btnStopCalibrateDoor";
            this.btnStopCalibrateDoor.Size = new System.Drawing.Size(150, 77);
            this.btnStopCalibrateDoor.TabIndex = 54;
            this.btnStopCalibrateDoor.Text = "Stop calibrate door";
            this.btnStopCalibrateDoor.UseVisualStyleBackColor = true;
            this.btnStopCalibrateDoor.Click += new System.EventHandler(this.btnStopCalibrateDoor_Click);
            // 
            // doorTypeComboBox
            // 
            this.doorTypeComboBox.FormattingEnabled = true;
            this.doorTypeComboBox.Items.AddRange(new object[] {
            "Hinged door",
            "Sliding door"});
            this.doorTypeComboBox.Location = new System.Drawing.Point(1606, 879);
            this.doorTypeComboBox.Name = "doorTypeComboBox";
            this.doorTypeComboBox.Size = new System.Drawing.Size(170, 28);
            this.doorTypeComboBox.TabIndex = 55;
            this.doorTypeComboBox.Text = "Door type";
            // 
            // btnRecalibrateDoor
            // 
            this.btnRecalibrateDoor.Location = new System.Drawing.Point(1407, 889);
            this.btnRecalibrateDoor.Name = "btnRecalibrateDoor";
            this.btnRecalibrateDoor.Size = new System.Drawing.Size(150, 77);
            this.btnRecalibrateDoor.TabIndex = 56;
            this.btnRecalibrateDoor.Text = "Recalibrate door";
            this.btnRecalibrateDoor.UseVisualStyleBackColor = true;
            this.btnRecalibrateDoor.Click += new System.EventHandler(this.btnRecalibrateDoor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2124, 1090);
            this.Controls.Add(this.btnRecalibrateDoor);
            this.Controls.Add(this.doorTypeComboBox);
            this.Controls.Add(this.btnStopCalibrateDoor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.doorWeightInput);
            this.Controls.Add(this.doorLengthInput);
            this.Controls.Add(this.btnCalibrateDoor);
            this.Controls.Add(this.btnPrintCurrentContinuously);
            this.Controls.Add(this.btnPrintFroceContinuously);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.knownLoadInput);
            this.Controls.Add(this.btnCalibrateLoadcellKnownLoad);
            this.Controls.Add(this.btnCalibrateLoadcellNoLoad);
            this.Controls.Add(this.btnCalibrateLoadcell);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.currentChannelsCombobox);
            this.Controls.Add(this.waitTimeInput);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnStopTest);
            this.Controls.Add(this.kinetcEnergyInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.returnDataBox);
            this.Controls.Add(this.btnChangeSavePlace);
            this.Controls.Add(this.rbtnLoggerOn);
            this.Controls.Add(this.lblSelectedFolder);
            this.Controls.Add(this.btnRunTestOneTime);
            this.Controls.Add(this.btnRunTest);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.runTimesInput);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.kineticEnergyLable);
            this.Controls.Add(this.currentLimitLable);
            this.Controls.Add(this.forceLimitLable);
            this.Controls.Add(this.limitValuesLable);
            this.Controls.Add(this.currentLimitInput);
            this.Controls.Add(this.forceLimitInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadPorts);
            this.Controls.Add(this.comboBoxBaudRate);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Door Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Button btnLoadPorts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox forceLimitInput;
        private System.Windows.Forms.TextBox currentLimitInput;
        
        private System.Windows.Forms.Label limitValuesLable;
        private System.Windows.Forms.Label forceLimitLable;
        private System.Windows.Forms.Label currentLimitLable;
        private System.Windows.Forms.Label kineticEnergyLable;
        private System.Windows.Forms.ListBox consoleBox;
        private System.Windows.Forms.TextBox runTimesInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRunTest;
        private System.Windows.Forms.Button btnRunTestOneTime;
        private System.Windows.Forms.Label lblSelectedFolder;
        private System.Windows.Forms.RadioButton rbtnLoggerOn;
        private System.Windows.Forms.Button btnChangeSavePlace;
        private System.Windows.Forms.ListBox returnDataBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox kinetcEnergyInput;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox waitTimeInput;
        private System.Windows.Forms.ComboBox currentChannelsCombobox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCalibrateLoadcell;
        private System.Windows.Forms.Button btnCalibrateLoadcellNoLoad;
        private System.Windows.Forms.Button btnCalibrateLoadcellKnownLoad;
        private System.Windows.Forms.TextBox knownLoadInput;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPrintFroceContinuously;
        private System.Windows.Forms.Button btnPrintCurrentContinuously;
        private System.Windows.Forms.Button btnCalibrateDoor;
        private System.Windows.Forms.TextBox doorLengthInput;
        private System.Windows.Forms.TextBox doorWeightInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStopCalibrateDoor;
        private System.Windows.Forms.ComboBox doorTypeComboBox;
        private System.Windows.Forms.Button btnRecalibrateDoor;
    }
}

