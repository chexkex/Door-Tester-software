using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Komunikation_test
{
    public partial class Form1
    {
        //Starting next test if count is still over 1
        public async void RunNextTest()
        {
            if (timesToRun > 0) { await Task.Delay(waitTime * 1000); SendData(111111); }

            else
            {
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

        //Saving to file
        private void SaveDataToCSV(string filePath, params object[] values)
        {
            string line = string.Join(";", values);

            try { using (StreamWriter sw = new StreamWriter(filePath, true)) { sw.WriteLine(line); } }

            catch (Exception ex) { MessageBox.Show("Fel vid sparande av data till fil: " + ex.Message); }

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

        //Prevent and restor Sleep mode on PC
        public static void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        }

        public static void RestoreSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }
    }
}
