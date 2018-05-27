using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassCodeBLL
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        public static string GenerateCodeBLL(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
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

                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
                    result += "//                                                                                                  // " + Environment.NewLine;
                    result += "// Purpose:                                                                                         //" + Environment.NewLine;
                    result += "// Created Date:" + DateTime.Now.ToString() + "                                           //" + Environment.NewLine;
                    result += "// Last Modified Date: " + DateTime.Now.ToString() + "                                              //" + Environment.NewLine;
                    result += "// Author: DSI Group                                                                                //" + Environment.NewLine;
                    result += "//                                                                                                  //" + Environment.NewLine;
                    result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;

                    #region Set DLL

                    result += "using System;" + Environment.NewLine;
                    result += "using System.Collections.Generic;" + Environment.NewLine;
                    result += "using System.Linq;" + Environment.NewLine;
                    result += "using System.Text;" + Environment.NewLine;

                    result += "using " + namespaceIn + ".DBMS.DataAccess;" + Environment.NewLine;
                    result += "using " + namespaceIn + ".DBMS.API;" + Environment.NewLine;
                    result += "using System.Reflection;" + Environment.NewLine;

                    #endregion Set DLL

                    #region Namespace

                    result += "namespace " + namespaceIn + ".BLL" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Class

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "BLL : I" + tableName + "API" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "BLL : I" + tableName + "API" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t\tpublic TransactionCommandAck Save" + tableName + "(" + tableName + " " + tableName.ToLower() + "Obj)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t      TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;
                    result += "\t\t     List<String> lstInValidProperty = new List<string>();" + Environment.NewLine;
                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t        transAck = " + tableName.ToLower() + "DA.Save" + tableName + "(" + tableName.ToLower() + "Obj);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return transAck;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\tpublic TransactionCommandAck DeleteByID" + tableName + "(" + entityType.TypeField.ToString() + " " + entityType.NameField.ToString() + ")" + Environment.NewLine;
                        result += "\t\t{" + Environment.NewLine;
                        result += "\t\t    TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;
                        result += "\t\t    try" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                        result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                        result += "\t\t        transAck = " + tableName.ToLower() + "DA.DeleteByID" + tableName + "(" + entityType.NameField.ToString() + ");" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                        result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t   }" + Environment.NewLine;
                        result += "\t\t   return transAck;" + Environment.NewLine;
                        result += "\t\t}" + Environment.NewLine;

                        result += "" + Environment.NewLine;

                        result += "\t\tpublic TransactionGet" + tableName + " Search" + tableName + "(string keyword, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                        result += "\t\t{" + Environment.NewLine;
                        result += "\t\t    TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new " + "TransactionGet" + tableName + "();" + Environment.NewLine;
                        result += "\t\t    try" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                        result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                        result += "\t\t        " + tableName.ToLower() + "TransAckList = " + tableName.ToLower() + "DA.Search" + tableName + "(keyword, searchCondition, isANDSearch);" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                        result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t   }" + Environment.NewLine;
                        result += "\t\t   return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                        result += "\t\t}" + Environment.NewLine;
                    }
                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + " Search" + tableName + "(DateTime startDate, DateTime endDate, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new " + "TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "TransAckList = " + tableName.ToLower() + "DA.Search" + tableName + "(startDate, endDate, isANDSearch);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + "Viewers " + " Search" + tableName + "(" + tableName + "Filter" + "obj" + tableName + "Filter" + "," + "List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewerTransAckList = new " + "TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "ViewerTransAckList = " + tableName.ToLower() + "DA.Search" + tableName + "(" + "obj" + tableName + "Filter" + ", lstReturnFields, objSearchCondition, isANDSearch);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewerTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + "Viewers " + " Search" + tableName + "(" + "string keyword, DateTime startDate, DateTime endDate" + "," + "List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewerTransAckList = new " + "TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DA " + tableName.ToLower() + "DA = new " + tableName + "DA();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "ViewerTransAckList = " + tableName.ToLower() + "DA.Search" + tableName + "(keyword, startDate, endDate, lstReturnFields, searchCondition, isANDSearch);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewerTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "}" + Environment.NewLine;

                    #endregion Class

                    result += "}" + Environment.NewLine;

                    #endregion Namespace
                }
            }
            return result;
        }
    }
}