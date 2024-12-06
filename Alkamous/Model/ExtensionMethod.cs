﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Alkamous.Model
{
    public static class ExtensionMethod
    {

        #region ExtensionMethod to DataTable ToList<T>

        public static List<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            List<T> list = new List<T>();
            var properties = typeof(T).GetProperties();


            foreach (DataRow row in table.Rows)
            {
                T item = new T();
                foreach (var property in properties)
                {
                    if (table.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                    {
                        PropertyInfo propertyInfo = property;
                        property.SetValue(item, Convert.ChangeType(row[property.Name], propertyInfo.PropertyType));
                    }
                }
                list.Add(item);
            }

            return list;
        }
        #endregion       

    }
}
