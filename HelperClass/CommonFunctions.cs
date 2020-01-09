using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace AngularApp.HelperClass
{
    public static class CommonFunctions
    {
        /// <summary>
        /// Extension method of type object to convert object value to Boolean value
        /// </summary>
        /// <returns>Returns Boolean value of current object value</returns>
        public static bool ToBool(this object a)
        {
            try
            {
                return Convert.ToBoolean(a);
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Reads the data from sqldatareader object passed to parameter dr and
        /// Sets The Value of Generic Type's each property<T>
        /// </summary>
        /// <param name="dr">SqlDataReader Object</param>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <returns>Returns Generic type <T></returns>
        public static T GetListItem<T>(SqlDataReader dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower().Split('_')[0] == dr.GetName(i).ToLower().Split('_')[0])
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[i])))
                        {
                            if (pro.PropertyType.Name == "String")
                                pro.SetValue(obj, Convert.ToString(dr[i]));
                            else if (pro.PropertyType.Name == "Byte[]" && string.IsNullOrEmpty(Convert.ToString(dr[i])))
                                pro.SetValue(obj, new byte[0]);
                            else
                                pro.SetValue(obj, dr[i]);
                        }
                        else
                        {
                            if (pro.PropertyType.Name == "String")
                                pro.SetValue(obj, "");
                        }
                        break;
                    }
                }
            }
            return obj;
        }
    }
}
