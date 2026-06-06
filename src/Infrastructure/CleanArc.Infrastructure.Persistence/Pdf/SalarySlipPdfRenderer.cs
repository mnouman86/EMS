using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Models.Expense;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace CleanArc.Infrastructure.Persistence.Pdf
{
    /// <summary>
    /// EXP-05: Renders a per-employee salary slip PDF (one page, A5).
    /// Earnings / Deductions / Net layout with TSSS header.
    /// </summary>
    public class SalarySlipPdfRenderer : IPdfRenderer<SalarySlipModel>
    {
        public byte[] Render(SalarySlipModel m)
        {
            var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(m.Month);
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text(m.SchoolName).SemiBold().FontSize(14);
                        if (!string.IsNullOrWhiteSpace(m.SchoolAddress))
                            col.Item().AlignCenter().Text(m.SchoolAddress).FontSize(9);
                        col.Item().PaddingTop(6).AlignCenter().Text($"SALARY SLIP — {monthName} {m.Year}").SemiBold().FontSize(12);
                        col.Item().AlignCenter().Text($"Slip No: {m.SlipNo}").FontSize(9);
                        col.Item().PaddingTop(4).LineHorizontal(1);
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingTop(6).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Employee ID: ").SemiBold(); t.Span(m.EmployeeCode); });
                                c.Item().Text(t => { t.Span("Name: ").SemiBold();        t.Span(m.EmployeeFullName); });
                                c.Item().Text(t => { t.Span("Designation: ").SemiBold(); t.Span(m.Designation ?? "-"); });
                            });
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Bank: ").SemiBold();    t.Span(m.BankName ?? "-"); });
                                c.Item().Text(t => { t.Span("Account: ").SemiBold(); t.Span(m.AccountNumber ?? "-"); });
                                if (m.ConfirmedAt.HasValue)
                                    c.Item().Text(t => { t.Span("Date: ").SemiBold(); t.Span(m.ConfirmedAt.Value.ToString("dd-MM-yyyy")); });
                            });
                        });

                        col.Item().PaddingTop(10).Row(row =>
                        {
                            row.RelativeItem().Border(0.5f).Padding(8).Column(c =>
                            {
                                c.Item().AlignCenter().Text("EARNINGS").SemiBold();
                                c.Item().PaddingTop(4).LineHorizontal(0.5f);
                                AmountRow(c, "Basic Salary", m.BasicSalary);
                                AmountRow(c, "Allowances", m.Allowances);
                                c.Item().PaddingTop(4).LineHorizontal(0.5f);
                                AmountRow(c, "Gross", m.Gross, semibold: true);
                            });

                            row.ConstantItem(6);

                            row.RelativeItem().Border(0.5f).Padding(8).Column(c =>
                            {
                                c.Item().AlignCenter().Text("DEDUCTIONS").SemiBold();
                                c.Item().PaddingTop(4).LineHorizontal(0.5f);
                                AmountRow(c, "Fixed", m.FixedDeductions);
                                AmountRow(c, "Advance Repayment", m.AdvanceDeduction);
                                AmountRow(c, "Fines", m.FineDeduction);
                                AmountRow(c, "Other", m.OtherDeduction);
                                c.Item().PaddingTop(4).LineHorizontal(0.5f);
                                AmountRow(c, "Total Deductions", m.TotalDeductions, semibold: true);
                            });
                        });

                        col.Item().PaddingTop(10).Border(1).Padding(8).Row(r =>
                        {
                            r.RelativeItem().Text("NET PAYABLE").SemiBold().FontSize(12);
                            r.ConstantItem(120).AlignRight().Text(m.NetPayable.ToString("N2")).SemiBold().FontSize(12);
                        });

                        col.Item().PaddingTop(30).Row(r =>
                        {
                            r.RelativeItem().Text("Employee Signature").FontSize(9);
                            r.RelativeItem().AlignRight().Text("Authorised by TSSS").FontSize(9);
                        });
                    });

                    page.Footer().AlignCenter().Text($"Generated: {System.DateTime.Now:dd-MM-yyyy HH:mm}").FontSize(8);
                });
            }).GeneratePdf();
        }

        private static void AmountRow(ColumnDescriptor col, string label, decimal value, bool semibold = false)
        {
            col.Item().PaddingVertical(2).Row(r =>
            {
                var labelText = r.RelativeItem().Text(label);
                var valueText = r.ConstantItem(70).AlignRight().Text(value.ToString("N2"));
                if (semibold) { labelText.SemiBold(); valueText.SemiBold(); }
            });
        }
    }
}
