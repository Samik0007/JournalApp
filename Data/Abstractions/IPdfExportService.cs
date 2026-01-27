namespace Journal_App.Data.Abstractions;

public interface IPdfExportService
{
    Task<string> ExportQuestPdfAsync(CancellationToken ct = default);
}
