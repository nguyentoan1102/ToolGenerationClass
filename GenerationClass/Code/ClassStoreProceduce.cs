using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public class ClassStoreProceduce
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        //public static string GenerateStoreProceduce(List<Entity> entities, List<GetType> entitiesDataType, string namespaceIn, string classModifiers, string tableName)
        //{
        //    var result = string.Empty;
        //    var staticTxt = string.Empty;

        //    if (DataTypes.Count > 0)
        //    {
        //        if (entities.Count > 0)
        //        {
        //            result += " " + Environment.NewLine;
        //            result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
        //            result += "//                                                                                                  // " + Environment.NewLine;
        //            result += "// Purpose:                                                                                         //" + Environment.NewLine;
        //            result += "// Created Date:" + DateTime.Now.ToString() + "                                          //" + Environment.NewLine;
        //            result += "// Last Modified Date: " + DateTime.Now.ToString() + "                                              //" + Environment.NewLine;
        //            result += "// Author: DSI Group                                                                                //" + Environment.NewLine;
        //            result += "//                                                                                                  //" + Environment.NewLine;
        //            result += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
        //            result += " " + Environment.NewLine;
        //            result += "create  procedure " + "sp" + tableName + "_Insert" + Environment.NewLine;
        //            //foreach (var entity in entities)
        //            //{
        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                foreach (var entityType in entitiesDataType)
        //                {
        //                    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
        //                    {
        //                    }
        //                    else
        //                    {
        //                        if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim().ToUpper() == "STRING")
        //                        {
        //                            result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")," + Environment.NewLine;
        //                        }
        //                        else
        //                        {
        //                            result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + "," + Environment.NewLine;

        //                        }
        //                    }
        //                }
        //            }

        //            //}
        //            result += "@ID_Output int output" + Environment.NewLine;
        //            result += " as" + Environment.NewLine;

        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                foreach (var entityType in entitiesDataType)
        //                {
        //                    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
        //                    {
        //                    }
        //                    else
        //                    {
        //                        if (entities[i].Type.ToString().ToUpper() == "INT")
        //                        {
        //                            result += "if (" + "@" + entities[i].Field.ToString() + " = 0 set " + "@" + entities[i].Field.ToString() + " = null" + Environment.NewLine;
        //                        }
        //                        else
        //                        {
        //                            result += "if (" + "@" + entities[i].Field.ToString() + " = ' ' set " + "@" + entities[i].Field.ToString() + " = null" + Environment.NewLine;

        //                        }
        //                    }
        //                }

        //            }

        //            result += "insert into " + tableName + "(" + Environment.NewLine;

        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                foreach (var entityType in entitiesDataType)
        //                {
        //                    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
        //                    {
        //                    }
        //                    else
        //                    {
        //                        if (i < entities.Count - 1)
        //                        {
        //                            result += entities[i].Field.ToString() + "," + Environment.NewLine;
        //                        }
        //                        else
        //                        {
        //                            result += entities[i].Field.ToString() + Environment.NewLine;
        //                        }
        //                    }
        //                }
        //            }

        //            //string _params = result.TrimEnd(new char[] {});

        //            //for (int i = 0; i <

        //            result += ")" + Environment.NewLine;
        //            result += "values " + tableName + "(" + Environment.NewLine;

        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                if (i < entities.Count - 1)
        //                {
        //                    result += "@" + entities[i].Field.ToString() + "," + Environment.NewLine;
        //                }
        //                else
        //                {
        //                    result += "@" + entities[i].Field.ToString() + Environment.NewLine;
        //                }

        //            }

        //            result += ")" + Environment.NewLine;

        //            result += "SET @ID_Output = CAST(SCOPE_IDENTITY() AS INT)" + Environment.NewLine;

        //            result += Environment.NewLine;
        //            result += Environment.NewLine;
        //            result += Environment.NewLine;

        //            result += "create  procedure " + "sp" + tableName + "_Update" + Environment.NewLine;

        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                if (GetType(entities[i].Type.ToString()).Replace("System.", "").Trim().ToUpper() == "STRING")
        //                {
        //                    result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + " (" + entities[i].Length.ToString() + ")," + Environment.NewLine;
        //                }
        //                else
        //                {
        //                    result += "@" + entities[i].Field.ToString() + " as " + entities[i].Type.ToString() + "," + Environment.NewLine;

        //                }
        //            }

        //            result += "@ID_Output int output" + Environment.NewLine;
        //            result += " as" + Environment.NewLine;
        //            foreach (var entityType in entitiesDataType)
        //            {
        //                result += " set @ID_Output = " + "@" + entityType.NameField.ToString() + Environment.NewLine;
        //            }

        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                foreach (var entityType in entitiesDataType)
        //                {
        //                    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
        //                    {
        //                    }
        //                    else
        //                    {
        //                        if (entities[i].Type.ToString().ToUpper() == "INT")
        //                        {
        //                            result += "if (" + "@" + entities[i].Field.ToString() + " = 0 set " + "@" + entities[i].Field.ToString() + " = null" + Environment.NewLine;
        //                        }
        //                        else
        //                        {
        //                            result += "if (" + "@" + entities[i].Field.ToString() + " = ' ' set " + "@" + entities[i].Field.ToString() + " = null" + Environment.NewLine;

        //                        }
        //                    }
        //                }
        //            }

        //            result += "update " + tableName + " set " + Environment.NewLine;
        //            for (int i = 0; i < entities.Count; i++)
        //            {
        //                foreach (var entityType in entitiesDataType)
        //                {
        //                    if (entities[i].Field.ToString().ToUpper() == entityType.NameField.ToString().ToUpper())
        //                    {
        //                    }
        //                    else
        //                    {
        //                        if (i < entities.Count - 1)
        //                        {
        //                            result += entities[i].Field.ToString() + " = " + "@" + entities[i].Field.ToString() + "," + Environment.NewLine;
        //                        }
        //                        else
        //                        {
        //                            result += entities[i].Field.ToString() + " = " + "@" + entities[i].Field.ToString() + Environment.NewLine;
        //                        }
        //                    }
        //                }
        //            }

        //            foreach (var entityType in entitiesDataType)
        //            {
        //                result += "where (@" + entityType.NameField.ToString() + " = " + tableName + "." + entityType.NameField.ToString() + Environment.NewLine;
        //            }

        //            result += ")" + Environment.NewLine;

        //            result += Environment.NewLine;
        //            result += Environment.NewLine;
        //            result += Environment.NewLine;

        //            result += "create  procedure " + "sp" + tableName + "_Delete" + Environment.NewLine;
        //            foreach (var entityType in entitiesDataType)
        //            {
        //                result += "@" + entityType.NameField.ToString() + " as " + entityType.TypeField.ToString() + "," + Environment.NewLine;
        //                result += "@ID_Output int output" + Environment.NewLine;
        //                result += " as" + Environment.NewLine;
        //                result += " set @ID_Output = " + entityType.NameField.ToString() + Environment.NewLine;
        //                result += "delete " + tableName + Environment.NewLine;
        //                result += "where (@" + entityType.NameField.ToString() + " = " + tableName + "." + entityType.NameField.ToString() + Environment.NewLine;

        //            }

        //        }
        //    }
        //    return result;
        //}
    }
}