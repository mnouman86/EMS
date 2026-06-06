using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Models.Fee;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CleanArc.Infrastructure.Persistence.Pdf
{
    /// <summary>
    /// FEE-04: Renders a fee payment receipt PDF.
    /// Shows DUPLICATE watermark when <see cref="FeeReceiptModel.IsDuplicate"/> is true.
    /// </summary>
    public class FeeReceiptPdfRenderer : IPdfRenderer<FeeReceiptModel>
    {
        public byte[] Render(FeeReceiptModel model)
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
                        col.Item().AlignCenter().Text(model.SchoolName).SemiBold().FontSize(14);
                        if (!string.IsNullOrWhiteSpace(model.SchoolAddress))
                            col.Item().AlignCenter().Text(model.SchoolAddress).FontSize(9);
                        col.Item().PaddingTop(8).LineHorizontal(1);
                        col.Item().PaddingTop(4).AlignCenter().Text("FEE RECEIPT").SemiBold().FontSize(12);
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingTop(8).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Receipt #: ").SemiBold(); t.Span(model.ReceiptNo); });
                                c.Item().Text(t => { t.Span("Date: ").SemiBold(); t.Span(model.ReceiptDate.ToString("dd-MM-yyyy")); });
                                c.Item().Text(t => { t.Span("Mode: ").SemiBold(); t.Span(model.PaymentMode ?? "-"); });
                                if (!string.IsNullOrWhiteSpace(model.ReferenceNo))
                                    c.Item().Text(t => { t.Span("Ref: ").SemiBold(); t.Span(model.ReferenceNo); });
                                if (model.ChequeClearanceDate.HasValue)
                                    c.Item().Text(t => { t.Span("Cleared: ").SemiBold(); t.Span(model.ChequeClearanceDate.Value.ToString("dd-MM-yyyy")); });
                            });
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(t => { t.Span("Student ID: ").SemiBold(); t.Span(model.StudentCode); });
                                c.Item().Text(t => { t.Span("Name: ").SemiBold(); t.Span(model.StudentFullName); });
                                c.Item().Text(t => { t.Span("Father: ").SemiBold(); t.Span(model.FatherOrParentName ?? "-"); });
                                c.Item().Text(t => { t.Span("Class: ").SemiBold(); t.Span(model.ClassName); });
                            });
                        });

                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Fee Type").SemiBold();
                                h.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Period").SemiBold();
                                h.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignRight().Text("Amount").SemiBold();
                            });
                            foreach (var line in model.Lines)
                            {
                                table.Cell().Padding(4).Text(line.FeeType ?? "");
                                table.Cell().Padding(4).Text(line.Period ?? "");
                                table.Cell().Padding(4).AlignRight().Text(line.Amount.ToString("N2"));
                            }
                        });

                        col.Item().PaddingTop(8).LineHorizontal(0.5f);
                        col.Item().PaddingTop(6).Row(row =>
                        {
                            row.RelativeItem();
                            row.ConstantItem(160).Column(c =>
                            {
                                c.Item().Row(rr =>
                                {
                                    rr.RelativeItem().Text("Total Paid:").SemiBold();
                                    rr.ConstantItem(80).AlignRight().Text(model.AmountPaid.ToString("N2")).SemiBold();
                                });
                                if (model.BalanceRemaining != 0)
                                {
                                    c.Item().Row(rr =>
                                    {
                                        rr.RelativeItem().Text("Balance:");
                                        rr.ConstantItem(80).AlignRight().Text(model.BalanceRemaining.ToString("N2"));
                                    });
                                }
                            });
                        });

                        col.Item().PaddingTop(20).Row(row =>
                        {
                            row.RelativeItem().Text(t =>
                            {
                                t.Span("Cashier: ").SemiBold();
                                t.Span(model.CashierName ?? "-");
                            });
                            row.RelativeItem().AlignRight().Text("________________________").FontSize(9);
                        });
                        col.Item().AlignRight().PaddingRight(5).Text("TSSS Stamp").FontSize(8).Italic();
                    });

                    page.Footer().AlignCenter().Text($"Generated: {System.DateTime.Now:dd-MM-yyyy HH:mm}").FontSize(8);

                    if (model.IsDuplicate)
                    {
                        page.Foreground().AlignCenter().AlignMiddle().Text("DUPLICATE")
                            .FontSize(72).Bold().FontColor(Colors.Grey.Lighten2);
                    }
                });
            }).GeneratePdf();
        }
    }
}
