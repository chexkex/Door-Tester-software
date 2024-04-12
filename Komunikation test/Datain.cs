using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komunikation_test
{
    public partial class Form1
    {
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

                if (calculatedChecksum == receivedChecksum)
                {

                    //Add impementation for checksums for ESP32 to PC

                }

                if (data.Trim().Equals("1111217")) { SendData(lastDataSent); }

                if (receivedNumber == 411115 && oneTimeCall)
                {
                    MessageBox.Show("Calibration whid no load is completed! Calibrat whid known load now.");
                    SetButtonEnabled(btnCalibrateLoadcellKnownLoad, true);
                    SetTextBoxReadOnly(knownLoadInput, false);
                    oneTimeCall = false;

                }
                if (receivedNumber == 411116 && oneTimeCall)
                {
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
                //Calibrate door
                if (doorIsInCalibration)
                {
                    if (receivedNumber > 159999 && receivedNumber < 170000) { totalPulse = receivedNumber % 10000; }   
                    if (receivedNumber > 209999 && receivedNumber < 220000) { totalPulseLong = receivedNumber % 10000; if (totalPulseLong > 0) { totalPulseToLong = true; } }
               
                   
                    if (receivedNumber == 111116) {
                        if (totalPulseToLong)
                        {
                            totalPulseToLong = false;
                            TotalPulseOneOpening = ((totalPulse * 10) + totalPulseLong) / 2;
                        }
                        else { TotalPulseOneOpening = totalPulse / 2; }

                        doorIsInCalibration = false; AddItemToListBox("!");  
                        AddItemToListBox(TotalPulseOneOpening.ToString() + " One opening pulses");
                        doorIsCalibrated = true;
                        SetButtonEnabled(btnRunTestOneTime, true);
                        if (rbtnLoggerOn.Checked) { SetButtonEnabled(btnRunTest, true); }
                        SetButtonEnabled(btnCalibrateLoadcell, true);
                        SetButtonEnabled(btnPrintCurrentContinuously, true);
                        SetButtonEnabled(btnPrintFroceContinuously, true);
                        SetButtonEnabled(btnRecalibrateDoor, true);
                    }
                    
                }

                //Is test is runing
                if (testing && firstTest)
                {
                    //Splits the numbers to right varible
                    firstTest = false;
                    if (receivedNumber == 111113 || receivedNumber == 111115)
                    {

                        MessageBox.Show("Test cant be run something wrong whid the door. Press continue butten when problem is solved");
                        SetButtonEnabled(btnContinue, true);
                        if (!rbtnLoggerOn.Checked) { timesToRun++; }
                    }

                    if (receivedNumber == 111116)
                    {

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
                            else if (IntConvet(currentChannels) == 3)
                            {
                                SaveDataToCSV(filePath, timesRuned.ToString(), forceReturn.ToString(), currentReturn1.ToString(), currentReturn2.ToString(), currentReturn3.ToString(), hallTotalReturn.ToString(),
                             hallDiffReturn.ToString(), speedReturn.ToString());
                            }

                            timesRuned++;

                            //Stopps the program if limit values exceeded
                            if (forceReturn > forceLimit) { MessageBox.Show("Froce has exceeded its limlit value. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn1 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 1. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn2 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 2. Press continue to run again"); errorOcurred = true; }
                            if (currentReturn3 > currentLimit) { MessageBox.Show("Current has exceeded its limit value on current sensor 3. Press continue to run again"); errorOcurred = true; }
                            if (calculatedKineticEnergy > kineticEnergyLimit) { MessageBox.Show("The kinetic energi has exceeded its limit value. Press continue to run again"); errorOcurred = true; }
                            if (errorOcurred) { SetButtonEnabled(btnContinue, true); errorOcurred = false; }
                            else { RunNextTest(); }

                        }
                        else { RunNextTest(); }


                    }

                    if (receivedNumber == 111112) { /*inATest = true;*/ AddItemToListBox("Next test has begin"); }

                    if (receivedNumber == 111114)
                    {

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

                        int recivedSpeed = receivedNumber % 10000;
                        float recivedSpeedF = (float)recivedSpeed / 100;
                        if (doorType == "Hinged door")
                        {
                            float distance90Degrees = (float)((doorLenght * 2 * 3.141592) / 4);
                            float distanceOn10Pulses = (distance90Degrees / TotalPulseOneOpening) * 10;
                            speedReturn = (int)(distanceOn10Pulses / (recivedSpeedF / 1000));
                            AddItemToListBox(speedReturn.ToString() + " mm/s, Maxmimum speed");
                        }
                        else if (doorType == "Sliding door") {
                            float distanceOn10Pulses = (float)(doorLenght / TotalPulseOneOpening) * 10;
                            speedReturn = (int)(distanceOn10Pulses / (recivedSpeedF / 1000));
                            AddItemToListBox(speedReturn.ToString() + " mm/s, Maxmimum speed");
                        }
                        

                    }

                    if (receivedNumber > 209999 && receivedNumber < 220000) { totalPulseLong = receivedNumber % 10000; if (totalPulseLong > 0) { totalPulseToLong = true; } }


                }
                else if ((btnPrintCurrentcon || btnPrintFrocecon) && printConDataonce)
                {
                    printConDataonce = false;
                    if (receivedNumber > 119999 && receivedNumber < 130000)
                    {
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
    }
}
