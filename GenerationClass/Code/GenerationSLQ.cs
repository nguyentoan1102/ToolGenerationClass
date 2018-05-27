using System.Data;
using System.Data.SqlClient;

namespace GenerationClass.Code
{
    internal class GenerationSLQ
    {
        private string mTableName = "";
        private string connStr = "";

        public GenerationSLQ(string connectionString, string TableName)
        {
            mTableName = TableName;
            connStr = connectionString;
        }

        private string CreateProperty()
        {
            string ret = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ret += "\t\tprivate string m_" + rdr[0].ToString() + ";" +
                            "\n" +
                            "\t\tpublic string " + rdr[0].ToString() + "\n" +
                            "\t\t{\n" +
                            "\t\t\tget { return m_" + rdr[0].ToString() + "; }\n" +
                            "\t\t\tset { m_" + rdr[0].ToString() + " = value; }\n" +
                            "\t\t}\n";
                }
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            return ret;
        }

        private string CreateInsert()
        {
            string ret = "";
            string tValue = "";
            string m_value = "string ";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_value += rdr[0].ToString() + " , string ";
                    tValue += "\t\t\tcmd.Parameters.AddWithValue(@" + rdr[0].ToString() + " ," + rdr[0].ToString() + " );\n";
                }
                m_value = m_value.Substring(0, m_value.Length - 10);
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            ret += "\t\tpublic void Insert(" + m_value + ")\n" +
                            "\t\t{\n" +
                            "\t\t\tSqlCommand cmd = new SqlCommand(\"Insert_" + mTableName + "\", UserAuth.dbCNN);\n" +
                            "\t\t\tcmd.CommandType = CommandType.StoredProcedure;\n" +
                            tValue +
                            "\n" +
                            "\t\t\ttry\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Open();\n" +
                            "\t\t\t\tcmd.ExecuteNonQuery();\n" +
                            "\t\t\t\tUserAuth.Log(DBAction.Insert,\"" + mTableName + "\");\n" +
                            "\t\t\t}\n" +
                            "\t\t\tcatch\n" +
                            "\t\t\t{}\n" +
                            "\t\t\tfinally\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Close();\n" +
                            "\t\t\t\tcmd.Dispose();\n" +
                            "\t\t\t}\n" +
                            "\t\t}\n";

