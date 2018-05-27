using GenerationClass.Code;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenerationClass
{
    public partial class ChangeColumnTable : Form
    {
        #region Properties

        private string columnName;
        private string dataType;
        private bool checkNull;
        private List<EntityTable> entityForm;

        public ConnectionData connectionData = null;
        public static string lConnectionString = "";
        public static string lDataBase = "";
        public static string ConnectProvider = "";
        private string sPath;
        public static bool checkBoxWindowsAuthentication;
        public static int providerServer = 0;
        public string tableNameDetail;

        public string ColumnName
        {
            get { return columnName; }
        }

        public string DataType
        {
            get { return dataType; }
        }

        public bool CheckNull
        {
            get { return checkNull; }
        }

        public List<EntityTable> EntityForm
        {
            get { return entityForm; }
        }

        #endregion Properties

        #region Public

        public ChangeColumnTable()
        {
            InitializeComponent();

            ShowPanel();
            sPath = "D:\\Temp";
            lblPath.Text = sPath;
        }

        public ChangeColumnTable(string ConnStr, string DB, bool checkBoxMainAuthentication, int providerMain, string tableNameMain)
        {
            InitializeComponent();

            lConnectionString = ConnStr;
            lDataBase = DB;
            checkBoxWindowsAuthentication = checkBoxMainAuthentication;
            providerServer = providerMain;
            tableNameDetail = tableNameMain;

            ShowPanel();
            sPath = "D:\\Temp";
            lblPath.Text = sPath;
        }

        #endregion Public

        #region Private

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radEditColum_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditColum.Visible = true;
            pnlAddNewColum.Visible = false;
        }

        private void radAddNewColum_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditColum.Visible = false;
            pnlAddNewColum.Visible = true;
        }

        private void ShowPanel()
        {
            if (radAddNewColum.Checked)
            {
                pnlAddNewColum.Visible = true;
                pnlEditColum.Visible = false;
            }
            else if (radEditColum.Checked)
            {
                pnlAddNewColum.Visible = false;
                pnlEditColum.Visible = true;
            }
        }

        private void ShowList()
        {
            List<string> listDataType = new List<string>();
            List<ListDataType> entityDataType;
        }

        private void GetInforTableIntoGridview()
        {
            List<EntityTable> entityTable;
            entityTable = DataRetrieve.GetMsSqlTableOneInfo(tableNameDetail, connectionData, false);
        }

        #endregion Private
    }
}