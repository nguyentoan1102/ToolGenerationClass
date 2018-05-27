using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassCodeContract
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        public static string GenerateCodeContract(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        {
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
                    result += " " + Environment.NewLine;
                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
                    result += "//                                                                                                  // " + Environment.NewLine;
                    result += "// Purpose:                                                                                         //" + Environment.NewLine;
                    result += "// Created Date:" + DateTime.Now.ToString() + "                                              //" + Environment.NewLine;
                    result += "// Last Modified Date: " + DateTime.Now.ToString() + "                                              //" + Environment.NewLine;
                    result += "// Author: DSI Group                                                                                //" + Environment.NewLine;
                    result += "//                                                                                                  //" + Environment.NewLine;
                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;

                    #region Set DLL

                    result += "using System;" + Environment.NewLine;
                    result += "using System.Collections.Generic;" + Environment.NewLine;
                    result += "using System.Linq;" + Environment.NewLine;
                    result += "using System.Text;" + Environment.NewLine;
                    result += "using System.Runtime.Serialization;" + Environment.NewLine;
                    result += "using System.ServiceModel;" + Environment.NewLine;

                    #endregion Set DLL

                    #region Namespace

                    result += "namespace " + namespaceIn + ".Contract.Service" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Class

                    result += "\t" + "[KnownType(typeof(" + namespaceIn + ".DBMS.API." + tableName + "Viewer))]" + Environment.NewLine;
                    result += "\t" + "[DataContract(Name = " + tableName + "Viewer" + ")]" + Environment.NewLine;

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Service : " + namespaceIn + ".DBMS.API." + tableName + "Viewer" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Service : " + namespaceIn + ".DBMS.API." + tableName + "Viewer" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    #region Fields & Props

                    for (int i = 0; i < entities.Count; i++)
                    {
                        //foreach (var entity in entities)
                        //{
                        result += "\t \t[DataMember]" + Environment.NewLine;

                        if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim().ToUpper() == "STRING")
                        {
                            //result += "\t \t [DataMember]" + Environment.NewLine;
                            result += "\t \tpublic " + "override " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " " +
                                      entities[i].Field.ToString() + Environment.NewLine;
                            result += "\t \t{" + Environment.NewLine;

                            result += "\t\t\tget" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\treturn " + "base." + entities[i].Field.ToString() + ";" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t\t\tset" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\t" + "base." + entities[i].Field.ToString() + " = value;" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t \t}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString().ToUpper() == "INT")
                        {
                            //result += "\t \t [DataMember]" + Environment.NewLine;
                            result += "\t \tpublic " + "override " + entities[i].Type.ToString() + "? " +
                                      entities[i].Field.ToString() + Environment.NewLine;
                            result += "\t \t{" + Environment.NewLine;

                            result += "\t\t\tget" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\treturn " + "base." + entities[i].Field.ToString() + ";" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t\t\tset" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\t" + "base." + entities[i].Field.ToString() + " = value;" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t \t}" + Environment.NewLine;
                        }
                        else
                        {
                            //result += "\t \t [DataMember]" + Environment.NewLine;
                            result += "\t \tpublic " + "override " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + "? " +
                                      entities[i].Field.ToString() + Environment.NewLine;
                            result += "\t \t{" + Environment.NewLine;

                            result += "\t\t\tget" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\treturn " + "base." + entities[i].Field.ToString() + ";" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t\t\tset" + Environment.NewLine;
                            result += "\t\t\t{" + Environment.NewLine;
                            result += "\t\t\t\t" + "base." + entities[i].Field.ToString() + " = value;" + Environment.NewLine;
                            result += "\t\t\t}" + Environment.NewLine;

                            result += "\t \t}" + Environment.NewLine;
                        }
                        //}
                    }

                    #endregion Fields & Props

                    result += "\t}" + Environment.NewLine;

                    #endregion Class

                    #region Interface

                    result += "\t[ServiceContract]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + "public interface" + " I" + tableName + "Service" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + "public interface" + " I" + tableName + "Service" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionCommandAck Save" + tableName + "(" + namespaceIn + ".DBMS.API." + tableName + " " + tableName.ToLower() + "Obj);" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionGet" + tableName + "Fields Save" + tableName + "(" + namespaceIn + ".DBMS.API." + tableName + " " + tableName.ToLower() + "Obj," + " String strUserId" + ");" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\t" + namespaceIn + ".DBMS.API.TransactionCommandAck DeleteByID" + tableName + "(" + entityType.TypeField.ToString() + " " + entityType.NameField.ToString() + ");" + Environment.NewLine;
                    }
                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionGet" + tableName + " Search" + tableName + "(" + "string keyword," + namespaceIn + ".DBMS.API.SearchCondition searchCondition, " + "bool isANDSearch);" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionGet" + tableName + " Search" + tableName + "(" + "DateTime startDate, DateTime endDate, bool isANDSearch" + ");" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionGet" + tableName + "Viewers" + " Search" + tableName + "(" + namespaceIn + ".DBMS.API." + tableName + "Filter " + "obj" + tableName + "Filter, " + "List<" + namespaceIn + ".DBMS.API." + tableName + "ReturnField> lstReturnFields, " + namespaceIn + ".DBMS.API.SearchCondition objSearchCondition, bool isANDSearch" + ");" + Environment.NewLine;

                    result += "\t\t[OperationContract]" + Environment.NewLine;
                    result += "\t\t" + namespaceIn + ".DBMS.API.TransactionGet" + tableName + "Viewers" + " Search" + tableName + "(" + "string keyword, DateTime startDate, DateTime endDate," + "List<" + namespaceIn + ".DBMS.API." + tableName + "ReturnField> lstReturnFields, " + namespaceIn + ".DBMS.API.SearchCondition objSearchCondition, bool isANDSearch" + ");" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion Interface

                    result += "}" + Environment.NewLine;

                    #endregion Namespace
                }
            }
            return result;
        }
    }
}