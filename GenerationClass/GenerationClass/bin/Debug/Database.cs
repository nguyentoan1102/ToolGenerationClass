﻿namespace DAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.OleDb;
    using System.Runtime.InteropServices;
    using System.Configuration;

    public class Database : IDisposable
    {
       
         string _connectionString = ConfigurationManager.ConnectionStrings["project"].ConnectionString;        
        private SqlConnection con = new SqlConnection();
        public Database()
        {
            con.ConnectionString = _connectionString;
            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            finally {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
           
            
        }
        
        public void Close()
        {
            if (this.con != null)
            {
                this.con.Close();
            }
        }
      
        private SqlCommand CreateCommand(string procName)
        {
            this.Open();
            return new SqlCommand(procName, this.con) { CommandType = CommandType.StoredProcedure };
        }

        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            this.Open();
            SqlCommand command = new SqlCommand(procName, this.con) {
                CommandType = CommandType.StoredProcedure
            };
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        private SqlCommand CreateCommandCMD(string procName, SqlParameter[] prams)
        {
            this.Open();
            SqlCommand command = new SqlCommand(procName, this.con);
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        public void RunCodeSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, this.con);
            cmd.ExecuteNonQuery();
        }
        private SqlCommand CreateCommand_WithConnTimeout(string procName)
        {
            this.Open();
            return new SqlCommand(procName, this.con) { CommandTimeout = 0xe10, CommandType = CommandType.StoredProcedure };
        }

        private SqlCommand CreateCommand_WithConnTimeout(string procName, SqlParameter[] prams)
        {
            this.Open();
            SqlCommand command = new SqlCommand(procName, this.con) {
                CommandTimeout = 0xe10,
                CommandType = CommandType.StoredProcedure
            };
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        private SqlCommand CreateCommandOnMaster(string procName, SqlParameter[] prams)
        {
            this.OpenOnMaster();
            SqlCommand command = new SqlCommand(procName, this.con) {
                CommandType = CommandType.StoredProcedure
            };
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        private SqlCommand CreateCommandOnServer(string procName, SqlParameter[] prams)
        {
          //  this.OpenOnServer();
            SqlCommand command = new SqlCommand(procName, this.con) {
                CommandType = CommandType.StoredProcedure
            };
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    command.Parameters.Add(parameter);
                }
            }
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        public void Dispose()
        {
            if (this.con != null)
            {
                this.con.Dispose();
                this.con = null;
            }
        }
        public string GetNameFromID(string proName, string rowid, string parametet, SqlDbType sqldatatype, int scorenumber)
        {
            Database database = new Database();
            SqlParameter[] prams = new SqlParameter[] { database.MakeInParam(parametet, sqldatatype, scorenumber, rowid) };
            database.RunProc(proName, prams);
            database.Dispose();
            return prams[1].Value.ToString();
        }

        public static float GetDecimal(string procName, string ID)
        {
            SqlDataReader reader;
            Database database = new Database();
            SqlParameter[] prams = new SqlParameter[] { database.MakeInParam("@ID", SqlDbType.VarChar, 100, ID) };
            database.RunProc(procName, prams, out reader);
            ArrayList list = new ArrayList();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    return reader.GetFloat(0);
                }
            }
            return 0;
        }

        public static ArrayList GetIntANDString(string procName, int ID, string Name)
        {
            SqlDataReader reader;
            SqlParameter[] parameterArray;
            ArrayList list;
            object[] objArray;
            Database database = new Database();
            if (ID != -1)
            {
                parameterArray = new SqlParameter[] { database.MakeInParam("@ID", SqlDbType.Int, 4, ID) };
                database.RunProc(procName, parameterArray, out reader);
                list = new ArrayList();
                while (reader.Read())
                {
                    objArray = new object[2];
                    if (!reader.IsDBNull(0))
                    {
                        objArray[0] = reader.GetString(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        objArray[1] = reader.GetString(1);
                    }
                    list.Add(objArray);
                }
                return list;
            }
            if (!Name.Equals(""))
            {
                parameterArray = new SqlParameter[] { database.MakeInParam("@Name", SqlDbType.VarChar, 10, Name) };
                database.RunProc(procName, parameterArray, out reader);
                list = new ArrayList();
                while (reader.Read())
                {
                    objArray = new object[2];
                    if (!reader.IsDBNull(0))
                    {
                        objArray[0] = reader.GetString(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        objArray[1] = reader.GetString(1);
                    }
                    list.Add(objArray);
                }
                return list;
            }
            database.RunProc(procName, out reader);
            list = new ArrayList();
            while (reader.Read())
            {
                objArray = new object[2];
                if (!reader.IsDBNull(0))
                {
                    objArray[0] = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    objArray[1] = reader.GetString(1);
                }
                list.Add(objArray);
            }
            return list;
        }

        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return this.MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return this.MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        private SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            SqlParameter parameter;
            if (Size > 0)
            {
                parameter = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                parameter = new SqlParameter(ParamName, DbType);
            }
            parameter.Direction = Direction;
            if ((Direction != ParameterDirection.Output) || (Value != null))
            {
                parameter.Value = Value;
            }
            return parameter;
        }

        private void Open()
        {
            if (this.con == null|| this.con.State==ConnectionState.Closed)
            {               
                this.con.Open();
            }
        }

        private void OpenOnMaster()
        {
            if (this.con == null)
            {
                this.con = new SqlConnection(_connectionString);
                this.con.Open();
            }
        }

        private void OpenOnServer()
        {
            if (this.con == null)
            {
                this.con = new SqlConnection(_connectionString);
                this.con.Open();
            }
        }

        public DataSet RunExecProc(string procName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(this.CreateCommand(procName));
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this.Close();
            return dataSet;
        }

        public DataSet RunExecProc(string procName, params SqlParameter[] prams)
        {
            SqlCommand selectCommand = this.CreateCommand(procName, prams);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this.Close();
            return dataSet;
        }
        public DataSet RunExecProcCommand(string sql, params SqlParameter[] prams)
        {
            SqlCommand selectCommand = this.CreateCommandCMD(sql, prams);   
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this.Close();
            return dataSet;
        }
        public DataSet RunExecProc_WithConnTimeout(string procName, params SqlParameter[] prams)
        {
            SqlCommand selectCommand = this.CreateCommand_WithConnTimeout(procName, prams);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this.Close();
            return dataSet;
        }

        public int RunProc(string procName)
        {
            this.Open();
            SqlCommand command = this.CreateCommand(procName, null);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public void RunProc(string procName, out SqlDataReader dataReader)
        {
            dataReader = this.CreateCommand(procName, null).ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int RunProc(string procName, SqlParameter[] prams)
        {
            this.Open();
            SqlCommand command = this.CreateCommand(procName, prams);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            dataReader = this.CreateCommand(procName, prams).ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void RunProc_WithConnTimeout(string procName)
        {
            this.CreateCommand_WithConnTimeout(procName).ExecuteNonQuery();
            this.Close();
        }

        public int RunProc_WithConnTimeout(string procName, SqlParameter[] prams)
        {
            SqlCommand command = this.CreateCommand_WithConnTimeout(procName, prams);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public float RunProcMocBai_ReturnDecimal(string procName, SqlParameter[] prams)
        {
            SqlCommand command = this.CreateCommandOnServer(procName, prams);
            command.ExecuteNonQuery();
            this.Close();
            return (float) command.Parameters["@ReturnValue"].Value;
        }

        public int RunProcOnMaster(string procName)
        {
            SqlCommand command = this.CreateCommandOnMaster(procName, null);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public int RunProcOnMaster(string procName, SqlParameter[] prams)
        {
            SqlCommand command = this.CreateCommandOnMaster(procName, prams);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public int RunProcOnServer(string procName)
        {
            SqlCommand command = this.CreateCommandOnServer(procName, null);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public DataSet RunProcOnServer(string procName, double temp)
        {
            DataSet dataSet = new DataSet();
            new SqlDataAdapter(this.CreateCommandOnServer(procName, null)).Fill(dataSet);
            return dataSet;
        }

        public int RunProcOnServer(string procName, SqlParameter[] prams)
        {
            SqlCommand command = this.CreateCommandOnServer(procName, prams);
            command.ExecuteNonQuery();
            this.Close();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        public SqlDataReader RunProcOnServer(string procName, int empty)
        {
            return this.CreateCommandOnServer(procName, null).ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void RunProcOnServer(string procName, out SqlDataReader dataReader)
        {
            dataReader = this.CreateCommandOnServer(procName, null).ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void RunProcOnServer(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            dataReader = this.CreateCommandOnServer(procName, prams).ExecuteReader(CommandBehavior.CloseConnection);
        }
        public string ChangeStringToUpper(string strInput)
        {
            //string sResult = "";
            string sResult = "";
            int i = 0;
            sResult = strInput.ToLower();
            while (i != sResult.Length)
            {
                if (sResult[i] == ' ' && sResult[i + 1] == ' ')
                {
                    sResult = sResult.Remove(i + 1, 1);
                    i = 0;
                }
                i++;
            }
            string p = sResult[0].ToString();
            p = p.ToUpper();
            sResult = sResult.Remove(0, 1);
            sResult = sResult.Insert(0, p);
            i = 0;
            while (i < sResult.Length)
            {
                if (sResult[i] == ' ')
                {
                    p = sResult[i + 1].ToString();
                    p = p.ToUpper();
                    sResult = sResult.Remove(i + 1, 1);
                    sResult = sResult.Insert(i + 1, p);
                }
                i++;
            }

            return sResult;
        }
    }
}

