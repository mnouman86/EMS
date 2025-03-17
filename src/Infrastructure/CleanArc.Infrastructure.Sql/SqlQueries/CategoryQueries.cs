using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CategoryQueries
    {
       
        public static string Create_Category => "Create_Category";
        public static string Update_Category => "Update_Category";
        public static string Delete_Category => "Delete_Category";
        public static string GetAll_Category => "usp_GetAll_Category";
        public static string GetByID_Category => "usp_GetByID_Category";


    }
}

