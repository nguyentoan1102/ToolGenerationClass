using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GenerationClass.Code
{
    public static class DataRetrieve
    {
        #region Get tables List

        public static List<string> GetMsSqlTablesList(ConnectionData connectionData, bool windowsAuthentification)
        {
            var tablesList = new List<string>();

            try
            {
                SqlConnection connection;

                if (windowsAuthentification)
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";Trusted_Connection=Yes;");
                }
                else
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";User id=" + connectionData.UserName + ";Password=" + connectionData.UserPassword);
                }
                connection.Open();

                const string mySelectQuery = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                var myCommand = new SqlCommand(mySelectQuery, connection);

                SqlDataReader myReader = myCommand.ExecuteReader();
                if (myReader != null)
                {
                    while (myReader.Read())
                    {
                        tablesList.Add(myReader.GetString(0));
                    }
                    myReader.Close();
                }

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return tablesList;
        }

        public static List<string> GetMySqlTablesList(ConnectionData connectionData)
        {
            var tablesList = new List<string>();

            try
            {
                var connection = new MySqlConnection("server=" + connectionData.Server + ";database=" + connectionData.DataBaseName + ";port=" + connectionData.DataBasePort + ";uid=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                const string mySelectQuery = "show tables;";
                var myCommand = new MySqlCommand(mySelectQuery, connection);

                MySqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    tablesList.Add(myReader.GetString(0));
                }
                myReader.Close();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return tablesList;
        }

        public static List<string> GetOracleTablesList(ConnectionData connectionData)
        {
            var tablesList = new List<string>();

            try
            {
                var connection = new OracleConnection("Data Source=" + connectionData.Server + ";user id=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                const string mySelectQuery = "select table_name from user_tables";
                var myCommand = new OracleCommand(mySelectQuery, connection);

                OracleDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    tablesList.Add(myReader.GetString(0));
                }
                myReader.Close();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return tablesList;
        }

        #endregion Get tables List

        #region Get Table Info

        public static List<Entity> GetMySqlTableInfo(string tableName, ConnectionData connectionData)
        {
            var entityList = new List<Entity>();

            try
            {
                var connection = new MySqlConnection("server=" + connectionData.Server + ";database=" + connectionData.DataBaseName + ";port=" + connectionData.DataBasePort + ";uid=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                string mySelectQuery = "SHOW COLUMNS FROM " + tableName + ";";
                var myCommand = new MySqlCommand(mySelectQuery, connection);

                MySqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var entity = new Entity
                    {
                        Field = myReader.GetValue(0),
                        Type = myReader.GetValue(1),
                        Length = myReader.GetValue(2)
                    };

                    entityList.Add(entity);
                }
                myReader.Close();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return entityList;
        }

        public static List<GetType> GetMySqlTableDataTypeInfo(string tableName, ConnectionData connectionData)
        {
            var entityDataTypeList = new List<GetType>();

            try
            {
                var connection = new MySqlConnection("server=" + connectionData.Server + ";database=" + connectionData.DataBaseName + ";port=" + connectionData.DataBasePort + ";uid=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                // Get DataType from Table
                string mySelectDataType = "SELECT  col.COLUMN_NAME,col.Data_type FROM INFORMATION_SCHEMA.COLUMNS col";
                mySelectDataType += " WHERE col.TABLE_NAME = '" + tableName + "'" + " and col.COLUMN_NAME = ";
                mySelectDataType += " (select 	c.COLUMN_NAME from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ";
                mySelectDataType += "where 	pk.TABLE_NAME = '" + tableName + "'" + "and	CONSTRAINT_TYPE = 'PRIMARY KEY'" + " and	c.TABLE_NAME = pk.TABLE_NAME";
                mySelectDataType += " and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME)";
                var myCommand = new MySqlCommand(mySelectDataType, connection);

                MySqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var type = new GetType
                    {
                        NameField = myReader.GetValue(0),
                        TypeField = myReader.GetValue(1)
                    };

                    entityDataTypeList.Add(type);
                }
                myReader.Close();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                // MessageBox.Show(exception.Message);
            }

            return entityDataTypeList;
        }

        public static List<Entity> GetMsSqlTableInfo(string tableName, ConnectionData connectionData, bool windowsAuthentification)
        {
            var entityList = new List<Entity>();
            var entityDataTypeList = new List<GetType>();

            try
            {
                SqlConnection connection;

                if (windowsAuthentification)
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";Trusted_Connection=Yes;");
                }
                else
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";User id=" + connectionData.UserName + ";Password=" + connectionData.UserPassword);
                }

                connection.Open();

                string mySelectQuery = "SELECT col.COLUMN_NAME, col.Data_type, col.CHARACTER_OCTET_LENGTH,col.IS_NULLABLE";
                mySelectQuery += " FROM INFORMATION_SCHEMA.COLUMNS col";
                mySelectQuery += " WHERE col.TABLE_NAME = '" + tableName + "'";
                var myCommand = new SqlCommand(mySelectQuery, connection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader != null)
                {
                    while (myReader.Read())
                    {
                        var entity = new Entity
                        {
                            Field = myReader.GetValue(0),
                            Type = myReader.GetValue(1),
                            Length = myReader.GetValue(2),
                            IsNullAble = myReader.GetValue(3)
                        };

                        entityList.Add(entity);
                    }
                    myReader.Close();
                }

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return entityList;
        }

        public static List<GetType> GetMsSqlTableDataTypeInfo(string tableName, ConnectionData connectionData, bool windowsAuthentification)
        {
            var entityDataTypeList = new List<GetType>();

            try
            {
                SqlConnection connection;

                if (windowsAuthentification)
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";Trusted_Connection=Yes;");
                }
                else
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";User id=" + connectionData.UserName + ";Password=" + connectionData.UserPassword);
                }

                connection.Open();

                // Get DataType from Table
                String mySelectDataType = "if((select 	Count(c.COLUMN_NAME) as 'len'" + Environment.NewLine;
                mySelectDataType += " from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,INFORMATION_SCHEMA.KEY_COLUMN_USAGE c where 	pk.TABLE_NAME =  '" + tableName + "'" + " and" + Environment.NewLine;
                mySelectDataType += "CONSTRAINT_TYPE = 'PRIMARY KEY' and	c.TABLE_NAME = pk.TABLE_NAME and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME) > 1)" + Environment.NewLine;

                mySelectDataType += "begin" + Environment.NewLine;
                mySelectDataType += "SELECT  col.COLUMN_NAME,col.Data_type,col.CHARACTER_OCTET_LENGTH FROM INFORMATION_SCHEMA.COLUMNS col" + Environment.NewLine;
                mySelectDataType += "    WHERE col.TABLE_NAME = '" + tableName + "'" + " and col.COLUMN_NAME in " + Environment.NewLine;
                mySelectDataType += "    (select 	c.COLUMN_NAME from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,INFORMATION_SCHEMA.KEY_COLUMN_USAGE c " + Environment.NewLine;
                mySelectDataType += "    where 	pk.TABLE_NAME = '" + tableName + "'" + "and	CONSTRAINT_TYPE = 'PRIMARY KEY'" + " and	c.TABLE_NAME = pk.TABLE_NAME" + Environment.NewLine;
                mySelectDataType += "    and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME)" + Environment.NewLine;
                mySelectDataType += "    end" + Environment.NewLine;
                mySelectDataType += "else" + Environment.NewLine;
                mySelectDataType += "  begin" + Environment.NewLine;
                mySelectDataType += "    SELECT  col.COLUMN_NAME,col.Data_type,col.CHARACTER_OCTET_LENGTH FROM INFORMATION_SCHEMA.COLUMNS col" + Environment.NewLine;
                mySelectDataType += "    WHERE col.TABLE_NAME = '" + tableName + "'" + " and col.COLUMN_NAME = " + Environment.NewLine;
                mySelectDataType += "    (  select 	c.COLUMN_NAME from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,INFORMATION_SCHEMA.KEY_COLUMN_USAGE c " + Environment.NewLine;
                mySelectDataType += "where 	pk.TABLE_NAME = '" + tableName + "'" + "and	CONSTRAINT_TYPE = 'PRIMARY KEY'" + " and	c.TABLE_NAME = pk.TABLE_NAME" + Environment.NewLine;
                mySelectDataType += "    and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME)";
                mySelectDataType += "end" + Environment.NewLine;
                var myCommandDataType = new SqlCommand(mySelectDataType, connection);

                SqlDataReader myReaderDataType = myCommandDataType.ExecuteReader();

                if (myReaderDataType != null)
                {
                    while (myReaderDataType.Read())
                    {
                        var type = new GetType
                        {
                            NameField = myReaderDataType.GetValue(0),
                            TypeField = myReaderDataType.GetValue(1),
                            Length = myReaderDataType.GetValue(2)
                        };

                        entityDataTypeList.Add(type);
                    }
                    myReaderDataType.Close();
                }

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                // MessageBox.Show(exception.Message);
            }

            return entityDataTypeList;
        }

        public static List<Entity> GetOracleTableInfo(string tableName, ConnectionData connectionData)
        {
            var entityList = new List<Entity>();

            try
            {
                var connection = new OracleConnection("Data Source=" + connectionData.Server + ";user id=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                string mySelectQuery = "select COLUMN_NAME, DATA_TYPE,CHARACTER_OCTET_LENGTH from USER_TAB_COLUMNS";
                mySelectQuery += " where table_name = '" + tableName + "'";
                var myCommand = new OracleCommand(mySelectQuery, connection);

                OracleDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var entity = new Entity
                    {
                        Field = myReader.GetValue(0),
                        Type = myReader.GetValue(1),
                        Length = myReader.GetValue(2)
                    };

                    entityList.Add(entity);
                }
                myReader.Close();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return entityList;
        }

        public static List<GetType> GetOralcalTableDataTypeInfo(string tableName, ConnectionData connectionData)
        {
            var entityDataTypeList = new List<GetType>();

            try
            {
                var connection = new OracleConnection("Data Source=" + connectionData.Server + ";user id=" + connectionData.UserName + ";password=" + connectionData.UserPassword);
                connection.Open();

                // Get DataType from Table
                string mySelectDataType = "SELECT  col.COLUMN_NAME,col.Data_type FROM INFORMATION_SCHEMA.COLUMNS col";
                mySelectDataType += " WHERE col.TABLE_NAME = '" + tableName + "'" + " and col.COLUMN_NAME = ";
                mySelectDataType += " (select 	c.COLUMN_NAME from 	INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,INFORMATION_SCHEMA.KEY_COLUMN_USAGE c ";
                mySelectDataType += "where 	pk.TABLE_NAME = '" + tableName + "'" + "and	CONSTRAINT_TYPE = 'PRIMARY KEY'" + " and	c.TABLE_NAME = pk.TABLE_NAME";
                mySelectDataType += " and	c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME)";
                string mySelectQuery = "select COLUMN_NAME, DATA_TYPE,CHARACTER_OCTET_LENGTH from USER_TAB_COLUMNS";
                mySelectQuery += " where table_name = '" + tableName + "'";

                var myCommand = new OracleCommand(mySelectQuery, connection);

                OracleDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var type = new GetType
                    {
                        NameField = myReader.GetValue(0),
                        TypeField = myReader.GetValue(1)
                    };

                    entityDataTypeList.Add(type);
                }
                myReader.Close();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                // MessageBox.Show(exception.Message);
            }

            return entityDataTypeList;
        }

        public static List<EntityTable> GetMsSqlTableOneInfo(string tableName, ConnectionData connectionData, bool windowsAuthentification)
        {
            var entityList = new List<EntityTable>();
            var entityDataTypeList = new List<GetType>();

            try
            {
                SqlConnection connection;

                if (windowsAuthentification)
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";Trusted_Connection=Yes;");
                }
                else
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";User id=" + connectionData.UserName + ";Password=" + connectionData.UserPassword);
                }

                connection.Open();

                string mySelectQuery = "SELECT col.COLUMN_NAME, col.Data_type, col.CHARACTER_OCTET_LENGTH,col.IS_NULLABLE";
                mySelectQuery += " FROM INFORMATION_SCHEMA.COLUMNS col";
                mySelectQuery += " WHERE col.TABLE_NAME = '" + tableName + "'";
                var myCommand = new SqlCommand(mySelectQuery, connection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader != null)
                {
                    while (myReader.Read())
                    {
                        var entity = new EntityTable
                        {
                            Field = myReader.GetValue(0),
                            Type = myReader.GetValue(1),
                            Length = myReader.GetValue(2),
                            AllowNull = myReader.GetValue(3)
                        };

                        entityList.Add(entity);
                    }
                    myReader.Close();
                }

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return entityList;
        }

        public static List<ListDataType> GetDataTypeMsSqlServer(ConnectionData connectionData, bool windowsAuthentification)
        {
            var entityDataTypeList = new List<ListDataType>();

            try
            {
                SqlConnection connection;

                if (windowsAuthentification)
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";Trusted_Connection=Yes;");
                }
                else
                {
                    connection = new SqlConnection("Data Source=" + connectionData.Server + ";Initial Catalog=" + connectionData.DataBaseName + ";User id=" + connectionData.UserName + ";Password=" + connectionData.UserPassword);
                }

                connection.Open();

                string mySelectQuery = "select distinct  data_type  from information_schema.columns";

                var myCommand = new SqlCommand(mySelectQuery, connection);

                SqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader != null)
                {
                    while (myReader.Read())
                    {
                        var entity = new ListDataType
                        {
                            DataTypeName = myReader.GetValue(0),
                        };

                        entityDataTypeList.Add(entity);
                    }
                    myReader.Close();
                }

                connection.Close();
                connection.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            return entityDataTypeList;
        }

        #endregion Get Table Info
    }
}