            return ret;
        }

        private string CreateUpdate()
        {
            string ret = "";
            string tValue = "";
            string m_value = "string ";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_value += rdr[0].ToString() + " , string ";
                    tValue += "\t\t\tcmd.Parameters.AddWithValue(@" + rdr[0].ToString() + " ," + rdr[0].ToString() + " );\n";
                }
                m_value = m_value.Substring(0, m_value.Length - 10);
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            ret += "\t\tpublic void Update(" + m_value + ")\n" +
                            "\t\t{\n" +
                            "\t\t\tSqlCommand cmd = new SqlCommand(\"Update_" + mTableName + "\", UserAuth.dbCNN);\n" +
                            "\t\t\tcmd.CommandType = CommandType.StoredProcedure;\n" +
                            tValue +
                            "\n" +
                            "\t\t\ttry\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Open();\n" +
                            "\t\t\t\tcmd.ExecuteNonQuery();\n" +
                            "\t\t\t\tUserAuth.Log(DBAction.Update,\"" + mTableName + "\");\n" +
                            "\t\t\t}\n" +
                            "\t\t\tcatch\n" +
                            "\t\t\t{}\n" +
                            "\t\t\tfinally\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Close();\n" +
                            "\t\t\t\tcmd.Dispose();\n" +
                            "\t\t\t}\n" +
                            "\t\t}\n";

            return ret;
        }

        private string CreateDelete()
        {
            string ret = "";
            ret += "\t\tpublic void Delete(string ID)\n" +
                            "\t\t{\n" +
                            "\t\t\tSqlCommand cmd = new SqlCommand(\"Delete_" + mTableName + "\", UserAuth.dbCNN);\n" +
                            "\t\t\tcmd.CommandType = CommandType.StoredProcedure;\n" +
                            "\t\t\tcmd.Parameters.AddWithValue(@ID,ID);\n" +
                            "\n" +
                            "\t\t\ttry\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Open();\n" +
                            "\t\t\t\tcmd.ExecuteNonQuery();\n" +
                            "\t\t\t\tUserAuth.Log(DBAction.Delete,\"" + mTableName + "\");\n" +
                            "\t\t\t}\n" +
                            "\t\t\tcatch\n" +
                            "\t\t\t{}\n" +
                            "\t\t\tfinally\n" +
                            "\t\t\t{\n" +
                            "\t\t\t\tUserAuth.dbCNN.Close();\n" +
                            "\t\t\t\tcmd.Dispose();\n" +
                            "\t\t\t}\n" +
                            "\t\t}\n";
            return ret;
        }

        public string ReturnSQLDelete()
        {
            string ret = "";
            ret = "CREATE PROCEDURE [delete_" + mTableName + "]\n" +
                    "\t(@ID 	[int])\n" +
                    "AS DELETE [" + mTableName + "]\n" +
                    "WHERE\n" +
                    "\t( [ID]	 = @ID)\n";
            return ret;
        }

        public string ReturnSQLSelect()
        {
            string ret = "";
            string m_value = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name,Data_Type,Character_Maximum_Length From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr[0].ToString().Length > 4)
                    {
                        if (rdr[0].ToString().Substring(rdr[0].ToString().Length - 4, 4) == "Code")
                            m_value += "\t\t(Select Title From " + rdr[0].ToString().Substring(0, rdr[0].ToString().Length - 5) + " Where Code = " + rdr[0].ToString() + ")\tAS\t'" + rdr[0].ToString() + "',\n";
                        else
                            m_value += "\t\t" + rdr[0].ToString() + "\tAS\t'" + rdr[0].ToString() + "',\n";
                    }
                    else
                        m_value += "\t\t" + rdr[0].ToString() + "\tAS\t'" + rdr[0].ToString() + "',\n";
                }
                m_value = m_value.Substring(0, m_value.Length - 2);
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            ret = "CREATE View [Select_" + mTableName + "]\n" +
                    "AS\n" +
                    "\nSelect " + m_value + "\n\n" +
                    "From\t" + mTableName + "";

            return ret;
        }

        public string ReturnSQLUpdate()
        {
            string ret = "";
            string tValue = "";
            string tValue1 = "";
            string m_value = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name,Data_Type,Character_Maximum_Length From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_value += "\t\t" + rdr[0].ToString() + "\t\t\t=\t@" + rdr[0].ToString() + ",\n";
                    if (rdr[2].ToString() == "" || rdr[1].ToString() == "ntext" || rdr[1].ToString() == "image")
                        tValue += "\t@" + rdr[0].ToString() + "\t\t\t\t\t\t[" + rdr[1].ToString() + "]" + ",\n";
                    else
                        tValue += "\t@" + rdr[0].ToString() + "\t\t\t\t\t\t[" + rdr[1].ToString() + "](" + rdr[2].ToString() + ")" + ",\n";
                    tValue1 += "\t@" + rdr[0].ToString() + ",\n";
                }
                m_value = m_value.Substring(0, m_value.Length - 2);
                tValue = tValue.Substring(0, tValue.Length - 2);
                tValue1 = tValue1.Substring(0, tValue1.Length - 2);
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            ret = "CREATE PROCEDURE [update_" + mTableName + "]\n" +
                    "(\n" + tValue + "\n)\n" +
                    "AS UPDATE " + mTableName + "\n" +
                    "SET\n" + m_value + "\n" +
                    "WHERE\n" +
                    "(\n\tID\t=\t@ID\n)";

            return ret;
        }

        public string ReturnSQLInsert()
        {
            string ret = "";

            string tValue = "";
            string tValue1 = "";
            string m_value = "";

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Column_Name,Data_Type,Character_Maximum_Length From information_Schema.columns Where Table_Name='" + mTableName + "'", conn);
            SqlDataReader rdr;
            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m_value += "\t" + rdr[0].ToString() + ",\n";
                    if (rdr[2].ToString() == "" || rdr[1].ToString() == "ntext" || rdr[1].ToString() == "image")
                        tValue += "\t@" + rdr[0].ToString() + "\t\t\t\t\t\t[" + rdr[1].ToString() + "]" + ",\n";
                    else
                        tValue += "\t@" + rdr[0].ToString() + "\t\t\t\t\t\t[" + rdr[1].ToString() + "](" + rdr[2].ToString() + ")" + ",\n";
                    tValue1 += "\t@" + rdr[0].ToString() + ",\n";
                }
                m_value = m_value.Substring(0, m_value.Length - 2);
                tValue = tValue.Substring(0, tValue.Length - 2);
                tValue1 = tValue1.Substring(0, tValue1.Length - 2);
            }
            catch
            { }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
            }

            ret = "CREATE PROCEDURE [insert_" + mTableName + "]\n" +
                    "\t(\n" + tValue + "\n\t)\n" +
                    "AS INSERT INTO " + mTableName + "\n" +
                    "\t(\n" + m_value + "\n\t)\n" +
                    "VALUES\n" +
                    "\t(\n" + tValue1 + "\n\t)\n";

            return ret;
        }

        public string CreateSelect()
        {
            string ret = "";

            ret = "\t\tpublic DataSet Select()\n" +
                   "\t\t{\n" +
                   "\t\t\tSqlDataAdapter cmd = new SqlDataAdapter(\"SELECT * FROM Select_" + mTableName + "\", UserAuth.dbCNN);\n" +
                   "\t\t\tDataSet dts = new DataSet();\n" +
                   "\t\t\ttry\n" +
                   "\t\t\t{\n" +
                   "\t\t\t\tUserAuth.dbCNN.Open();\n" +
                   "\t\t\t\tcmd.Fill(dts);\n" +
                   "\t\t\t\treturn dts;\n" +
                   "\t\t\t}\n" +
                   "\t\t\tcatch\n" +
                   "\t\t\t{ }\n" +
                   "\t\t\tfinally\n" +
                   "\t\t\t{\n" +
                   "\t\t\t\tUserAuth.dbCNN.Close();\n" +
                   "\t\t\t\tcmd.Dispose();\n" +
                   "\t\t\t}\n" +
                    "\t\t\treturn dts;\n" +
                   "\t\t}\n";

            return ret;
        }

        public string ReturnClass()
        {
            string ret = "";

            ret = "using System;\n" +
                    "using System.Collections.Generic;\n" +
                    "using System.Text;\n" +
                    "using System.Data;\n" +
                    "using System.Data.SqlClient;\n" +
                    "using System.Collections;\n" +
                    "using System.Configuration;\n" +
                    "using System.Web;\n" +
                    "using System.Web.Security;\n" +
                    "using System.Web.UI;\n" +
                    "using System.Web.UI.WebControls;\n" +
                    "using System.Web.UI.WebControls.WebParts;\n" +
                    "using System.Web.UI.HtmlControls;\n" +
                    "\n" +
                    "namespace Hoze\n" +
                    "{\n" +
                    "\tclass " + mTableName + "\n" +
                    "\t{\n" +
                    "\n" +
                    "\t\tprivate string ConnectionString;\n" +
                    "\t\tpublic " + mTableName + "(string ConnStr)\n" +
                    "\t\t{\n" +
                    "\t\t\tConnectionString = ConnStr;\n" +
                    "\t\t}\n" +
                    "\n" +
                    CreateProperty() +
                    "\n" +
                    CreateUpdate() +
                    "\n" +
                    CreateInsert() +
                    "\n" +
                    CreateDelete() +
                    "\n" +
                    CreateSelect() +
                    "\t}\n" +
                    "}\n";

            return ret;
        }
    }
}