using System;

namespace CleanArc.Application.Models.Expense
{
    public class SalarySlipModel
    {
        public string SchoolName { get; set; } = "The Saviour's Secondary School (TSSS)";
        public string SchoolAddress { get; set; }
        public string SlipNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string Designation { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Gross { get; set; }

        public decimal FixedDeductions { get; set; }
        public decimal AdvanceDeduction { get; set; }
        public decimal FineDeduction { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal TotalDeductions { get; set; }

        public decimal NetPayable { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }
}
