namespace SynchServiceNS
{
    partial class FormJobDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJobDetails));
            this.groupBox_JobDetails = new System.Windows.Forms.GroupBox();
            this.checkBox_RunAt = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_RunAt = new System.Windows.Forms.DateTimePicker();
            this.checkBox_UseSynshFramework = new System.Windows.Forms.CheckBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_Minutes = new System.Windows.Forms.Label();
            this.numericUpDown_Interval = new System.Windows.Forms.NumericUpDown();
            this.button_SaveJobDetails = new System.Windows.Forms.Button();
            this.label_RunJobInterval = new System.Windows.Forms.Label();
            this.label_LogLevel = new System.Windows.Forms.Label();
            this.comboBox_LogLevel = new System.Windows.Forms.ComboBox();
            this.textBox_SubDirExcFilter = new System.Windows.Forms.TextBox();
            this.label_SubDirExcFilter = new System.Windows.Forms.Label();
            this.textBox_FileIncFilter = new System.Windows.Forms.TextBox();
            this.label_FileIncFilter = new System.Windows.Forms.Label();
            this.textBox_FileExFilter = new System.Windows.Forms.TextBox();
            this.label_FileExFilter = new System.Windows.Forms.Label();
            this.button_RemoveDest = new System.Windows.Forms.Button();
            this.button_AddDest = new System.Windows.Forms.Button();
            this.label_SynchType = new System.Windows.Forms.Label();
            this.comboBox_SynchType = new System.Windows.Forms.ComboBox();
            this.label_Destinations = new System.Windows.Forms.Label();
            this.listBox_Destinations = new System.Windows.Forms.ListBox();
            this.button_Source = new System.Windows.Forms.Button();
            this.textBox_Source = new System.Windows.Forms.TextBox();
            this.label_Source = new System.Windows.Forms.Label();
            this.label_JobDescription = new System.Windows.Forms.Label();
            this.textBox_JobID = new System.Windows.Forms.TextBox();
            this.textBox_JobDescription = new System.Windows.Forms.TextBox();
            this.label_JobID = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox_JobDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_JobDetails
            // 
            this.groupBox_JobDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_JobDetails.Controls.Add(this.checkBox_RunAt);
            this.groupBox_JobDetails.Controls.Add(this.dateTimePicker_RunAt);
            this.groupBox_JobDetails.Controls.Add(this.checkBox_UseSynshFramework);
            this.groupBox_JobDetails.Controls.Add(this.button_Close);
            this.groupBox_JobDetails.Controls.Add(this.button_Cancel);
            this.groupBox_JobDetails.Controls.Add(this.label_Minutes);
            this.groupBox_JobDetails.Controls.Add(this.numericUpDown_Interval);
            this.groupBox_JobDetails.Controls.Add(this.button_SaveJobDetails);
            this.groupBox_JobDetails.Controls.Add(this.label_RunJobInterval);
            this.groupBox_JobDetails.Controls.Add(this.label_LogLevel);
            this.groupBox_JobDetails.Controls.Add(this.comboBox_LogLevel);
            this.groupBox_JobDetails.Controls.Add(this.textBox_SubDirExcFilter);
            this.groupBox_JobDetails.Controls.Add(this.label_SubDirExcFilter);
            this.groupBox_JobDetails.Controls.Add(this.textBox_FileIncFilter);
            this.groupBox_JobDetails.Controls.Add(this.label_FileIncFilter);
            this.groupBox_JobDetails.Controls.Add(this.textBox_FileExFilter);
            this.groupBox_JobDetails.Controls.Add(this.label_FileExFilter);
            this.groupBox_JobDetails.Controls.Add(this.button_RemoveDest);
            this.groupBox_JobDetails.Controls.Add(this.button_AddDest);
            this.groupBox_JobDetails.Controls.Add(this.label_SynchType);
            this.groupBox_JobDetails.Controls.Add(this.comboBox_SynchType);
            this.groupBox_JobDetails.Controls.Add(this.label_Destinations);
            this.groupBox_JobDetails.Controls.Add(this.listBox_Destinations);
            this.groupBox_JobDetails.Controls.Add(this.button_Source);
            this.groupBox_JobDetails.Controls.Add(this.textBox_Source);
            this.groupBox_JobDetails.Controls.Add(this.label_Source);
            this.groupBox_JobDetails.Controls.Add(this.label_JobDescription);
            this.groupBox_JobDetails.Controls.Add(this.textBox_JobID);
            this.groupBox_JobDetails.Controls.Add(this.textBox_JobDescription);
            this.groupBox_JobDetails.Controls.Add(this.label_JobID);
            this.groupBox_JobDetails.Location = new System.Drawing.Point(12, 12);
            this.groupBox_JobDetails.Name = "groupBox_JobDetails";
            this.groupBox_JobDetails.Size = new System.Drawing.Size(1118, 416);
            this.groupBox_JobDetails.TabIndex = 7;
            this.groupBox_JobDetails.TabStop = false;
            this.groupBox_JobDetails.Text = "Job Details";
            // 
            // checkBox_RunAt
            // 
            this.checkBox_RunAt.AutoSize = true;
            this.checkBox_RunAt.Location = new System.Drawing.Point(365, 327);
            this.checkBox_RunAt.Name = "checkBox_RunAt";
            this.checkBox_RunAt.Size = new System.Drawing.Size(99, 17);
            this.checkBox_RunAt.TabIndex = 54;
            this.checkBox_RunAt.Text = "Use preset time";
            this.checkBox_RunAt.UseVisualStyleBackColor = true;
            this.checkBox_RunAt.CheckedChanged += new System.EventHandler(this.CheckBox_RunAt_CheckedChanged);
            // 
            // dateTimePicker_RunAt
            // 
            this.dateTimePicker_RunAt.CustomFormat = "HH:mm";
            this.dateTimePicker_RunAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_RunAt.Location = new System.Drawing.Point(470, 326);
            this.dateTimePicker_RunAt.Name = "dateTimePicker_RunAt";
            this.dateTimePicker_RunAt.ShowUpDown = true;
            this.dateTimePicker_RunAt.Size = new System.Drawing.Size(71, 20);
            this.dateTimePicker_RunAt.TabIndex = 53;
            this.toolTip.SetToolTip(this.dateTimePicker_RunAt, "Select time of the day to launch synch job");
            this.dateTimePicker_RunAt.Value = new System.DateTime(2010, 10, 9, 4, 29, 0, 0);
            this.dateTimePicker_RunAt.ValueChanged += new System.EventHandler(this.DateTimePicker_RunAt_ValueChanged);
            // 
            // checkBox_UseSynshFramework
            // 
            this.checkBox_UseSynshFramework.AutoSize = true;
            this.checkBox_UseSynshFramework.Location = new System.Drawing.Point(299, 40);
            this.checkBox_UseSynshFramework.Name = "checkBox_UseSynshFramework";
            this.checkBox_UseSynshFramework.Size = new System.Drawing.Size(179, 17);
            this.checkBox_UseSynshFramework.TabIndex = 52;
            this.checkBox_UseSynshFramework.Text = "Use Microsoft Synch Framework";
            this.checkBox_UseSynshFramework.UseVisualStyleBackColor = true;
            this.checkBox_UseSynshFramework.Visible = false;
            this.checkBox_UseSynshFramework.CheckedChanged += new System.EventHandler(this.CheckBox_UseSynshFramework_CheckedChanged);
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(1021, 386);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 51;
            this.button_Close.Text = "Close";
            this.toolTip.SetToolTip(this.button_Close, "Close this dialog");
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(940, 387);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 50;
            this.button_Cancel.Text = "Cancel";
            this.toolTip.SetToolTip(this.button_Cancel, "Cancel change");
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // label_Minutes
            // 
            this.label_Minutes.AutoSize = true;
            this.label_Minutes.Location = new System.Drawing.Point(296, 328);
            this.label_Minutes.Name = "label_Minutes";
            this.label_Minutes.Size = new System.Drawing.Size(44, 13);
            this.label_Minutes.TabIndex = 49;
            this.label_Minutes.Text = "Minutes";
            // 
            // numericUpDown_Interval
            // 
            this.numericUpDown_Interval.Location = new System.Drawing.Point(148, 326);
            this.numericUpDown_Interval.Maximum = new decimal(new int[] {
            43200,
            0,
            0,
            0});
            this.numericUpDown_Interval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Interval.Name = "numericUpDown_Interval";
            this.numericUpDown_Interval.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Interval.TabIndex = 48;
            this.toolTip.SetToolTip(this.numericUpDown_Interval, "Job Run Interval in Minutes. 1440 Min = 24 Hrs");
            this.numericUpDown_Interval.Value = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDown_Interval.ValueChanged += new System.EventHandler(this.NumericUpDown_Interval_ValueChanged);
            // 
            // button_SaveJobDetails
            // 
            this.button_SaveJobDetails.Location = new System.Drawing.Point(858, 386);
            this.button_SaveJobDetails.Name = "button_SaveJobDetails";
            this.button_SaveJobDetails.Size = new System.Drawing.Size(75, 23);
            this.button_SaveJobDetails.TabIndex = 47;
            this.button_SaveJobDetails.Text = "Save";
            this.toolTip.SetToolTip(this.button_SaveJobDetails, "Save the change");
            this.button_SaveJobDetails.UseVisualStyleBackColor = true;
            this.button_SaveJobDetails.Click += new System.EventHandler(this.Button_SaveJobDetails_Click);
            // 
            // label_RunJobInterval
            // 
            this.label_RunJobInterval.AutoSize = true;
            this.label_RunJobInterval.Location = new System.Drawing.Point(14, 328);
            this.label_RunJobInterval.Name = "label_RunJobInterval";
            this.label_RunJobInterval.Size = new System.Drawing.Size(85, 13);
            this.label_RunJobInterval.TabIndex = 46;
            this.label_RunJobInterval.Text = "Run Job Interval";
            // 
            // label_LogLevel
            // 
            this.label_LogLevel.AutoSize = true;
            this.label_LogLevel.Location = new System.Drawing.Point(14, 355);
            this.label_LogLevel.Name = "label_LogLevel";
            this.label_LogLevel.Size = new System.Drawing.Size(54, 13);
            this.label_LogLevel.TabIndex = 45;
            this.label_LogLevel.Text = "Log Level";
            // 
            // comboBox_LogLevel
            // 
            this.comboBox_LogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LogLevel.FormattingEnabled = true;
            this.comboBox_LogLevel.Items.AddRange(new object[] {
            "1,Catastrophie",
            "2,Error",
            "3,Warning",
            "4,Information",
            "5,Debug"});
            this.comboBox_LogLevel.Location = new System.Drawing.Point(148, 352);
            this.comboBox_LogLevel.Name = "comboBox_LogLevel";
            this.comboBox_LogLevel.Size = new System.Drawing.Size(238, 21);
            this.comboBox_LogLevel.TabIndex = 44;
            this.toolTip.SetToolTip(this.comboBox_LogLevel, "Log level for this job. Every job has its own logging level");
            this.comboBox_LogLevel.SelectedIndexChanged += new System.EventHandler(this.ComboBox_LogLevel_SelectedIndexChanged);
            this.comboBox_LogLevel.Click += new System.EventHandler(this.ComboBox_LogLevel_SelectedIndexChanged);
            // 
            // textBox_SubDirExcFilter
            // 
            this.textBox_SubDirExcFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "*.*",
            "*.aspx",
            "*.exe",
            "*.txt"});
            this.textBox_SubDirExcFilter.Location = new System.Drawing.Point(148, 299);
            this.textBox_SubDirExcFilter.MaxLength = 2000;
            this.textBox_SubDirExcFilter.Name = "textBox_SubDirExcFilter";
            this.textBox_SubDirExcFilter.Size = new System.Drawing.Size(857, 20);
            this.textBox_SubDirExcFilter.TabIndex = 43;
            this.toolTip.SetToolTip(this.textBox_SubDirExcFilter, "Comma separated list in the form \"\\\\Sub Dir Name\" or \"Sub Dir Name\"\r\nNo wild char" +
                    "s allowed\r\nEscape character \"\\\" like \\\\");
            this.textBox_SubDirExcFilter.TextChanged += new System.EventHandler(this.TextBox_SubDirExcFilter_TextChanged);
            // 
            // label_SubDirExcFilter
            // 
            this.label_SubDirExcFilter.AutoSize = true;
            this.label_SubDirExcFilter.Location = new System.Drawing.Point(11, 302);
            this.label_SubDirExcFilter.Name = "label_SubDirExcFilter";
            this.label_SubDirExcFilter.Size = new System.Drawing.Size(115, 13);
            this.label_SubDirExcFilter.TabIndex = 42;
            this.label_SubDirExcFilter.Text = "Sub Dir Exclusion Filter";
            // 
            // textBox_FileIncFilter
            // 
            this.textBox_FileIncFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "*.*",
            "*.aspx",
            "*.exe",
            "*.txt"});
            this.textBox_FileIncFilter.Location = new System.Drawing.Point(148, 273);
            this.textBox_FileIncFilter.MaxLength = 2000;
            this.textBox_FileIncFilter.Name = "textBox_FileIncFilter";
            this.textBox_FileIncFilter.Size = new System.Drawing.Size(857, 20);
            this.textBox_FileIncFilter.TabIndex = 41;
            this.toolTip.SetToolTip(this.textBox_FileIncFilter, resources.GetString("textBox_FileIncFilter.ToolTip"));
            this.textBox_FileIncFilter.TextChanged += new System.EventHandler(this.TextBox_FileIncFilter_TextChanged);
            // 
            // label_FileIncFilter
            // 
            this.label_FileIncFilter.AutoSize = true;
            this.label_FileIncFilter.Location = new System.Drawing.Point(11, 276);
            this.label_FileIncFilter.Name = "label_FileIncFilter";
            this.label_FileIncFilter.Size = new System.Drawing.Size(98, 13);
            this.label_FileIncFilter.TabIndex = 40;
            this.label_FileIncFilter.Text = "File Inclusion Filters";
            // 
            // textBox_FileExFilter
            // 
            this.textBox_FileExFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "*.tmp",
            "*.bak"});
            this.textBox_FileExFilter.Location = new System.Drawing.Point(148, 247);
            this.textBox_FileExFilter.MaxLength = 2000;
            this.textBox_FileExFilter.Name = "textBox_FileExFilter";
            this.textBox_FileExFilter.Size = new System.Drawing.Size(857, 20);
            this.textBox_FileExFilter.TabIndex = 39;
            this.toolTip.SetToolTip(this.textBox_FileExFilter, resources.GetString("textBox_FileExFilter.ToolTip"));
            this.textBox_FileExFilter.TextChanged += new System.EventHandler(this.TextBox_FileExFilter_TextChanged);
            // 
            // label_FileExFilter
            // 
            this.label_FileExFilter.AutoSize = true;
            this.label_FileExFilter.Location = new System.Drawing.Point(11, 250);
            this.label_FileExFilter.Name = "label_FileExFilter";
            this.label_FileExFilter.Size = new System.Drawing.Size(101, 13);
            this.label_FileExFilter.TabIndex = 38;
            this.label_FileExFilter.Text = "File Exclusion Filters";
            // 
            // button_RemoveDest
            // 
            this.button_RemoveDest.Location = new System.Drawing.Point(1022, 193);
            this.button_RemoveDest.Name = "button_RemoveDest";
            this.button_RemoveDest.Size = new System.Drawing.Size(26, 23);
            this.button_RemoveDest.TabIndex = 37;
            this.button_RemoveDest.Text = "--";
            this.button_RemoveDest.UseVisualStyleBackColor = true;
            this.button_RemoveDest.Click += new System.EventHandler(this.Button_RemoveDest_Click);
            // 
            // button_AddDest
            // 
            this.button_AddDest.Location = new System.Drawing.Point(1022, 121);
            this.button_AddDest.Name = "button_AddDest";
            this.button_AddDest.Size = new System.Drawing.Size(26, 23);
            this.button_AddDest.TabIndex = 36;
            this.button_AddDest.Text = "+";
            this.button_AddDest.UseVisualStyleBackColor = true;
            this.button_AddDest.Click += new System.EventHandler(this.Button_AddDest_Click);
            // 
            // label_SynchType
            // 
            this.label_SynchType.AutoSize = true;
            this.label_SynchType.Location = new System.Drawing.Point(14, 226);
            this.label_SynchType.Name = "label_SynchType";
            this.label_SynchType.Size = new System.Drawing.Size(64, 13);
            this.label_SynchType.TabIndex = 35;
            this.label_SynchType.Text = "Synch Type";
            // 
            // comboBox_SynchType
            // 
            this.comboBox_SynchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SynchType.FormattingEnabled = true;
            this.comboBox_SynchType.Items.AddRange(new object[] {
            "1,Source to Destination (s)",
            "2,Synch Bothways",
            "3,Destination (s) to Source"});
            this.comboBox_SynchType.Location = new System.Drawing.Point(148, 222);
            this.comboBox_SynchType.Name = "comboBox_SynchType";
            this.comboBox_SynchType.Size = new System.Drawing.Size(238, 21);
            this.comboBox_SynchType.TabIndex = 34;
            this.toolTip.SetToolTip(this.comboBox_SynchType, "Synchronization Type");
            this.comboBox_SynchType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SynchType_SelectedIndexChanged);
            this.comboBox_SynchType.Click += new System.EventHandler(this.ComboBox_SynchType_SelectedIndexChanged);
            // 
            // label_Destinations
            // 
            this.label_Destinations.AutoSize = true;
            this.label_Destinations.Location = new System.Drawing.Point(14, 121);
            this.label_Destinations.Name = "label_Destinations";
            this.label_Destinations.Size = new System.Drawing.Size(98, 13);
            this.label_Destinations.TabIndex = 33;
            this.label_Destinations.Text = "Synch Destinations";
            // 
            // listBox_Destinations
            // 
            this.listBox_Destinations.FormattingEnabled = true;
            this.listBox_Destinations.Location = new System.Drawing.Point(148, 121);
            this.listBox_Destinations.Name = "listBox_Destinations";
            this.listBox_Destinations.Size = new System.Drawing.Size(857, 95);
            this.listBox_Destinations.TabIndex = 32;
            this.toolTip.SetToolTip(this.listBox_Destinations, "Destinations. Use add button to add destinations as many as you want. Only it sho" +
                    "uld have write permission for user under which synch service is running");
            // 
            // button_Source
            // 
            this.button_Source.Location = new System.Drawing.Point(1022, 90);
            this.button_Source.Name = "button_Source";
            this.button_Source.Size = new System.Drawing.Size(75, 23);
            this.button_Source.TabIndex = 31;
            this.button_Source.Text = "Browse";
            this.button_Source.UseVisualStyleBackColor = true;
            this.button_Source.Click += new System.EventHandler(this.Button_Source_Click);
            // 
            // textBox_Source
            // 
            this.textBox_Source.Location = new System.Drawing.Point(148, 92);
            this.textBox_Source.MaxLength = 200;
            this.textBox_Source.Name = "textBox_Source";
            this.textBox_Source.Size = new System.Drawing.Size(857, 20);
            this.textBox_Source.TabIndex = 30;
            this.toolTip.SetToolTip(this.textBox_Source, "Source Directory");
            this.textBox_Source.TextChanged += new System.EventHandler(this.TextBox_Source_TextChanged);
            // 
            // label_Source
            // 
            this.label_Source.AutoSize = true;
            this.label_Source.Location = new System.Drawing.Point(14, 95);
            this.label_Source.Name = "label_Source";
            this.label_Source.Size = new System.Drawing.Size(74, 13);
            this.label_Source.TabIndex = 29;
            this.label_Source.Text = "Synch Source";
            // 
            // label_JobDescription
            // 
            this.label_JobDescription.AutoSize = true;
            this.label_JobDescription.Location = new System.Drawing.Point(14, 67);
            this.label_JobDescription.Name = "label_JobDescription";
            this.label_JobDescription.Size = new System.Drawing.Size(80, 13);
            this.label_JobDescription.TabIndex = 6;
            this.label_JobDescription.Text = "Job Description";
            // 
            // textBox_JobID
            // 
            this.textBox_JobID.Location = new System.Drawing.Point(148, 38);
            this.textBox_JobID.MaxLength = 20;
            this.textBox_JobID.Name = "textBox_JobID";
            this.textBox_JobID.Size = new System.Drawing.Size(145, 20);
            this.textBox_JobID.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBox_JobID, "Unique job identity");
            this.textBox_JobID.TextChanged += new System.EventHandler(this.TextBox_JobID_TextChanged);
            // 
            // textBox_JobDescription
            // 
            this.textBox_JobDescription.Location = new System.Drawing.Point(148, 64);
            this.textBox_JobDescription.MaxLength = 100;
            this.textBox_JobDescription.Name = "textBox_JobDescription";
            this.textBox_JobDescription.Size = new System.Drawing.Size(857, 20);
            this.textBox_JobDescription.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBox_JobDescription, "Job Description");
            this.textBox_JobDescription.TextChanged += new System.EventHandler(this.TextBox_JobDescription_TextChanged);
            // 
            // label_JobID
            // 
            this.label_JobID.AutoSize = true;
            this.label_JobID.Location = new System.Drawing.Point(14, 41);
            this.label_JobID.Name = "label_JobID";
            this.label_JobID.Size = new System.Drawing.Size(38, 13);
            this.label_JobID.TabIndex = 0;
            this.label_JobID.Text = "Job ID";
            // 
            // FormJobDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 445);
            this.Controls.Add(this.groupBox_JobDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormJobDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Job Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormJobDetails_FormClosing);
            this.Load += new System.EventHandler(this.FormJobDetails_Load);
            this.groupBox_JobDetails.ResumeLayout(false);
            this.groupBox_JobDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_JobDetails;
        private System.Windows.Forms.Label label_JobDescription;
        private System.Windows.Forms.TextBox textBox_JobID;
        private System.Windows.Forms.TextBox textBox_JobDescription;
        private System.Windows.Forms.Label label_JobID;
        private System.Windows.Forms.Label label_Minutes;
        private System.Windows.Forms.NumericUpDown numericUpDown_Interval;
        private System.Windows.Forms.Button button_SaveJobDetails;
        private System.Windows.Forms.Label label_RunJobInterval;
        private System.Windows.Forms.Label label_LogLevel;
        private System.Windows.Forms.ComboBox comboBox_LogLevel;
        private System.Windows.Forms.TextBox textBox_SubDirExcFilter;
        private System.Windows.Forms.Label label_SubDirExcFilter;
        private System.Windows.Forms.TextBox textBox_FileIncFilter;
        private System.Windows.Forms.Label label_FileIncFilter;
        private System.Windows.Forms.TextBox textBox_FileExFilter;
        private System.Windows.Forms.Label label_FileExFilter;
        private System.Windows.Forms.Button button_RemoveDest;
        private System.Windows.Forms.Button button_AddDest;
        private System.Windows.Forms.Label label_SynchType;
        private System.Windows.Forms.ComboBox comboBox_SynchType;
        private System.Windows.Forms.Label label_Destinations;
        private System.Windows.Forms.ListBox listBox_Destinations;
        private System.Windows.Forms.Button button_Source;
        private System.Windows.Forms.TextBox textBox_Source;
        private System.Windows.Forms.Label label_Source;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBox_UseSynshFramework;
        private System.Windows.Forms.DateTimePicker dateTimePicker_RunAt;
        private System.Windows.Forms.CheckBox checkBox_RunAt;

    }
}