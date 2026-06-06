using System;

namespace CleanArc.Application.Models.Finance
{
    public class SetOpeningCashBalanceDTO
    {
        public int AcademicYearId { get; set; }
        public decimal OpeningAmount { get; set; }
        public DateTime AsOfDate { get; set; }
        public int SetByUserId { get; set; }
    }
}
