using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassSeviceAPI
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        #region Code Generator Core

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

                    comment.GetDefaultCreate();

                    #endregion Create User, Title

                    #region Set DLL

                    //Create  DLL
                    comment.GetDLLAPICreate();

                    #endregion Set DLL

                    #region Namespace

                    result += "namespace " + namespaceIn + ".DBMS.API" + Environment.NewLine;
                    result += "{" + Environment.NewLine;

                    #region Table Shema

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Schema" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Schema" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    for (int i = 0; i < entities.Count; i++)
                    {
                        result += "\t \tpublic" + " static " + "String" + " " + entities[i].Field.ToString() + " = " + "\"" + entities[i].Field.ToString() + "\"" + ";" + Environment.NewLine;
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Table Shema

                    #region Table API

                    result += "\t[Serializable]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;
                    result += "\t\t" + "public " + tableName + "() { }" + Environment.NewLine;
                    for (int i = 0; i < entities.Count; i++)
                    {
                        //foreach (var entity in entities)
                        //{
                        //result += "\t \tprivate " + staticTxt + GetType(entity.Type.ToString()) + " " + entity.Field.ToString().ToLower() + ";" + Environment.NewLine;
                        //result += "\t\t\t" + "public " + tableName + " () { }" + Environment.NewLine;
                        if (entities[i].Type.ToString() == "bit")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Boolean>" + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "bigint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Int64>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "binary" || entities[i].Type.ToString() == "image" || entities[i].Type.ToString() == "rowversion" || entities[i].Type.ToString() == "timestamp" || entities[i].Type.ToString() == "varbinary")
                        {
                            //result += "\t\t//Decimal here" + Environment.NewLine;
                            result += "\t \tpublic" + staticTxt + " virtual " + "Byte[]" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "date" || entities[i].Type.ToString() == "datetime" || entities[i].Type.ToString() == "smalldatetime")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<DateTime>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "decimal" || entities[i].Type.ToString() == "money" || entities[i].Type.ToString() == "numeric" || entities[i].Type.ToString() == "smallmoney")
                        {
                            //result += "\t\t//Decimal here" + Environment.NewLine;
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Decimal>" + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "float")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Double>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "int")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Int32>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "smallint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Int>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "real")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Single>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "time")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<TimeSpan>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "tinyint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Byte>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "uniqueidentifier")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Guid>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "xml")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<Xml>" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() != "String")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Nullable<" + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ">" + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " " +
                                      entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }

                        //}
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Table API

                    #region Table API Moblie

                    result += "\t[Serializable]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Mobile" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Mobile" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;
                    result += "\t\t" + "public " + tableName + "Mobile" + "() { }" + Environment.NewLine;
                    for (int i = 0; i < entities.Count; i++)
                    {
                        //foreach (var entity in entities)
                        //{
                        //result += "\t \tprivate " + staticTxt + GetType(entity.Type.ToString()) + " " + entity.Field.ToString().ToLower() + ";" + Environment.NewLine;
                        //result += "\t\t\t" + "public " + tableName + " () { }" + Environment.NewLine;
                        if (entities[i].Type.ToString() == "bit")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Boolean" + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "bigint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Int64" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "binary" || entities[i].Type.ToString() == "image" || entities[i].Type.ToString() == "rowversion" || entities[i].Type.ToString() == "timestamp" || entities[i].Type.ToString() == "varbinary")
                        {
                            //result += "\t\t//Decimal here" + Environment.NewLine;
                            result += "\t \tpublic" + staticTxt + " virtual " + "Byte[]" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "date" || entities[i].Type.ToString() == "datetime" || entities[i].Type.ToString() == "smalldatetime")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "DateTime" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "decimal" || entities[i].Type.ToString() == "money" || entities[i].Type.ToString() == "numeric" || entities[i].Type.ToString() == "smallmoney")
                        {
                            //result += "\t\t//Decimal here" + Environment.NewLine;
                            result += "\t \tpublic" + staticTxt + " virtual " + "Decimal" + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "float")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Double" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "int")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Int32" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "smallint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Int" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "real")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Single" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "time")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "TimeSpan" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "tinyint")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Byte" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "uniqueidentifier")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Guid" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "xml")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + "Xml" + " " +
                                    entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() != "String")
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " " +
                                     entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t \tpublic" + staticTxt + " virtual " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + " " +
                                      entities[i].Field.ToString() + " { get ; set;}" + Environment.NewLine;
                        }

                        //}
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Table API Moblie

                    #region Entity

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Entity : " + tableName + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Entity : " + tableName + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    for (int i = 0; i < entities.Count; i++)
                    {
                        //foreach (var entity in entities)
                        //{
                        result += "\t \tpublic " + "object " + staticTxt + "set" +
                                 entities[i].Field.ToString() + Environment.NewLine;
                        result += "\t \t{" + Environment.NewLine;

                        if (entities[i].Type.ToString() == "bit")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Boolean.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "bigint")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Int64.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "binary" || entities[i].Type.ToString() == "image" || entities[i].Type.ToString() == "rowversion" || entities[i].Type.ToString() == "timestamp" || entities[i].Type.ToString() == "varbinary")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "value as Byte[];" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "date" || entities[i].Type.ToString() == "datetime" || entities[i].Type.ToString() == "smalldatetime")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "DateTime.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "decimal" || entities[i].Type.ToString() == "money" || entities[i].Type.ToString() == "numeric" || entities[i].Type.ToString() == "smallmoney")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Decimal.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "float")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Double.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "int")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Int32.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "smallint")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Int64.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "real")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Int.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "time")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "TimeSpan.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "tinyint")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Byte.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "uniqueidentifier")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Guid.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (entities[i].Type.ToString() == "xml")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "Xml.Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        else if (ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "").Trim() == "String")
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + "value.ToString();" + "}" + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t\t\tset" + " { " + entities[i].Field.ToString() + " = " + ClassHelperMethod.GetType(entities[i].Type.ToString()).Replace("System.", "") + ".Parse(value.ToString());" + "}" + Environment.NewLine;
                        }
                        result += "\t \t}" + Environment.NewLine;
                        // }
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Entity

                    #region Table View

                    result += "\t/// <summary>" + Environment.NewLine;
                    result += "\t/// create a class to store additional data related to customer" + Environment.NewLine;
                    result += "\t/// </summary>" + Environment.NewLine;
                    result += "\t[Serializable]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Viewer : " + tableName + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Viewer : " + tableName + Environment.NewLine;
                    }
                    result += "\t{" + Environment.NewLine;
                    result += "\t}" + Environment.NewLine;

                    #endregion Table View

                    #region Table Mobile View

                    result += "\t/// <summary>" + Environment.NewLine;
                    result += "\t/// create a class to store additional data related " + tableName + "MobileViewer : " + tableName + "Mobile" + Environment.NewLine;
                    result += "\t/// </summary>" + Environment.NewLine;
                    result += "\t[Serializable]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "MobileViewer : " + tableName + "Mobile" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "MobileViewer : " + tableName + "Mobile" + Environment.NewLine;
                    }
                    result += "\t{" + Environment.NewLine;
                    result += "\t}" + Environment.NewLine;

                    #endregion Table Mobile View

                    #region Class ReturnField

                    result += "\t/// <summary>" + Environment.NewLine;
                    result += "\t/// customer return fields" + Environment.NewLine;
                    result += "\t/// </summary>" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\tpublic" + " enum" + " " + tableName + "ReturnField" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\tpublic" + " enum " + tableName + "ReturnField" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    for (int i = 0; i < entities.Count; i++)
                    {
                        //foreach (var entity in entities)
                        //{
                        if (i < entities.Count - 1)
                        {
                            result += "\t\t" + "[StringValue(" + "\"" + tableName + "." + entities[i].Field.ToString() + "\"" + ")]" + Environment.NewLine;

                            result += "\t \t" + entities[i].Field.ToString() + "," + Environment.NewLine;
                        }
                        else
                        {
                            result += "\t\t" + "[StringValue(" + "\"" + tableName + "." + entities[i].Field.ToString() + "\"" + ")]" + Environment.NewLine;

                            result += "\t \t" + entities[i].Field.ToString() + Environment.NewLine;
                        }

                        //}
                    }

                    result += "\t}" + Environment.NewLine;

                    #endregion Class ReturnField

                    #region Table Filte

                    result += "\t[Serializable]" + Environment.NewLine;
                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + tableName + "Filter : " + tableName + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + tableName + "Filter : " + tableName + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion Table Filte

                    #region TransactionAck

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + "TransactionGet" + tableName + " : TransactionAck<" + tableName + ">" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + "TransactionGet" + tableName + " : TransactionAck<" + tableName + ">" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion TransactionAck

                    #region TransactionAckResultList Table

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + "TransactionGet" + tableName + "s" + " : TransactionAckResultList<" + tableName + ">" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + "TransactionGet" + tableName + "s" + " : TransactionAckResultList<" + tableName + ">" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion TransactionAckResultList Table

                    #region TransactionAckResultList Field String

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + "TransactionGet" + tableName + "Fields" + " : TransactionAckResultList<String>" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + "TransactionGet" + tableName + "Fields" + " : TransactionAckResultList<String>" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion TransactionAckResultList Field String

                    #region TransactionAck TableView

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + classModifiers + " class " + "TransactionGet" + tableName + "Viewer : TransactionAck<" + tableName + "Viewer>" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + " class " + "TransactionGet" + tableName + "Viewer : TransactionAck<" + tableName + "Viewer>" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion TransactionAck TableView

                    #region TransactionAckResultList Table View

                    if (classModifiers.Length > 0)
                    {
                        result += "\tpublic" + " class TransactionGet" + tableName + "Viewers : TransactionAckResultList<" + tableName + "Viewer>" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + "class TransactionGet" + tableName + "Viewers : TransactionAckResultList<" + tableName + "Viewer>" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    result += "\t}" + Environment.NewLine;

                    #endregion TransactionAckResultList Table View

                    #region Interface Table API

                    if (classModifiers.Length > 0)
                    {
                        result += "\t" + "public interface I" + tableName + "API" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t" + "public interface I" + tableName + "API" + Environment.NewLine;
                    }

                    result += "\t{" + Environment.NewLine;

                    if (entitiesDataType.Count > 1)
                    {
                        result += "\t\tTransactionCommandAck Save" + tableName + " (" + tableName + " " + tableName + "Obj , bool isUpdate );" + Environment.NewLine;
                    }
                    else
                    {
                        result += "\t\tList<TransactionCommandAck> Save" + tableName + " (" + tableName + " " + tableName + "Obj, String strTableName, String strUserId, Domain.Core.Validation.IValidateFunction iValFunc);" + Environment.NewLine;
                    }

                    if (entitiesDataType.Count > 1)
                    {
                        result += "\t\tTransactionCommandAck DeleteByID" + tableName + " (" + ClassHelperMethod.GetType(entitiesDataType[0].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[0].NameField + "," + ClassHelperMethod.GetType(entitiesDataType[1].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[1].NameField + ");" + Environment.NewLine;
                    }
                    else
                    {
                        foreach (var entityType in entitiesDataType)
                        {
                            if (entityType.NameField.ToString() == null | entityType.NameField.ToString() == "")
                            {
                            }
                            else
                            {
                                if (entityType.TypeField.ToString().ToUpper() == "CHAR" | entityType.TypeField.ToString().ToUpper() == "NCHAR" | entityType.TypeField.ToString().ToUpper() == "VARCHAR" | entityType.TypeField.ToString().ToUpper() == "NVARCHAR")
                                {
                                    result += "\t\tTransactionCommandAck DeleteByID" + tableName + " (string " + entityType.NameField + ");" + Environment.NewLine;
                                }
                                else
                                {
                                    result += "\t\tTransactionCommandAck DeleteByID" + tableName + " (" + entityType.TypeField.ToString() + " " + entityType.NameField + ");" + Environment.NewLine;
                                }
                            }
                        }
                    }

                    //result += "\t\tTransactionCommandAck DeleteByID" + tableName + " (" + " int" + tableName + "ID );" + Environment.NewLine;
                    //result += "\t\tTransactionGet" + tableName + "s" + " Search" + tableName + "(string keyword, SearchCondition searchCondition, bool isANDSearch);" + Environment.NewLine;
                    //result += "\t\tTransactionGet" + tableName + "s" + " Search" + tableName + "(DateTime startDate, DateTime endDate, bool isANDSearch);" + Environment.NewLine;
                    //result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(string keyword, DateTime startDate, DateTime endDate, List<" + tableName + "ReturnField> lstReturnFields, SearchCondition searchCondition, bool isANDSearch);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(string keyword, DateTime startDate, DateTime endDate, List<" + tableName + "ReturnField> lstReturnFields, SearchCondition searchCondition, bool isANDSearch, String strCustomerID);" + Environment.NewLine;
                    // result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(" + tableName + "Filter" + " obj" + tableName + "Filter" + ", List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(" + tableName + "Filter" + " obj" + tableName + "Filter" + ", List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch, String strCustomerID);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "( List<" + tableName + "ReturnField> lstReturnFields, String strWhereStatement, List<SearchSchema> lstSearchSchemas);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(string keyword, DateTime startDate, DateTime endDate, List<" + tableName + "ReturnField> lstReturnFields, SearchCondition searchCondition, bool isANDSearch, String strCustomerID, bool isDistinct);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(" + tableName + "Filter obj" + tableName + "Filter, List<" + tableName + "ReturnField> lstReturnFields, SearchCondition objSearchCondition, bool isANDSearch, String strCustomerID, bool isDistinct);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(List<" + tableName + "ReturnField> lstReturnFields, String strWhereStatement, List<SearchSchema> lstSearchSchemas, bool isDistinct);" + Environment.NewLine;
                    result += "\t\tTransactionGet" + tableName + "Viewers Search" + tableName + "(String strSelectStatement, String strJoinStatement, String strWhereStatement, List<SqlParameter> lstParameters);" + Environment.NewLine;
                    result += "" + Environment.NewLine;
                    result += "\t\t//mobile" + Environment.NewLine;
                    result += "\t\tint Mobile_Insert" + tableName + "(" + tableName + "Mobile obj" + tableName + ");" + Environment.NewLine;
                    result += "\t\tint Mobile_Update" + tableName + "(" + tableName + "Mobile obj" + tableName + ");" + Environment.NewLine;
                    if (entitiesDataType.Count > 1)
                    {
                        result += "\t\tint Mobile_Delete" + tableName + "(" + ClassHelperMethod.GetType(entitiesDataType[0].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[0].NameField + "," + ClassHelperMethod.GetType(entitiesDataType[1].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[1].NameField + ");" + Environment.NewLine;
                    }
                    else
                    {
                        foreach (var entityType in entitiesDataType)
                        {
                            if (entityType.NameField.ToString() == null | entityType.NameField.ToString() == "")
                            {
                            }
                            else
                            {
                                result += "\t\tint Mobile_Delete" + tableName + "(" + entityType.TypeField.ToString() + " " + entityType.NameField + ");" + Environment.NewLine;
                            }
                        }
                    }
                    if (entitiesDataType.Count > 1)
                    {
                        result += "\t\tint Mobile_Delete" + tableName + "ByListOf" + ClassHelperMethod.GetType(entitiesDataType[0].TypeField.ToString()).Replace("System.", "") + "(List<int> lst" + ClassHelperMethod.GetType(entitiesDataType[0].TypeField.ToString()).Replace("System.", "") + ");" + Environment.NewLine;
                        //result += "\t\tint Mobile_Delete" + tableName + "(" + ClassHelperMethod.GetType(entitiesDataType[0].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[0].NameField + "," + ClassHelperMethod.GetType(entitiesDataType[1].TypeField.ToString()).Replace("System.", "") + " " + entitiesDataType[1].NameField + ");" + Environment.NewLine;
                    }
                    else
                    {
                        foreach (var entityType in entitiesDataType)
                        {
                            if (entityType.NameField.ToString() == null | entityType.NameField.ToString() == "")
                            {
                            }
                            else
                            {
                                result += "\t\tint Mobile_Delete" + tableName + "ByListOf" + entityType.NameField + "(List<int> lst" + entityType.NameField + ");" + Environment.NewLine;
                            }
                        }
                    }
                    result += "\t\tList<" + tableName + "Mobile> Mobile_Get" + tableName + "ByCustomerID(String strCustomerID);" + Environment.NewLine;
                    result += "\t}" + Environment.NewLine;

                    #endregion Interface Table API

                    result += "}" + Environment.NewLine;

                    #endregion Namespace
                }
            }
            return result;
        }

        #endregion Code Generator Core
    }
}