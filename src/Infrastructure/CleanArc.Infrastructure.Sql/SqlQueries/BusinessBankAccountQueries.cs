using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries
{
    public static class BusinessBankAccountQueries
    {
        public static string Create_BusinessBankAccount => "Create_BusinessBankAccount";
        public static string Update_BusinessBankAccount => "Update_BusinessBankAccount";
        public static string Delete_BusinessBankAccount => "Delete_BusinessBankAccount";
        public static string usp_GetAll_BusinessBankAccount => "usp_GetAll_BusinessBankAccount";
        public static string usp_GetByID_BusinessBankAccount => "usp_GetByID_BusinessBankAccount";
    }
}
