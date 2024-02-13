namespace SynchServiceNS
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl_Log = new System.Windows.Forms.TabControl();
            this.tabPage_Log = new System.Windows.Forms.TabPage();
            this.richTextBox_Status = new System.Windows.Forms.RichTextBox();
            this.label_ThreadStatus = new System.Windows.Forms.Label();
            this.buttonStopService = new System.Windows.Forms.Button();
            this.button_StartService = new System.Windows.Forms.Button();
            this.label_Refresh = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.richTextBox_Log = new System.Windows.Forms.RichTextBox();
            this.tabPage_Jobs = new System.Windows.Forms.TabPage();
            this.groupBox_SavedJobs = new System.Windows.Forms.GroupBox();
            this.button_ManualRun = new System.Windows.Forms.Button();
            this.button_Edit = new System.Windows.Forms.Button();
            this.button_AddNew = new System.Windows.Forms.Button();
            this.button_Enable = new System.Windows.Forms.Button();
            this.button_Disable = new System.Windows.Forms.Button();
            this.button_Remove = new System.Windows.Forms.Button();
            this.listView_SavedJobs = new System.Windows.Forms.ListView();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl_Log.SuspendLayout();
            this.tabPage_Log.SuspendLayout();
            this.tabPage_Jobs.SuspendLayout();
            this.groupBox_SavedJobs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_Log
            // 
            this.tabControl_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_Log.Controls.Add(this.tabPage_Log);
            this.tabControl_Log.Controls.Add(this.tabPage_Jobs);
            this.tabControl_Log.Location = new System.Drawing.Point(12, 12);
            this.tabControl_Log.Name = "tabControl_Log";
            this.tabControl_Log.SelectedIndex = 0;
            this.tabControl_Log.Size = new System.Drawing.Size(1154, 709);
            this.tabControl_Log.TabIndex = 0;
            // 
            // tabPage_Log
            // 
            this.tabPage_Log.Controls.Add(this.richTextBox_Status);
            this.tabPage_Log.Controls.Add(this.label_ThreadStatus);
            this.tabPage_Log.Controls.Add(this.buttonStopService);
            this.tabPage_Log.Controls.Add(this.button_StartService);
            this.tabPage_Log.Controls.Add(this.label_Refresh);
            this.tabPage_Log.Controls.Add(this.button_Clear);
            this.tabPage_Log.Controls.Add(this.richTextBox_Log);
            this.tabPage_Log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Log.Name = "tabPage_Log";
            this.tabPage_Log.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Log.Size = new System.Drawing.Size(1146, 683);
            this.tabPage_Log.TabIndex = 0;
            this.tabPage_Log.Text = "Log";
            this.tabPage_Log.UseVisualStyleBackColor = true;
            // 
            // richTextBox_Status
            // 
            this.richTextBox_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_Status.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox_Status.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox_Status.Location = new System.Drawing.Point(17, 19);
            this.richTextBox_Status.Name = "richTextBox_Status";
            this.richTextBox_Status.ReadOnly = true;
            this.richTextBox_Status.Size = new System.Drawing.Size(1113, 288);
            this.richTextBox_Status.TabIndex = 7;
            this.richTextBox_Status.TabStop = false;
            this.richTextBox_Status.Text = "";
            this.toolTip.SetToolTip(this.richTextBox_Status, "Log Created by Synch Service. Refreshes after every 5 seconds");
            this.richTextBox_Status.WordWrap = false;
            // 
            // label_ThreadStatus
            // 
            this.label_ThreadStatus.AutoSize = true;
            this.label_ThreadStatus.Location = new System.Drawing.Point(14, 3);
            this.label_ThreadStatus.Name = "label_ThreadStatus";
            this.label_ThreadStatus.Size = new System.Drawing.Size(122, 13);
            this.label_ThreadStatus.TabIndex = 6;
            this.label_ThreadStatus.Text = "Manual - Thread Activity";
            // 
            // buttonStopService
            // 
            this.buttonStopService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStopService.Location = new System.Drawing.Point(128, 654);
            this.buttonStopService.Name = "buttonStopService";
            this.buttonStopService.Size = new System.Drawing.Size(105, 23);
            this.buttonStopService.TabIndex = 5;
            this.buttonStopService.Text = "Stop Service";
            this.buttonStopService.UseVisualStyleBackColor = true;
            this.buttonStopService.Click += new System.EventHandler(this.ButtonStopService_Click);
            // 
            // button_StartService
            // 
            this.button_StartService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_StartService.Location = new System.Drawing.Point(17, 654);
            this.button_StartService.Name = "button_StartService";
            this.button_StartService.Size = new System.Drawing.Size(105, 23);
            this.button_StartService.TabIndex = 4;
            this.button_StartService.Text = "Start Service";
            this.button_StartService.UseVisualStyleBackColor = true;
            this.button_StartService.Click += new System.EventHandler(this.Button_StartService_Click);
            // 
            // label_Refresh
            // 
            this.label_Refresh.AutoSize = true;
            this.label_Refresh.Location = new System.Drawing.Point(14, 310);
            this.label_Refresh.Name = "label_Refresh";
            this.label_Refresh.Size = new System.Drawing.Size(166, 13);
            this.label_Refresh.TabIndex = 3;
            this.label_Refresh.Text = "Log - Refreshes Every 5 Seconds";
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.Location = new System.Drawing.Point(1065, 654);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 2;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // richTextBox_Log
            // 
            this.richTextBox_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_Log.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox_Log.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox_Log.Location = new System.Drawing.Point(17, 326);
            this.richTextBox_Log.Name = "richTextBox_Log";
            this.richTextBox_Log.ReadOnly = true;
            this.richTextBox_Log.Size = new System.Drawing.Size(1113, 322);
            this.richTextBox_Log.TabIndex = 1;
            this.richTextBox_Log.TabStop = false;
            this.richTextBox_Log.Text = "";
            this.toolTip.SetToolTip(this.richTextBox_Log, "Log Created by Synch Service. Refreshes after every 5 seconds");
            this.richTextBox_Log.WordWrap = false;
            // 
            // tabPage_Jobs
            // 
            this.tabPage_Jobs.Controls.Add(this.groupBox_SavedJobs);
            this.tabPage_Jobs.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Jobs.Name = "tabPage_Jobs";
            this.tabPage_Jobs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Jobs.Size = new System.Drawing.Size(1146, 683);
            this.tabPage_Jobs.TabIndex = 1;
            this.tabPage_Jobs.Text = "Synch Jobs";
            this.tabPage_Jobs.UseVisualStyleBackColor = true;
            // 
            // groupBox_SavedJobs
            // 
            this.groupBox_SavedJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_SavedJobs.Controls.Add(this.button_ManualRun);
            this.groupBox_SavedJobs.Controls.Add(this.button_Edit);
            this.groupBox_SavedJobs.Controls.Add(this.button_AddNew);
            this.groupBox_SavedJobs.Controls.Add(this.button_Enable);
            this.groupBox_SavedJobs.Controls.Add(this.button_Disable);
            this.groupBox_SavedJobs.Controls.Add(this.button_Remove);
            this.groupBox_SavedJobs.Controls.Add(this.listView_SavedJobs);
            this.groupBox_SavedJobs.Location = new System.Drawing.Point(15, 17);
            this.groupBox_SavedJobs.Name = "groupBox_SavedJobs";
            this.groupBox_SavedJobs.Size = new System.Drawing.Size(1112, 660);
            this.groupBox_SavedJobs.TabIndex = 7;
            this.groupBox_SavedJobs.TabStop = false;
            this.groupBox_SavedJobs.Text = "Saved Jobs";
            // 
            // button_ManualRun
            // 
            this.button_ManualRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ManualRun.Location = new System.Drawing.Point(1018, 631);
            this.button_ManualRun.Name = "button_ManualRun";
            this.button_ManualRun.Size = new System.Drawing.Size(75, 23);
            this.button_ManualRun.TabIndex = 12;
            this.button_ManualRun.Text = "Manual Run";
            this.button_ManualRun.UseVisualStyleBackColor = true;
            this.button_ManualRun.Click += new System.EventHandler(this.Button_ManualRun_Click);
            // 
            // button_Edit
            // 
            this.button_Edit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Edit.Location = new System.Drawing.Point(666, 631);
            this.button_Edit.Name = "button_Edit";
            this.button_Edit.Size = new System.Drawing.Size(75, 23);
            this.button_Edit.TabIndex = 11;
            this.button_Edit.Text = "Edit";
            this.toolTip.SetToolTip(this.button_Edit, "Edit Selected Job");
            this.button_Edit.UseVisualStyleBackColor = true;
            this.button_Edit.Click += new System.EventHandler(this.Button_Edit_Click);
            // 
            // button_AddNew
            // 
            this.button_AddNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_AddNew.Location = new System.Drawing.Point(504, 631);
            this.button_AddNew.Name = "button_AddNew";
            this.button_AddNew.Size = new System.Drawing.Size(75, 23);
            this.button_AddNew.TabIndex = 10;
            this.button_AddNew.Text = "Add";
            this.toolTip.SetToolTip(this.button_AddNew, "Add New Job");
            this.button_AddNew.UseVisualStyleBackColor = true;
            this.button_AddNew.Click += new System.EventHandler(this.Button_AddNewJob_Click);
            // 
            // button_Enable
            // 
            this.button_Enable.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Enable.Location = new System.Drawing.Point(342, 631);
            this.button_Enable.Name = "button_Enable";
            this.button_Enable.Size = new System.Drawing.Size(75, 23);
            this.button_Enable.TabIndex = 9;
            this.button_Enable.Text = "Enable";
            this.toolTip.SetToolTip(this.button_Enable, "Enable Selected Job");
            this.button_Enable.UseVisualStyleBackColor = true;
            this.button_Enable.Click += new System.EventHandler(this.Button_Enable_Click);
            // 
            // button_Disable
            // 
            this.button_Disable.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Disable.Location = new System.Drawing.Point(423, 631);
            this.button_Disable.Name = "button_Disable";
            this.button_Disable.Size = new System.Drawing.Size(75, 23);
            this.button_Disable.TabIndex = 8;
            this.button_Disable.Text = "Disable";
            this.toolTip.SetToolTip(this.button_Disable, "Disable Selected Job");
            this.button_Disable.UseVisualStyleBackColor = true;
            this.button_Disable.Click += new System.EventHandler(this.Button_Disable_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Remove.Location = new System.Drawing.Point(585, 631);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(75, 23);
            this.button_Remove.TabIndex = 7;
            this.button_Remove.Text = "Remove";
            this.toolTip.SetToolTip(this.button_Remove, "Remove Selected Job");
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // listView_SavedJobs
            // 
            this.listView_SavedJobs.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView_SavedJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_SavedJobs.FullRowSelect = true;
            this.listView_SavedJobs.GridLines = true;
            this.listView_SavedJobs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_SavedJobs.HideSelection = false;
            this.listView_SavedJobs.Location = new System.Drawing.Point(11, 19);
            this.listView_SavedJobs.MultiSelect = false;
            this.listView_SavedJobs.Name = "listView_SavedJobs";
            this.listView_SavedJobs.ShowGroups = false;
            this.listView_SavedJobs.Size = new System.Drawing.Size(1082, 592);
            this.listView_SavedJobs.TabIndex = 1;
            this.listView_SavedJobs.UseCompatibleStateImageBehavior = false;
            this.listView_SavedJobs.View = System.Windows.Forms.View.Details;
            this.listView_SavedJobs.SelectedIndexChanged += new System.EventHandler(this.ListView_SavedJobs_SelectedIndexChanged);
            this.listView_SavedJobs.DoubleClick += new System.EventHandler(this.Button_Edit_Click);
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 733);
            this.Controls.Add(this.tabControl_Log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synch Manager";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl_Log.ResumeLayout(false);
            this.tabPage_Log.ResumeLayout(false);
            this.tabPage_Log.PerformLayout();
            this.tabPage_Jobs.ResumeLayout(false);
            this.groupBox_SavedJobs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Log;
        private System.Windows.Forms.TabPage tabPage_Log;
        private System.Windows.Forms.RichTextBox richTextBox_Log;
        private System.Windows.Forms.TabPage tabPage_Jobs;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label_Refresh;
        private System.Windows.Forms.GroupBox groupBox_SavedJobs;
        private System.Windows.Forms.ListView listView_SavedJobs;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button_Enable;
        private System.Windows.Forms.Button button_Disable;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_AddNew;
        private System.Windows.Forms.Button button_Edit;
        private System.Windows.Forms.Button buttonStopService;
        private System.Windows.Forms.Button button_StartService;
        private System.Windows.Forms.Button button_ManualRun;
        private System.Windows.Forms.Label label_ThreadStatus;
        private System.Windows.Forms.RichTextBox richTextBox_Status;
    }
}

