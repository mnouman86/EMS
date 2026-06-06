using System;
using System.Collections.Generic;

namespace CleanArc.Application.Models.Finance
{
    public class PnLStatementModel
    {
        public string SchoolName { get; set; } = "The Saviour's Secondary School (TSSS)";
        public string SchoolAddress { get; set; }
        public string PeriodLabel { get; set; }      // e.g. "Sep 2025 – Jun 2026"
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
        public string GeneratedBy { get; set; }
        public List<PnLStatementLine> Lines { get; set; } = new();
        public decimal NetSurplusOrDeficit { get; set; }
    }

    public class PnLStatementLine
    {
        public string Section { get; set; }    // Income / Expense / Net
        public string Line { get; set; }
        public decimal Amount { get; set; }
    }
}
