using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenerationClass.Code
{
    public static class Generation
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        #region Fill types dictionary

        public static void FillTypesDict()
        {
            DataTypes.Clear();

            DataTypes.Add("PLS_INTEGER", typeof(int));
            DataTypes.Add("BINARY_INTEGER", typeof(int));
            DataTypes.Add("TINYINT", typeof(int));
            DataTypes.Add("SMALLINT", typeof(int));
            DataTypes.Add("MEDIUMINT", typeof(int));
            DataTypes.Add("BIGINT", typeof(int));
            DataTypes.Add("UNSIGNED", typeof(uint));
            DataTypes.Add("INTEGER", typeof(int));
            DataTypes.Add("INT", typeof(int));
            DataTypes.Add("NUMBER", typeof(double));
            DataTypes.Add("VARCHAR2", typeof(string));
            DataTypes.Add("VARCHAR", typeof(string));
            DataTypes.Add("LONG", typeof(string));
            DataTypes.Add("DATETIME", typeof(DateTime));
            DataTypes.Add("DATE", typeof(DateTime));
            DataTypes.Add("TIMESTAMP", typeof(DateTime));
            DataTypes.Add("TIME", typeof(DateTime));
            DataTypes.Add("YEAR", typeof(int));
            DataTypes.Add("TEXT", typeof(string));
            DataTypes.Add("CHAR", typeof(string));
            DataTypes.Add("FLOAT", typeof(float));
            DataTypes.Add("DOUBLE", typeof(double));
            DataTypes.Add("DECIMAL", typeof(double));
            DataTypes.Add("SMALLMONEY", typeof(double));
            DataTypes.Add("MONEY", typeof(double));
        }

        #endregion Fill types dictionary

        public static string GeneralMethodGetObjectFromDataRow(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
            GenrationComment comment = new GenrationComment();
            var result = string.Empty;
            var staticTxt = string.Empty;
            string table = tableName.ToLower();
            //Create  DLL
            result += comment.GetDLLAPICreate();
            result += "namespace Public" + Environment.NewLine;
            result += "{" + Environment.NewLine;

            #region Table Shema

            if (classModifiers.Length > 0)
            {
                result += "\t" + classModifiers + " class " + tableName + "public" + Environment.NewLine;
            }
            else
            {
                result += "\t" + " class " + tableName + "public" + Environment.NewLine;
            }

            result += "\t{" + Environment.NewLine;
            result += "\t\tpublic  " + tableName + " GetByDataRow" + "(DataRow row)" + Environment.NewLine;
            result += "\t\t{" + Environment.NewLine;
            result += "\t\t\t" + tableName + "  " + table + " = new " + tableName + "();" + Environment.NewLine;

            for (int i = 0; i < entities.Count; i++)
            {
                bool b = false;
                if (entities[i].Type.ToString() == "bit")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + "=" + " row[\"" + entities[i].Field + "\"]+== DBNull.Value ?false :" + "(" + "bool " + ")" + "row[\"" + entities[i].Field + "\"]" + "; " + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + "=" + "(" + "bool " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "bigint")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "long " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =" + "(" + "long " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "binary" || entities[i].Type.ToString() == "image" || entities[i].Type.ToString() == "rowversion" || entities[i].Type.ToString() == "timestamp" || entities[i].Type.ToString() == "varbinary")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Byte[] " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Byte[] " + ")" +
                         " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "date" || entities[i].Type.ToString() == "datetime" || entities[i].Type.ToString() == "smalldatetime")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? DateTime.MinValue : " + "(" + "DateTime " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "DateTime " + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "decimal" || entities[i].Type.ToString() == "money" || entities[i].Type.ToString() == "numeric" || entities[i].Type.ToString() == "smallmoney")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Decimal" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Decimal " + ")" +
                       " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "float")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "double" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "double " + ")" +
                        " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "int" || entities[i].Type.ToString() == "smallint")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "int" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "int " + ")" +
                            " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "real")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Single" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Single " + ")" +
                        " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "time")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "TimeSpan" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "TimeSpan " + ")" +
                       " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "tinyint")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Byte" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Byte " + ")" +
                       " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "uniqueidentifier")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Guid" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Guid " + ")" +
                       " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (entities[i].Type.ToString() == "xml")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? 0 : " + "(" + "Xml" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "Xml " + ")" +
                        " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "string")
                {
                    if (entities[i].AllowNull.ToString() == "YES")
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " = " + " row[\"" + entities[i].Field + "\"]" + " == DBNull.Value ? string.Empty : " + "(" + "string" + ")" + " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\t\t" + table + "." + entities[i].Field + " =(" + "string " + ")" +
                       " row[\"" + entities[i].Field + "\"]" + ";" + Environment.NewLine;
                    }
                }
                else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() != "String")
                {
                    result += "\t\t\t" + table + "." + entities[i].Field + " =(" + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ")" +
                             entities[i].Field + Environment.NewLine;
                }
                else
                {
                    result += "\t\t\t" + table + "." + entities[i].Field + " =(" + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ")" +
                            entities[i].Field + Environment.NewLine;
                }
            }

            result += "\t\t}" + Environment.NewLine;
            result += "\t}" + Environment.NewLine;
            result += "}";

            #endregion Table Shema

            return result;
        }

        public static string GenerateCode(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
            GenrationComment comment = new GenrationComment();
            var result = string.Empty;
            var staticTxt = string.Empty;

            if (DataTypes.Count > 0)
            {
                if (entities.Count > 0)
                {
                    if (classModifiers.Contains("static"))
                    {
                        staticTxt = "static ";
                    }

                    #region Create User, Title

                    result += comment.GetDefaultCreate();

                    #endregion Create User, Title

                    #region Set DLL

                    //Create  DLL
                    result += comment.GetDLLAPICreate();

                    #endregion Set DLL

                    result += "namespace Public" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Table Shema

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Public" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Public" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    for (int i = 0; i < entities.Count; i++)
                    {
                        if (entities[i].Type.ToString() == "bit")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "bool " + " _" +
                                     entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  bool " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += "_" + entities[i].Field + "=value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "bigint")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "long " + " _" +
                                    entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  long " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "binary" || entities[i].Type.ToString() == "image" || entities[i].Type.ToString() == "rowversion" || entities[i].Type.ToString() == "timestamp" || entities[i].Type.ToString() == "varbinary")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Byte[] " + " _" +
                                   entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Byte[] " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "date" || entities[i].Type.ToString() == "datetime" || entities[i].Type.ToString() == "smalldatetime")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "DateTime " + " _" +
                                  entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  DateTime " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "decimal" || entities[i].Type.ToString() == "money" || entities[i].Type.ToString() == "numeric" || entities[i].Type.ToString() == "smallmoney")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Decimal " + " _" +
                                  entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Decimal " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += "  _" + entities[i].Field + "=value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "float")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "double " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  double " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "int" || entities[i].Type.ToString() == "smallint")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "int " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  int " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += "_" + entities[i].Field + "= value ;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "real")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Single " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Single " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "time")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "TimeSpan " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  TimeSpan " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "tinyint")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Byte " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Byte " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "uniqueidentifier")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Guid " + " _" +
                                  entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Guid " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += "_" + entities[i].Field + "= value ;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "xml")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "Xml " + " _" +
                                  entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  Xml " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "string")
                        {
                            result += "\t \tprotected" + staticTxt + " " + "string " + " _" +
                                 entities[i].Field + ";" + Environment.NewLine;
                            result += "\t \tpublic  string " + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() != "String")
                        {
                            result += "\t \tprotected" + staticTxt + " " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " _" +
                                     entities[i].Field + " { get ; set;}";
                            result += "\t \tpublic   " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t \tprotected" + staticTxt + " " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " _" +
                                    entities[i].Field + " { get ; set;}";
                            result += "\t \tpublic   " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + entities[i].Field + Environment.NewLine;
                            result += "\t \t\t{" + Environment.NewLine;
                            result += " \t \t\t\t get \t{ return _" + entities[i].Field + ";\t}" + Environment.NewLine;
                            result += "\t \t\t\tset \t{";
                            result += " _" + entities[i].Field + "= value;\t}" + Environment.NewLine;
                            result += "\t \t\t}" + Environment.NewLine;
                        }
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Table Shema

                    result += "}";
                }
            }
            return result;
        }

        public static string GenerateCodeDataAccess(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
            GenrationComment comment = new GenrationComment();
            var result = string.Empty;
            var staticTxt = string.Empty;

            if (DataTypes.Count > 0)
            {
                if (entities.Count > 0)
                {
                    if (classModifiers.Contains("static"))
                    {
                        staticTxt = "static ";
                    }

                    #region Create User, Title

                    result += comment.GetDefaultCreate();

                    #endregion Create User, Title

                    #region Set DLL

                    //Create  DLL
                    result += comment.GetDLLDataAccessCreate(namespaceIn);

                    #endregion Set DLL

                    result += "" + Environment.NewLine;
                    result += "namespace DAL" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Generation Class

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "DAL" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "DAL" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    #region new Database

                    // result += "\t\tprivate Database cn=new Database();" + Environment.NewLine;

                    #endregion new Database

                    #region check Primary key

                    if (entitiesDataType.Count == 0)
                    {
                        MessageBox.Show("Table " + tableName + " chưa tạo khóa chính!");
                    }

                    #endregion check Primary key

                    else
                    {
                        #region Insert

                        if (entitiesDataType.Count >= 1)
                        {
                            result += "\t\tpublic int Insert_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                            result += "\t\t{" + Environment.NewLine;
                            result += "\t\tDatabase cn = new Database();" + Environment.NewLine;
                            result += "\t\t        SqlParameter[] prams = new SqlParameter[]" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            for (int i = 0; i < entities.Count; i++)
                            {
                                #region para

                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entitiesDataType[j].TypeField.ToString().ToUpper() == "INT")
                                    {
                                        if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                            break;
                                    }
                                    else
                                    {
                                        switch (entities[i].Type.ToString())
                                        {
                                            case "bit":
                                                //result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field.ToString() + "\"" + ", " + "SqlDbType.Bit" + ", null, " + tableName.ToLower() + "Obj." + entities[i].Field.ToString() + ");" + Environment.NewLine;
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Bit" + ", 1, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Bit" + ", 1, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "bigint":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.BigInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.BigInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "image":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "binary":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + "??DBNull.Value" + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "rowversion":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "timestamp":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }

                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "varbinary":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarBinary" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarBinary" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "date":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Date" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Date" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "datetime":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + "??  DBNull.Value" + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "smalldatetime":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "decimal":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "money":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "numeric":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "smallmoney":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "float":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Float" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Float" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "int":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "smallint":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.SmallInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.SmallInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "real":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Real" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Real" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "time":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Time" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Time" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "tinyint":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.TinyInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.TinyInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "uniqueidentifier":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.UniqueIdentifier" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.UniqueIdentifier" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "xml":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Xml" + ", null, " + "(object)p." + entities[i].Field + "?? DBNull.Value " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Xml" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "char":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Char" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Char" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "nchar":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "ntext":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NText" + ",2147483647, " + "(object)p." + entities[i].Field + " ?? DBNull.Value " + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NText" + ",2147483647, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "nvarchar":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    if (entities[i].Length.ToString() != "-1")
                                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + "  ?? DBNull.Value " + ")," + Environment.NewLine;
                                                    else
                                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", 4000, " + "(object)p." + entities[i].Field + " ?? DBNull.Value" + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                if (entities[i].Length.ToString() != "-1")
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                else
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "text":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Text" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value" + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Text" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            case "varchar":
                                                if (entities[i].AllowNull.ToString() == "YES")
                                                {
                                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value" + ")," + Environment.NewLine;
                                                    break;
                                                }
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;

                                            default:
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType." + GetType(entities[i].Type.ToString()).Replace("System.", "") + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                                break;
                                        }
                                    }
                                }

                                #endregion para
                            }
                            for (int i = 0; i < entitiesDataType.Count - 1; i++)
                            {
                                if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    result += "\t\t        cn.MakeOutParam(" + "\"" + "@" + entities[i].Field + "_Output" + "\", " + "SqlDbType.Int" + ", 4" + ")," + Environment.NewLine;

                                //else
                                //    result += "\t\t        cn.MakeOutParam(" + "\"" + "@" + entities[i].Field.ToString() + "\"" + ", " + " SqlDbType." + entitiesDataType[i].TypeField.ToString() + "," + entitiesDataType[i].Length.ToString() + ");" + Environment.NewLine;
                            }
                            for (int i = entitiesDataType.Count - 1; i < entitiesDataType.Count; i++)
                            {
                                if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    result += "\t\t        cn.MakeOutParam(" + "\"" + "@" + entities[i].Field + "_Output" + "\", " + "SqlDbType.Int" + ", 4" + ")" + Environment.NewLine;

                                //else
                                //    result += "\t\t        cn.MakeOutParam(" + "\"" + "@" + entities[i].Field.ToString() + "\"" + ", " + " SqlDbType." + entitiesDataType[i].TypeField.ToString() + "," + entitiesDataType[i].Length.ToString() + ");" + Environment.NewLine;
                            }
                            result += "        \t\t};" + Environment.NewLine;

                            result += "\t\t        try" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            result += "\t\t          cn.RunProc(" + "\"sp_" + tableName + "_Insert" + "\"" + ",prams);" + Environment.NewLine;
                            result += "\t\t          return (int)prams[0].Value;" + Environment.NewLine;
                            result += "\t\t        }" + Environment.NewLine;
                            result += "\t\t        catch (Exception ex)" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            result += "\t\t         return 0;" + Environment.NewLine;
                            result += "\t\t       }" + Environment.NewLine;
                            result += "\t\t         finally" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            result += "\t\t        cn.Dispose();" + Environment.NewLine;
                            result += "\t\t       }" + Environment.NewLine;
                            result += "        }\t\t " + Environment.NewLine;
                        }

                        #endregion Insert

                        #region Update

                        if (entitiesDataType.Count >= 1)
                        {
                            result += "\t\tpublic int Update_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                            result += "\t\t{" + Environment.NewLine;
                            result += "\t\tDatabase cn = new Database();" + Environment.NewLine;
                            result += "\t\t        SqlParameter[] prams = new SqlParameter[]" + Environment.NewLine;
                            result += "        \t\t{" + Environment.NewLine;
                            for (int i = 0; i < entities.Count - 1; i++)
                            {
                                switch (entities[i].Type.ToString())
                                {
                                    case "bit":
                                        //result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field.ToString() + "\"" + ", " + "SqlDbType.Bit" + ", null, " + tableName.ToLower() + "Obj." + entities[i].Field.ToString() + ");" + Environment.NewLine;
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Bit" + ", 1, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Bit" + ", 1, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "bigint":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.BigInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.BigInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "image":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "binary":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + "??DBNull.Value" + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "rowversion":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "timestamp":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }

                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "varbinary":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarBinary" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarBinary" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "date":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Date" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Date" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "datetime":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + "??  DBNull.Value" + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "smalldatetime":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "decimal":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "money":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "numeric":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "smallmoney":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "float":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Float" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Float" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "int":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "smallint":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.SmallInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.SmallInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "real":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Real" + ", 20, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Real" + ", 20, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "time":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Time" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Time" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "tinyint":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.TinyInt" + ", 4, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.TinyInt" + ", 4, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "uniqueidentifier":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.UniqueIdentifier" + ", null, " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.UniqueIdentifier" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "xml":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Xml" + ", null, " + "(object)p." + entities[i].Field + "?? DBNull.Value " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Xml" + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "char":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Char" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Char" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "nchar":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value  " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "ntext":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NText" + ",2147483647, " + "(object)p." + entities[i].Field + " ?? DBNull.Value " + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NText" + ",2147483647, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "nvarchar":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            if (entities[i].Length.ToString() != "-1")
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + "  ?? DBNull.Value " + ")," + Environment.NewLine;
                                            else
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", 4000, " + "(object)p." + entities[i].Field + " ?? DBNull.Value" + ")," + Environment.NewLine;
                                            break;
                                        }
                                        if (entities[i].Length.ToString() != "-1")
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        else
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", 4000, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "text":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Text" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.ValueS" + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Text" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    case "varchar":
                                        if (entities[i].AllowNull.ToString() == "YES")
                                        {
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + " ?? DBNull.Value" + ")," + Environment.NewLine;
                                            break;
                                        }
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;

                                    default:
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType." + GetType(entities[i].Type.ToString()).Replace("System.", "") + ", null, " + "(object)p." + entities[i].Field + ")," + Environment.NewLine;
                                        break;
                                }

                                #endregion Update
                            }
                            for (int i = entities.Count - 1; i < entities.Count; i++)
                            {
                                #region para

                                if (entities[i].Type.ToString() == "bit")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Bit" + ", 1, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "bigint")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.BigInt" + ", 4, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "image")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "binary")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Image" + ", 4000, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "rowversion")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "timestamp")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Timestamp" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "varbinary")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarBinary" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "date")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "datetime")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "smalldatetime")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.DateTime" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "decimal")
                                {
                                    result += "\t\t        sqlCmd.SetPara(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "money")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Money" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "numeric")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "smallmoney")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.SmallMoney" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "float")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Float" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "int")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "smallint")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "real")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Decimal" + ", 20, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "time")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Time" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "tinyint")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "uniqueidentifier")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.UniqueIdentifier" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "xml")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Xml" + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "char")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Char" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "nchar")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "ntext")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NText" + ",2147483647, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "nvarchar")
                                {
                                    if (entities[i].Length.ToString() != "-1")
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                    else
                                        result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.NVarChar" + ", 4000, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "text")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Text" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else if (entities[i].Type.ToString() == "varchar")
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.VarChar" + ", " + entities[i].Length + ", " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }
                                else
                                {
                                    result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType." + GetType(entities[i].Type.ToString()).Replace("System.", "") + ", null, " + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                }

                                #endregion para
                            }
                            result += "        \t\t};" + Environment.NewLine;

                            result += "\t\t        try" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            result += "\t\t          cn.RunProc(" + "\"sp_" + tableName + "_Update" + "\"" + ",prams);" + Environment.NewLine;
                            result += "\t\t          return (int)prams[0].Value;" + Environment.NewLine;
                            result += "\t\t        }" + Environment.NewLine;
                            result += "\t\t        catch (Exception ex)" + Environment.NewLine;
                            result += "\t\t        {" + Environment.NewLine;
                            result += "\t\t         return 0;" + Environment.NewLine;
                            result += "\t\t       }" + Environment.NewLine;
                            result += "\t\t       finally" + Environment.NewLine;
                            result += "\t\t       {" + Environment.NewLine;
                            result += "\t\t        cn.Dispose();" + Environment.NewLine;
                            result += "\t\t       }" + Environment.NewLine;
                            result += "        }\t\t " + Environment.NewLine;

                            #endregion Generation Class

                            #region Delete

                            if (entitiesDataType.Count >= 1)
                            {
                                result += "\t\tpublic int Delete_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                                result += "\t\t{" + Environment.NewLine;
                                result += "\t\tDatabase cn = new Database();" + Environment.NewLine;
                                result += "\t\t        SqlParameter[] prams = new SqlParameter[]" + Environment.NewLine;
                                result += "        \t\t{" + Environment.NewLine;

                                #region para

                                int i = 0;
                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                    {
                                        if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                            result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + "SqlDbType.Int" + ", 4," + "(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                        else
                                        {
                                            if (entitiesDataType[i].TypeField.ToString().ToUpper() == "NCHAR")
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + " SqlDbType.NChar," + entitiesDataType[i].Length + ",(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                            else if (entitiesDataType[i].TypeField.ToString().ToLower() == "nvarchar")
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + " SqlDbType.NVarChar," + entitiesDataType[i].Length + ",(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                            else
                                                result += "\t\t        cn.MakeInParam(" + "\"" + "@" + entities[i].Field + "\"" + ", " + " SqlDbType." + entitiesDataType[i].TypeField + "," + entitiesDataType[i].Length + ",(object)p." + entities[i].Field + ")" + Environment.NewLine;
                                        }
                                    }
                                    else
                                        break;
                                }

                                #endregion para

                                result += "        \t\t};" + Environment.NewLine;
                                result += "\t\t        try" + Environment.NewLine;
                                result += "\t\t        {" + Environment.NewLine;
                                result += "\t\t          cn.RunProc(" + "\"sp_" + tableName + "_Delete" + "\"" + ",prams);" + Environment.NewLine;
                                result += "\t\t          return (int)prams[0].Value;" + Environment.NewLine;
                                result += "\t\t        }" + Environment.NewLine;
                                result += "\t\t        catch (Exception ex)" + Environment.NewLine;
                                result += "\t\t        {" + Environment.NewLine;
                                result += "\t\t         return 0;" + Environment.NewLine;
                                result += "\t\t       }" + Environment.NewLine;
                                result += "\t\t       finally" + Environment.NewLine;
                                result += "\t\t       {" + Environment.NewLine;
                                result += "\t\t          cn.Dispose();" + Environment.NewLine;
                                result += "\t\t       }" + Environment.NewLine;
                                result += "        }\t\t " + Environment.NewLine;
                            }

                            #endregion Delete

                            #region Load All Field

                            if (entitiesDataType.Count >= 1)
                            {
                                result += "\t\tpublic DataTable All_" + tableName + "()" + Environment.NewLine;
                                result += "\t\t{" + Environment.NewLine;
                                result += "\t\tDatabase cn = new Database();" + Environment.NewLine;
                                result += "\t\t        try" + Environment.NewLine;
                                result += "\t\t        {" + Environment.NewLine;
                                result += "\t\t            DataTable dt=  cn.RunExecProc(" + "\"sp_" + tableName + "_All" + "\"" + ").Tables[0];" + Environment.NewLine;
                                result += "\t\t            return dt;" + Environment.NewLine;
                                result += "\t\t        }" + Environment.NewLine;
                                result += "\t\t        catch (Exception ex)" + Environment.NewLine;
                                result += "\t\t        {" + Environment.NewLine;
                                result += "\t\t         return null;" + Environment.NewLine;
                                result += "\t\t        }" + Environment.NewLine;
                                result += "\t\t       finally" + Environment.NewLine;
                                result += "\t\t       {" + Environment.NewLine;
                                result += "\t\t        cn.Dispose();" + Environment.NewLine;
                                result += "\t\t       }" + Environment.NewLine;
                                result += "\t\t}" + Environment.NewLine;
                            }

                            #endregion Load All Field

                            result += "\t}" + Environment.NewLine;
                            result += "}" + Environment.NewLine;
                        }
                    }
                }
            }
            return result;
        }

        public static string GenerateCodeBLL(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
            GenrationComment comment = new GenrationComment();
            var result = string.Empty;
            var staticTxt = string.Empty;

            if (DataTypes.Count > 0)
            {
                if (entities.Count > 0)
                {
                    if (classModifiers.Contains("static"))
                    {
                        staticTxt = "static ";
                    }

                    #region Create User, Title

                    result += comment.GetDefaultCreate();

                    #endregion Create User, Title

                    #region Set DLL

                    //Create  DLL
                    result += comment.GetDLLBLLCreate(namespaceIn);

                    #endregion Set DLL

                    #region Namespace

                    result += "namespace BUS" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Class

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "BUS" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "BUS" + Environment.NewLine;
                    }
                    result += "\t{" + Environment.NewLine;

                    #region new Database

                    result += "\t\tprivate " + tableName + "DAL cls=new " + tableName + "DAL();" + Environment.NewLine;

                    #endregion new Database

                    #region Insert

                    result += "\t\tpublic int Insert_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;

                    result += "\t\t        return cls.Insert_" + tableName + "(p);" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    #endregion Insert

                    #region Update

                    result += "\t\tpublic int Update_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;

                    result += "\t\t        return cls.Update_" + tableName + "(p);" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    #endregion Update

                    #region Delete

                    result += "\t\tpublic int Delete_" + tableName + "(" + tableName + "Public" + " p)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;

                    result += "\t\t        return cls.Delete_" + tableName + "(p);" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    #endregion Delete

                    #region Load All Field

                    result += "\t\tpublic DataTable All_" + tableName + "()" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;

                    result += "\t\t        return cls.All_" + tableName + "();" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    #endregion Load All Field

                    result += "\t}" + Environment.NewLine;

                    #endregion Class

                    result += "}" + Environment.NewLine;

                    #endregion Namespace
                }
            }
            return result;
        }

        public static string GenerateStoreProceduce(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
            var result = string.Empty;
            var staticTxt = string.Empty;

            if (DataTypes.Count > 0)
            {
                if (entities.Count > 0)
                {
                    result += "/* " + Environment.NewLine;
                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
                    result += "//                                                                                                  // " + Environment.NewLine;
                    result += "// Ngày tạo:" + DateTime.Now.ToString() + "                                          //" + Environment.NewLine;

                    result += "//                                                                                                  //" + Environment.NewLine;
                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
                    result += " */" + Environment.NewLine;

                    #region Insert Table

                    result += "if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[" + "sp_" + tableName + "_Insert" + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)" + Environment.NewLine;
                    result += "drop procedure dbo.[" + "sp_" + tableName + "_Insert" + "]" + Environment.NewLine;
                    result += "GO" + Environment.NewLine;

                    result += "create  procedure " + "sp_" + tableName + "_Insert" + Environment.NewLine;
                    //foreach (var entity in entities)
                    //{
                    if (entitiesDataType.Count >= 1)
                    {
                        for (int i = 0; i < entities.Count - 1; i++)
                        {
                            foreach (var entityType in entitiesDataType)
                            {
                                if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                        break;
                                }
                                if (entities[i].Type.ToString() == "bit")
                                {
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + "," + Environment.NewLine;
                                }
                                else if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                                {
                                    if (entities[i].Length.ToString() == "-1")
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + " (max)," + Environment.NewLine;
                                    else
                                        result += "@" + entities[i].Field + " as Nvarchar (max)," + Environment.NewLine;
                                    //result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")," + Environment.NewLine;
                                }
                                else
                                {
                                    if (entities[i].Type.ToString() != "image" && entities[i].Type.ToString() != "real" && entities[i].Type.ToString() != "int" && entities[i].Type.ToString() != "datetime" && entities[i].Type.ToString() != "smalldatetime")
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + " (" + entities[i].Length + ")," + Environment.NewLine;
                                    else
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + "," + Environment.NewLine;
                                }
                            }
                        }
                        for (int i = entities.Count - 1; i < entities.Count; i++)
                        {
                            foreach (var entityType in entitiesDataType)
                            {
                                if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                        break;
                                }
                                if (entities[i].Type.ToString() == "bit")
                                {
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + "";
                                }
                                else if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                                {
                                    if (entities[i].Length.ToString() == "-1")
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + " (max)";
                                    else
                                        //result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")";
                                        result += "@" + entities[i].Field + " as Nvarchar (max)," + Environment.NewLine;
                                }
                                else
                                {
                                    if (entities[i].Type.ToString() != "image" && entities[i].Type.ToString() != "real" && entities[i].Type.ToString() != "int" && entities[i].Type.ToString() != "datetime" && entities[i].Type.ToString() != "smalldatetime")
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + " (" + entities[i].Length + ")";
                                    else
                                        result += "@" + entities[i].Field + " as " + entities[i].Type + "";
                                }
                            }
                        }
                        for (int i = 0; i < entitiesDataType.Count; i++)
                        {
                            if (entitiesDataType[i].Length.ToString() != "")
                            {
                                if (i < entitiesDataType.Count - 1)
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    {
                                        result += "," + Environment.NewLine;
                                        result += "@" + entitiesDataType[i].NameField + "_Output " + entitiesDataType[i].TypeField + " (" + entitiesDataType[i].Length + ")" + " output ," + Environment.NewLine;
                                    }
                                }
                                else
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    {
                                        result += "," + Environment.NewLine;
                                        result += "@" + entitiesDataType[i].NameField + "_Output " + entitiesDataType[i].TypeField + " (" + entitiesDataType[i].Length + ")" + " output " + Environment.NewLine;
                                    }
                                }
                            }
                            else
                            {
                                if (i < entitiesDataType.Count - 1)
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    {
                                        result += "," + Environment.NewLine;
                                        result += "@" + entitiesDataType[i].NameField + "_Output " + entitiesDataType[i].TypeField + " output ," + Environment.NewLine;
                                    }
                                }
                                else
                                {
                                    if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                    {
                                        result += "," + Environment.NewLine;
                                        result += "@" + entitiesDataType[i].NameField + "_Output " + entitiesDataType[i].TypeField + " output " + Environment.NewLine;
                                    }
                                }
                            }
                        }

                        result += " as" + Environment.NewLine;

                        result += "insert into " + tableName + "(" + Environment.NewLine;
                        if (entitiesDataType.Count >= 1)
                        {
                            for (int i = 0; i < entities.Count - 1; i++)
                            {
                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                    {
                                        if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                            break;
                                    }
                                    // else
                                    result += entities[i].Field + "," + Environment.NewLine;
                                }
                            }
                            for (int i = entities.Count - 1; i < entities.Count; i++)
                            {
                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                    {
                                        if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                            break;
                                    }
                                    // else
                                    result += entities[i].Field + "" + Environment.NewLine;
                                }
                            }
                        }

                        result += ")" + Environment.NewLine;
                        result += "values (" + Environment.NewLine;

                        if (entitiesDataType.Count >= 1)
                        {
                            for (int i = 0; i < entities.Count - 1; i++)
                            {
                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                    {
                                        if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                            break;
                                    }
                                    // else
                                    result += "@" + entities[i].Field + "," + Environment.NewLine;
                                }
                            }
                            for (int i = entities.Count - 1; i < entities.Count; i++)
                            {
                                for (int j = 0; j < entitiesDataType.Count; j++)
                                {
                                    if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                                    {
                                        if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                                            break;
                                    }
                                    // else
                                    result += "@" + entities[i].Field + "" + Environment.NewLine;
                                }
                            }
                        }

                        result += ")" + Environment.NewLine;
                    }
                    //else
                    //{
                    //    for (int i = 0; i < entities.Count; i++)
                    //    {
                    //        foreach (var entityType in entitiesDataType)
                    //        {
                    //            if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                    //            {
                    //            }
                    //            else
                    //            {
                    //                if (entities[i].Type.ToString() == "bit")
                    //                {
                    //                    result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + "," + Environment.NewLine;
                    //                }

                    //                else if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                    //                {
                    //                    if(entities[i].Length.ToString()=="-1")
                    //                       result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (max)," + Environment.NewLine;
                    //                    else
                    //                        result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " ("+ entities[i].Length.ToString()+")," + Environment.NewLine;
                    //                }
                    //                else
                    //                {
                    //                    if (entities[i].Type.ToString() != "image" && entities[i].Type.ToString() != "real" && entities[i].Type.ToString() != "int" && entities[i].Type.ToString() != "datetime" && entities[i].Type.ToString() != "smalldatetime")
                    //                        result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")," + Environment.NewLine;
                    //                    else
                    //                        result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + "," + Environment.NewLine;

                    //                }
                    //            }
                    //        }

                    //    }

                    //    //}
                    //    result += "@ID_Output int output" + Environment.NewLine;
                    //    result += " as" + Environment.NewLine;

                    //    result += "insert into " + tableName + "(" + Environment.NewLine;

                    //    for (int i = 0; i < entities.Count; i++)
                    //    {
                    //        for (int j = 0; j < entitiesDataType.Count; j++)
                    //            {
                    //                if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                    //                {
                    //                    //if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                    //                    //    break;
                    //                }
                    //                else
                    //                {
                    //                    if (i < entities.Count - 1)
                    //                    {
                    //                        result += entities[i].Field.ToString() + "," + Environment.NewLine;
                    //                    }
                    //                    else
                    //                    {
                    //                        result += entities[i].Field.ToString() + Environment.NewLine;
                    //                    }
                    //                }
                    //            }
                    //    }

                    //    result += ")" + Environment.NewLine;
                    //    result += "values (" + Environment.NewLine;

                    //    for (int i = 0; i < entities.Count; i++)
                    //    {
                    //        for (int j = 0; j < entitiesDataType.Count; j++)
                    //        {
                    //            if (entities[i].Field.ToString() == entitiesDataType[j].NameField.ToString())
                    //            {
                    //                //if (entitiesDataType[i].TypeField.ToString().ToUpper() == "INT")
                    //                //    break;
                    //            }
                    //            else
                    //            {
                    //                if (i < entities.Count - 1)
                    //                {
                    //                    result +="@"+ entities[i].Field.ToString() + "," + Environment.NewLine;
                    //                }
                    //                else
                    //                {
                    //                    result += "@" + entities[i].Field.ToString() + Environment.NewLine;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    result += ")" + Environment.NewLine;
                    //    result += "SET @ID_Output = CAST(SCOPE_IDENTITY() AS INT)" + Environment.NewLine;
                    //}

                    #endregion Insert Table

                    #region Update Table

                    result += "go" + Environment.NewLine;

                    result += Environment.NewLine;
                    result += Environment.NewLine;
                    result += Environment.NewLine;

                    result += "if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[" + "sp_" + tableName + "_Update" + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)" + Environment.NewLine;
                    result += "drop procedure dbo.[" + "sp_" + tableName + "_Update" + "]" + Environment.NewLine;
                    result += "GO" + Environment.NewLine;

                    result += "create  procedure " + "sp_" + tableName + "_Update" + Environment.NewLine;

                    if (entitiesDataType.Count > 1)
                    {
                        for (int i = 0; i < entities.Count; i++)
                        {
                            string a = (i == entities.Count - 1) ? "" : ",";

                            if (entities[i].Type.ToString() == "bit")
                            {
                                result += "@" + entities[i].Field + " as " + entities[i].Type + a + Environment.NewLine;
                            }
                            else if (entities[i].Type.ToString() == "numeric")
                            {
                                result += "@" + entities[i].Field + " as " + entities[i].Type + a + Environment.NewLine;
                            }
                            else if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                            {
                                if (entities[i].Length.ToString() == "-1")
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + " (max)" + a + Environment.NewLine;
                                else
                                    // result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")" + a + Environment.NewLine;
                                    result += "@" + entities[i].Field + " as Nvarchar (max)," + Environment.NewLine;
                            }
                            else
                            {
                                if (entities[i].Type.ToString() != "image" && entities[i].Type.ToString() != "real" && entities[i].Type.ToString() != "int" && entities[i].Type.ToString() != "datetime" && entities[i].Type.ToString() != "smalldatetime")
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + " (" + entities[i].Length + ")" + a + Environment.NewLine;
                                else
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + a + Environment.NewLine;
                            }
                        }

                        result += " as" + Environment.NewLine;
                        //foreach (var entityType in entitiesDataType)
                        //{
                        //    result += " set @I" + entityType.NameField.ToString() + "_Output  = " + "@" + entityType.NameField.ToString() + Environment.NewLine;
                        //}
                        //result += " set @" + entitiesDataType[0].NameField.ToString() + "_Output  = " + "@" + entitiesDataType[0].NameField.ToString() + Environment.NewLine;
                        //result += " set @" + entitiesDataType[1].NameField.ToString() + "_Output  = " + "@" + entitiesDataType[1].NameField.ToString() + Environment.NewLine;

                        result += "update " + tableName + " set " + Environment.NewLine;
                        for (int i = 0; i < entities.Count; i++)
                        {
                            //foreach (var entityType in entitiesDataType)
                            //{
                            //    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                            //    {
                            //    }
                            //    else
                            //    {
                            if (i < entities.Count - 1)
                            {
                                result += entities[i].Field + " = " + "@" + entities[i].Field + "," + Environment.NewLine;
                            }
                            else
                            {
                                result += entities[i].Field + " = " + "@" + entities[i].Field + Environment.NewLine;
                            }
                            //    }
                            //}
                        }

                        for (int i = 0; i < entitiesDataType.Count; i++)
                        {
                            if (i < 1)
                            {
                                result += "where (@" + entitiesDataType[i].NameField + " = " + tableName + "." + entitiesDataType[i].NameField + " and " + Environment.NewLine;
                            }
                            else
                            {
                                result += "@" + entitiesDataType[i].NameField + " = " + tableName + "." + entitiesDataType[i].NameField + "" + Environment.NewLine;
                            }
                        }

                        //foreach (var entityType in entitiesDataType)
                        //{
                        //    result += "where (@" + entityType.NameField.ToString() + " = " + tableName + "." + entityType.NameField.ToString() + Environment.NewLine;
                        //}

                        result += ")" + Environment.NewLine;
                    }
                    else
                    {
                        for (int i = 0; i < entities.Count; i++)
                        {
                            string a = (i == entities.Count - 1) ? "" : ",";

                            if (entities[i].Type.ToString() == "bit")
                            {
                                result += "@" + entities[i].Field + " as " + entities[i].Type + a + Environment.NewLine;
                            }
                            else if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                            {
                                if (entities[i].Length.ToString() == "-1")
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + " (max)" + a + Environment.NewLine;
                                else
                                    // result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")" + a + Environment.NewLine;
                                    result += "@" + entities[i].Field + " as Nvarchar (max)," + Environment.NewLine;
                            }
                            else
                            {
                                if (entities[i].Type.ToString() != "image" && entities[i].Type.ToString() != "real" && entities[i].Type.ToString() != "int" && entities[i].Type.ToString() != "datetime" && entities[i].Type.ToString() != "smalldatetime")
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + " (" + entities[i].Length + ")" + a + Environment.NewLine;
                                else
                                    result += "@" + entities[i].Field + " as " + entities[i].Type + a + Environment.NewLine;
                            }
                        }

                        //  result += "@ID_Output int output" + Environment.NewLine;
                        result += " as" + Environment.NewLine;
                        //foreach (var entityType in entitiesDataType)
                        //{
                        //    result += " set @ID_Output = " + "@" + entityType.NameField.ToString() + Environment.NewLine;
                        //}

                        result += "update " + tableName + " set " + Environment.NewLine;
                        for (int i = 0; i < entities.Count; i++)
                        {
                            foreach (var entityType in entitiesDataType)
                            {
                                if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                                {
                                }
                                else
                                {
                                    if (i < entities.Count - 1)
                                    {
                                        result += entities[i].Field + " = " + "@" + entities[i].Field + "," + Environment.NewLine;
                                    }
                                    else
                                    {
                                        result += entities[i].Field + " = " + "@" + entities[i].Field + Environment.NewLine;
                                    }
                                }
                            }
                        }

                        foreach (var entityType in entitiesDataType)
                        {
                            result += "where " + tableName + "." + entityType.NameField + " =  @" + entityType.NameField + Environment.NewLine;
                        }

                        result += Environment.NewLine;
                    }

                    #endregion Update Table

                    #region Delete Table

                    result += Environment.NewLine;
                    result += "go" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += Environment.NewLine;

                    result += "if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[" + "sp_" + tableName + "_Delete" + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)" + Environment.NewLine;
                    result += "drop procedure dbo.[" + "sp_" + tableName + "_Delete" + "]" + Environment.NewLine;
                    result += "GO" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += "create  procedure " + "sp_" + tableName + "_Delete" + Environment.NewLine;
                    //if (entitiesDataType.Count > 1)
                    //{
                    //    result += " as" + Environment.NewLine;
                    //    result += "delete " + tableName + Environment.NewLine;
                    //    result += "where (@" + entitiesDataType[0].NameField.ToString() + " = " + tableName + "." + entitiesDataType[0].NameField.ToString() + " and " + Environment.NewLine;
                    //    result += "@" + entitiesDataType[1].NameField.ToString() + " = " + tableName + "." + entitiesDataType[1].NameField.ToString() +")"+ Environment.NewLine;
                    //}
                    //else
                    //{
                    int k = 0;
                    foreach (var entityType in entitiesDataType)
                    {
                        string a = (k == entitiesDataType.Count - 1) ? "" : ",";
                        if (entityType.TypeField.ToString() != "image" && entityType.TypeField.ToString() != "real" && entityType.TypeField.ToString() != "int" && entityType.TypeField.ToString() != "datetime" && entityType.TypeField.ToString() != "smalldatetime")
                            result += "@" + entityType.NameField + " as " + entityType.TypeField + " (" + entityType.Length + ")" + a + Environment.NewLine;
                        else
                            result += "@" + entityType.NameField + " as " + entityType.TypeField + a + Environment.NewLine;
                        //result += "@ID_Output int output" + Environment.NewLine;
                        result += " as" + Environment.NewLine;
                        //  result += " set @ID_Output = " + "@"+entityType.NameField.ToString() + Environment.NewLine;
                        result += "delete " + tableName + Environment.NewLine;
                        result += "where (" + tableName + "." + entityType.NameField + "=" + "@" + entityType.NameField + ")" + Environment.NewLine;
                        k++;
                    }
                    // }

                    #endregion Delete Table

                    #region Load All Field

                    result += Environment.NewLine;
                    result += "go" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += Environment.NewLine;

                    result += "if exists (select * from dbo.sysobjects where id = object_id(N'dbo.[" + "sp_" + tableName + "_All" + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)" + Environment.NewLine;
                    result += "drop procedure dbo.[" + "sp_" + tableName + "_All" + "]" + Environment.NewLine;
                    result += "GO" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += "create  procedure " + "sp_" + tableName + "_All" + Environment.NewLine;
                    result += "as" + Environment.NewLine;
                    result += "Begin" + Environment.NewLine;
                    result += "select * from " + tableName + Environment.NewLine;
                    result += "End" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += "go" + Environment.NewLine;
                    result += Environment.NewLine;
                    result += Environment.NewLine;

                    #endregion Load All Field
                }
            }
            return result;
        }

        #region Helper Methods

        private static string GetType(string valueIn)
        {
            string value = valueIn.ToUpper();
            string result = "string";

            foreach (var key in DataTypes.Keys)
            {
                if (value.Contains(key))
                {
                    DataTypes.TryGetValue(key, out Type type);

                    if (type != null)
                    {
                        return type.ToString();
                    }
                }
            }

            return result;
        }

        private static string FirstToUpper(string input)
        {
            string temp = input.Substring(0, 1);
            return temp.ToUpper() + input.Remove(0, 1).ToLower();
        }

        #endregion Helper Methods
    }
}