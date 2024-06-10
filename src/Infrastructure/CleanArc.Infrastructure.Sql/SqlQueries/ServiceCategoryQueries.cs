using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class ServiceCategoryQueries
    {
        public static string Create_ServiceCategory => "Create_ServiceCategory";
        public static string Update_ServiceCategory => "Update_ServiceCategory";
        public static string Delete_ServiceCategory => "Delete_ServiceCategory";
        public static string usp_GetALL_ServiceCategory => "usp_GetALL_ServiceCategory";
        public static string usp_GetByID_ServiceCategory => "usp_GetByID_ServiceCategory";
    }
}
