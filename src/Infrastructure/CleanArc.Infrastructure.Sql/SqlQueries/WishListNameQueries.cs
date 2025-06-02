using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class WishListNameQueries
    {
        public static string Create_WishListName => "usp_Create_WishListName";
        public static string Update_WishListName => "usp_Update_WishListName";
        public static string Delete_WishListName => "usp_Delete_WishListName";
        public static string GetALL_WishListName => "usp_GetALL_WishListName";
        public static string GetByID_WishListName => "usp_GetByID_WishListName";
    }
}
