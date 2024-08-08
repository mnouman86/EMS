using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessTypeQueries
    {
        public static string Create_BusinessType => "Create_BusinessType";
        public static string Update_BusinessType => "Update_BusinessType";
        public static string Delete_BusinessType => "Delete_BusinessType";
        public static string usp_GetAll_BusinessType => "usp_GetAll_BusinessType";
        public static string usp_GetByID_BusinessType => "usp_GetByID_BusinessType";
    }
}
