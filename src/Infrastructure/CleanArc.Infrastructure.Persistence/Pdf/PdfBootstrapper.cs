using QuestPDF.Infrastructure;

namespace CleanArc.Infrastructure.Persistence.Pdf
{
    /// <summary>
    /// One-time QuestPDF setup. Called from <c>AddPersistenceServices</c> at
    /// startup. The Community licence is free for non-commercial / small-business
    /// use; switch to <see cref="LicenseType.Professional"/> if/when needed.
    /// </summary>
    public static class PdfBootstrapper
    {
        public static void Configure()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
    }
}
