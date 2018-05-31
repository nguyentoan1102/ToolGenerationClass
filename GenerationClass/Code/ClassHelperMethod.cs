using System;
using System.Collections.Generic;

namespace GenerationClass.Code
{
    public static class ClassHelperMethod
    {
        private static Dictionary<string, Type> DataTypes = new Dictionary<string, Type>();

        #region Helper Methods

        public static string GetType(string valueIn)
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