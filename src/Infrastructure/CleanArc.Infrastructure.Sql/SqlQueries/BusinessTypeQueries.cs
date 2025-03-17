using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessTypeQueries
    {
        public static string Create_BusinessType => "usp_Create_BusinessType";
        public static string Update_BusinessType => "usp_Update_BusinessType";
        public static string Delete_BusinessType => "usp_Delete_BusinessType";
        public static string GetAll_BusinessType => "usp_GetAll_BusinessType";
        public static string GetByID_BusinessType => "usp_GetByID_BusinessType";
    }
}
