using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class CustomerReviewQueries
    {
        public static string Create_CustomerReview => "usp_Create_CustomerReview";
        public static string Update_CustomerReview => "usp_Update_CustomerReview";
        public static string Delete_CustomerReview => "usp_Delete_CustomerReview";
        public static string GetALL_CustomerReview => "usp_GetAll_CustomerReview";
        public static string GetByID_CustomerReview => "usp_GetByID_CustomerReview";
    }
}
