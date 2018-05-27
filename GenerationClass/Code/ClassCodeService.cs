using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassCodeService
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        public static string GenerateCodeService(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
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
                    result += "using " + namespaceIn + ".BLL;" + Environment.NewLine;
                    result += "using " + namespaceIn + ".DBMS.API;" + Environment.NewLine;

                    #endregion Set DLL

                    #region Namespace

                    result += "namespace " + namespaceIn + ".Contract.Service" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Class

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "ServiceImpl : " + "I" + tableName + "Service" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "ServiceImpl : " + "I" + tableName + "Service" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t\tpublic TransactionCommandAck Save" + tableName + "(" + tableName + " " + tableName.ToLower() + "Obj)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t      TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;

                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;

                    result += "\t\t        transAck = " + tableName.ToLower() + "BLL" + ".Save" + tableName + "(" + tableName.ToLower() + "Obj);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return transAck;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + "Fields Save" + tableName + "(" + tableName + " " + tableName.ToLower() + "Obj, " + "string strUserId" + ")" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t     TransactionGet" + tableName + "Fields " + "trans" + tableName + "Fields = new TransactionGet" + tableName + "Fields();" + Environment.NewLine;
                    result += "\t\t     Domain.Core.Validation.SystemValidation objSysVal = new Domain.Core.Validation.SystemValidation();" + Environment.NewLine;
                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "DLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;

                    result += "\t\t        List<String> lstInvalidFields = objSysVal.GetInvalidFields(" + tableName.ToLower() + "Obj, strUserId);" + Environment.NewLine;

                    result += "\t\t        if (lstInvalidFields.Count > 0)" + Environment.NewLine;
                    result += "\t\t        {" + Environment.NewLine;
                    result += "\t\t            trans" + tableName + "Fields.returnObjects = lstInvalidFields;" + Environment.NewLine;
                    result += "\t\t            trans" + tableName + "Fields.message =" + "\"" + "Some fields should not be left blank." + "\"" + ";" + Environment.NewLine;
                    result += "\t\t            trans" + tableName + "Fields.isSuccess = false;" + Environment.NewLine;
                    result += "\t\t        }" + Environment.NewLine;
                    result += "\t\t        else" + Environment.NewLine;
                    result += "\t\t        {" + Environment.NewLine;
                    result += "\t\t            TransactionCommandAck transAck = cusBLL.Save" + tableName + "(" + tableName.ToLower() + "Obj);" + Environment.NewLine;
                    result += "\t\t            trans" + tableName + "Fields.message = transAck.message;" + Environment.NewLine;
                    result += "\t\t            trans" + tableName + "Fields.isSuccess = transAck.isSuccess;" + Environment.NewLine;
                    result += "\t\t        }" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + "trans" + tableName + "Fields;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\tpublic TransactionCommandAck DeleteByID" + tableName + "(" + entityType.TypeField.ToString() + " " + entityType.NameField.ToString() + ")" + Environment.NewLine;
                        result += "\t\t{" + Environment.NewLine;
                        result += "\t\t      TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;
                        result += "\t\t     try" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        " + tableName + "BLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;
                        result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                        result += "\t\t        transAck = " + tableName.ToLower() + "BLL" + ".DeleteByID" + tableName + "(" + entityType.NameField.ToString() + ");" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                        result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t   }" + Environment.NewLine;
                        result += "\t\t   return transAck;" + Environment.NewLine;
                        result += "\t\t}" + Environment.NewLine;
                    }
                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + " Search" + tableName + "(string keyword, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t      TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new " + "TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "BLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList = " + tableName.ToLower() + "BLL" + ".Search" + tableName + "(keyword, searchCondition, isANDSearch);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + " Search" + tableName + "(DateTime startDate, DateTime endDate, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t      TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new " + "TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "BLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "TransAckList = " + tableName.ToLower() + "BLL" + ".Search" + tableName + "(startDate, endDate, isANDSearch);" + Environment.NewLine;
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
                    result += "\t\t        " + tableName + "BLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "ViewerTransAckList = " + tableName.ToLower() + "BLL" + ".Search" + tableName + "(objCusFilter, lstReturnFields, objSearchCondition, isANDSearch);" + Environment.NewLine;
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
                    result += "\t\t      TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewerTransAckList = new " + "TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t     try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        " + tableName + "BLL " + tableName.ToLower() + "BLL" + "= new " + tableName + "BLL();" + Environment.NewLine;
                    result += "\t\t        //if all are valid, save the object" + Environment.NewLine;
                    result += "\t\t       " + tableName.ToLower() + "ViewerTransAckList = " + tableName.ToLower() + "BLL" + ".Search" + tableName + "(keyword, startDate, endDate, lstReturnFields, searchCondition, isANDSearch);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewerTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion Class

                    result += "}" + Environment.NewLine;

                    #endregion Namespace
                }
            }
            return result;
        }
    }
}