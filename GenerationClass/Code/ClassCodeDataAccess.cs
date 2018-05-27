using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassCodeDataAccess
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        public static string GenerateCodeDataAccess(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
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

                    result += "using System.Data;" + Environment.NewLine;
                    result += "using System.Data.SqlClient;" + Environment.NewLine;
                    result += "using " + namespaceIn + ".DBMS.API;" + Environment.NewLine;
                    result += "using System.Reflection;" + Environment.NewLine;

                    #endregion Set DLL

                    #region Namespace

                    result += "" + Environment.NewLine;
                    result += "namespace " + namespaceIn + ".DBMS.DataAccess" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Class

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "DA : I" + tableName + "API" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "DA : I" + tableName + "API" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;
                    result += "\t\t private DataAccessHelper<" + tableName + "> daHelper;" + Environment.NewLine;
                    result += "\t\t private DataAccessHelper<" + tableName + "Viewer> daHelper" + tableName + "Viewer;" + Environment.NewLine;

                    result += "\t\t public " + tableName + "DA()" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t   daHelper = new DataAccessHelper<" + tableName + ">();" + Environment.NewLine;
                    result += "\t\t   daHelper" + tableName + "Viewer = new DataAccessHelper<" + tableName + "Viewer>();" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    result += Environment.NewLine;
                    /// <summary>
                    /// insert/update a customer
                    /// </summary>
                    /// <param name="customerObj">the customer object to insert/update</param>
                    /// <returns>
                    /// true if executed successfully along with the number of record affected.
                    /// false if fail to execute
                    /// </returns>
                    result += "\t\tpublic TransactionCommandAck Save" + tableName + "(" + tableName + " " + tableName + "Obj)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                    result += "\t\t    TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t\t      //specify the procedure name" + Environment.NewLine;
                    result += "\t\t\t       string strProcedureName = null;" + Environment.NewLine;
                    result += "\t\t\t         //if the customer ID not null" + Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\t\t    if(" + tableName + "Obj." + entityType.NameField + " !=null)" + Environment.NewLine;
                        result += "\t\t\t    {" + Environment.NewLine;
                        result += "\t\t\t         strProcedureName = " + "\"" + "sp" + tableName + "_Update" + "\"" + ";" + Environment.NewLine;
                        result += "\t\t\t    }" + Environment.NewLine;
                        result += "\t\t\t    else" + Environment.NewLine;
                        result += "\t\t\t    {" + Environment.NewLine;
                        result += "\t\t\t         strProcedureName = " + "\"" + "sp" + tableName + "_Insert" + "\"" + ";" + Environment.NewLine;
                        result += "\t\t\t    }" + Environment.NewLine;

                        result += "\t\t\t    SqlCommand sqlCmd = new SqlCommand(strProcedureName);" + Environment.NewLine;
                        result += "\t\t\t    sqlCmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine;

                        #region Fields & Props

                        result += "\t\t\t    if(" + tableName + "Obj." + entityType.NameField.ToString() + " !=null)" + Environment.NewLine;
                        result += "\t\t\t    {" + Environment.NewLine;
                        result += "\t\t\t\t    sqlCmd.Parameters.Add(" + "@" + entityType.NameField.ToString() + " , " + "SqlDbType." + entityType.TypeField.ToString() + ").Value = " + tableName + "Obj." + entityType.NameField.ToString() + ";" + Environment.NewLine;
                        result += "\t\t\t    }" + Environment.NewLine;
                        for (int i = 0; i < entities.Count; i++)
                        {
                            //foreach (var entity in entities)
                            //{
                            //if (entity.Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
                            //{
                            if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim().ToUpper() == "STRING")
                            {
                                result += "\t\t\t         sqlCmd.Parameters.Add(" + "@" + entities[i].Field.ToString() + ", " + "SqlDbType." + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ", " + entities[i].Length.ToString() + ").Value = " + tableName + "Obj." + entities[i].Field.ToString() + ";" + Environment.NewLine;
                            }
                            else
                            {
                                result += "\t\t\t         sqlCmd.Parameters.Add(" + "@" + entities[i].Field.ToString() + ", " + "SqlDbType." + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ").Value = " + tableName + "Obj." + entities[i].Field.ToString() + ";" + Environment.NewLine;
                            }
                        }
                    }
                    result += "\t\t\t         SqlParameter outputID = sqlCmd.Parameters.Add(" + "@ID_Output," + " SqlDbType.Int);" + Environment.NewLine;
                    result += "\t\t\t         outputID.Direction = ParameterDirection.Output;" + Environment.NewLine;

                    result += "\t\t\t         transAck = daHelper.ExecuteNonQuery(sqlCmd);" + Environment.NewLine;
                    result += "\t\t\t         if (transAck.returnObject.recordAffected > 0)" + Environment.NewLine;
                    result += "\t\t\t         {" + Environment.NewLine;
                    result += "\t\t\t            transAck.returnValue = outputID.Value.ToString();" + Environment.NewLine;
                    result += "\t\t\t         }" + Environment.NewLine;

                    #endregion Fields & Props

                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return transAck;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// delete the customer by customer ID
                    /// </summary>
                    /// <param name="customerID">the customer ID to delete</param>
                    /// <returns>
                    /// true if executed successfully along with the number of record affected.
                    /// false if fail to execute
                    /// </returns>
                    result += Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\tpublic TransactionCommandAck DeleteByID" + tableName + "(" + entityType.TypeField.ToString() + " " + entityType.NameField.ToString() + ")" + Environment.NewLine;
                        result += "\t\t{" + Environment.NewLine;
                        result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                        result += "\t\t    TransactionCommandAck transAck = new TransactionCommandAck();" + Environment.NewLine;
                        result += "\t\t    try" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t\t      SqlCommand sqlCmd = new SqlCommand(" + "\"" + "sp" + tableName + "_Delete" + "\"" + ");" + Environment.NewLine;
                        result += "\t\t\t      sqlCmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine;
                        result += "\t\t\t      sqlCmd.Parameters.Add(" + "@" + tableName + "ID, " + "SqlDbType.Int).Value = " + tableName + "ID;" + Environment.NewLine;
                        result += "\t\t\t      SqlParameter outputID = sqlCmd.Parameters.Add(" + "@ID_Output," + " SqlDbType.Int,10);" + Environment.NewLine;
                        result += "\t\t\t      outputID.Direction = ParameterDirection.Output;" + Environment.NewLine;
                        result += "\t\t\t      transAck = daHelper.ExecuteNonQuery(sqlCmd);" + Environment.NewLine;
                        result += "\t\t\t     if (transAck.returnObject.recordAffected > 0)" + Environment.NewLine;
                        result += "\t\t\t     {" + Environment.NewLine;
                        result += "\t\t\t         transAck.returnValue = outputID.Value.ToString();" + Environment.NewLine;
                        result += "\t\t\t     }" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                        result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                        result += "\t\t   return transAck;" + Environment.NewLine;
                        result += "\t\t}" + Environment.NewLine;
                    }

                    /// <summary>
                    /// get customer by ID/name/location
                    /// </summary>
                    /// <param name="keyword">keyword to search</param>
                    /// <returns>true if the customer name already exists in the database, otherwise, false</returns>

                    result += Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + " " + "Search" + tableName + "(string keyword, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new   TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t\t      SqlCommand sqlCmd = new SqlCommand(" + "\"" + "spSearch" + "\"" + ");" + Environment.NewLine;
                    result += "\t\t\t      sqlCmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine;
                    result += "\t\t\t      string selectCmd = daHelper.CreateSelectSQLCommand();" + Environment.NewLine;
                    result += "\t\t\t      string tableNameCmd = daHelper.CreateTableNameCommand();" + Environment.NewLine;
                    result += "\t\t\t      string whereCondCmd = CreateWhereConditionCommandFor" + tableName + "(keyword, searchCondition, isANDSearch);" + Environment.NewLine;

                    result += "\t\t\t      sqlCmd.Parameters.Add(" + "@selectSQL," + "SqlDbType.Int).Value = " + "selectCmd;" + Environment.NewLine;
                    result += "\t\t\t      sqlCmd.Parameters.Add(" + "@tableName," + " SqlDbType.Text).Value = tableNameCmd;" + Environment.NewLine;
                    result += "\t\t\t      sqlCmd.Parameters.Add(" + "@whereCondition," + " SqlDbType.Text).Value = whereCondCmd;" + Environment.NewLine;
                    result += "\t\t\t      " + tableName.ToLower() + "TransAckList = GetTransactionsList(sqlCmd);" + Environment.NewLine;

                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// search customer by specified date range
                    /// </summary>
                    /// <param name="startDate">start date</param>
                    /// <param name="endDate">end date</param>
                    /// <returns>true if the customer name already exists in the database, otherwise, false</returns>
                    result += Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + " " + "Search" + tableName + "(DateTime startDate, DateTime endDate, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new   TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t\t       SqlCommand sqlCmd = new SqlCommand(" + "\"" + "spSearch" + "\"" + ");" + Environment.NewLine;
                    result += "\t\t\t       sqlCmd.CommandType = CommandType.StoredProcedure;" + Environment.NewLine;
                    result += "\t\t\t       string selectCmd = daHelper.CreateSelectSQLCommand();" + Environment.NewLine;
                    result += "\t\t\t       string tableNameCmd = daHelper.CreateTableNameCommand();" + Environment.NewLine;
                    result += "\t\t\t       string whereCondCmd = CreateWhereConditionCommandForDateTime" + tableName + "(startDate, endDate, isANDSearch);" + Environment.NewLine;

                    result += "\t\t\t       sqlCmd.Parameters.Add(" + "@selectSQL," + "SqlDbType.Int).Value = " + "selectCmd;" + Environment.NewLine;
                    result += "\t\t\t       sqlCmd.Parameters.Add(" + "@tableName," + " SqlDbType.Text).Value = tableNameCmd;" + Environment.NewLine;
                    result += "\t\t\t       sqlCmd.Parameters.Add(" + "@whereCondition," + " SqlDbType.Text).Value = whereCondCmd;" + Environment.NewLine;
                    result += "\t\t\t       " + tableName.ToLower() + "TransAckList = GetTransactionsList(sqlCmd);" + Environment.NewLine;

                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return" + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// get the transaction list
                    /// </summary>
                    /// <param name="cusTranList">the transaction list</param>
                    /// <returns>the result list</returns>
                    result += Environment.NewLine;

                    result += "\t\tprivate TransactionGet" + tableName + " " + "GetTransactionsList(SqlCommand sqlCmd)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + " " + tableName.ToLower() + "TransAckList = new   TransactionGet" + tableName + "();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        TransactionAckResultList<" + tableName + "> " + tableName.ToLower() + "TranList = daHelper.populateTransList(sqlCmd);" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList.isSuccess = " + tableName.ToLower() + "TranList.isSuccess;" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList.message = " + tableName.ToLower() + "TranList.message;" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList.numOfEffect = " + tableName.ToLower() + "TranList.numOfEffect;" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList.returnObjects = " + tableName.ToLower() + "TranList.returnObjects;" + Environment.NewLine;
                    result += "\t\t        " + tableName.ToLower() + "TransAckList.returnValue = " + tableName.ToLower() + "TranList.returnValue;" + Environment.NewLine;
                    result += "\t\t        return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "TransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// get the transaction list
                    /// </summary>
                    /// <param name="cusTranList">the transaction list</param>
                    /// <returns>the result list</returns>
                    result += Environment.NewLine;

                    result += "\t\tprivate TransactionGet" + tableName + "Viewers " + "GetTransactionsList" + tableName + "Viewers" + "(SqlCommand sqlCmd)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t //set the object value if the property is null" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewersTransAckList = new   TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t         TransactionAckResultList<" + tableName + "Viewers" + "> " + tableName.ToLower() + "TranList = daHelper" + tableName + "Viewer.populateTransList(sqlCmd);" + Environment.NewLine;
                    result += "\t\t         " + tableName.ToLower() + "ViewersTransAckList.isSuccess = " + tableName.ToLower() + "TranList.isSuccess;" + Environment.NewLine;
                    result += "\t\t         " + tableName.ToLower() + "ViewersTransAckList.message = " + tableName.ToLower() + "TranList.message;" + Environment.NewLine;
                    result += "\t\t         " + tableName.ToLower() + "ViewersTransAckList.numOfEffect = " + tableName.ToLower() + "TranList.numOfEffect;" + Environment.NewLine;
                    result += "\t\t         " + tableName.ToLower() + "ViewersTransAckList.returnObjects =" + tableName.ToLower() + "TranList.returnObjects;" + Environment.NewLine;
                    result += "\t\t         " + tableName.ToLower() + "ViewersTransAckList.returnValue =" + tableName.ToLower() + "TranList.returnValue;" + Environment.NewLine;
                    result += "\t\t         return " + tableName.ToLower() + "ViewersTransAckList;" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewersTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// search customer by input conditions
                    /// </summary>
                    /// <param name="objCusFilter">the object containing the search condition</param>
                    /// <param name="lstReturnFields">the list of return value</param>
                    /// <param name="objSearchCondition">the search condition (Any, StartWith, EndWith, Exact)</param>
                    /// <param name="isANDSearch">use AND search or not</param>
                    /// <returns>the list of searched results</returns>

                    result += Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + "Viewers " + " Search" + tableName + "(" + tableName + "Filter" + "obj" + tableName + "Filter" + "," + "List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewerTransAckList = new " + "TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t          List<String> lstReturnFieldString = GetReturnFieldString(lstReturnFields);" + Environment.NewLine;
                    result += "\t\t          " + tableName + "Search " + "obj" + tableName + "Search = new " + tableName + "Search();" + Environment.NewLine;
                    result += "\t\t          string strSelectStatement = obj" + tableName + "Search.VerifySelectStatement(lstReturnFieldString);" + Environment.NewLine;
                    result += "\t\t          string strJoinStatement = obj" + tableName + "Search.VerifyJoinStatement(lstReturnFieldString);" + Environment.NewLine;
                    result += "\t\t          string strWhereStatement = obj" + tableName + "Search.VerifyWhereStatement(obj" + tableName + "CusFilter, objSearchCondition, isANDSearch);" + Environment.NewLine;

                    result += "\t\t          SqlCommand sqlCmd = new SqlCommand();" + Environment.NewLine;
                    result += "\t\t          sqlCmd.CommandText = strSelectStatement + strJoinStatement + strWhereStatement;" + Environment.NewLine;
                    result += "\t\t          sqlCmd = daHelper" + tableName + "Viewer.SetParameterForCommand(sqlCmd, obj" + tableName + "Filter);" + Environment.NewLine;
                    result += "\t\t          return " + tableName.ToLower() + "ViewersTransAckList;" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewersTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// get list of returned field string
                    /// </summary>
                    /// <param name="lstReturnedField">list of returned fields</param>
                    /// <returns>list of returned field string</returns>
                    ///
                    result += Environment.NewLine;

                    result += "\t\tpublic List<String> GetReturnFieldString(List<" + tableName + "ReturnField> lstReturnedField)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    List<String> lstReturnFieldString = new List<string>();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        foreach (CustomerReturnField returnField in lstReturnedField)" + Environment.NewLine;
                    result += "\t\t         {" + Environment.NewLine;
                    result += "\t\t            lstReturnFieldString.Add(daHelper" + tableName + "Viewer.GetStringValue(returnField));" + Environment.NewLine;
                    result += "\t\t         }" + Environment.NewLine;
                    result += "\t\t         return lstReturnFieldString;" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;

                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// search the customer by the input condition
                    /// </summary>
                    /// <param name="keyword">the key word to search</param>
                    /// <param name="searchCondition">the search condition (any, startwith, endwith, exact)</param>
                    /// <param name="startDate">start date</param>
                    /// <param name="endDate">end date</param>
                    /// <param name="lstReturnFields">the returned fields</param>
                    /// <param name="isANDSearch">use AND search or not</param>
                    /// <returns>the search results</returns>
                    result += "" + Environment.NewLine;

                    result += "\t\tpublic TransactionGet" + tableName + "Viewers " + " Search" + tableName + "(" + "string keyword, DateTime startDate, DateTime endDate" + "," + "List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t    TransactionGet" + tableName + "Viewers " + tableName.ToLower() + "ViewerTransAckList = new " + "TransactionGet" + tableName + "Viewers();" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t          List<String> lstReturnFieldString = GetReturnFieldString(lstReturnFields);" + Environment.NewLine;
                    result += "\t\t          " + tableName + "Search " + "obj" + tableName + "Search = new " + tableName + "Search();" + Environment.NewLine;
                    result += "\t\t          string strSelectStatement = obj" + tableName + "Search.VerifySelectStatement(lstReturnFieldString);" + Environment.NewLine;
                    result += "\t\t          string strJoinStatement = obj" + tableName + "Search.VerifyJoinStatement(lstReturnFieldString);" + Environment.NewLine;
                    result += "\t\t          string strWhereStatement = obj" + tableName + "Search.VerifyWhereStatement(obj" + tableName + "CusFilter, objSearchCondition, isANDSearch);" + Environment.NewLine;
                    result += "\t\t          SqlCommand sqlCmd = new SqlCommand();" + Environment.NewLine;
                    result += "\t\t          sqlCmd.CommandText = strSelectStatement + strJoinStatement + strWhereStatement;" + Environment.NewLine;
                    result += "\t\t          sqlCmd = daHelper" + tableName + "Viewer.SetParameterForCommand(sqlCmd, keyword, startDate, endDate);" + Environment.NewLine;
                    result += "\t\t          " + tableName.ToLower() + "ViewerTransAckList = GetTransactionsList" + tableName + "Viewer(sqlCmd);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t   }" + Environment.NewLine;
                    result += "\t\t   return " + tableName.ToLower() + "ViewerTransAckList;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// create the where condition command
                    /// </summary>
                    /// <param name="startDate">the start date</param>
                    /// <param name="endDate">the end date</param>
                    /// <returns>the where condition command</returns>
                    result += Environment.NewLine;

                    result += "\t\tprivate string CreateWhereConditionCommandFor" + tableName + "(string keyword, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t " + Environment.NewLine;
                    result += "\t\t    string strWhereConditionCommand = null;" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t       string searchCond = null;" + Environment.NewLine;
                    result += "\t\t      if (isANDSearch)" + Environment.NewLine;
                    result += "\t\t      {" + Environment.NewLine;
                    result += "\t\t         searchCond = " + "\"" + "AND" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t      }" + Environment.NewLine;
                    result += "\t\t     else" + Environment.NewLine;
                    result += "\t\t     {" + Environment.NewLine;
                    result += "\t\t        searchCond = " + "\"" + "OR" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t     }" + Environment.NewLine;
                    result += "\t\t     int outNum;" + Environment.NewLine;
                    result += "\t\t     if (Int32.TryParse(keyword, out outNum))" + Environment.NewLine;
                    result += "\t\t     {" + Environment.NewLine;

                    result += "\t\t       strWhereConditionCommand += " + "\"" + "(CustomerID=" + "\"" + " + keyword + " + "\"" + ")" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t     }" + Environment.NewLine;

                    result += "\t\t     else" + Environment.NewLine;
                    result += "\t\t     {" + Environment.NewLine;
                    result += "\t\t         switch (searchCondition)" + Environment.NewLine;
                    result += "\t\t         {" + Environment.NewLine;
                    result += "\t\t             case SearchCondition.Any:" + Environment.NewLine;
                    result += "\t\t                keyword = " + "\"" + "like '%" + "\"" + " + keyword + " + "\"" + "%'" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t                 break;" + Environment.NewLine;
                    result += "\t\t             case SearchCondition.StartWith:" + Environment.NewLine;
                    result += "\t\t                keyword = " + "\"" + "like '" + "\"" + " + keyword + " + "\"" + "%'" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t                 break;" + Environment.NewLine;
                    result += "\t\t             case SearchCondition.EndWith:" + Environment.NewLine;
                    result += "\t\t                keyword = " + "\"" + "like '%" + "\"" + " + keyword + " + "\"" + "'" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t                  break;" + Environment.NewLine;
                    result += "\t\t             case SearchCondition.Exact:" + Environment.NewLine;
                    result += "\t\t                 keyword = " + "\"" + "='" + "\"" + " + keyword + " + "\"" + "'" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t                  break;" + Environment.NewLine;
                    result += "\t\t       }" + Environment.NewLine;

                    result += "\t\t       strWhereConditionCommand += " + "\"" + "(CustomerName " + "\"" + " + keyword + " + "\"" + ") " + "searchCond + " + "(Address " + "+ keyword + " + ") " + "+searchCond + " + " (City " + "+ keyword + " + ")+ searchCond + " + " (State " + "+ keyword + " + "\"" + ")" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t      }" + Environment.NewLine;

                    result += "\t\t      strWhereConditionCommand = " + "\"" + "where" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t      strWhereConditionCommand += " + "\"" + "((('" + "\"" + "+ startDate.ToShortDateString() +" + "\"" + "' < DateCreated) AND (DateCreated < '" + "\"" + " + endDate.ToShortDateString() + " + "\"" + "')) OR (('" + "\"" + " + startDate.ToShortDateString() +" + "\"" + "' < LastUpdate) AND (LastUpdate < '" + "\"" + " + endDate.ToShortDateString() + " + "\"" + "')))" + "\"" + ";" + Environment.NewLine;

                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return strWhereConditionCommand;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// create the where condition command
                    /// </summary>
                    /// <param name="keyword">the keyword to search</param>
                    /// <returns>the where condition command</returns>
                    result += Environment.NewLine;

                    result += "\t\tprivate string CreateWhereConditionCommandForDateTime(DateTime startDate, DateTime endDate, bool isANDSearch)" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;
                    result += "\t\t " + Environment.NewLine;
                    result += "\t\t    string strWhereConditionCommand = null;" + Environment.NewLine;
                    result += "\t\t    try" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t       strWhereConditionCommand = " + "\"" + "where" + "\"" + ";" + Environment.NewLine;
                    result += "\t\t       strWhereConditionCommand += " + "\"" + "((('" + "\"" + "+ startDate.ToShortDateString() +" + "\"" + "' < DateCreated) AND (DateCreated < '" + "\"" + " + endDate.ToShortDateString() + " + "\"" + "')) OR (('" + "\"" + " + startDate.ToShortDateString() +" + "\"" + "' < LastUpdate) AND (LastUpdate < '" + "\"" + " + endDate.ToShortDateString() + " + "\"" + "')))" + "\"" + ";" + Environment.NewLine;

                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t    catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;
                    result += "\t\t   return strWhereConditionCommand;" + Environment.NewLine;
                    result += "\t\t}" + Environment.NewLine;

                    /// <summary>
                    /// specify the search condition for the search functionality
                    /// </summary>
                    result += Environment.NewLine;

                    result += "\t\tprivate class " + tableName + "Search : ISearchFunction" + Environment.NewLine;
                    result += "\t\t{" + Environment.NewLine;

                    /// <summary>
                    /// create the join statement
                    /// </summary>
                    /// <param name="lstReturnFields">the returned filed specified by the user</param>
                    /// <returns>the join statement</returns>
                    result += Environment.NewLine;

                    result += "\t\t    public string VerifyJoinStatement(List<string> lstReturnFields)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;
                    result += "\t\t        string strJoinStatement = " + "\"" + "from + " + tableName + "\"" + ";" + Environment.NewLine;
                    result += "\t\t        DataAccessHelper<" + tableName + "Viewer> daHelper" + tableName + "Viewer = new DataAccessHelper<" + tableName + "Viewer>();" + Environment.NewLine;

                    result += "\t\t       try" + Environment.NewLine;
                    result += "\t\t       {" + Environment.NewLine;
                    result += "\t\t            string strClassName = null;" + Environment.NewLine;

                    //loop through the returned fieds list
                    result += "\t\t             foreach (string strReturnedField in lstReturnFields)" + Environment.NewLine;
                    result += "\t\t             {" + Environment.NewLine;
                    result += "\t\t                 strClassName = daHelper" + tableName + "Viewer.GetStringBeforeDot(strReturnedField);" + Environment.NewLine;
                    result += "\t\t                 switch (strClassName)" + Environment.NewLine;
                    result += "\t\t                     {" + Environment.NewLine;

                    result += "\t\t                     }" + Environment.NewLine;
                    result += "\t\t             }" + Environment.NewLine;

                    result += "\t\t            return strJoinStatement;" + Environment.NewLine;
                    result += "\t\t        }" + Environment.NewLine;
                    result += "\t\t        catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t        {" + Environment.NewLine;
                    result += "\t\t            throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t        }" + Environment.NewLine;

                    result += "\t\t    }" + Environment.NewLine;

                    /// <summary>
                    /// create the where statement
                    /// </summary>
                    /// <param name="objFilterRuntimeObject">the runtime object</param>
                    /// <param name="searchCondition">the search condition</param>
                    /// <returns>the where statement</returns>
                    result += Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\t    public string VerifyWhereStatement(object objFilterRuntimeObject, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t         string strWhereStatement = null;" + Environment.NewLine;
                        result += "\t\t         DataAccessHelper<" + tableName + "Viewer> daHelper" + tableName + "Viewer = new DataAccessHelper<" + tableName + "Viewer>();" + Environment.NewLine;

                        result += "\t\t      try" + Environment.NewLine;
                        result += "\t\t      {" + Environment.NewLine;
                        result += "\t\t         strWhereStatement = daHelperCustomerViewer.CreateSearchCondition(searchCondition, objFilterRuntimeObject, isANDSearch);" + Environment.NewLine;
                        result += "\t\t         if (strWhereStatement.Contains(" + "\"" + "'@" + entityType.NameField.ToString() + "'" + "\"" + "))" + Environment.NewLine;
                        result += "\t\t         {" + Environment.NewLine;
                        result += "\t\t             strWhereStatement = strWhereStatement.Replace(" + "\"" + "'@" + entityType.NameField.ToString() + "'" + "\"" + ", " + "\"" + "@" + entityType.NameField.ToString() + "\"" + ");" + Environment.NewLine;
                        result += "\t\t         }" + Environment.NewLine;

                        result += "\t\t         return strWhereStatement;" + Environment.NewLine;
                        result += "\t\t      }" + Environment.NewLine;
                        result += "\t\t      catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t      {" + Environment.NewLine;
                        result += "\t\t         throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t      }" + Environment.NewLine;
                        result += "\t\t    }" + Environment.NewLine;
                    }
                    /// <summary>
                    /// create the select statement
                    /// </summary>
                    /// <param name="lstReturnFields">the returned field</param>
                    /// <returns>the select statement</returns>
                    result += Environment.NewLine;

                    result += "\t\t    public string VerifySelectStatement(List<string> lstReturnFields)" + Environment.NewLine;
                    result += "\t\t    {" + Environment.NewLine;

                    result += "\t\t         string strSelectStatement = null;" + Environment.NewLine;
                    result += "\t\t         DataAccessHelper<" + tableName + "Viewer> daHelper" + tableName + "Viewer = new DataAccessHelper<" + tableName + "Viewer>();" + Environment.NewLine;

                    result += "\t\t         try" + Environment.NewLine;
                    result += "\t\t         {" + Environment.NewLine;
                    result += "\t\t             strSelectStatement = daHelper" + tableName + "Viewer.CreateSelectCommand(lstReturnFields);" + Environment.NewLine;
                    result += "\t\t             return strSelectStatement;" + Environment.NewLine;
                    result += "\t\t         }" + Environment.NewLine;
                    result += "\t\t         catch (Exception ex)" + Environment.NewLine;
                    result += "\t\t         {" + Environment.NewLine;
                    result += "\t\t             throw new Exception(ex.Message);" + Environment.NewLine;
                    result += "\t\t         }" + Environment.NewLine;
                    result += "\t\t    }" + Environment.NewLine;

                    /// <summary>
                    /// create where statement when search by keyword
                    /// </summary>
                    /// <param name="strKeyword">the keyword to search</param>
                    /// <param name="dtStartDate">start date</param>
                    /// <param name="dtEndDate">end date</param>
                    /// <param name="searchCondition">the search condition</param>
                    /// <returns>the where statement</returns>
                    result += Environment.NewLine;
                    foreach (var entityType in entitiesDataType)
                    {
                        result += "\t\t    public string VerifyWhereStatementSearchByKeyword(string strKeyword, DateTime dtStartDate, DateTime dtEndDate, SearchCondition searchCondition, bool isANDSearch)" + Environment.NewLine;
                        result += "\t\t    {" + Environment.NewLine;
                        result += "\t\t         string strWhereStatement = " + "\"" + "where (" + "\"" + ";" + Environment.NewLine;
                        result += "\t\t         DataAccessHelper<" + tableName + "Viewer> daHelper" + tableName + "Viewer = new DataAccessHelper<" + tableName + "Viewer>();" + Environment.NewLine;

                        result += "\t\t         try" + Environment.NewLine;
                        result += "\t\t         {" + Environment.NewLine;
                        result += "\t\t             string strSearchOption = daHelper" + tableName + "Viewer.GetSearchOption(searchCondition);" + Environment.NewLine;
                        result += "\t\t             string strTimeField = " + "\"" + "LastUpdate" + "\"" + ";" + Environment.NewLine;
                        result += "\t\t             string strTableName = " + "\"" + tableName + "\"" + ";" + Environment.NewLine;

                        result += "\t\t             int outNum;" + Environment.NewLine;
                        result += "\t\t             if (Int32.TryParse(strKeyword, out outNum))" + Environment.NewLine;
                        result += "\t\t             {" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement += " + "\"" + tableName + "." + entityType.NameField.ToString() + "=@keyword OR " + "\"" + ";" + Environment.NewLine;
                        result += "\t\t             }" + Environment.NewLine;

                        //specify the field list
                        result += "\t\t             List<String> lstKeywordFields = new List<string>();" + Environment.NewLine;
                        result += "\t\t             lstKeywordFields.Add(" + "\"" + "Customer.Address" + "\"" + ");" + Environment.NewLine;

                        result += "\t\t             if (strKeyword != null)" + Environment.NewLine;
                        result += "\t\t             {" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement += daHelperCustomerViewer.GetWhereConditionByKeyword(lstKeywordFields, searchCondition, isANDSearch);" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement += " + "\"" + " AND " + "\"" + ";" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement += daHelperCustomerViewer.GetWhereConditionByDatetime(dtStartDate, dtEndDate, strTimeField, strTableName);" + Environment.NewLine;
                        result += "\t\t             }" + Environment.NewLine;
                        result += "\t\t             else if (strKeyword == null)" + Environment.NewLine;
                        result += "\t\t             {" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement += daHelperCustomerViewer.GetWhereConditionByDatetime(dtStartDate, dtEndDate, strTimeField, strTableName);" + Environment.NewLine;
                        result += "\t\t             }" + Environment.NewLine;
                        result += "\t\t             else if (strKeyword == null && dtStartDate == null && dtEndDate == null)" + Environment.NewLine;
                        result += "\t\t             {" + Environment.NewLine;
                        result += "\t\t                 strWhereStatement = " + "\"" + "\"" + ";" + Environment.NewLine;
                        result += "\t\t             }" + Environment.NewLine;

                        result += "\t\t             return strWhereStatement;" + Environment.NewLine;
                        result += "\t\t         }" + Environment.NewLine;
                        result += "\t\t         catch (Exception ex)" + Environment.NewLine;
                        result += "\t\t         {" + Environment.NewLine;
                        result += "\t\t                throw new Exception(ex.Message);" + Environment.NewLine;
                        result += "\t\t      }" + Environment.NewLine;

                        result += "\t\t   }" + Environment.NewLine;
                    }

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