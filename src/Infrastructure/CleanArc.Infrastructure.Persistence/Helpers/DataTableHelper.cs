using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Helpers
{
    public static class DataTableHelper
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            foreach (T item in items)
            {
                DataRow row = dataTable.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    row[property.Name] = property.GetValue(item, null) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
