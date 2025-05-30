using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class WishListQueries
    {
        public static string Create_WishList => "usp_Create_WishList";
        public static string Update_WishList => "usp_Update_WishList";
        public static string Delete_WishList => "usp_Delete_WishList";
        public static string GetALL_WishList => "usp_GetALL_WishList";
        public static string GetByID_WishList => "usp_GetByID_WishList";
    }
}
