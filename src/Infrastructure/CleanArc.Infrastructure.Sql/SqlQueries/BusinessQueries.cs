using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessQueries
    {
        public static string Create_Business => "usp_Create_Business";
        public static string Update_Business => "usp_Update_Business";
        public static string Delete_Business => "usp_Delete_Business";
        public static string GetAll_Business => "usp_GetAll_Business";
        public static string GetByID_Business => "usp_GetByID_Business";
    }
}
