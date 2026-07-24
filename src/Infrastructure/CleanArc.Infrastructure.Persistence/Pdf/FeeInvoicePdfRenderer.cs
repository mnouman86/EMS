using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Models.Fee;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CleanArc.Infrastructure.Persistence.Pdf
{
    /// <summary>
    /// FEE-02: Renders a monthly invoice PDF. Shows CANCELLED watermark when
    /// the invoice was cancelled, and appends an override-history table when
    /// any admin edited the Net amount in the preview grid.
    /// </summary>
    public class FeeInvoicePdfRenderer : IPdfRenderer<FeeInvoiceModel>
    {
        public byte[] Render(FeeInvoiceModel m)
        {
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
                        col.Item().PaddingTop(8).LineHorizontal(1);
                        col.Item().PaddingTop(4).AlignCenter().Text("FEE INVOICE").SemiBold().FontSize(12);
                    });

                    page.Content().Column(col =>
                    {
                        // -- Header row --
                        col.Item().PaddingTop(8).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Invoice #: ").SemiBold(); t.Span(m.InvoiceNo); });
                                c.Item().Text(t => { t.Span("Generated: ").SemiBold(); t.Span(m.GeneratedAt.ToString("dd-MM-yyyy")); });
                                c.Item().Text(t => { t.Span("Due date: ").SemiBold(); t.Span(m.DueDate.ToString("dd-MM-yyyy")); });
                                c.Item().Text(t => { t.Span("Period: ").SemiBold(); t.Span($"{MonthName(m.BillingMonth)} {m.BillingYear}"); });
                                c.Item().Text(t => { t.Span("Session: ").SemiBold(); t.Span(m.AcademicYear); });
                            });
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Student ID: ").SemiBold(); t.Span(m.StudentCode); });
                                c.Item().Text(t => { t.Span("Name: ").SemiBold(); t.Span(m.StudentFullName); });
                                c.Item().Text(t => { t.Span("Class: ").SemiBold(); t.Span(m.ClassName); });
                                if (!string.IsNullOrWhiteSpace(m.ParentName))
                                    c.Item().Text(t => { t.Span("Parent: ").SemiBold(); t.Span(m.ParentName); });
                                if (!string.IsNullOrWhiteSpace(m.ParentMobile))
                                    c.Item().Text(t => { t.Span("Contact: ").SemiBold(); t.Span(m.ParentMobile); });
                            });
                        });

                        // -- Fee-type lines --
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn();
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });
                            table.Header(header =>
                            {
                                header.Cell().Border(0.5f).Padding(4).Text("Fee Type").SemiBold();
                                header.Cell().Border(0.5f).Padding(4).AlignRight().Text("Base").SemiBold();
                                header.Cell().Border(0.5f).Padding(4).AlignRight().Text("Concession").SemiBold();
                                header.Cell().Border(0.5f).Padding(4).AlignRight().Text("Net").SemiBold();
                            });
                            foreach (var l in m.Lines)
                            {
                                table.Cell().Border(0.5f).Padding(4).Text(l.FeeTypeName);
                                table.Cell().Border(0.5f).Padding(4).AlignRight().Text($"Rs. {l.BaseAmount:N0}");
                                table.Cell().Border(0.5f).Padding(4).AlignRight().Text($"Rs. {l.ConcessionAmount:N0}");
                                table.Cell().Border(0.5f).Padding(4).AlignRight().Text($"Rs. {l.NetAmount:N0}");
                            }
                        });

                        // -- Totals --
                        col.Item().PaddingTop(10).AlignRight().Column(c =>
                        {
                            c.Item().Row(r =>
                            {
                                r.RelativeItem().Text(t => { t.Span("Current month (Net): ").SemiBold(); });
                                r.ConstantItem(90).AlignRight().Text($"Rs. {m.NetDue:N0}");
                            });
                            c.Item().Row(r =>
                            {
                                r.RelativeItem().Text(t => { t.Span("Prior arrears brought: ").SemiBold(); });
                                r.ConstantItem(90).AlignRight().Text($"Rs. {m.PriorArrearsBrought:N0}");
                            });
                            c.Item().PaddingTop(4).Row(r =>
                            {
                                r.RelativeItem().Text(t => { t.Span("TOTAL PAYABLE: ").SemiBold().FontSize(11); });
                                r.ConstantItem(90).AlignRight().Text($"Rs. {m.TotalPayable:N0}").SemiBold().FontSize(11);
                            });
                            if (m.AmountPaid > 0)
                            {
                                c.Item().Row(r =>
                                {
                                    r.RelativeItem().Text(t => { t.Span("Paid to date: "); });
                                    r.ConstantItem(90).AlignRight().Text($"Rs. {m.AmountPaid:N0}");
                                });
                                c.Item().Row(r =>
                                {
                                    r.RelativeItem().Text(t => { t.Span("Balance due: ").SemiBold(); });
                                    r.ConstantItem(90).AlignRight().Text($"Rs. {m.BalanceDue:N0}").SemiBold();
                                });
                            }
                        });

                        // -- Override history --
                        if (m.Overrides.Count > 0)
                        {
                            col.Item().PaddingTop(14).Text("Amount adjustments").SemiBold().FontSize(10);
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn();
                                    c.RelativeColumn();
                                    c.RelativeColumn(2);
                                    c.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Border(0.5f).Padding(4).AlignRight().Text("Original").SemiBold();
                                    header.Cell().Border(0.5f).Padding(4).AlignRight().Text("Adjusted").SemiBold();
                                    header.Cell().Border(0.5f).Padding(4).Text("Reason").SemiBold();
                                    header.Cell().Border(0.5f).Padding(4).Text("When").SemiBold();
                                });
                                foreach (var o in m.Overrides)
                                {
                                    table.Cell().Border(0.5f).Padding(4).AlignRight().Text($"Rs. {o.OriginalNetAmount:N0}");
                                    table.Cell().Border(0.5f).Padding(4).AlignRight().Text($"Rs. {o.OverrideNetAmount:N0}");
                                    table.Cell().Border(0.5f).Padding(4).Text(o.Reason);
                                    table.Cell().Border(0.5f).Padding(4).Text(o.ActorAt.ToString("dd-MM-yyyy"));
                                }
                            });
                        }

                        // -- Cancelled watermark / note --
                        if (m.IsCancelled)
                        {
                            col.Item().PaddingTop(10).AlignCenter()
                                .Text($"** CANCELLED — {m.CancelReason ?? ""} **")
                                .SemiBold().FontColor(Colors.Red.Medium).FontSize(12);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("This invoice was system-generated on ");
                        x.Span(m.GeneratedAt.ToString("dd-MM-yyyy HH:mm"));
                        x.Span(". Please pay by due date to avoid late fees.");
                    });
                });
            }).GeneratePdf();
        }

        private static string MonthName(int m)
        {
            if (m < 1 || m > 12) return string.Empty;
            return System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(m);
        }
    }
}
