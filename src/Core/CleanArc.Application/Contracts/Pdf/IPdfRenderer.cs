namespace CleanArc.Application.Contracts.Pdf
{
    /// <summary>
    /// Generic PDF rendering contract. Each consumer module defines its own
    /// strongly-typed model (e.g. FeeReceiptModel, SalarySlipModel) and an
    /// implementation that renders it. Keeps the rendering library (QuestPDF)
    /// out of the Application/Domain layers.
    /// </summary>
    public interface IPdfRenderer<in TModel>
    {
        byte[] Render(TModel model);
    }
}
