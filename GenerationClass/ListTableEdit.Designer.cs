namespace GenerationClass
{
    partial class ListTableEdit
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
            this.grvListTable = new System.Windows.Forms.DataGridView();
            this.View = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvListTable)).BeginInit();
            this.SuspendLayout();
            // 
            // grvListTable
            // 
            this.grvListTable.AllowUserToAddRows = false;
            this.grvListTable.AllowUserToDeleteRows = false;
            this.grvListTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvListTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.View});
            this.grvListTable.Location = new System.Drawing.Point(12, 25);
            this.grvListTable.Name = "grvListTable";
            this.grvListTable.Size = new System.Drawing.Size(295, 176);
            this.grvListTable.TabIndex = 0;
            this.grvListTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvListTable_CellClick);
            // 
            // View
            // 
            this.View.HeaderText = "Edit Table";
            this.View.Name = "View";
            this.View.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.View.Text = "Edit";
            this.View.UseColumnTextForLinkValue = true;
            this.View.Width = 70;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(232, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ListTableEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 246);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grvListTable);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListTableEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListTableEdit";
            ((System.ComponentModel.ISupportInitialize)(this.grvListTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grvListTable;
        private System.Windows.Forms.DataGridViewLinkColumn View;
        private System.Windows.Forms.Button btnClose;
    }
}