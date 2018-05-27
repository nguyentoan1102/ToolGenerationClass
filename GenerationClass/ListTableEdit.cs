using GenerationClass.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GenerationClass
{
    public partial class ListTableEdit : Form
    {
        public ConnectionData connectionData = null;
        public static string lConnectionString = "";
        public static string lDataBase = "";
        public static string ConnectProvider = "";
        private string sPath;
        public static bool checkBoxWindowsAuthentication;
        public static int providerServer = 0;

        public static List<string> listTable;

        //public static List<string> ListTable
        //{
        //    get { return listTable; }
        //    set { listTable = value; }
        //}
        // DataTable tableName;

        public ListTableEdit()
        {
            InitializeComponent();
        }

        public ListTableEdit(string ConnStr, string DB, bool checkBoxMainAuthentication, int providerMain, List<string> listTableMain)
        {
            InitializeComponent();

            lConnectionString = ConnStr;
            lDataBase = DB;
            checkBoxWindowsAuthentication = checkBoxMainAuthentication;
            providerServer = providerMain;
            listTable = listTableMain;
            ShowGridView();
        }

        private void ShowGridView()
        {
            grvListTable.DataSource = GetTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ShowGridView();
        }

        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private static DataTable GetTable()
        {
            //
            // Here we create a DataTable with four columns.
            //
            Button btn;
            DataTable table = new DataTable("ListTable");
            table.Columns.Add("TableName", typeof(string));
            // table.Columns.Add("View", typeof(string));
            ////table.Columns.Add("View", typeof(string));
            DataRow workRow;
            for (int i = 0; i < listTable.Count; i++)
            {
                workRow = table.NewRow();
                workRow["TableName"] = listTable[i].ToString();
                //workRow["View"] = "View";
                table.Rows.Add(workRow);
            }
            return table;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvListTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string valueRow = grvListTable.Rows[e.RowIndex].Cells[1].ToString();
            ChangeColumnTable frm = new ChangeColumnTable(lConnectionString, lDataBase, checkBoxWindowsAuthentication, providerServer, valueRow);

            frm.ShowDialog();
        }
    }
}