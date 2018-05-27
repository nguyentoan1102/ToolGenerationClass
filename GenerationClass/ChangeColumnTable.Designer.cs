namespace GenerationClass
{
    partial class ChangeColumnTable
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveScript = new System.Windows.Forms.Button();
            this.fbdProcess = new System.Windows.Forms.FolderBrowserDialog();
            this.radEditColum = new System.Windows.Forms.RadioButton();
            this.grvAddNewColumn = new System.Windows.Forms.DataGridView();
            this.NewColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAddNewColum = new System.Windows.Forms.Panel();
            this.pnlEditColum = new System.Windows.Forms.Panel();
            this.grvEditColums = new System.Windows.Forms.DataGridView();
            this.EditColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.EditLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EditDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radAddNewColum = new System.Windows.Forms.RadioButton();
            this.btnSelectFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvAddNewColumn)).BeginInit();
            this.pnlAddNewColum.SuspendLayout();
            this.pnlEditColum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvEditColums)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(382, 250);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 39;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPath
            // 
            this.lblPath.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPath.Location = new System.Drawing.Point(20, 224);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(324, 23);
            this.lblPath.TabIndex = 40;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(203, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveScript
            // 
            this.btnSaveScript.Location = new System.Drawing.Point(383, 222);
            this.btnSaveScript.Name = "btnSaveScript";
            this.btnSaveScript.Size = new System.Drawing.Size(80, 23);
            this.btnSaveScript.TabIndex = 35;
            this.btnSaveScript.Text = "Save Script";
            this.btnSaveScript.UseVisualStyleBackColor = true;
            // 
            // fbdProcess
            // 
            this.fbdProcess.Description = "Select a folder to save the generated classes";
            // 
            // radEditColum
            // 
            this.radEditColum.AutoSize = true;
            this.radEditColum.Location = new System.Drawing.Point(17, 16);
            this.radEditColum.Name = "radEditColum";
            this.radEditColum.Size = new System.Drawing.Size(86, 17);
            this.radEditColum.TabIndex = 32;
            this.radEditColum.TabStop = true;
            this.radEditColum.Text = "Edit Columns";
            this.radEditColum.UseVisualStyleBackColor = true;
            this.radEditColum.CheckedChanged += new System.EventHandler(this.radEditColum_CheckedChanged);
            // 
            // grvAddNewColumn
            // 
            this.grvAddNewColumn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvAddNewColumn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NewColumnName,
            this.NewDataType,
            this.Length,
            this.NewAllowNull,
            this.NewDefaultValue});
            this.grvAddNewColumn.Location = new System.Drawing.Point(0, 3);
            this.grvAddNewColumn.Name = "grvAddNewColumn";
            this.grvAddNewColumn.Size = new System.Drawing.Size(446, 174);
            this.grvAddNewColumn.TabIndex = 0;
            // 
            // NewColumnName
            // 
            this.NewColumnName.HeaderText = "Column Name";
            this.NewColumnName.Name = "NewColumnName";
            // 
            // NewDataType
            // 
            this.NewDataType.HeaderText = "Data Type";
            this.NewDataType.Name = "NewDataType";
            this.NewDataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NewDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Length
            // 
            this.Length.HeaderText = "Length";
            this.Length.Name = "Length";
            this.Length.Width = 50;
            // 
            // NewAllowNull
            // 
            this.NewAllowNull.HeaderText = "Allow Null";
            this.NewAllowNull.Name = "NewAllowNull";
            this.NewAllowNull.Width = 60;
            // 
            // NewDefaultValue
            // 
            this.NewDefaultValue.HeaderText = "Default Value";
            this.NewDefaultValue.Name = "NewDefaultValue";
            this.NewDefaultValue.Width = 90;
            // 
            // pnlAddNewColum
            // 
            this.pnlAddNewColum.Controls.Add(this.grvAddNewColumn);
            this.pnlAddNewColum.Location = new System.Drawing.Point(17, 43);
            this.pnlAddNewColum.Name = "pnlAddNewColum";
            this.pnlAddNewColum.Size = new System.Drawing.Size(446, 174);
            this.pnlAddNewColum.TabIndex = 36;
            this.pnlAddNewColum.Visible = false;
            // 
            // pnlEditColum
            // 
            this.pnlEditColum.Controls.Add(this.grvEditColums);
            this.pnlEditColum.Location = new System.Drawing.Point(17, 43);
            this.pnlEditColum.Name = "pnlEditColum";
            this.pnlEditColum.Size = new System.Drawing.Size(446, 174);
            this.pnlEditColum.TabIndex = 34;
            // 
            // grvEditColums
            // 
            this.grvEditColums.AllowUserToAddRows = false;
            this.grvEditColums.AllowUserToDeleteRows = false;
            this.grvEditColums.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvEditColums.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditColumnName,
            this.EditDataType,
            this.EditLength,
            this.AllowNull,
            this.EditDefaultValue});
            this.grvEditColums.Location = new System.Drawing.Point(0, 3);
            this.grvEditColums.Name = "grvEditColums";
            this.grvEditColums.Size = new System.Drawing.Size(446, 174);
            this.grvEditColums.TabIndex = 0;
            // 
            // EditColumnName
            // 
            this.EditColumnName.HeaderText = "Column Name";
            this.EditColumnName.Name = "EditColumnName";
            // 
            // EditDataType
            // 
            this.EditDataType.HeaderText = "Data Type";
            this.EditDataType.Name = "EditDataType";
            this.EditDataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EditDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EditLength
            // 
            this.EditLength.HeaderText = "Length";
            this.EditLength.Name = "EditLength";
            this.EditLength.Width = 50;
            // 
            // AllowNull
            // 
            this.AllowNull.HeaderText = "Allow Null";
            this.AllowNull.Name = "AllowNull";
            this.AllowNull.Width = 60;
            // 
            // EditDefaultValue
            // 
            this.EditDefaultValue.HeaderText = "Default Value";
            this.EditDefaultValue.Name = "EditDefaultValue";
            this.EditDefaultValue.Width = 90;
            // 
            // radAddNewColum
            // 
            this.radAddNewColum.AutoSize = true;
            this.radAddNewColum.Location = new System.Drawing.Point(171, 16);
            this.radAddNewColum.Name = "radAddNewColum";
            this.radAddNewColum.Size = new System.Drawing.Size(112, 17);
            this.radAddNewColum.TabIndex = 33;
            this.radAddNewColum.TabStop = true;
            this.radAddNewColum.Text = "Add New Columns";
            this.radAddNewColum.UseVisualStyleBackColor = true;
            this.radAddNewColum.CheckedChanged += new System.EventHandler(this.radAddNewColum_CheckedChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFile.Location = new System.Drawing.Point(350, 222);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(27, 23);
            this.btnSelectFile.TabIndex = 41;
            this.btnSelectFile.TabStop = false;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // ChangeColumnTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 293);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSaveScript);
            this.Controls.Add(this.radEditColum);
            this.Controls.Add(this.pnlAddNewColum);
            this.Controls.Add(this.pnlEditColum);
            this.Controls.Add(this.radAddNewColum);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "ChangeColumnTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangeColumnTable";
            ((System.ComponentModel.ISupportInitialize)(this.grvAddNewColumn)).EndInit();
            this.pnlAddNewColum.ResumeLayout(false);
            this.pnlEditColum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvEditColums)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveScript;
        private System.Windows.Forms.FolderBrowserDialog fbdProcess;
        private System.Windows.Forms.RadioButton radEditColum;
        private System.Windows.Forms.DataGridView grvAddNewColumn;
        private System.Windows.Forms.Panel pnlAddNewColum;
        private System.Windows.Forms.Panel pnlEditColum;
        private System.Windows.Forms.DataGridView grvEditColums;
        private System.Windows.Forms.RadioButton radAddNewColum;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn NewDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NewAllowNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewDefaultValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn EditColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn EditDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EditLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AllowNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn EditDefaultValue;
    }
}