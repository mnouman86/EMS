using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BankQueries
    {
        public static string Create_Bank => "usp_Create_Bank";
        public static string Update_Bank => "usp_Update_Bank";
        public static string Delete_Bank => "usp_Delete_Bank";
        public static string GetAll_Bank => "usp_GetAll_Bank";
        public static string GetByID_Bank => "usp_GetByID_Bank";
    }
}
