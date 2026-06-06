using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Models.Finance;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CleanArc.Infrastructure.Persistence.Pdf
{
    /// <summary>
    /// FIN-03: Renders a P&L statement PDF — Income block, Expense block, Net line.
    /// </summary>
    public class PnLStatementPdfRenderer : IPdfRenderer<PnLStatementModel>
    {
        public byte[] Render(PnLStatementModel m)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().AlignCenter().Text(m.SchoolName).SemiBold().FontSize(14);
                        if (!string.IsNullOrWhiteSpace(m.SchoolAddress))
                            col.Item().AlignCenter().Text(m.SchoolAddress).FontSize(9);
                        col.Item().PaddingTop(8).AlignCenter().Text("PROFIT & LOSS STATEMENT").SemiBold().FontSize(12);
                        if (!string.IsNullOrWhiteSpace(m.PeriodLabel))
                            col.Item().AlignCenter().Text($"Period: {m.PeriodLabel}").FontSize(10);
                        col.Item().PaddingTop(6).LineHorizontal(1);
                    });

                    page.Content().Column(col =>
                    {
                        Section(col, m, "Income");
                        col.Item().PaddingTop(8);
                        Section(col, m, "Expense");

                        col.Item().PaddingTop(12).Border(1).Padding(8).Row(r =>
                        {
                            r.RelativeItem().Text("NET SURPLUS / (DEFICIT)").SemiBold().FontSize(12);
                            r.ConstantItem(140).AlignRight()
                                .Text(m.NetSurplusOrDeficit.ToString("N2"))
                                .SemiBold().FontSize(12)
                                .FontColor(m.NetSurplusOrDeficit >= 0 ? Colors.Green.Darken2 : Colors.Red.Darken2);
                        });
                    });

                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span($"Generated: {m.GeneratedAt:dd-MM-yyyy HH:mm}").FontSize(8);
                        if (!string.IsNullOrWhiteSpace(m.GeneratedBy))
                            t.Span($"  •  by {m.GeneratedBy}").FontSize(8);
                    });
                });
            }).GeneratePdf();
        }

        private static void Section(ColumnDescriptor col, PnLStatementModel m, string section)
        {
            decimal total = 0;
            col.Item().PaddingTop(6).Text(section.ToUpper()).SemiBold().FontSize(11);
            col.Item().PaddingTop(2).LineHorizontal(0.5f);

            foreach (var line in m.Lines)
            {
                if (!string.Equals(line.Section, section, System.StringComparison.OrdinalIgnoreCase)) continue;
                total += line.Amount;
                col.Item().PaddingVertical(2).Row(r =>
                {
                    r.RelativeItem().PaddingLeft(8).Text(line.Line ?? "-");
                    r.ConstantItem(120).AlignRight().Text(line.Amount.ToString("N2"));
                });
            }

            col.Item().PaddingTop(2).LineHorizontal(0.5f);
            col.Item().PaddingVertical(2).Row(r =>
            {
                r.RelativeItem().Text($"Total {section}").SemiBold();
                r.ConstantItem(120).AlignRight().Text(total.ToString("N2")).SemiBold();
            });
        }
    }
}
