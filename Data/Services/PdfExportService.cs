namespace Journal_App.Data.Services;

using Journal_App.Data.Abstractions;

public sealed class PdfExportService : IPdfExportService
{
    private const string AssetName = "quest.pdf";

    public async Task<string> ExportQuestPdfAsync(CancellationToken ct = default)
    {
        try
        {
            var destinationPath = Path.Combine(FileSystem.AppDataDirectory, AssetName);

            await using var src = await FileSystem.OpenAppPackageFileAsync(AssetName);
            await using var dst = File.Open(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);

            await src.CopyToAsync(dst, ct);

            return destinationPath;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to export quest.pdf.", ex);
        }
    }
}
