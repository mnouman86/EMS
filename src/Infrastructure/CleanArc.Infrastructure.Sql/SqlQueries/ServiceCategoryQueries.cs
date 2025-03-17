using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ServiceCategoryQueries
    {
        public static string Create_ServiceCategory => "usp_Create_ServiceCategory";
        public static string Update_ServiceCategory => "usp_Update_ServiceCategory";
        public static string Delete_ServiceCategory => "usp_Delete_ServiceCategory";
        public static string GetALL_ServiceCategory => "usp_GetALL_ServiceCategory";
        public static string GetByID_ServiceCategory => "usp_GetByID_ServiceCategory";
    }
}
