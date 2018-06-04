using GenerationClass.Code;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GenerationClass
{
    //Set Provider
    public enum ConnectionType
    {
        MySql,
        MsSql,
        Oracle
    }

    public partial class Generation : Form
    {
        public ConnectionData connectionData = null;
        public static string lConnectionString = "";
        public static string lDataBase = "";
        public static string ConnectProvider = "";
        private string sPath;
        public static bool checkBoxWindowsAuthentication;
        public static int providerServer = 0;

        public Generation()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
        }

        public Generation(string ConnStr, string DB, bool checkBoxMainAuthentication, int providerMain)
        {
            InitializeComponent();

            lConnectionString = ConnStr;
            lDataBase = DB;
            checkBoxWindowsAuthentication = checkBoxMainAuthentication;
            providerServer = providerMain;
            // Instantiate the objects collection.
            this.objects = new Collection<string>();
            // Display the connection dialog.
            //ReConnect();

            // If the connection could be made...
            //if (Globals.CanConnect)
            //{
            // Refresh the list of database objects.
            this.RefreshLists();
            sPath = @"E:\oop";
            lblPath.Text = sPath;
            //}
        }

        #region Private Members

        /// <summary>
        /// Storage for Tables and Views.
        /// </summary>
        private Collection<string> objects;

        #endregion Private Members

        #region Local Methods

        /// <summary>
        /// Populate a collection with a list of all of the tables and views
        /// used in the database defined by the current connection string.
        /// </summary>
        private void QueryDBSchema()
        {
            // Schema information for the current database connection.
            DataTable schema;
            //DataSet schema;

            // Loop counter.
            int loop = 0;

            // Clean up the menu so the menu item does not hang while this function executes.
            this.Refresh();

            // Make sure we have at least an Initial Catalog...
            if (!string.IsNullOrEmpty(lDataBase))
            {
                // Instantiate an OleDbConnection object.
                using (SqlConnection sqlConnection = new SqlConnection(lConnectionString))
                {
                    try
                    {
                        // Open the connection.
                        sqlConnection.Open();
                        // Retrieve the Table objects.
                        //schema = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "table" });
                        schema = sqlConnection.GetSchema("Tables");
                        //schema = sqlConnection.GetSchema(
                        // Store the table names in the object collection.
                        for (loop = 0; loop < schema.Rows.Count; loop++)
                        {
                            objects.Add(schema.Rows[loop].ItemArray[2].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);//Messages.BadConnection(e);
                    }
                }
            }
        }

        /// <summary>
        /// Re-populate the list view.
        /// </summary>
        private void RefreshLists()
        {
            this.QueryDBSchema();

            if (this.objects.Count != 0)
            {
                this.lstObjects.DataSource = this.objects;
                this.lstObjects.SelectedIndex = -1;
                this.btnProcess.Enabled = true;
                this.chkSelectAll.Enabled = true;
            }
            else
            {
                this.btnProcess.Enabled = false;
                this.chkSelectAll.Enabled = false;
                //this.lblInfo.Text = null;
            }
        }

        #endregion Local Methods

        private void btnProcess_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        //Get NamePage
        private string GetNameSpace()
        {
            if (!string.IsNullOrEmpty(txtNamespace.Text))
            {
                return txtNamespace.Text;
            }
            return "namespace Generated";
        }

        #region Helper Methods

        private string GetClassModifiers()
        {
            string result = string.Empty;

            result += "public";

            return result;
        }

        #endregion Helper Methods

        private void AddToResultTextBox(string result)
        {
            //textBoxResult.Clear();
            //textBoxResult.AppendText(result);
        }

        private void AddToTablesList(List<string> tables)
        {
            lstObjects.Items.Clear();

            foreach (var table in tables)
            {
                lstObjects.Items.Add(table);
            }
            if (lstObjects.Items.Count > 0)
            {
                lstObjects.SetSelected(0, true);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int loop = 0; loop < this.lstObjects.Items.Count; loop++)
            {
                this.lstObjects.SetItemChecked(loop, this.chkSelectAll.Checked);
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            // Display the folder browser dialog.
            this.fbdProcess.ShowDialog();

            if (string.IsNullOrEmpty(this.fbdProcess.SelectedPath))
            {
                this.lblPath.Text = sPath;
            }
            else
            {
                this.lblPath.Text = this.fbdProcess.SelectedPath;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.progressBar1.Value < 50)
            {
                this.progressBar1.Value++;
                if (this.progressBar1.Value == 50)
                {
                    MessageBox.Show("ffgfgf");
                }
            }
            else
            {
                this.timer1.Enabled = false;
            }
        }

        private void Generation_Load(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = lstObjects.Items.Count;
            //this.timer1.Interval = 100;
            //this.timer1.Enabled = true;

            List<Entity> entity;
            List<GetType> entityType;
            string apiCode = "";
            string bllCode = "";
            string getDataRow = "";
            string contractCode = "";
            string dataAceeCode = "";
            string serviceCode = "";
            string procedueCode = "";
            string viewModelCode = "";
            string validationCode = "";
            sPath = lblPath.Text;
            DirectoryInfo info = new DirectoryInfo(sPath);
            DirectoryInfo[] arr = info.GetDirectories();
            for (int i = 0; i < arr.Length; i++)
            {
                Directory.Delete(arr[i].FullName);
            }

            if (this.lstObjects.CheckedItems.Count == 0)
            {
                MessageBox.Show("Không có table để tạo", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (int item in this.lstObjects.CheckedIndices)
                {
                    this.progressBar1.Value += 1;
                    entity = DataRetrieve.GetMsSqlTableInfo(lstObjects.Items[item].ToString(), connectionData, false);
                    entityType = DataRetrieve.GetMsSqlTableDataTypeInfo(lstObjects.Items[item].ToString(), connectionData, false);
                    Code.File f = new GenerationClass.Code.File();
                    apiCode = Code.Generation.GenerateCode(entity, entityType, GetNameSpace(), GetClassModifiers(), lstObjects.Items[item].ToString());
                    bllCode = Code.Generation.GenerateCodeBLL(entity, entityType, GetNameSpace(), GetClassModifiers(), lstObjects.Items[item].ToString());
                    dataAceeCode = Code.Generation.GenerateCodeDataAccess(entity, entityType, GetNameSpace(), GetClassModifiers(), lstObjects.Items[item].ToString());
                    procedueCode = Code.Generation.GenerateStoreProceduce(entity, entityType, GetNameSpace(), GetClassModifiers(), lstObjects.Items[item].ToString());
                    getDataRow = Code.Generation.GeneralMethodGetObjectFromDataRow(entity, entityType, GetNameSpace(), GetClassModifiers(), lstObjects.Items[item].ToString());

                    f.Save(lstObjects.Items[item] + "Public", sPath, "Public", apiCode, "cs");
                    f.Save(lstObjects.Items[item] + "BUS", sPath, "BUS", bllCode, "cs");
                    f.Save(lstObjects.Items[item] + "DAL", sPath, "DAL", dataAceeCode, "cs");
                    f.Save(lstObjects.Items[item] + "DataRow", sPath, "ROW", getDataRow, "cs");

                    f.Save(lstObjects.Items[item] + "StoreProceduce", sPath, "StoreProceduce", procedueCode, "sql");
                    // f.Save(lstObjects.Items[item].ToString() + "Validation", sPath, "Validation", validationCode, "cs");
                }
                //Copy database.cs
                FileInfo f1 = new FileInfo(Application.StartupPath + @"\Database.cs");
                f1.CopyTo(sPath + @"\DAL\Database.cs", true);
                this.Text = "Bạn đã tạo được " + lstObjects.CheckedIndices.Count + " table";
                //  MessageBox.Show("Bạn đã tạo được " + lstObjects.CheckedIndices.Count + " table", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string text = "";
            string path = lblPath.Text + @"\StoreProceduce";
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileInfo[] arrFile = info.GetFiles();
                progressBar1.Value = 0;
                progressBar1.Maximum = arrFile.Length;
                for (int i = 0; i < arrFile.Length; i++)
                {
                    this.progressBar1.Value += 1;
                    string pathFile = arrFile[i].FullName;
                    StreamReader read = new StreamReader(pathFile);
                    text += read.ReadToEnd() + Environment.NewLine;
                    text += "--------------" + Environment.NewLine;
                }
                StreamWriter write = new StreamWriter(path + @"\TotalSQL.sql");
                write.Write(text);
                write.Flush();
                this.Text = "Bạn đã tạo được " + lstObjects.CheckedIndices.Count + " Store procedure!";
                MessageBox.Show("Bạn đã tạo được " + lstObjects.CheckedIndices.Count + " table", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        }
    }
}