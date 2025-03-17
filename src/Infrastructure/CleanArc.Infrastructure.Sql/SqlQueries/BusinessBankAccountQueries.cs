using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessBankAccountQueries
    {
        public static string Create_BusinessBankAccount => "usp_Create_BusinessBankAccount";
        public static string Update_BusinessBankAccount => "usp_Update_BusinessBankAccount";
        public static string Delete_BusinessBankAccount => "usp_Delete_BusinessBankAccount";
        public static string GetAll_BusinessBankAccount => "usp_GetAll_BusinessBankAccount";
        public static string GetByID_BusinessBankAccount => "usp_GetByID_BusinessBankAccount";
    }
}
