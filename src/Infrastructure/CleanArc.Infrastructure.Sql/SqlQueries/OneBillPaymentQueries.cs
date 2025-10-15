using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Sql.SqlQueries;

public static class OneBillPaymentQueries
{
    public static string Get_OneBillLoanInquiry => "usp_LoanInquiry";
    public static string Get_OneBillLoanPayment => "usp_LoanPayment";

}
