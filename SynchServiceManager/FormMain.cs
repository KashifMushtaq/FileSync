using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;

namespace SynchServiceNS
{
    public partial class FormMain : Form
    {
        private const string SPLIT_CHAR_MAIN = "|";
        private const string SPLIT_CHAR = ",";
        private const string SPLIT_CHAR_EQUAL = "=";
        static private object threadLock = new object();
        private static ServiceController serviceController = new ServiceController();

        private Logging m_logger = new Logging();
        static INI m_INI = new INI();

        public delegate void SetTextCallback(string text);

        public FormMain()
        {
            InitializeComponent();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                this.richTextBox_Log.Text = "";
                m_logger.ClearLogFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LogMessage(string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.richTextBox_Log.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogMessage);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox_Status.AppendText(  text );
                Application.DoEvents();
                Thread.Sleep(500);
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            serviceController.Refresh();

            richTextBox_Log.Text = m_logger.getLogText();
            try
            {
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    button_StartService.Enabled = false;
                    buttonStopService.Enabled = true;
                }
                else if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    button_StartService.Enabled = true;
                    buttonStopService.Enabled = false;
                }
            }
            catch (Exception)
            {

                button_StartService.Enabled = false;
                buttonStopService.Enabled = false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_logger.EnableLogBuffer = true;

            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Job ID";
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader1.Width = 150;

            ColumnHeader columnHeader2 = new ColumnHeader();
            columnHeader2.Text = "Job Description";
            columnHeader2.TextAlign = HorizontalAlignment.Left;
            columnHeader2.Width = 400;

            ColumnHeader columnHeader3 = new ColumnHeader();
            columnHeader3.Text = "Job Status";
            columnHeader3.TextAlign = HorizontalAlignment.Center;
            columnHeader3.Width = 150;

            this.listView_SavedJobs.Columns.Add(columnHeader1);
            this.listView_SavedJobs.Columns.Add(columnHeader2);
            this.listView_SavedJobs.Columns.Add(columnHeader3);


            loadAllJobs();

            if (listView_SavedJobs.Items.Count > 0) this.listView_SavedJobs.Items[0].Selected = true;

            try
            {
                if (DoesServiceExist("SynchService", "."))
                {
                    serviceController.MachineName = ".";
                    serviceController.ServiceName = "SynchService";

                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        button_StartService.Enabled = false;
                        buttonStopService.Enabled = true;
                    }
                    else if (serviceController.Status == ServiceControllerStatus.Stopped)
                    {
                        button_StartService.Enabled = true;
                        buttonStopService.Enabled = false;
                    }
                    timer.Enabled = true;
                }
                else
                {
                    button_StartService.Enabled = false;
                    buttonStopService.Enabled = false;
                    timer.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_StartService.Enabled = false;
                buttonStopService.Enabled = false;
            }
        }
        bool DoesServiceExist(string serviceName, string machineName)
        {
            bool bR = false;
            ServiceController[] services = ServiceController.GetServices(machineName);
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                {
                    bR = true;
                    break;
                }
            }
            return bR;
        }
        private void loadAllJobs()
        {
            listView_SavedJobs.Items.Clear();

            string[] iniSECTION;

            lock (threadLock)
            {
                //INI File
                //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter

                //read all jobs from ini file
                iniSECTION = m_INI.ReadSection();
            }


            List<string> Jobs = new List<string>();

            if (iniSECTION != null)
            {
                //make job list
                foreach (string iniValue in iniSECTION)
                {
                    string JobName = iniValue.Split(SPLIT_CHAR_EQUAL[0])[0];
                    if (!string.IsNullOrEmpty(JobName))
                    {
                        Jobs.Add(JobName.Trim());
                    }
                }

                //get job data
                foreach (string Job in Jobs)
                {
                    string JobValue = m_INI.IniReadValue(Job);

                    if (!string.IsNullOrEmpty(JobValue))
                    {
                        //INI File
                        //Job_Name=Job Description|Source|Destination 1...n|FileFilterIn 1...n|FileFilterEx 1...n|DirFilterEx 1...n|SynchType|JobStatus|LastRun|RunAfter
                        string[] JobData = JobValue.Split(SPLIT_CHAR_MAIN[0]);

                        try
                        {
                            clsArguments arg = new clsArguments();
                            arg.LoadData(Job, JobData);

                            addJob(Job, JobData[0], (JobData[7] == "1") ? "Enabled" : "Disabled", arg);
                        }
                        catch (Exception Ex)
                        {
                            //error in this job parameters, move to next job
                            MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            continue;
                        }

                    }
                }

            }
        }
        private bool addJob(string JobID, string JobDescription, string Enabled, clsArguments arg)
        {
            bool bR = false;

            if (!checkDuplicate(JobID, listView_SavedJobs))
            {
                ListViewItem item = new ListViewItem(JobID, 0);
                ListViewItem.ListViewSubItem sub1 = item.SubItems.Add(JobDescription);
                ListViewItem.ListViewSubItem sub2 = item.SubItems.Add(Enabled);


                ListViewItem thisItem = listView_SavedJobs.Items.Add(item);
                thisItem.Tag = arg;
                bR = true;
            }
            return bR;
        }





        private bool checkDuplicate(String FindItem, ComboBox comboBox)
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
        private bool checkDuplicate(String FindItem, ListBox listBox)
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
        private bool checkDuplicate(String FindItem, ListView listBox)
        {

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

        private String getItemsPiped(ListView oObj)
        {
            if (oObj.Items.Count > 0)
            {
                String sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (String)oObj.Items[iC].SubItems[0].Text;
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }

        private String getItemsPiped(ListBox oObj)
        {
            if (oObj.Items.Count > 0)
            {
                String sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (String)oObj.Items[iC];
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }
        private String getItemsPiped(ComboBox oObj)
        {
            if (oObj.Items.Count > 0)
            {
                String sPiped = "";
                for (int iC = 0; iC < oObj.Items.Count; iC++)
                {
                    if (sPiped != "") sPiped += SPLIT_CHAR;
                    sPiped += (String)oObj.Items[iC];
                }
                if (sPiped != "")
                {
                    return sPiped.ToLower();
                }
            }
            return "";
        }


        private void button_AddNewJob_Click(object sender, EventArgs e)
        {
            FormJobDetails fd = new FormJobDetails();
            fd.ShowDialog();
            fd.m_JobID = string.Empty;
            loadAllJobs();
        }

        private void button_Disable_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    string JobID = listView_SavedJobs.Items[index].SubItems[0].Text;

                    listView_SavedJobs.Items[index].SubItems[2].Text = "Disabled";
                    clsArguments arg = (clsArguments)listView_SavedJobs.Items[index].Tag;
                    arg.JobStatus = "0";

                    try
                    {
                        m_INI.IniWriteValue(JobID, arg.getValueStringForINI());
                        button_Enable.Enabled = true;
                        button_Disable.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button_Enable_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    string JobID = listView_SavedJobs.Items[index].SubItems[0].Text;

                    listView_SavedJobs.Items[index].SubItems[2].Text = "Enabled";
                    clsArguments arg = (clsArguments)listView_SavedJobs.Items[index].Tag;
                    arg.JobStatus = "1";

                    try
                    {
                        m_INI.IniWriteValue(JobID, arg.getValueStringForINI());
                        button_Enable.Enabled = false;
                        button_Disable.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void listView_SavedJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    clsArguments arg = (clsArguments)listView_SavedJobs.Items[index].Tag;
                    if (arg.JobStatus == "1")
                    {
                        button_Disable.Enabled = true;
                        button_Enable.Enabled = false;
                    }
                    else
                    {
                        button_Disable.Enabled = false;
                        button_Enable.Enabled = true;
                    }
                }
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    string JobID = listView_SavedJobs.Items[index].SubItems[0].Text;
                    try
                    {
                        m_INI.IniWriteValue(JobID, null);
                        loadAllJobs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    string JobID = listView_SavedJobs.Items[index].SubItems[0].Text;
                    FormJobDetails fd = new FormJobDetails();
                    fd.m_JobID = JobID;
                    fd.m_arg = (clsArguments)listView_SavedJobs.Items[index].Tag;
                    fd.ShowDialog();
                    loadAllJobs();
                }
            }
        }

        private void button_StartService_Click(object sender, EventArgs e)
        {
            try
            {
                serviceController.Refresh();
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromTicks(DateTime.Now.AddMinutes(1).Ticks));
                button_StartService.Enabled = false;
                buttonStopService.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonStopService_Click(object sender, EventArgs e)
        {
            try
            {
                serviceController.Refresh();
                serviceController.Stop();
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromTicks(DateTime.Now.AddMinutes(1).Ticks));
                button_StartService.Enabled = true;
                buttonStopService.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_ManualRun_Click(object sender, EventArgs e)
        {
            this.richTextBox_Status.Text = string.Empty;

            ListView.SelectedIndexCollection indexes = listView_SavedJobs.SelectedIndices;
            foreach (int index in indexes)
            {
                if (index != -1)
                {
                    try
                    {
                        string JobID = listView_SavedJobs.Items[index].SubItems[0].Text;
                        clsArguments arg = (clsArguments)listView_SavedJobs.Items[index].Tag;

                        this.tabControl_Log.SelectedTab = tabPage_Log;
                        this.richTextBox_Status.Focus();
                        this.richTextBox_Status.ScrollToCaret();
                        clsCopy.RunJob(arg, this);
                        Application.DoEvents();
                        string LastMessage = string.Empty;
                        string NewMessage = string.Empty;
                        this.richTextBox_Status.AppendText("Job completed.\r\n");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Select Job to Run?", "Manual Run", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}