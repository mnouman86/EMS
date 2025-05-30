using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class FilterCategoryQueries
    {
        public static string Create_FilterCategory => "usp_Create_FilterCategory";
        public static string Update_FilterCategory => "usp_Update_FilterCategory";
        public static string Delete_FilterCategory => "usp_Delete_FilterCategory";
        public static string GetALL_FilterCategory => "usp_GetALL_FilterCategory";
        public static string GetByID_FilterCategory => "usp_GetByID_FilterCategory";
    }
}
