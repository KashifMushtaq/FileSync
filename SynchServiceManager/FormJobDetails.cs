using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SynchServiceNS
{
    public partial class FormJobDetails : Form
    {
        public string m_JobID = string.Empty;
        private const string SPLIT_CHAR_MAIN = "|";
        private const string SPLIT_CHAR = ",";
        private const string SPLIT_CHAR_EQUAL = "=";
        private bool m_bDataChanged = false;
        private bool m_bIsLoading = false;

        private object threadLock = new object();

        private INI m_INI = new INI();
        public clsArguments m_arg;

        public FormJobDetails()
        {
            InitializeComponent();
        }

        private void FormJobDetails_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_JobID))
            {
                try
                {
                    m_bIsLoading = true;

                    ShowJobData(m_JobID);
                    textBox_JobID.ReadOnly = true;
                    m_bIsLoading = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_bIsLoading = false;
                }
            }
            else 
            {
                int newId = m_INI.GetNextJobId();

                m_arg = new clsArguments();
                m_arg.JobLastRun = "0";
                m_arg.JobStatus = "1";
                m_arg.SynchType = "1";
                m_arg.RunAfter = numericUpDown_Interval.Value.ToString();
                m_arg.LogLevel = "3";

                comboBox_LogLevel.SelectedIndex = 2;
                comboBox_SynchType.SelectedIndex = 0;

                textBox_JobID.Text = newId.ToString();
            }

            textBox_JobID.ReadOnly = true;
        }

        private void Button_Source_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog.SelectedPath = this.textBox_Source.Text;
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.Description = "Select Source Files Folder";
            DialogResult dR = folderBrowserDialog.ShowDialog();
            if (dR == DialogResult.OK)
            {
                string sPath = folderBrowserDialog.SelectedPath;
                if (sPath != this.textBox_Source.Text)
                {
                    this.textBox_Source.Text = sPath;
                    m_bDataChanged = true;
                    m_arg.JobSource = textBox_Source.Text;
                }
            }
        }

        private void NumericUpDown_Interval_ValueChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
 
            m_arg.RunAfter = numericUpDown_Interval.Value.ToString();
            m_bDataChanged = true;
        }
        private void SaveJob()
        {
            if (m_bDataChanged)
            {
                if (string.IsNullOrEmpty(m_JobID))
                {
                    string JobID = textBox_JobID.Text.Trim();
                    if (!CheckDuplicateJob(JobID) && !string.IsNullOrEmpty(JobID))
                    {
                        m_JobID = JobID;
                        m_arg.JobName = JobID;
                        m_bDataChanged = true;
                    }
                    else
                    {
                        MessageBox.Show("Can not add duplicate job name. Job name must be unique.");
                        textBox_JobID.Text = string.Empty;
                        m_JobID = "";
                        m_arg.JobName = "";
                        return;
                    }
                }
                
                if (string.IsNullOrEmpty(m_arg.JobSource))
                {
                    MessageBox.Show("Source can not be empty.");
                    return;
                }

                if(string.IsNullOrEmpty(GetItemsPiped(listBox_Destinations)))
                {
                    MessageBox.Show("There should be at-least on destination.");
                    return;
                }

                m_INI.IniWriteValue(m_JobID, m_arg.getValueStringForINI());
                m_bDataChanged = false;
                MessageBox.Show("Changes Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Nothing Changed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowJobData(string Job)
        {
            string JobValue = m_INI.IniReadValue(Job);

            if (!string.IsNullOrEmpty(JobValue))
            {
                //INI File
                //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter
                string[] JobData = JobValue.Split(SPLIT_CHAR_MAIN[0]);

                this.textBox_JobID.Text = Job;
                this.textBox_JobDescription.Text = JobData[0];
                this.textBox_Source.Text = JobData[1];

                listBox_Destinations.Items.Clear();
                listBox_Destinations.Items.AddRange(JobData[2].Split(SPLIT_CHAR[0]));

                this.numericUpDown_Interval.Value = decimal.Parse(JobData[9]);

                this.textBox_FileExFilter.Text = JobData[4];
                this.textBox_FileIncFilter.Text = JobData[3];
                this.textBox_SubDirExcFilter.Text = JobData[5];


                if (JobData[6] == "1")
                {
                    comboBox_SynchType.SelectedIndex = 0;
                }
                else if (JobData[6] == "2")
                {
                    comboBox_SynchType.SelectedIndex = 1;
                }
                else
                {
                    comboBox_SynchType.SelectedIndex = 2;
                }

                if (JobData[10] == "1")
                {
                    comboBox_LogLevel.SelectedIndex = 0;
                }
                else if (JobData[10] == "2")
                {
                    comboBox_LogLevel.SelectedIndex = 1;
                }
                else if (JobData[10] == "3")
                {
                    comboBox_LogLevel.SelectedIndex = 2;
                }

                else if (JobData[10] == "4")
                {
                    comboBox_LogLevel.SelectedIndex = 3;
                }
                else
                {
                    comboBox_LogLevel.SelectedIndex = 4;
                }

                if (JobData[11] == "1")
                {
                    checkBox_UseSynshFramework.Checked=true;
                }
                else
                {
                    checkBox_UseSynshFramework.Checked = false;
                }
                if (JobData[12] == "1")
                {
                    checkBox_RunAt.Checked = true;
                }
                else
                {
                    checkBox_RunAt.Checked = false;
                }
                dateTimePicker_RunAt.Text = JobData[13];
            }
        }

        private void TextBox_JobDescription_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.JobDescription = textBox_JobDescription.Text;
            m_bDataChanged = true;
        }

        private void Button_AddDest_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog.SelectedPath = this.textBox_Source.Text;
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.Description = "Select Destination Folder";
            DialogResult dR = folderBrowserDialog.ShowDialog();
            if (dR == DialogResult.OK)
            {
                string sPath = folderBrowserDialog.SelectedPath;
                if (!CheckDuplicate(sPath, this.listBox_Destinations))
                {
                    string folderName = new DirectoryInfo(textBox_Source.Text).Name;
                    if (!string.IsNullOrEmpty(folderName))
                    {
                        if (!sPath.EndsWith(folderName))
                        {
                            sPath = Path.Combine(sPath, folderName);
                        }
                    }

                    this.listBox_Destinations.Items.Add(sPath);
                    m_bDataChanged = true;
                    m_arg.JobDestinations = GetItemsPiped(listBox_Destinations).Split(SPLIT_CHAR[0]);
                    
                }
            }
        }

        private void Button_RemoveDest_Click(object sender, EventArgs e)
        {
            if (this.listBox_Destinations.SelectedIndex != -1)
            {
                this.listBox_Destinations.Items.RemoveAt(this.listBox_Destinations.SelectedIndex);
                m_bDataChanged = true;
                if (listBox_Destinations.Items.Count > 0)
                {
                    m_arg.JobDestinations = GetItemsPiped(listBox_Destinations).Split(SPLIT_CHAR[0]);
                }
                else
                {
                    m_arg.JobDestinations = null;
                }
            }
        }

        private void ComboBox_SynchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.SynchType = (comboBox_SynchType.SelectedIndex + 1).ToString();
            m_bDataChanged = true;
        }

        private void TextBox_FileExFilter_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.FileFiltersEx = textBox_FileExFilter.Text.Split(SPLIT_CHAR[0]);
            m_bDataChanged = true;
        }

        private void TextBox_FileIncFilter_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.FileFiltersIn = textBox_FileIncFilter.Text.Split(SPLIT_CHAR[0]);
            m_bDataChanged = true;
        }

        private void TextBox_SubDirExcFilter_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_bDataChanged = true;

            if (textBox_SubDirExcFilter.Text.IndexOf("*") != -1)
            {
                MessageBox.Show("Can not have Wild-card i.e * in Sub Dir Filters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            m_arg.DirFiltersEx = textBox_SubDirExcFilter.Text.Split(SPLIT_CHAR[0]);
            
        }

        private void ComboBox_LogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.LogLevel = (comboBox_LogLevel.SelectedIndex + 1).ToString();
            m_bDataChanged = true;
        }

        private void Button_SaveJobDetails_Click(object sender, EventArgs e)
        {
            if (m_bDataChanged)
            {
                try
                {
                    SaveJob();
                    m_bDataChanged = false;
                    this.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nothing Changed");
            }
        }


        private bool CheckDuplicateJob(string thisJob)
        {
            bool bR = false;
            try
            {
                INI ini = new INI();
                string[] Jobs = ini.ReadSection();
                foreach (string iniValue in Jobs)
                {
                    string JobName = iniValue.Split(SPLIT_CHAR_EQUAL[0])[0];
                    if (thisJob.Trim().ToLower() == JobName.Trim().ToLower())
                    {
                        bR = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
            return bR;
        }





        private bool CheckDuplicate(string FindItem, ComboBox comboBox)
        {

            if (comboBox.Items.Count > 0)
            {
                for (int iC = 0; iC < comboBox.Items.Count; iC++)
                {
                    string sVal = comboBox.Items[iC].ToString();
                    if (sVal.Contains(FindItem) && sVal.Equals(FindItem, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        private bool CheckDuplicate(string FindItem, ListBox listBox)
        {

            if (listBox.Items.Count > 0)
            {
                for (int iC = 0; iC < listBox.Items.Count; iC++)
                {
                    string sVal = listBox.Items[iC].ToString();
                    if (sVal.Contains(FindItem) && sVal.Equals(FindItem, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        private bool CheckDuplicate(string FindItem, ListView listBox)
        {

            if (listBox == null) return false;

            if (listBox.Items.Count > 0)
            {
                for (int iC = 0; iC < listBox.Items.Count; iC++)
                {
                    string sVal = listBox.Items[iC].SubItems[0].Text;
                    if (sVal.Contains(FindItem) && sVal.Equals(FindItem, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private string GetItemsPiped(ListView oObj)
        {
            if (oObj.Items.Count > 0)
            {
                string sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (string)oObj.Items[iC].SubItems[0].Text;
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }

        private string GetItemsPiped(ListBox oObj)
        {
            if (oObj.Items.Count > 0)
            {
                string sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (string)oObj.Items[iC];
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }
        private string GetItemsPiped(ComboBox oObj)
        {
            if (oObj.Items.Count > 0)
            {
                string sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (string)oObj.Items[iC];
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }

        private void TextBox_JobID_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_bDataChanged = true;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            if (m_bDataChanged)
            {
                if (MessageBox.Show("Job data has been changed. Discard changes?", "Detected Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    m_bDataChanged = false;
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void FormJobDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bDataChanged)
            {
                if (MessageBox.Show("Job data has been changed. Discard changes?", "Detected Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                }
                else
                {
                    e.Cancel=true;
                }
            }
        }

        private void TextBox_Source_TextChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            if (Directory.Exists(textBox_Source.Text))
            {
                //MessageBox.Show("Directory does not exist or is in-accessible. If it's a network location, make sure it's a valid path", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                m_arg.JobSource = textBox_Source.Text;
                m_bDataChanged = true;
            }                
        }

        private void CheckBox_UseSynshFramework_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.UseSynchFramework = (checkBox_UseSynshFramework.Checked)?"1":"0";
            m_bDataChanged = true;
        }

        private void CheckBox_RunAt_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.UseRunAt = (checkBox_RunAt.Checked) ? "1" : "0";
            m_bDataChanged = true;
        }

        private void DateTimePicker_RunAt_ValueChanged(object sender, EventArgs e)
        {
            if (m_bIsLoading) return;
            m_arg.RunAt = dateTimePicker_RunAt.Text;
            m_bDataChanged = true;
        }

   }
}
