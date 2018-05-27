using GenerationClass.Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GenerationClass
{
    public partial class ConnectData : Form
    {
        public static string ConnectionString = "";
        public static string DataBase = "";
        public static bool checkBoxMainAuthentication = false;
        public static int providerServer = 0;

        public ConnectData()
        {
            InitializeComponent();
            Code.Generation.FillTypesDict();
        }

        private void cboAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 1)
            {
                txtPWD.Enabled = false;
                txtUID.Enabled = false;
            }
            else
            {
                txtPWD.Enabled = true;
                txtUID.Enabled = true;
            }
        }

        private void cboDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            butOk.Enabled = true;
        }

        //private ConnectionType GetConnectionType()
        //{
        //    return ConnectionType.MsSql;
        //}

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            //var connectionObject = GetConnectionData();
            //List<Entity> entity;

            if (cboAuthentication.SelectedIndex == 0)
            {
                cboDB.Items.Clear();
                SqlConnection conn = new SqlConnection("Server=" + txtServer.Text + ";DataBase=Master;UID=" + txtUID.Text + ";PWD=" + txtPWD.Text + ";");
                SqlCommand cmd = new SqlCommand("sp_databases", conn);
                SqlDataReader rdr;
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        cboDB.Items.Add(rdr[0].ToString());
                    }
                    MessageBox.Show("Kết nối thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                    cmd.Dispose();
                }
            }
            if (cboAuthentication.SelectedIndex == 1)
            {
                cboDB.Items.Clear();
                SqlConnection conn = new SqlConnection("Server=" + txtServer.Text + ";DataBase=Master;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand("sp_databases", conn);
                SqlDataReader rdr;
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        cboDB.Items.Add(rdr[0].ToString());
                    }
                    MessageBox.Show("Kết nối thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                    cmd.Dispose();
                }
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (cboDB.Text != "")
            {
                providerServer = 0;
                if (cboAuthentication.SelectedIndex == 0)
                {
                    DataBase = cboDB.Text;
                    ConnectionString = "Server=" + txtServer.Text + ";DataBase=" + cboDB.Text + ";UID=" + txtUID.Text + ";PWD=" + txtPWD.Text + ";";
                    //chkSerVer.Checked = false;
                    //checkBoxMainAuthentication = chkSerVer.Checked;
                    checkBoxMainAuthentication = false;
                }
                else
                {
                    DataBase = cboDB.Text;
                    ConnectionString = "Server=" + txtServer.Text + ";DataBase=" + cboDB.Text + ";Integrated Security=true;";
                    checkBoxMainAuthentication = true;
                }

                this.Hide();
                Generation formGeneration = new Generation(ConnectionString, DataBase, checkBoxMainAuthentication, providerServer);
                formGeneration.connectionData = GetConnectionData();
                formGeneration.Show();
            }
        }

        private ConnectionData GetConnectionData()
        {
            var connectionData = new ConnectionData
            {
                Server = txtServer.Text,
                DataBaseName = cboDB.Text,
                DataBasePort = "",
                UserName = txtUID.Text,
                UserPassword = txtPWD.Text
            };

            return connectionData;
        }
    }
}