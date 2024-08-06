using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessTypeQueries
    {
        public static string Create_Business => "Create_Business";
        public static string Update_Business => "Update_Business";
        public static string Delete_Business => "Delete_Business";
        public static string usp_GetAll_BusinessType => "usp_GetAll_BusinessType";
        public static string usp_GetByID_Business => "usp_GetByID_Business";
    }
}
