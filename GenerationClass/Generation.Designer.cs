namespace GenerationClass
{
    partial class Generation
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
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lstObjects = new System.Windows.Forms.CheckedListBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.ttPrimary = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fbdProcess = new System.Windows.Forms.FolderBrowserDialog();
            this.gbLocation = new System.Windows.Forms.GroupBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblSelectLocation = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.gbLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Enabled = false;
            this.chkSelectAll.Location = new System.Drawing.Point(12, 284);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(82, 17);
            this.chkSelectAll.TabIndex = 6;
            this.chkSelectAll.Text = "Chọn tất cả";
            this.ttPrimary.SetToolTip(this.chkSelectAll, "Select/Deselect all listed objects");
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lstObjects
            // 
            this.lstObjects.CheckOnClick = true;
            this.lstObjects.FormattingEnabled = true;
            this.lstObjects.Location = new System.Drawing.Point(2, 12);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(436, 260);
            this.lstObjects.Sorted = true;
            this.lstObjects.TabIndex = 5;
            this.lstObjects.ThreeDCheckBoxes = true;
            // 
            // btnProcess
            // 
            this.btnProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProcess.Enabled = false;
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProcess.Location = new System.Drawing.Point(271, 399);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.TabStop = false;
            this.btnProcess.Text = "Xử lý";
            this.ttPrimary.SetToolTip(this.btnProcess, "Process selected objects");
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFile.Location = new System.Drawing.Point(301, 51);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(27, 23);
            this.btnSelectFile.TabIndex = 27;
            this.btnSelectFile.TabStop = false;
            this.btnSelectFile.Text = "...";
            this.ttPrimary.SetToolTip(this.btnSelectFile, "Process selected objects");
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(252, 283);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(186, 21);
            this.txtNamespace.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Namespace :";
            // 
            // fbdProcess
            // 
            this.fbdProcess.Description = "Select a folder to save the generated classes";
            // 
            // gbLocation
            // 
            this.gbLocation.Controls.Add(this.lblPath);
            this.gbLocation.Controls.Add(this.btnSelectFile);
            this.gbLocation.Controls.Add(this.lblSelectLocation);
            this.gbLocation.Location = new System.Drawing.Point(12, 308);
            this.gbLocation.Name = "gbLocation";
            this.gbLocation.Size = new System.Drawing.Size(426, 85);
            this.gbLocation.TabIndex = 27;
            this.gbLocation.TabStop = false;
            this.gbLocation.Text = "Đường dẫn lưu file";
            // 
            // lblPath
            // 
            this.lblPath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPath.Location = new System.Drawing.Point(6, 53);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(285, 23);
            this.lblPath.TabIndex = 28;
            // 
            // lblSelectLocation
            // 
            this.lblSelectLocation.Location = new System.Drawing.Point(6, 18);
            this.lblSelectLocation.Name = "lblSelectLocation";
            this.lblSelectLocation.Size = new System.Drawing.Size(322, 30);
            this.lblSelectLocation.TabIndex = 2;
            this.lblSelectLocation.Text = "Chọn 1 đường dẫn để lưu code C# xuất ra";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBar1.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.progressBar1.Location = new System.Drawing.Point(12, 399);
            this.progressBar1.Maximum = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(253, 23);
            this.progressBar1.TabIndex = 28;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // Generation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 430);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gbLocation);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.lstObjects);
            this.Controls.Add(this.btnProcess);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Generation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn Table";
            this.Load += new System.EventHandler(this.Generation_Load);
            this.gbLocation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.CheckedListBox lstObjects;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ToolTip ttPrimary;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog fbdProcess;
        private System.Windows.Forms.GroupBox gbLocation;
        internal System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnSelectFile;
        internal System.Windows.Forms.Label lblSelectLocation;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}